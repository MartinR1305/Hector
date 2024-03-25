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
        private BDD Base_De_Donnees;
        private string Valeur_Noeud;
        private string Type_Noeud;

        public FormAjouterArticle(BDD Base_De_Donnes_Main, string Valeur_Noeud_Main, string Type_Noeud_Main)
        {
            InitializeComponent();

            // On prends les données de la fenêtre principale.
            Base_De_Donnees = Base_De_Donnes_Main;

            // On prends la valeur et le type du noeud sélectionné dans la fenêtre principale.
            Valeur_Noeud = Valeur_Noeud_Main;
            Type_Noeud = Type_Noeud_Main;

            // On modifie la fenêtre pour qu'elle soit de taille fixe afin que l'utilisateur ne puisse pas modifier sa taille.
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // On désactive le bouton d'agrandissement.
            MaximizeBox = false;

            // On remplit les comboBox.
            Base_De_Donnees.Ajouter_Marques_Dans_ComboBox(Marque_ComboBox);
            Base_De_Donnees.Ajouter_Familles_Dans_ComboBox(Famille_ComboBox);

            // On vérifie que l'on est pas dans la liste de tous les articles.
            if(Type_Noeud != "Tous les articles")
            {
                // On pré-remplie la combobBox de la marque.
                if(Type_Noeud == "Article Marque")
                {
                    Marque_ComboBox.Text = Valeur_Noeud;
                }

                // On pré-remplie la comboBox de la sous-famille et de la famille.
                else if(Type_Noeud == "Article Sous_Famille")
                {
                    Sous_Famille_ComboBox.Enabled = true;
                    foreach (SousFamille Sous_Famille in Base_De_Donnees.Lire_Liste_Sous_Famille())
                    {
                        if(Sous_Famille.Lire_Nom_Sous_Famille() == Valeur_Noeud)
                        {
                            Famille_ComboBox.Text = Sous_Famille.Lire_Famille().Lire_Nom_Famille();
                            Sous_Famille_ComboBox.Text = Valeur_Noeud;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Permets d'ajouter un article dans la BDD.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajouter_Bouton_Click(object sender, EventArgs e)
        {
            // On vérifie que tout les champs sont remplie.
            if(Reference_TextBox.Text != "" && Description_TextBox.Text != "" && Marque_ComboBox.Text != "" && Famille_ComboBox.Text != "" && Sous_Famille_ComboBox.Text != "" && PrixHT_TextBox.Text != "" && Quantite_TextBox.Text != "")
            {
                int Result_Int; // Variable pour tester la conversio en int.
                double Result_Double; // Variable pour tester la conversion en double.

                // On vérifie la référence, le prix et la quantité sont bien des int / double.
                if (int.TryParse(Reference_TextBox.Text, out Result_Int) && double.TryParse(PrixHT_TextBox.Text, out Result_Double) && int.TryParse(Quantite_TextBox.Text, out Result_Int))
                {
                    // On vérique le prix n'est pas négatif.
                    if (Convert.ToDouble(PrixHT_TextBox.Text) >= 0D)
                    {
                        // On vérifie que la quantité est positive.
                        if (Convert.ToInt32(Quantite_TextBox.Text) >= 0)
                        {
                            // On vérifie que la référence comporte 7 chiffres.
                            if(Reference_TextBox.Text.Count() == 7)
                            {
                                // On vérifie que la référence est unique.
                                if (!Base_De_Donnees.Is_Reference_Presente(Reference_TextBox.Text))
                                {
                                    // Récupération des attributs.
                                    string Reference_Article = "F" + Reference_TextBox.Text;
                                    string Description_Article = Description_TextBox.Text;
                                    Marque Marque_Article = Base_De_Donnees.Obtenir_Marque_Par_Nom(Marque_ComboBox.Text);
                                    SousFamille Sous_Famille_Article = Base_De_Donnees.Obtenir_Sous_Famille_Par_Nom(Sous_Famille_ComboBox.Text);
                                    double PrixHT = Convert.ToDouble(PrixHT_TextBox.Text);
                                    int Quantite = Convert.ToInt32(Quantite_TextBox.Text);

                                    // Création de l'objet BDD et ajout dans la liste.
                                    Article Article = new Article(Reference_Article, Description_Article, Sous_Famille_Article, Marque_Article, PrixHT, Quantite);
                                    Base_De_Donnees.Lire_Liste_Article().Add(Article);

                                    // Ajout de l'article dans la BDD.
                                    Base_De_Donnees.Ajouter_Un_Article_BDD(Article);

                                    MessageBox.Show("L'article a été ajouté avec succès.", "Ajout de l'article réussi.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // On ferme la fenêtre.
                                    this.Close();
                                }

                                // La référence est déjà présente.
                                else
                                {
                                    MessageBox.Show("Cette référence est déjà possèder par un autre article.", "Erreur : Référence article non unique.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            // Référence inférieur ou supérieur à 7 chiffres.
                            else
                            {
                                MessageBox.Show("La référence doit posséder 7 chiffres.", "Erreur : Mauvais format de référence.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
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
        
        /// <summary>
        /// Permets de charger et activer la comboBox des sous-familles lorsque que l'on a choisi une famille.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Famille_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Sous_Famille_ComboBox.Enabled = true;
            Sous_Famille_ComboBox.Items.Clear();
            Base_De_Donnees.Ajouter_Sous_Familles_Dans_ComboBox(Sous_Famille_ComboBox, Famille_ComboBox);
        }
    }
}