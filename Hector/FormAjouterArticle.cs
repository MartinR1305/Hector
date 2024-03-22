using System;
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
    public partial class FormAjouterArticle : Form
    {
        public FormAjouterArticle()
        {
            InitializeComponent();

            // On modifie la fenêtre pour qu'elle soit de taille fixe afin que l'utilisateur ne puisse pas modifier sa taille.
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // On désactive le bouton d'agrandissement.
            MaximizeBox = false;
        }

        /// <summary>
        /// Permets d'ajouter un article dans la BDD.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajouter_Bouton_Click(object sender, EventArgs e)
        {
            // On vérifie que tout les champs sont remplie.
            if(Reference_TextBox.Text == "" || Description_TextBox.Text == "" || Marque_ComboBox.Text == "" || Famille_ComboBox.Text == "" || Sous_Famille_ComboBox.Text == "" || PrixHT_TextBox.Text == "" || Quantie_TextBox.Text == "")
            {
                MessageBox.Show("Vous devez remplir tous les champs pour pouvoir ajouter un article.", "Erreur : Champ vide", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {

            }
        }
    }
}