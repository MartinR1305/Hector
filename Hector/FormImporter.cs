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

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public FormImporter()
        {
            InitializeComponent();

            Base_de_Donnees = new BDD();
            Base_de_Donnees.Obtenir_Chemin_Base_de_Donnees();

            Chemin_Fichier_CSV_String = "";
            Nom_Fichier_CSV_Label.Text = "Aucun fichier sélectionné pour le moment.";
            ProgressBar.Visible = false;
            Integration_En_Cours_Label.Visible = false;

            // On modifie la fenêtre pour qu'elle soit de taille fixe afin que l'utilisateur ne puisse pas modifier sa taille.
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // On désactive le bouton d'agrandissement.
            MaximizeBox = false;
        }

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

        private void Importation_Mode_Ajout_Bouton_Click(object sender, EventArgs e)
        {

        }

        private void Importation_Mode_Ecrasement_Boutton_Click(object sender, EventArgs e)
        {

        }
    }
}
