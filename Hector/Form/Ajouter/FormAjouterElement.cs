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
    public partial class FormAjouterElement : Form
    {

        private BDD Base_De_Donnees;
        private string Type_Noeud;
        private string Valeur_Noeud;


        /// <summary>
        /// Constructeur de recopie de la classe
        /// </summary>
        /// <param name="Base_De_Donnes_Main"></param>
        /// <param name="Valeur_Noeud_Main"></param>
        /// <param name="Type_Noeud_Main"></param>
        public FormAjouterElement(BDD Base_De_Donnes_Main, string Valeur_Noeud_Main, string Type_Noeud_Main)
        {
            InitializeComponent();

            // On prends les données de la fenêtre principale.
            Base_De_Donnees = Base_De_Donnes_Main;

            // On prends la valeur et le type du noeud sélectionné dans la fenêtre principale.
            Type_Noeud = Type_Noeud_Main;
            Valeur_Noeud = Valeur_Noeud_Main;

            // On modifie la fenêtre pour qu'elle soit de taille fixe afin que l'utilisateur ne puisse pas modifier sa taille.
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // On désactive le bouton d'agrandissement.
            MaximizeBox = false;

            Console.WriteLine(Type_Noeud);

            if (Type_Noeud == "Famille")
            {
                Element_Label.Text = "Nom de la Famille :";

            }
            else if (Type_Noeud == "Sous_Famille")
            {
                Element_Label.Text = "Nom de la Sous-Famille :";

            }
            else if (Type_Noeud == "Marque")
            {
                Element_Label.Text = "Nom de la de Marque : ";

            }

        }

        /// <summary>
        /// Méthode qui permet d'ajouter un element (Famille/SousFamille/Marque) dans la bdd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ajouter_Element_Click(object sender, EventArgs e)
        {
            if (Element_Label.Text != "")
            {
                if (Type_Noeud == "Famille")
                {
                    string Nom_Famille = Element_TextBox.Text;
                    Famille Famille = new Famille(0, Nom_Famille);

                    // On ajoute la famille dans la BDD
                    Base_De_Donnees.Ajouter_Une_Famille_BDD(Famille);

                    // Message de succés
                    MessageBox.Show("La Famille a été ajouté avec succès.", "Ajout de la famille réussi.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // On ferme la fenêtre.
                    this.Close();
                }
                else if (Type_Noeud == "Sous_Famille")
                {
                    string Nom_Sous_Famille = Element_TextBox.Text;

                    // On ajoute la sous famille dans la BDD
                    Base_De_Donnees.Ajouter_Une_Sous_Famille_BDD(Nom_Sous_Famille, Valeur_Noeud);

                    // Message de succés
                    MessageBox.Show("La Sous-Famille a été ajouté avec succès.", "Ajout de la Sous-Famille réussi.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // On ferme la fenêtre.
                    this.Close();

                }
                else if (Type_Noeud == "Marque")
                {
                    string Nom_Marque = Element_TextBox.Text;
                    Marque Marque = new Marque(0, Nom_Marque);

                    // On ajoute la marque dans la BDD
                    Base_De_Donnees.Ajouter_Une_Marque_BDD(Marque);

                    // Message de succés
                    MessageBox.Show("La Marque a été ajouté avec succès.", "Ajout de la Marque réussi.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // On ferme la fenêtre.
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Il y a une erreur dans la BDD", "Erreur : BDD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Vous devez remplir le champs pour pouvoir ajouter un élément.", "Erreur : Champ vide", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

