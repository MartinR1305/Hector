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
    public partial class FormModifierArticle : Form
    {
        private BDD Base_De_Donnees;
        private string Valeur_Noeud;
        private string Type_Noeud;
        private Article Article_A_Modifier;

        public FormModifierArticle(BDD Base_De_Donnes_Main, Article Article)
        {
            InitializeComponent();

            // On prends les données de la fenêtre principale.
            Base_De_Donnees = Base_De_Donnes_Main;

            // On prends l'article à modifier.
            Article_A_Modifier = Article;

            // On modifie la fenêtre pour qu'elle soit de taille fixe afin que l'utilisateur ne puisse pas modifier sa taille.
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // On désactive le bouton d'agrandissement.
            MaximizeBox = false;

            // On remplit les comboBox.
            Base_De_Donnees.Ajouter_Marques_Dans_ComboBox(Marque_ComboBox);
            Base_De_Donnees.Ajouter_Familles_Dans_ComboBox(Famille_ComboBox);
            Base_De_Donnees.Ajouter_Sous_Familles_Dans_ComboBox(Sous_Famille_ComboBox, Famille_ComboBox);

            // On pré-rempli tous les champs et comboBox.
            Description_TextBox.Text = Article.Lire_Description();
            PrixHT_TextBox.Text = Convert.ToString(Article.Lire_PrixHT());
            Quantite_TextBox.Text = Convert.ToString(Article.Lire_Quantite());

            Marque_ComboBox.Text = Article.Lire_Marque().Lire_Nom_Marque();
            Famille_ComboBox.Text = Article.Lire_Sous_Famille().Lire_Famille().Lire_Nom_Famille();
        }

        /// <summary>
        /// Permets de charger et activer la comboBox des sous-familles lorsque que l'on a choisi une famille.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Famille_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sous_Famille_ComboBox.Items.Clear();
            Base_De_Donnees.Ajouter_Sous_Familles_Dans_ComboBox(Sous_Famille_ComboBox, Famille_ComboBox);
        }
    }
}
