using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Hector
{
    public partial class FormExporter : Form
    {

        private BDD Base_de_Donnees;
        private string CheminFichierCSV;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public FormExporter(BDD Base_de_donnees_Main)
        {
            InitializeComponent();

            Base_de_Donnees = Base_de_donnees_Main;

            ProgressBar_Exporter.Visible = false;
            Exportation_En_Cours_Label.Visible = false;

            // On modifie la fenêtre pour qu'elle soit de taille fixe afin que l'utilisateur ne puisse pas modifier sa taille.
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // On désactive le bouton d'agrandissement.
            MaximizeBox = false;
        }

        /// <summary>
        /// Ouvre une fenetre pour choisir l'emplacement de l'exportation du fichier CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selectionner_Dossier_Bouton_Exporter_Click(object sender, EventArgs e)
        {
            // Afficher la boîte de dialogue pour sauvegarder le fichier CSV
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Définir les propriétés du SaveFileDialog
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.Filter = "Fichiers CSV (*.csv)|*.csv";
                saveFileDialog.Title = "Exporter vers un fichier CSV";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                // Obtenir le nom de fichier actuel dans le TextBox
                saveFileDialog.FileName = Chemin_Dossier_TextBox.Text;

                // Afficher la boîte de dialogue et attendre que l'utilisateur sélectionne un emplacement et un nom de fichier
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Mettre à jour le TextBox avec le nom de fichier sélectionné par l'utilisateur
                    Chemin_Dossier_TextBox.Text = Path.GetFileName(saveFileDialog.FileName);

                    ProgressBar_Exporter.Visible = true;
                    Exportation_En_Cours_Label.Visible = true;

                    // Enregistrer le chemin du fichier sélectionné
                    CheminFichierCSV = saveFileDialog.FileName;

                    // Démarrer le travail en arrière-plan pour exporter les données vers le fichier CSV
                    Background_Worker_Exporter.RunWorkerAsync();

                }
            }

        }

        /// <summary>
        /// Méthode qui permet de recupere les données de la BDD et ecrit sur le fichier CSV
        /// </summary>
        /// <param name="CheminFichierCSVParam"></param>
        private void ExporterDonneesVersCSV(string CheminFichierCSVParam)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(CheminFichierCSVParam, false, Encoding.UTF8))
                {
                    // Écrire l'en-tête du fichier CSV (à adapter selon vos données)
                    writer.WriteLine("Description; Ref; Marque; Famille; Sous-Famille; Prix H.T.");

                    List<Article> Articles = Base_de_Donnees.Lire_Liste_Article();
                    int TotalArticles = Articles.Count;
                    int ArticlesExportes = 0;

                    // Parcourir chaque article et écrire ses informations dans le fichier CSV
                    foreach (Article Article in Articles)
                    {
                        // Construire une ligne avec les données de l'article, en utilisant le point-virgule comme séparateur
                        string Ligne = $"{Article.Lire_Description()};{Article.Lire_Ref_Article()};{Article.Lire_Marque().Lire_Nom_Marque()};{Article.Lire_Sous_Famille().Lire_Nom_Sous_Famille()};{Article.Lire_Sous_Famille().Lire_Famille().Lire_Nom_Famille()};{Article.Lire_PrixHT()}";

                        // Écrire la ligne dans le fichier CSV
                        writer.WriteLine(Ligne);

                        // Mettre à jour la progression
                        ArticlesExportes++;
                        int PourcentageProgression = (int)(((double)ArticlesExportes / TotalArticles) * 100);
                        Background_Worker_Exporter.ReportProgress(PourcentageProgression);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite lors de l'exportation : {ex.Message}", "Erreur lors de l'exportation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ce qu'il va être exécuté en arrière plan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Background_Worker_Exporter_DoWork(object sender, DoWorkEventArgs e)
        {
            // Exécuter l'exportation des données vers le fichier CSV
            ExporterDonneesVersCSV(CheminFichierCSV);

        }

        /// <summary>
        /// Permets de changer la valeur de progression de la barre de chargement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Background_Worker_Exporter_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Mettre à jour la barre de progression et le label de progression
            ProgressBar_Exporter.Value = e.ProgressPercentage;
            Exportation_En_Cours_Label.Text = $"Exportation en cours... {e.ProgressPercentage}%";
            ProgressBar_Exporter.Update();

        }

        /// <summary>
        /// Ce qu'il va se passer une fois le travail en arrière plan terminé.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Background_Worker_Exporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Vérifier si la barre de progression est à 100%
            if (ProgressBar_Exporter.Value == 100)
            {
                // Afficher le message de succès
                MessageBox.Show("Exportation terminée avec succès.", "Succès de l'exportation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Fermer la fenêtre
                this.Close();
            }
            // Masquer la barre de progression et le label de progression
            ProgressBar_Exporter.Visible = false;
            Exportation_En_Cours_Label.Visible = false;
        }

    }
}
