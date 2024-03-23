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
            Ajouter_Marques_Dans_ComboBox();
            Ajouter_Familles_Dans_ComboBox();

            // On vérifie que l'on est pas dans la liste de tous les articles.
            if(Type_Noeud != "Tous les articles")
            {
                // On pré-remplie la combobBox de la marque.
                if(Type_Noeud == "Marque")
                {
                    Marque_ComboBox.Text = Valeur_Noeud;
                }

                // On pré-remplie la comboBox de la sous-famille et de la famille.
                else if(Type_Noeud == "Sous_Famille")
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
                                if (!Is_Reference_Presente(Reference_TextBox.Text))
                                {
                                    // Récupération des attributs.
                                    string Reference_Article = "F" + Reference_TextBox.Text;
                                    string Description_Article = Description_TextBox.Text;
                                    Marque Marque_Article = Obtenir_Marque_Par_Nom(Marque_ComboBox.Text);
                                    Famille Famille_Article = Obtenir_Famille_Par_Nom(Famille_ComboBox.Text);
                                    SousFamille Sous_Famille_Article = Obtenir_Sous_Famille_Par_Nom(Sous_Famille_ComboBox.Text);
                                    double PrixHT = Convert.ToDouble(PrixHT_TextBox.Text);
                                    int Quantite = Convert.ToInt32(Quantite_TextBox.Text);

                                    // Création de l'objet BDD et ajout dans la liste.
                                    Article Article = new Article(Reference_Article, Description_Article, Sous_Famille_Article, Marque_Article, PrixHT, Quantite);
                                    Base_De_Donnees.Lire_Liste_Article().Add(Article);

                                    // Ajout de l'article dans la BDD.
                                    Base_De_Donnees.Ajouter_Un_Article_BDD(Article);

                                    MessageBox.Show("L'article a été ajouté avec succès.", "Ajout de l'article réussi.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Permets de remplir la comboBox avec le nom des marques.
        /// </summary>
        public void Ajouter_Marques_Dans_ComboBox()
        {
            foreach(Marque Marque in Base_De_Donnees.Lire_Liste_Marque())
            {
                Marque_ComboBox.Items.Add(Marque.Lire_Nom_Marque());
            }
        }

        /// <summary>
        /// Permets de remplir la comboBox avec le nom des familles.
        /// </summary>
        public void Ajouter_Familles_Dans_ComboBox()
        {
            foreach (Famille Famille in Base_De_Donnees.Lire_Liste_Famille())
            {
                Famille_ComboBox.Items.Add(Famille.Lire_Nom_Famille());
            }
        }

        /// <summary>
        /// Permets de remplir la comboBox avec le nom des sous-familles.
        /// </summary>
        public void Ajouter_Sous_Familles_Dans_ComboBox()
        {
            foreach (SousFamille Sous_Famille in Base_De_Donnees.Lire_Liste_Sous_Famille())
            {
                if(Sous_Famille.Lire_Famille().Lire_Nom_Famille() == Famille_ComboBox.Text)
                {
                    Sous_Famille_ComboBox.Items.Add(Sous_Famille.Lire_Nom_Sous_Famille());
                }
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
            Ajouter_Sous_Familles_Dans_ComboBox();
        }

        /// <summary>
        /// Permets de savoir si la référence est déjà présente dans la BDD ou non.
        /// </summary>
        /// <param name="Ref"> Référence que l'on veut vérifier. </param>
        /// <returns> bool, indique la référence est déjà présente ou non. </returns>
        private bool Is_Reference_Presente(string Ref)
        {
            foreach(Article Article in Base_De_Donnees.Lire_Liste_Article())
            {
                Console.WriteLine(Article.Lire_Ref_Article() + " | " + "F" + Convert.ToString(Ref));
                if(Article.Lire_Ref_Article() == "F" + Convert.ToString(Ref)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permets d'obtenir la marque à partir de son nom.
        /// </summary>
        /// <param name="Liste_Marque"> Liste de marque où l'on va chercher la marque. </param>
        /// <param name="Nom"> Nom de la marque que l'on recherche. </param>
        /// <returns> Marque, la marque que l'on cherche. </returns>
        public Marque Obtenir_Marque_Par_Nom(string Nom)
        {
            // On regarde chaque marque de la liste.
            foreach (Marque Marque in Base_De_Donnees.Lire_Liste_Marque())
            {
                // Si l'on trouve la marque voulue.
                if (Marque.Lire_Nom_Marque().ToUpper() == Nom.ToUpper())
                {
                    return Marque;
                }
            }
            return null;
        }

        /// <summary>
        /// Permets d'obtenir la sous-famille à partir de son nom.
        /// </summary>
        /// <param name="Liste_Sous_Famille"> Liste de sous-famille où l'on va chercher la sous-famille. </param>
        /// <param name="Nom"> Nom de la sous-famille que l'on recherche. </param>
        /// <returns> SousFamille, la sous-famille que l'on cherche. </returns>
        public SousFamille Obtenir_Sous_Famille_Par_Nom(string Nom)
        {
            // On regarde chaque sous-famille de la liste.
            foreach (SousFamille Sous_Famille in Base_De_Donnees.Lire_Liste_Sous_Famille())
            {
                // Si l'on trouve la sous-famille voulue.
                if (Sous_Famille.Lire_Nom_Sous_Famille().ToUpper() == Nom.ToUpper())
                {
                    return Sous_Famille;
                }
            }
            return null;
        }

        /// <summary>
        /// Permets d'obtenir la famille à partir de son nom.
        /// </summary>
        /// <param name="Liste_Famille"> Liste de famille où l'on chercher la famille. </param>
        /// <param name="Nom"> Nom de la famille que l'on recherche. </param>
        /// <returns> Famille, la famille que l'on cherche. </returns>
        public Famille Obtenir_Famille_Par_Nom(string Nom)
        {
            // On regarde chaque famille de la liste.
            foreach (Famille Famille in Base_De_Donnees.Lire_Liste_Famille())
            {
                // Si l'on trouve la famille voulue.
                if (Famille.Lire_Nom_Famille().ToUpper() == Nom.ToUpper())
                {
                    return Famille;
                }
            }
            return null;
        }
    }
}