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
    public partial class FormModifierElement : Form
    {
        private int ID_Element;
        private string Type_Element;
        private string Nom_De_Base;
        private BDD Base_De_Donnes;

        /// <summary>
        /// Constructeur de confort.
        /// </summary>
        /// <param name="ID_Element"> ID de l'élément que l'on veut modifier. </param>
        /// <param name="Type_Noeud_Main"> Type de l'élément que l'on veut modifier. </param>
        /// <param name="Base_De_Donnes_Main"> Listes des données de la fenêtre principale. </param>
        public FormModifierElement(int ID_Element_main, string Type_Noeud_Main, BDD Base_De_Donnes_Main)
        {
            InitializeComponent();

            // On récupère le type du noeud du main.
            Type_Element = Type_Noeud_Main;

            // On récupère les listes de la fenêtres principales.
            Base_De_Donnes = Base_De_Donnes_Main;

            // On récupère l'ID de l'élément que l'on veut modifier.
            ID_Element = ID_Element_main;

            // Si l'élément est une famille.
            if (Type_Element == "Famille")
            {
                // On modifie le titre, le label et texte du bouton de la fenêtre.
                this.Text = "Modifier Famille";
                Nom_Element_Label.Text = "Nom de la famille :";
                Modifier_Element_Bouton.Text = "Modifier Famille";

                // On pré-rempli le texte de la textBox et on sauvegarde le nom de base de l'élément.
                foreach(Famille Famille in Base_De_Donnes.Lire_Liste_Famille())
                {
                    if(Famille.Lire_Ref_Famille() == ID_Element)
                    {
                        Nom_De_Base = Famille.Lire_Nom_Famille();
                        Nom_Element_TextBox.Text = Nom_De_Base;
                    }
                }
            }

            // Si l'élément est une sous-famille.
            else if (Type_Element == "Sous_Famille")
            {
                // On modifie le titre, le label et texte du bouton de la fenêtre.
                this.Text = "Modifier Sous-Famille";
                Nom_Element_Label.Text = "Nom de la sous-famille :";
                Modifier_Element_Bouton.Text = "Modifier Sous-Famille";

                // On pré-rempli le texte de la textBox et on sauvegarde le nom de base de l'élément.
                foreach (SousFamille Sous_Famille in Base_De_Donnes.Lire_Liste_Sous_Famille())
                {
                    if (Sous_Famille.Lire_Ref_Sous_Famille() == ID_Element)
                    {
                        Nom_De_Base = Sous_Famille.Lire_Nom_Sous_Famille();
                        Nom_Element_TextBox.Text = Nom_De_Base;
                    }
                }
            }

            // Si l'élément est une marque.
            else if (Type_Element == "Marque")
            {
                // On modifie le titre, le label et texte du bouton de la fenêtre.
                this.Text = "Modifier Marque";
                Nom_Element_Label.Text = "Nom de la marque :";
                Modifier_Element_Bouton.Text = "Modifier Marque";

                // On pré-rempli le texte de la textBox et on sauvegarde le nom de base de l'élément.
                foreach (Marque Marque in Base_De_Donnes.Lire_Liste_Marque())
                {
                    if (Marque.Lire_Ref_Marque() == ID_Element)
                    {
                        Nom_De_Base = Marque.Lire_Nom_Marque();
                        Nom_Element_TextBox.Text = Nom_De_Base;
                    }
                }
            }
        }

        /// <summary>
        /// Permets de modifier l'élément.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modifier_Element_Bouton_Click(object sender, EventArgs e)
        {
            // On vérifie que le nom de l'élément n'est pas vide.
            if(!(Nom_Element_TextBox.Text == ""))
            {
                // Si le type de l'élément est une famille.
                if (Type_Element == "Famille")
                {
                    Modifier_Famille();
                }

                // Si le type de l'élément est une sous-famille.
                else if (Type_Element == "Sous_Famille")
                {
                    Modifier_Sous_Famille();
                }

                // Si le type de l'élément est une marque.
                else if (Type_Element == "Marque")
                {
                    Modifier_Marque();
                }
            }

            // Le nom de l'élément est vide.
            else
            {
                MessageBox.Show("Vous ne pouvez pas donner de nom vide.", "Erreur : Nom vide", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Permets de vérifier si le nom de famille est déjà prit par une autre famille ou non.
        /// </summary>
        /// <param name="Nom"> Nom que l'on veut vérifier. </param>
        /// <returns> bool, indique si le nom de la famille est déjà présente ou non. </returns>
        public bool Is_Nom_Famille_Deja_Present(string Nom)
        {
            foreach (Famille Famille in Base_De_Donnes.Lire_Liste_Famille())
            {
                if (Nom.ToUpper() == Famille.Lire_Nom_Famille().ToUpper() && Nom.ToUpper() != Nom_De_Base.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permets de vérifier si le nom de marque est déjà prit par une autre sous-famille ou non.
        /// </summary>
        /// <param name="Nom"> Nom que l'on veut vérifier. </param>
        /// <returns> bool, indique si le nom de la marque est déjà présente ou non. </returns>
        public bool Is_Nom_Marque_Deja_Present(string Nom)
        {
            foreach (Marque Marque in Base_De_Donnes.Lire_Liste_Marque())
            {
                if (Nom.ToUpper() == Marque.Lire_Nom_Marque().ToUpper() && Nom.ToUpper() != Nom_De_Base.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permets de vérifier si le nom de sous-famille est déjà prit par une autre sous-famille ou non.
        /// </summary>
        /// <param name="Nom"> Nom que l'on veut vérifier. </param>
        /// <returns> bool, indique si le nom de la sous-famille est déjà présente ou non. </returns>
        public bool Is_Nom_Sous_Famille_Deja_Present(string Nom)
        {
            foreach (SousFamille Sous_Famille in Base_De_Donnes.Lire_Liste_Sous_Famille())
            {
                if (Nom.ToUpper() == Sous_Famille.Lire_Nom_Sous_Famille().ToUpper() && Nom.ToUpper() != Nom_De_Base.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permets de modifier une famille.
        /// </summary>
        public void Modifier_Famille()
        {
            // On vérifie que le nom n'est pas déjà utilisé par une autre famille.
            if (!Is_Nom_Famille_Deja_Present(Nom_Element_TextBox.Text))
            {
                // On modifie le nom de la famille dans la liste.
                foreach (Famille Famille in Base_De_Donnes.Lire_Liste_Famille())
                {
                    if (Famille.Lire_Ref_Famille() == ID_Element)
                    {
                        Famille.Modifier_Nom_Famille(Nom_Element_TextBox.Text);
                    }
                }

                // On modifie le nom de la famille dans la BDD.
                Base_De_Donnes.Modifier_Famille_BDD(ID_Element, Nom_Element_TextBox.Text);

                MessageBox.Show("La famille a été modifié avec succès.", "Modification de la famille réussie.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // On ferme la fenêtre.
                this.Close();
            }

            else
            {
                MessageBox.Show("Ce nom de famille est déjà utilisé par une autre famille.", "Erreur : Nom de famille déjà utilisé", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Permets de modifier une sous-famille.
        /// </summary>
        public void Modifier_Sous_Famille()
        {
            // On vérifie que le nom n'est pas déjà utilisé par une autre sous-famille.
            if (!Is_Nom_Sous_Famille_Deja_Present(Nom_Element_TextBox.Text))
            {
                // On modifie le nom de la sous-famille dans la liste.
                foreach (SousFamille Sous_Famille in Base_De_Donnes.Lire_Liste_Sous_Famille())
                {
                    if (Sous_Famille.Lire_Ref_Sous_Famille() == ID_Element)
                    {
                        Sous_Famille.Modifier_Nom_Sous_Famille(Nom_Element_TextBox.Text);
                    }
                }

                // On modifie le nom de la famille dans la BDD.
                Base_De_Donnes.Modifier_Sous_Famille_BDD(ID_Element, Nom_Element_TextBox.Text);

                MessageBox.Show("La sous-famille a été modifié avec succès.", "Modification de la sous-famille réussie.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // On ferme la fenêtre.
                this.Close();
            }

            else
            {
                MessageBox.Show("Ce nom de sous-famille est déjà utilisé par une autre sous-famille.", "Erreur : Nom de sous-famille déjà utilisé", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Permets de modifier une marque.
        /// </summary>
        public void Modifier_Marque()
        {
            // On vérifie que le nom n'est pas déjà utilisé par une autre marque.
            if (!Is_Nom_Marque_Deja_Present(Nom_Element_TextBox.Text))
            {
                // On modifie le nom de la famille dans la liste.
                foreach (Marque Marque in Base_De_Donnes.Lire_Liste_Marque())
                {
                    if (Marque.Lire_Ref_Marque() == ID_Element)
                    {
                        Marque.Modifier_Nom_Marque(Nom_Element_TextBox.Text);
                    }
                }

                // On modifie le nom de la marque dans la BDD.
                Base_De_Donnes.Modifier_Marque_BDD(ID_Element, Nom_Element_TextBox.Text);

                MessageBox.Show("La marque a été modifié avec succès.", "Modification de la marque réussie.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // On ferme la fenêtre.
                this.Close();
            }

            else
            {
                MessageBox.Show("Ce nom de marque est déjà utilisé par une autre famille.", "Erreur : Nom de marque déjà utilisé", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
