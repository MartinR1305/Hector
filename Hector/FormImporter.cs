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
        private BackgroundWorker Back_ground_Worker;

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

            // Initialisation du BackgroundWorker
            Back_ground_Worker = new BackgroundWorker();
            Back_ground_Worker.WorkerReportsProgress = true;
            Back_ground_Worker.DoWork += new DoWorkEventHandler(Back_ground_Worker_DoWork);
            Back_ground_Worker.ProgressChanged += new ProgressChangedEventHandler(Back_ground_Worker_ProgressChanged);

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
                Parseur parseur = new Parseur(Chemin_Fichier_CSV_String);
                parseur.Remplir_Toutes_Les_Tables(Base_de_Donnees);

                int Nombre_Article_Avant_Ajout = Base_de_Donnees.Lire_Nombre_Article_BDD();

                Base_de_Donnees.Ajouter_Toutes_Les_Tables();

                ProgressBar.Visible = true;
                Integration_En_Cours_Label.Visible = true;

                int Nombre_Article_Apres_Ajout = Base_de_Donnees.Lire_Nombre_Article_BDD();
                int Nombre_Article_Ajouter = Nombre_Article_Apres_Ajout - Nombre_Article_Avant_Ajout;

                // On affiche un message de succès.
                MessageBox.Show("L'intégration des données a été effectuée avec succès.\n\n" +
                    "Vous avez ajouté " + Nombre_Article_Ajouter + " article(s) dans la base de données. \n" +
                    "Il y a maintenant " + Nombre_Article_Apres_Ajout + "articles dans la base de données.", "Succès de l'intégration", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // On ferme la fenêtre.
                this.Close();
            }
        }

        /// <summary>
        /// Permets de lancer une intégration en mode écrasement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Importation_Mode_Ecrasement_Boutton_Click(object sender, EventArgs e)
        {
            // On vérifier que l'utilisateur a bien renseigner un chemin.
            if (Chemin_Fichier_CSV_String == "")
            {
                MessageBox.Show("Vous devez sélectionner un fichier avant de faire une intégration.", "Erreur : Aucun fichier CSV sélectionné", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                Parseur parseur = new Parseur(Chemin_Fichier_CSV_String);
                parseur.Remplir_Toutes_Les_Tables(Base_de_Donnees);

                int Nombre_Article_Avant_Suppression = Base_de_Donnees.Lire_Nombre_Article_BDD();

                Base_de_Donnees.Vider_Toutes_Les_Tables();
                Base_de_Donnees.Ajouter_Toutes_Les_Tables();

                int Nombre_Article_Apres_Ajout = Base_de_Donnees.Lire_Nombre_Article_BDD();

                // On affiche un message de succès.
                MessageBox.Show("L'intégration des données a été effectuée avec succès. \n\n" +
                    "Vous avez supprimé " + Nombre_Article_Avant_Suppression + " articles dans la base de données. \n" +
                    "Vous avez ensuite ajouté " + Nombre_Article_Apres_Ajout + " articles dans la base de données.", "Succès de l'intégration", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // On ferme la fenêtre.
                this.Close();
            }
        }

        private void Back_ground_Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Méthode qui effectue le travail long (ajouter les tables dans votre cas)
            Base_de_Donnees.Ajouter_Toutes_Les_Tables();
        }

        private void Back_ground_Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Mise à jour de la ProgressBar
            ProgressBar.Value = e.ProgressPercentage;
        }
    }
}
