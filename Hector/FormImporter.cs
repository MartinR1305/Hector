using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hector
{
    public partial class FormImporter : Form
    {
        private string Chemin_Fichier_CSV_String;
        private BDD Base_de_Donnees;

        private int Nombre_Article_Avant_Manip;
        private int Nombre_Article_Apres_Ajout;
        private int Nombre_Article_Ajouter;

        private bool Besoin_De_Vider;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public FormImporter(BDD Base_de_Donnees_Main)
        {
            InitializeComponent();

            Base_de_Donnees = Base_de_Donnees_Main;

            Chemin_Fichier_CSV_String = "";
            Nom_Fichier_CSV_Label.Text = "Aucun fichier sélectionné pour le moment.";
            ProgressBar.Visible = false;
            Integration_En_Cours_Label.Visible = false;
            Besoin_De_Vider = false;

            // On modifie la fenêtre pour qu'elle soit de taille fixe afin que l'utilisateur ne puisse pas modifier sa taille.
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // On désactive le bouton d'agrandissement.
            MaximizeBox = false;
        }

        /// <summary>
        /// Ouvre une fenetre afin de sélectionner un fichier CSV.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selectionner_Fichier_Bouton_Click(object sender, EventArgs e)
        {
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                // Configure les propriétés de l'OpenFileDialog.
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                openFileDialog1.Filter = "Fichiers texte (*.csv)|*.csv";
                openFileDialog1.Title = "Choisir un fichier CSV";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                // Affiche la boîte de dialogue et attend que l'utilisateur choisisse un fichier CSV.
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Chemin_Fichier_CSV_String = openFileDialog1.FileName;
                    Nom_Fichier_CSV_Label.Text = "Nom du fichier sélectionné : " + Path.GetFileName(Chemin_Fichier_CSV_String);
                }
            }
        }

        /// <summary>
        /// Permets de lancer une importation en mode ajout.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Importation_Mode_Ajout_Bouton_Click(object sender, EventArgs e)
        {
            // On vérifier que l'utilisateur a bien renseigner un chemin.
            if (Chemin_Fichier_CSV_String == "")
            {
                MessageBox.Show("Vous devez sélectionner un fichier avant de faire une intégration.", "Erreur : Aucun fichier CSV sélectionné", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                // On affiche la barre de chargement et le texte de chargement.
                ProgressBar.Visible = true;
                Integration_En_Cours_Label.Visible = true;

                // On lance le travail en arrière plan.
                Besoin_De_Vider = false;
                Background_Worker.RunWorkerAsync(Base_de_Donnees);
            }
        }

        /// <summary>
        /// Permets de lancer une intégration en mode écrasement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Importation_Mode_Ecrasement_Boutton_Click(object sender, EventArgs e)
        {


            // On vérifie que le background_Worker n'est pas déjà en train d'exécuter un import.
            if (!Background_Worker.IsBusy)
            {
                // On vérifier que l'utilisateur a bien renseigner un chemin.
                if (Chemin_Fichier_CSV_String == "")
                {
                    MessageBox.Show("Vous devez sélectionner un fichier avant de faire une intégration.", "Erreur : Aucun fichier CSV sélectionné", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    // On affiche la barre de chargement et le texte de chargement.
                    ProgressBar.Visible = true;
                    Integration_En_Cours_Label.Visible = true;

                    // On lance le travail en arrière plan.
                    Besoin_De_Vider = true;
                    Background_Worker.RunWorkerAsync(Base_de_Donnees);
                }
            }
        }

        /// <summary>
        /// Permets de changer la valeur de progression de la barre de chargement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Background_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
            Integration_En_Cours_Label.Text = string.Format("Intégration en cours ... {0}%", e.ProgressPercentage);
            ProgressBar.Update();
        }

        /// <summary>
        /// Ce qu'il va être exécuté en arrière plan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Background_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Parseur parseur = new Parseur(Chemin_Fichier_CSV_String);

            // On remplit les informations du fichier en code.
            Background_Worker.ReportProgress(0);
            parseur.Remplir_Toutes_Les_Tables(Base_de_Donnees);
            Background_Worker.ReportProgress(33);

            Nombre_Article_Avant_Manip = Base_de_Donnees.Lire_Nombre_Article_BDD();

            // On vide la BDD si besoin.
            if (Besoin_De_Vider)
            {
                Base_de_Donnees.Vider_Toutes_Les_Tables();

            }

            // On ajoute les informations dans la BDD.
            Background_Worker.ReportProgress(66);
            Base_de_Donnees.Ajouter_Toutes_Les_Tables();
            Background_Worker.ReportProgress(99);

            Nombre_Article_Apres_Ajout = Base_de_Donnees.Lire_Nombre_Article_BDD();
            Nombre_Article_Ajouter = Nombre_Article_Apres_Ajout - Nombre_Article_Avant_Manip;

            Background_Worker.ReportProgress(100);
        }

        /// <summary>
        /// Ce qu'il va se passer une fois le travail en arrière plan terminé.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Background_Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Message de succès pour intégration en mode écrasement.
            if (Besoin_De_Vider)
            {
                MessageBox.Show("L'intégration des données a été effectuée avec succès. \n\n" +
                    "Vous avez supprimé " + Nombre_Article_Avant_Manip + " articles dans la base de données. \n" +
                    "Vous avez ensuite ajouté " + Nombre_Article_Apres_Ajout + " articles dans la base de données.", "Succès de l'intégration", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Message de succès pour intégration en mode ajout.
            else
            {
                MessageBox.Show("L'intégration des données a été effectuée avec succès.\n\n" +
                    "Vous avez ajouté " + Nombre_Article_Ajouter + " article(s) dans la base de données. \n" +
                    "Il y a maintenant " + Nombre_Article_Apres_Ajout + " article(s) dans la base de données.", "Succès de l'intégration", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // On ferme la fenêtre.
            this.Close();
        }
    }
}
