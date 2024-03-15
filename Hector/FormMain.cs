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
        private BDD Base_De_Donnee = new BDD();

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            // On définit que le treeView de la partie gauche ne pourra pas faire moins de 200 pixels lors de l'utilisateur de l'application.
            splitContainer1.Panel1MinSize = 200;

            // Obtient le chemin de la base de données SQLite
            Base_De_Donnee.Obtenir_Chemin_Base_de_Donnees();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }
    }
}
