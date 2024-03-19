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

        TreeNode Tous_Les_Articles = new TreeNode("Tous les articles");
        TreeNode Familles = new TreeNode("Familles");
        TreeNode Marques = new TreeNode("Marques");

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

            Ajouter_Noeuds_Racine();
        }

        /// <summary>
        /// Méthode qui va ouvrir une nouvelle fenêtre modale qui va permettre à l'utilisateur de sélectionner un fichier, 
        /// et d'importer les données de celui-ci dans la BDD en mode ajout ou ecrasément.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Importer_Bouton_Click(object sender, EventArgs e)
        {
            FormImporter Fenetre_Importer = new FormImporter(Base_de_Donnees);

            // On centre la fenêtre d'importation par rapport au centre de la fenêtre de l'application.
            Fenetre_Importer.StartPosition = FormStartPosition.Manual;
            Fenetre_Importer.Location = new Point(
                Location.X + (Width - Fenetre_Importer.Width) / 2,
                Location.Y + ((Height - Fenetre_Importer.Height) / 2)
            );

            // Ajout du gestionnaire d'événements pour lorsque l'on ferme la fenetre d'importation.
            Fenetre_Importer.FormClosed += Fenetre_Importer_FormClosed;

            // Afficher la FormImporter en tant que fenêtre modale.
            Fenetre_Importer.ShowDialog();
        }

        /// <summary>
        /// Ce que l'on va réaliser quand la fenêtre importer sera fermé par l'utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fenetre_Importer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Remplir_TreeView();
        }

        /// <summary>
        /// Permets d'ajouter les trois noeuds racine à dans le TreeView.
        /// </summary>
        public void Ajouter_Noeuds_Racine()
        {
            TreeView1.Nodes.Add(Tous_Les_Articles);
            TreeView1.Nodes.Add(Familles);
            TreeView1.Nodes.Add(Marques);
        }

        /// <summary>
        /// Permets de remplir tout les sous-noeuds du TreeView.
        /// </summary>
        public void Remplir_TreeView()
        {
            // Ajout des sous noeuds pour le nom des familles.
            foreach (Famille Famille in Base_de_Donnees.Lire_Liste_Famille())
            {
                string Nom_Famille = Famille.Lire_Nom_Famille();

                // On vérifie que le nom de famille n'est pas déjà présent dans le TreeView.
                if(!Is_Nom_Famille_Present_TreeView(Nom_Famille))
                {
                    TreeNode Noeud_Nom_Famille = new TreeNode(Nom_Famille);
                    Familles.Nodes.Add(Noeud_Nom_Famille);

                    // On ajoute ensuite tous les noeuds sous familles associé à cette famille.
                    foreach (SousFamille Sous_Famille in Base_de_Donnees.Lire_Liste_Sous_Famille())
                    {
                        if(Sous_Famille.Lire_Famille().Lire_Nom_Famille() == Nom_Famille)
                        {
                            TreeNode Noeud_Nom_Sous_Famille = new TreeNode(Sous_Famille.Lire_Nom_Sous_Famille());
                            Noeud_Nom_Famille.Nodes.Add(Noeud_Nom_Sous_Famille);
                        }
                    }
                }
            }

            // Ajout des sous noeuds pour le nom des marques.
            foreach (Marque Marque in Base_de_Donnees.Lire_Liste_Marque())
            {
                string Nom_Marque = Marque.Lire_Nom_Marque();

                // On vérifie que le nom de famille n'est pas déjà présent dans le TreeView.
                if (!Is_Nom_Marque_Present_TreeView(Nom_Marque))
                {
                    TreeNode Sous_Noeud = new TreeNode(Nom_Marque);
                    Marques.Nodes.Add(Sous_Noeud);
                }
            }
        }

        /// <summary>
        /// Permets de savoir si une famille est déjà présente dans le TreeView ou pas.
        /// </summary>
        /// <param name="Nom"></param>
        /// <returns></returns>
        public bool Is_Nom_Famille_Present_TreeView(string Nom)
        {
            foreach (TreeNode Noeud in Familles.Nodes)
            {
                if(Noeud.Text == Nom)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permets de savoir si une marque est déjà présente dans le TreeView ou pas.
        /// </summary>
        /// <param name="Nom"></param>
        /// <returns></returns>
        public bool Is_Nom_Marque_Present_TreeView(string Nom)
        {
            foreach (TreeNode Noeud in Marques.Nodes)
            {
                if (Noeud.Text == Nom)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permets de savoir à quel table appartient le nom.
        /// </summary>
        /// <param name="Nom"> Le nom que l'on cherche dans les tables. </param>
        /// <returns>string, table dan laquelle se trouve le nom. </returns>
        public string Is_Marque_or_Famille_or_Sous_Famille(string Nom)
        {
            // On regarde s'il s'agit d'un nom de marque.
            foreach (Marque Marque in Base_de_Donnees.Lire_Liste_Marque())
            {
                if(Marque.Lire_Nom_Marque() == Nom)
                {
                    return "Marque";
                }
            }

            // On regarde s'il s'agit d'un nom de famille.
            foreach (Famille Famille in Base_de_Donnees.Lire_Liste_Famille())
            {
                if (Famille.Lire_Nom_Famille() == Nom)
                {
                    return "Famille";
                }
            }

            // On regarde s'il s'agit d'un nom de sous-famille.
            foreach (SousFamille Sous_Famille in Base_de_Donnees.Lire_Liste_Sous_Famille())
            {
                if (Sous_Famille.Lire_Nom_Sous_Famille() == Nom)
                {
                    return "Sous_Famille";
                }
            }

            return "";
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
