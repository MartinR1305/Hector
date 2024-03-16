using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hector
{
    public partial class FormMain : Form
    {
        private BDD Base_de_Donnees;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            Base_de_Donnees = new BDD();

            // On centre la fenêtre à son lancement par rapport au centre de l'écran.
            StartPosition = FormStartPosition.CenterScreen;

            // On définit que le treeView de la partie gauche ne pourra pas faire moins de 200 pixels lors de l'utilisateur de l'application.
            SplitContainer1.Panel1MinSize = 200;

            // Obtient le chemin de la base de données SQLite
            Base_de_Donnees.Obtenir_Chemin_Base_de_Donnees();
        }

        /// <summary>
        /// Méthode qui va ouvrir une nouvelle fenêtre modale qui va permettre à l'utilisateur de sélectionner un fichier, 
        /// et d'importer les données de celui-ci dans la BDD en mode ajout ou ecrasément.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Importer_Bouton_Click(object sender, EventArgs e)
        {
            FormImporter Fenetre_Importer = new FormImporter();

            // On centre la fenêtre d'importation par rapport au centre de la fenêtre de l'application.
            Fenetre_Importer.StartPosition = FormStartPosition.Manual;
            Fenetre_Importer.Location = new Point(
                Location.X + (Width - Fenetre_Importer.Width) / 2,
                Location.Y + ((Height - Fenetre_Importer.Height) / 2)
            );

            // Afficher la FormImporter en tant que fenêtre modale.
            Fenetre_Importer.ShowDialog();
        }
    }
}
