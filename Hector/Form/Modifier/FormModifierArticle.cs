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
            Sous_Famille_ComboBox.Text = Article.Lire_Sous_Famille().Lire_Nom_Sous_Famille();
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

        /// <summary>
        /// Permets de modifier un article.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Bouton_Click(object sender, EventArgs e)
        {
            // On vérifie que tout les champs sont remplie.
            if (Description_TextBox.Text != "" && Marque_ComboBox.Text != "" && Famille_ComboBox.Text != "" && Sous_Famille_ComboBox.Text != "" && PrixHT_TextBox.Text != "" && Quantite_TextBox.Text != "")
            {
                int Result_Int; // Variable pour tester la conversio en int.
                double Result_Double; // Variable pour tester la conversion en double.

                // On vérifie la référence, le prix et la quantité sont bien des int / double.
                if (double.TryParse(PrixHT_TextBox.Text, out Result_Double) && int.TryParse(Quantite_TextBox.Text, out Result_Int))
                {
                    // On vérique le prix n'est pas négatif.
                    if (Convert.ToDouble(PrixHT_TextBox.Text) >= 0D)
                    {
                        // On vérifie que la quantité est positive.
                        if (Convert.ToInt32(Quantite_TextBox.Text) >= 0)
                        {
                            // Récupération des attributs.
                            string Description_Article = Description_TextBox.Text;
                            Marque Marque_Article = Base_De_Donnees.Obtenir_Marque_Par_Nom(Marque_ComboBox.Text);
                            SousFamille Sous_Famille_Article = Base_De_Donnees.Obtenir_Sous_Famille_Par_Nom(Sous_Famille_ComboBox.Text);
                            double PrixHT_Article = Convert.ToDouble(PrixHT_TextBox.Text);
                            int Quantite_Article = Convert.ToInt32(Quantite_TextBox.Text);

                            // Modification de l'objet BDD dans la liste.
                            foreach(Article Article_Boucle in Base_De_Donnees.Lire_Liste_Article())
                            {
                                if(Article_Boucle.Lire_Ref_Article() == Article_A_Modifier.Lire_Ref_Article())
                                {
                                    Article_Boucle.Modifier_Description(Description_Article);
                                    Article_Boucle.Modifier_Marque(Marque_Article);
                                    Article_Boucle.Modifier_Sous_Famille(Sous_Famille_Article);
                                    Article_Boucle.Modifier_PrixHT(PrixHT_Article);
                                    Article_Boucle.Modifier_Quantite(Quantite_Article);
                                }
                            }

                            // Modification de l'article dans la BDD.
                            Base_De_Donnees.Modifier_Article_BDD(Article_A_Modifier.Lire_Ref_Article(), Description_Article, Marque_Article, Sous_Famille_Article, PrixHT_Article, Quantite_Article);

                            MessageBox.Show("L'article a été modifié avec succès.", "Modification de l'article réussi.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // On ferme la fenêtre.
                            this.Close();
                        }

                        // Quantité négative.
                        else
                        {
                            MessageBox.Show("Vous ne pouvez pas ajouter d'article avec une quantité négative.", "Erreur : Quantité Négative", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    // Prix négatif.
                    else
                    {
                        MessageBox.Show("Vous ne pouvez pas ajouter d'article avec un prix négatif.", "Erreur : Prix négatif", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Référence != int || prix != int / double || quantité != int.
                else
                {
                    MessageBox.Show("Vosu ne respectez pas le type de certaines variables.\nLa référence doit être une valeur entière.\nLe prix doit être une valeur flottante.\nLa quantité doit être une valeur entière.", "Erreur : Mauvais type de variables.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            // Un champ est vide.
            else
            {
                MessageBox.Show("Vous devez remplir tous les champs pour pouvoir ajouter un article.", "Erreur : Champ vide", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
