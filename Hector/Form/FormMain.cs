using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

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

            // On définit que le treeView de la partie gauche ne pourra pas faire moins de 200 pixels lors de l'utilisateur de l'application.
            SplitContainer1.Panel1MinSize = 200;

            // Gestionnaire d'événement pour la pression de touches.
            this.KeyDown += Pression_Touche_Clavier;

            // Gestionnaire d'événement pour la pression de clics de la souris dans la listView.
            ListView1.MouseDown += ListView1_Clic_Souris;

            // Obtient le chemin de la base de données SQLite
            Base_de_Donnees.Obtenir_Chemin_Base_de_Donnees();

            Ajouter_Noeuds_Racine();

            // On mets la fenêtre en pleine écran au démarrage afin de faciliter la visibilité de la list view.
            //this.WindowState = FormWindowState.Maximized;

            // On actualise l'application avec le contenu de la BDD.
            Actualiser(false);

            // On regarde si la fenêtre a été fermé en plein ou écran.
            if(!Properties.Settings.Default.PleinEcran)
            {
                // On désactive le plein écran de la fenetre.
                this.WindowState = FormWindowState.Normal;

                // Changement de taille
                this.Width = Properties.Settings.Default.TailleX;
                this.Height = Properties.Settings.Default.TailleY;

                // Changement de position
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(Properties.Settings.Default.PositionX, Properties.Settings.Default.PositionY);
            }

            else
            {
                // On active le plein écran de la fenetre.
                this.WindowState = FormWindowState.Maximized;
            }
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
            Fenetre_Importer.FormClosed += Fenetre_FormClosed;

            // Afficher la FormImporter en tant que fenêtre modale.
            Fenetre_Importer.ShowDialog();
        }

        /// <summary>
        /// Méthode qui permet d'ouvir la fenetre d'exportation de la BDD en fichier CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exporter_Bouton_Click(object sender, EventArgs e)
        {
            FormExporter Fenetre_Exporter = new FormExporter(Base_de_Donnees);

            // On centre la fenêtre d'exportation par rapport au centre de la fenêtre de l'application.
            Fenetre_Exporter.StartPosition = FormStartPosition.Manual;
            Fenetre_Exporter.Location = new Point(
                Location.X + (Width - Fenetre_Exporter.Width) / 2,
                Location.Y + ((Height - Fenetre_Exporter.Height) / 2)
            );

            // Afficher la FormExporter en tant que fenêtre modale.
            Fenetre_Exporter.ShowDialog();
        }

        /// <summary>
        /// Ce que l'on va réaliser quand la fenêtre importer sera fermé par l'utilisateur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fenetre_FormClosed(object sender, FormClosedEventArgs e)
        {
            Actualiser(false);
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
            Familles.Nodes.Clear();

            // Ajout des sous noeuds pour le nom des familles.
            foreach (Famille Famille in Base_de_Donnees.Lire_Liste_Famille())
            {
                string Nom_Famille = Famille.Lire_Nom_Famille();

                // On vérifie que le nom de famille n'est pas déjà présent dans le TreeView.
                if (!Is_Nom_Famille_Present_TreeView(Nom_Famille))
                {
                    TreeNode Noeud_Nom_Famille = new TreeNode(Nom_Famille);
                    Familles.Nodes.Add(Noeud_Nom_Famille);

                    // On ajoute ensuite tous les noeuds sous familles associé à cette famille.
                    foreach (SousFamille Sous_Famille in Base_de_Donnees.Lire_Liste_Sous_Famille())
                    {

                        if (Sous_Famille.Lire_Famille().Lire_Nom_Famille() == Nom_Famille)
                        {
                            TreeNode Noeud_Nom_Sous_Famille = new TreeNode(Sous_Famille.Lire_Nom_Sous_Famille());
                            Noeud_Nom_Famille.Nodes.Add(Noeud_Nom_Sous_Famille);
                        }
                    }
                }
            }

            Marques.Nodes.Clear();

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
                if (Noeud.Text == Nom)
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
        public string Obtenir_Type_Noeud(string Nom)
        {
            if (Nom == "Marques")
            {
                return "Marque";
            }

            // On regarde s'il s'agit d'un nom de familles.
            else if (Nom == "Familles")
            {
                return "Famille";
            }

            // On regarde s'il s'agit d'un nom d'article.
            else if (Nom == "Tous les articles")
            {
                return "Tous les articles";
            }

            // On regarde s'il s'agit d'un nom de sous-famille.
            foreach (Famille Famille in Base_de_Donnees.Lire_Liste_Famille())
            {
                if (Famille.Lire_Nom_Famille() == Nom)
                {
                    return "Sous_Famille";
                }
            }

            // On regarde s'il s'agit d'un nom d'article dans une sous-famille.
            foreach (SousFamille Sous_Famille in Base_de_Donnees.Lire_Liste_Sous_Famille())
            {
                if (Sous_Famille.Lire_Nom_Sous_Famille() == Nom)
                {
                    return "Article Sous_Famille";
                }
            }

            // On regarde s'il s'agit d'un nom d'article dans une marque.
            foreach (Marque Marque in Base_de_Donnees.Lire_Liste_Marque())
            {
                if (Marque.Lire_Nom_Marque() == Nom)
                {
                    return "Article Marque";
                }
            }

            return "";
        }

        /// <summary>
        /// Permets d'afficher dans la liste view les éléments en fonction du noeud cliqué.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string Selected_Node_Text = e.Node.Text;
            Remplir_Listview(Selected_Node_Text);
        }

        /// <summary>
        /// Permets de remplir la list View en fonction du noeud.
        /// </summary>
        /// <param name="Selected_Node_Text"></param>
        public void Remplir_Listview(string Selected_Node_Text)
        {
            // L'utilisateur a cliqué sur le Nœud "Tous les articles".
            if (Selected_Node_Text == "Tous les articles")
            {
                ListView1.Clear();

                Ajouter_Colonnes_Articles_ListView();
                Ajouter_Articles_ListView();
                Ajuster_Largeur_Colonne_ListeView();

                ListView1.Groups.Clear();
            }

            // L'utilisateur a cliqué sur le Nœud "Familles".
            else if (Selected_Node_Text == "Familles")
            {
                ListView1.Clear();

                Ajouter_Colonnes_Familles_ListView();
                Ajouter_Familles_ListView();
                Ajuster_Largeur_Colonne_ListeView();

                ListView1.Groups.Clear();
            }

            // L'utilisateur a cliqué sur un Nœud "Famille".
            else if (Obtenir_Type_Noeud(Selected_Node_Text) == "Sous_Famille")
            {
                ListView1.Clear();

                Ajouter_Colonnes_Sous_Familles_ListView();
                Ajouter_Sous_Familles_ListView(Selected_Node_Text);
                Ajuster_Largeur_Colonne_ListeView();

                ListView1.Groups.Clear();
            }

            // L'utilisateur a cliqué sur un Nœud "Sous-Famille" pour lire des articles.
            else if (Obtenir_Type_Noeud(Selected_Node_Text) == "Article Sous_Famille")
            {
                ListView1.Clear();

                Ajouter_Colonnes_Articles_ListView();
                Ajouter_Articles_D_une_Sous_Famille(Selected_Node_Text);
                Ajuster_Largeur_Colonne_ListeView();

                ListView1.Groups.Clear();
            }

            // L'utilisateur a cliqué sur le Nœud "Marques".
            else if (Selected_Node_Text == "Marques")
            {
                ListView1.Clear();

                Ajouter_Colonnes_Marques_ListView();
                Ajouter_Marques_ListView();
                Ajuster_Largeur_Colonne_ListeView();

                ListView1.Groups.Clear();
            }

            // L'utilisateur a cliqué sur un Nœud "Marque" pour lire des articles.
            else if (Obtenir_Type_Noeud(Selected_Node_Text) == "Article Marque")
            {
                ListView1.Clear();
                Ajouter_Colonnes_Articles_ListView();
                Ajouter_Articles_D_une_Marque(Selected_Node_Text);
                Ajuster_Largeur_Colonne_ListeView();

                ListView1.Groups.Clear();
            }
        }

        /// <summary>
        /// Permets d'ajouter les colonnes correspondantes pour la lecture d'articles dans la liste view.
        /// </summary>
        public void Ajouter_Colonnes_Articles_ListView()
        {
            // Permets de ne pas afficher de colonnes s'il n'y a pas encore eu d'intégration.
            if (Base_de_Donnees.Lire_Liste_Article().Count != 0)
            {
                ListView1.Columns.Add("Référence");
                ListView1.Columns.Add("Description");
                ListView1.Columns.Add("Familles");
                ListView1.Columns.Add("Sous Familles");
                ListView1.Columns.Add("Marques");
                ListView1.Columns.Add("Quantité");
            }
        }

        /// <summary>
        /// Permets d'ajouter les colonnes correspondantes pour la lecture de familles dans la liste view.
        /// </summary>
        public void Ajouter_Colonnes_Familles_ListView()
        {
            // Permets de ne pas afficher de colonnes s'il n'y a pas encore eu d'intégration.
            if (Base_de_Donnees.Lire_Liste_Famille().Count != 0)
            {
                ListView1.Columns.Add("Référence");
                ListView1.Columns.Add("Nom");
            }
        }

        /// <summary>
        /// Permets d'ajouter les colonnes correspondantes pour la lecture de sous-familles dans la liste view.
        /// </summary>
        public void Ajouter_Colonnes_Sous_Familles_ListView()
        {
            // Permets de ne pas afficher de colonnes s'il n'y a pas encore eu d'intégration.
            if (Base_de_Donnees.Lire_Liste_Sous_Famille().Count != 0)
            {
                ListView1.Columns.Add("Référence");
                ListView1.Columns.Add("Nom");
            }
        }

        /// <summary>
        /// Permets d'ajouter les colonnes correspondantes pour la lecture de marques dans la liste view.
        /// </summary>
        public void Ajouter_Colonnes_Marques_ListView()
        {
            // Permets de ne pas afficher de colonnes s'il n'y a pas encore eu d'intégration.
            if (Base_de_Donnees.Lire_Liste_Marque().Count != 0)
            {
                ListView1.Columns.Add("Référence");
                ListView1.Columns.Add("Nom");
            }
        }

        /// <summary>
        /// Permets d'ajouter tous les articles dans la liste view.
        /// </summary>
        public void Ajouter_Articles_ListView()
        {
            foreach (Article Article in Base_de_Donnees.Lire_Liste_Article())
            {
                ListViewItem Item_Article = new ListViewItem(Article.Lire_Ref_Article());
                Item_Article.SubItems.Add(Article.Lire_Description());
                Item_Article.SubItems.Add(Article.Lire_Sous_Famille().Lire_Famille().Lire_Nom_Famille());
                Item_Article.SubItems.Add(Article.Lire_Sous_Famille().Lire_Nom_Sous_Famille());
                Item_Article.SubItems.Add(Article.Lire_Marque().Lire_Nom_Marque());
                Item_Article.SubItems.Add(Convert.ToString(Article.Lire_Quantite()));

                ListView1.Items.Add(Item_Article);
            }
        }

        /// <summary>
        /// Permets d'ajouter toutes les familles dans la liste view.
        /// </summary>
        public void Ajouter_Familles_ListView()
        {
            foreach (Famille Famille in Base_de_Donnees.Lire_Liste_Famille())
            {
                ListViewItem Item_Famille = new ListViewItem(Convert.ToString(Famille.Lire_Ref_Famille()));
                Item_Famille.SubItems.Add(Famille.Lire_Nom_Famille());

                ListView1.Items.Add(Item_Famille);
            }
        }

        /// <summary>
        /// ermets d'ajouter toutes les sous-familles d'une famille dans la liste view.
        /// </summary>
        /// <param name="Nom_Famille"> Famille à laquelle on cherche toutes les sous-familles. </param>
        public void Ajouter_Sous_Familles_ListView(string Nom_Famille)
        {
            foreach (SousFamille Sous_Famille in Base_de_Donnees.Lire_Liste_Sous_Famille())
            {
                if (Sous_Famille.Lire_Famille().Lire_Nom_Famille() == Nom_Famille)
                {
                    ListViewItem Item_Sous_Famille = new ListViewItem(Convert.ToString(Sous_Famille.Lire_Ref_Sous_Famille()));
                    Item_Sous_Famille.SubItems.Add(Sous_Famille.Lire_Nom_Sous_Famille());

                    ListView1.Items.Add(Item_Sous_Famille);
                }
            }
        }

        /// <summary>
        /// Permets d'ajouter uniquement les articles d'une certaine sous-famille dans la liste view.
        /// </summary>
        /// <param name="Nom_Sous_Famille"> Nom de la sous famille dont on cherche les articles. </param>
        public void Ajouter_Articles_D_une_Sous_Famille(string Nom_Sous_Famille)
        {
            foreach (Article Article in Base_de_Donnees.Lire_Liste_Article())
            {
                if (Article.Lire_Sous_Famille().Lire_Nom_Sous_Famille() == Nom_Sous_Famille)
                {
                    ListViewItem Item_Article = new ListViewItem(Article.Lire_Ref_Article());
                    Item_Article.SubItems.Add(Article.Lire_Description());
                    Item_Article.SubItems.Add(Article.Lire_Sous_Famille().Lire_Famille().Lire_Nom_Famille());
                    Item_Article.SubItems.Add(Article.Lire_Sous_Famille().Lire_Nom_Sous_Famille());
                    Item_Article.SubItems.Add(Article.Lire_Marque().Lire_Nom_Marque());
                    Item_Article.SubItems.Add(Convert.ToString(Article.Lire_Quantite()));

                    ListView1.Items.Add(Item_Article);
                }
            }
        }

        /// <summary>
        /// Permets d'ajouter toutes les marques dans la liste view.
        /// </summary>
        public void Ajouter_Marques_ListView()
        {
            foreach (Marque Marque in Base_de_Donnees.Lire_Liste_Marque())
            {
                ListViewItem Item_Marque = new ListViewItem(Convert.ToString(Marque.Lire_Ref_Marque()));
                Item_Marque.SubItems.Add(Marque.Lire_Nom_Marque());

                ListView1.Items.Add(Item_Marque);
            }
        }

        /// <summary>
        /// Permets d'ajouter uniquement les articles d'une certaine marque dans la liste view.
        /// </summary>
        /// <param name="Nom_Marque"> Nom de la marque dont on cherche les articles. </param>
        public void Ajouter_Articles_D_une_Marque(string Nom_Marque)
        {
            foreach (Article Article in Base_de_Donnees.Lire_Liste_Article())
            {
                if (Article.Lire_Marque().Lire_Nom_Marque() == Nom_Marque)
                {
                    ListViewItem Item_Article = new ListViewItem(Article.Lire_Ref_Article());
                    Item_Article.SubItems.Add(Article.Lire_Description());
                    Item_Article.SubItems.Add(Article.Lire_Sous_Famille().Lire_Famille().Lire_Nom_Famille());
                    Item_Article.SubItems.Add(Article.Lire_Sous_Famille().Lire_Nom_Sous_Famille());
                    Item_Article.SubItems.Add(Article.Lire_Marque().Lire_Nom_Marque());
                    Item_Article.SubItems.Add(Convert.ToString(Article.Lire_Quantite()));

                    ListView1.Items.Add(Item_Article);
                }
            }
        }

        /// <summary>
        /// Permets d'ajuster la largeur des colonnes en fonction du contenu et du titre des colonnes.
        /// </summary>
        public void Ajuster_Largeur_Colonne_ListeView()
        {
            // On ajuste la largeur des colonnes en fonction du contenue des colonnes.
            ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            // On ajuste la largeur des colonnes en fonction du titre des colonnes ( utile pour référence et quantité )
            ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            string Column_Name = ListView1.Columns[e.Column].Text;

            // Lorsque l'on clique sur la colonne Description dans une liste d'article.
            if (Column_Name == "Description")
            {
                Groupes_Selon_Description_Article();
            }

            // Lorsque l'on clique sur la colonne Description dans une liste d'article.
            else if (Column_Name == "Familles")
            {
                Groupes_Selon_Familles_Article();
            }

            // Lorsque l'on clique sur la colonne Description dans une liste d'article.
            else if (Column_Name == "Sous Familles")
            {
                Groupes_Selon_Sous_Familles_Article();
            }

            // Lorsque l'on clique sur la colonne Description dans une liste d'article.
            else if (Column_Name == "Marques")
            {
                Groupes_Selon_Marques_Article();
            }

            else if (Column_Name == "Nom")
            {
                Groupes_Selon_Nom();
            }
        }

        /// <summary>
        /// Permets de trier la liste view en groupes selon la première lettre de la description de l'article.
        /// </summary>
        public void Groupes_Selon_Description_Article()
        {
            for (char Lettre = 'A'; Lettre <= 'Z'; Lettre++)
            {
                // On crée le groupe correspondant à la lettre.
                ListViewGroup Groupe = new ListViewGroup(Convert.ToString(Lettre).ToUpper(), HorizontalAlignment.Left);

                // On l'ajoute à la listView.
                ListView1.Groups.Add(Groupe);

                foreach (ListViewItem Item in ListView1.Items)
                {
                    // On ajoute l'item si sa première lettre est la lettre actuelle.
                    if (Obtenir_1er_Lettre_String(Item.SubItems[1].Text) == Lettre)
                    {
                        ListViewItem Item_Temp = new ListViewItem(Item.SubItems[0].Text, Groupe);
                        ListView1.Items.Add(Item_Temp);
                        Item_Temp.SubItems.Add(Item.SubItems[1].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[2].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[3].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[4].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[5].Text);

                        ListView1.Items.Remove(Item);
                    }
                }
            }
        }

        /// <summary>
        /// Permets de trier la liste view en groupes selon la famille de l'article.
        /// </summary>
        public void Groupes_Selon_Familles_Article()
        {
            foreach (Famille Famille in Base_de_Donnees.Lire_Liste_Famille())
            {
                // On crée le groupe correspondant à la famille.
                ListViewGroup Groupe = new ListViewGroup(Famille.Lire_Nom_Famille(), HorizontalAlignment.Left);

                // On l'ajoute à la listView.
                ListView1.Groups.Add(Groupe);

                foreach (ListViewItem Item in ListView1.Items)
                {
                    // On ajoute l'item si famille de l'article est celle actuelle.
                    if (Item.SubItems[2].Text == Famille.Lire_Nom_Famille())
                    {
                        ListViewItem Item_Temp = new ListViewItem(Item.SubItems[0].Text, Groupe);
                        ListView1.Items.Add(Item_Temp);
                        Item_Temp.SubItems.Add(Item.SubItems[1].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[2].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[3].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[4].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[5].Text);

                        ListView1.Items.Remove(Item);
                    }
                }
            }
            Trier_Groupes_Par_Ordre_Alphabetique();
        }

        /// <summary>
        /// Permets de trier la liste view en groupes selon la sous-famille de l'article.
        /// </summary>
        public void Groupes_Selon_Sous_Familles_Article()
        {
            foreach (SousFamille Sous_Famille in Base_de_Donnees.Lire_Liste_Sous_Famille())
            {
                // On crée le groupe correspondant à la famille.
                ListViewGroup Groupe = new ListViewGroup(Sous_Famille.Lire_Nom_Sous_Famille(), HorizontalAlignment.Left);

                // On l'ajoute à la listView.
                ListView1.Groups.Add(Groupe);

                foreach (ListViewItem Item in ListView1.Items)
                {
                    // On ajoute l'item si famille de l'article est celle actuelle.
                    if (Item.SubItems[3].Text == Sous_Famille.Lire_Nom_Sous_Famille())
                    {
                        ListViewItem Item_Temp = new ListViewItem(Item.SubItems[0].Text, Groupe);
                        ListView1.Items.Add(Item_Temp);
                        Item_Temp.SubItems.Add(Item.SubItems[1].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[2].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[3].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[4].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[5].Text);

                        ListView1.Items.Remove(Item);
                    }
                }
            }
            Trier_Groupes_Par_Ordre_Alphabetique();
        }

        /// <summary>
        /// Permets de trier la liste view en groupes selon la marque de l'article.
        /// </summary>
        public void Groupes_Selon_Marques_Article()
        {
            foreach (Marque Marque in Base_de_Donnees.Lire_Liste_Marque())
            {
                // On crée le groupe correspondant à la lettre.
                ListViewGroup Groupe = new ListViewGroup(Marque.Lire_Nom_Marque(), HorizontalAlignment.Left);

                // On l'ajoute à la listView.
                ListView1.Groups.Add(Groupe);

                foreach (ListViewItem Item in ListView1.Items)
                {
                    // On ajoute l'item si sa première lettre est la lettre actuelle.
                    if (Item.SubItems[4].Text == Marque.Lire_Nom_Marque())
                    {
                        ListViewItem Item_Temp = new ListViewItem(Item.SubItems[0].Text, Groupe);
                        ListView1.Items.Add(Item_Temp);
                        Item_Temp.SubItems.Add(Item.SubItems[1].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[2].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[3].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[4].Text);
                        Item_Temp.SubItems.Add(Item.SubItems[5].Text);

                        ListView1.Items.Remove(Item);
                    }
                }
            }
            Trier_Groupes_Par_Ordre_Alphabetique();
        }

        /// <summary>
        /// Permets de trier la liste view en groupes selon la première lettre de la description de l'article.
        /// </summary>
        public void Groupes_Selon_Nom()
        {
            for (char Lettre = 'A'; Lettre <= 'Z'; Lettre++)
            {
                // On crée le groupe correspondant à la lettre.
                ListViewGroup Groupe = new ListViewGroup(Convert.ToString(Lettre).ToUpper(), HorizontalAlignment.Left);

                // On l'ajoute à la listView.
                ListView1.Groups.Add(Groupe);

                foreach (ListViewItem Item in ListView1.Items)
                {
                    // On ajoute l'item si sa première lettre est la lettre actuelle.
                    if (Obtenir_1er_Lettre_String(Item.SubItems[1].Text) == Lettre)
                    {
                        ListViewItem Item_Temp = new ListViewItem(Item.SubItems[0].Text, Groupe);
                        ListView1.Items.Add(Item_Temp);
                        Item_Temp.SubItems.Add(Item.SubItems[1].Text);

                        ListView1.Items.Remove(Item);
                    }
                }
            }
        }

        /// <summary>
        /// Permets d'obtenir la première alphabétique d'un string.
        /// </summary>
        /// <param name="Chaine"> Chaine de caractère où l'on veut chercher la première lettre alphabétique. </param>
        /// <returns> char, la première lettre de la chaine de caractère. </returns>
        public char Obtenir_1er_Lettre_String(string Chaine)
        {
            // Vérifier si la chaîne n'est pas vide
            if (!string.IsNullOrEmpty(Chaine))
            {
                // Parcourir chaque caractère de la chaîne
                foreach (char Caractere in Chaine)
                {
                    // Vérifier si le caractère est une lettre et que ce n'est pas un fois (x).
                    if (char.IsLetter(Caractere) && Convert.ToString(Caractere) != "x")
                    {
                        char Premiere_Lettre = char.ToUpper(Caractere);
                        return Premiere_Lettre;
                    }
                }
                throw new Exception("Pas de lettres dans la chaine de caractère.");
            }
            else
            {
                throw new Exception("La chaine de caractère est vide.");
            }
        }

        /// <summary>
        /// Permets de trier les groupes de la listeView par ordre alphabétique.
        /// </summary>
        private void Trier_Groupes_Par_Ordre_Alphabetique()
        {
            List<ListViewGroup> Liste_Groupes = new List<ListViewGroup>();

            // Ajouter tous les groupes actuels à la liste.
            foreach (ListViewGroup Groupe in ListView1.Groups)
            {
                Liste_Groupes.Add(Groupe);
            }

            // Trier la liste des groupes par leur nom.
            Liste_Groupes.Sort((x, y) => string.Compare(x.Header, y.Header));

            // Effacer tous les groupes du ListView.
            ListView1.Groups.Clear();

            // Ajouter les groupes triés au ListView.
            foreach (ListViewGroup Groupe in Liste_Groupes)
            {
                ListView1.Groups.Add(Groupe);
            }
        }

        /// <summary>
        /// On va actualiser les données lorsque l'on clique sur le bouton "Actualiser".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Actualiser_Bouton_Click(object sender, EventArgs e)
        {
            Actualiser(true);
        }

        /// <summary>
        /// Gestionnaire de pression de touches.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pression_Touche_Clavier(object sender, KeyEventArgs e)
        {
            // On regarde si la touche appuyée est "F5".
            if (e.KeyCode == Keys.F5)
            {
                Actualiser(true);
            }

            // On regarde si la touche appuyée est "Suppr".
            else if (e.KeyCode == Keys.Delete)
            {
                Supprimer_Element();
            }
        }

        /// <summary>
        /// Permets d'actualiser l'application à partir de la BDD.
        /// </summary>
        public void Actualiser(bool Afficher_MessageBox)
        {
            Base_de_Donnees.Remplir_Liste_Marque();
            Base_de_Donnees.Remplir_Liste_Famille();
            Base_de_Donnees.Remplir_Liste_Sous_Famille();
            Base_de_Donnees.Remplir_Liste_Article();
            Remplir_TreeView();

            // Si ce n'est pas l'actualisation de lancement de l'application.
            if (TreeView1.SelectedNode != null)
            {
                Remplir_Listview(TreeView1.SelectedNode.Text);
            }

            // On affiche un message de succès à part lors de l'actualisation au lancement de l'application.
            if (Afficher_MessageBox)
            {
                MessageBox.Show("Actualisation avec la base de données effectuée.", "Actualisation réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Gestionnaire de clic sur la souris.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView1_Clic_Souris(object sender, MouseEventArgs e)
        {
            // On regarde si c'est un clic droit.
            if (e.Button == MouseButtons.Right)
            {
                // On vérifie que l'on dans la listeView.
                if (ListView1.ClientRectangle.Contains(e.Location))
                {
                    // On cherche les coordonnées du clic.
                    Point Coordonnees_Clic = ListView1.PointToClient(MousePosition);

                    // Effectue un test pour savoir quel élément a été cliqué
                    ListViewItem Item_Clicked = ListView1.GetItemAt(Coordonnees_Clic.X, Coordonnees_Clic.Y);

                    // Si le clic se trouve sur un item.
                    if (Item_Clicked != null)
                    {
                        Menu_Contextuel_Modifier.Enabled = true;
                        Menu_Contextuel_Supprimer.Enabled = true;
                    }

                    // Si le clic ne se trouve pas sur un item.
                    else
                    {
                        Menu_Contextuel_Modifier.Enabled = false;
                        Menu_Contextuel_Supprimer.Enabled = false;
                    }

                    Menu_Contextuel.Show(ListView1, e.Location);
                }
            }
        }

        /// <summary>
        /// Permets d'ouvrir la fenêtre d'ajout d'un élément et de mettre les champs associé à ce que l'on veut ajouter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Contextuel_Ajouter_Click(object sender, EventArgs e)
        {
            string Nom_2eme_Colonne = ListView1.Columns[1].Text;
            string Valeur_Noeud = TreeView1.SelectedNode.Text;
            string Type_Noeud = Obtenir_Type_Noeud(TreeView1.SelectedNode.Text);

            FormAjouterArticle Fenetre_Ajouter_Article = new FormAjouterArticle(Base_de_Donnees, Valeur_Noeud, Type_Noeud);
            FormAjouterElement Fenetre_Ajouter_Element = new FormAjouterElement(Base_de_Donnees, TreeView1.SelectedNode.Text, Type_Noeud);


            // On regarde si la listView est remplie d'articles.
            if (Nom_2eme_Colonne == "Description")
            {
                // On centre la fenêtre d'importation par rapport au centre de la fenêtre de l'application.
                Fenetre_Ajouter_Article.StartPosition = FormStartPosition.Manual;
                Fenetre_Ajouter_Article.Location = new Point(
                    Location.X + (Width - Fenetre_Ajouter_Article.Width) / 2,
                    Location.Y + ((Height - Fenetre_Ajouter_Article.Height) / 2)
                );

                // Ajout du gestionnaire d'événements pour lorsque l'on ferme la fenetre de modification.
                Fenetre_Ajouter_Article.FormClosed += Fenetre_FormClosed;

                // Afficher la FormImporter en tant que fenêtre modale.
                Fenetre_Ajouter_Article.ShowDialog();
            }
            else
            {
                Fenetre_Ajouter_Element.StartPosition = FormStartPosition.Manual;
                Fenetre_Ajouter_Element.Location = new Point(
                    Location.X + (Width - Fenetre_Ajouter_Element.Width) / 2,
                    Location.Y + ((Height - Fenetre_Ajouter_Element.Height) / 2)
                );

                // Ajout du gestionnaire d'événements pour lorsque l'on ferme la fenetre de modification.
                Fenetre_Ajouter_Element.FormClosed += Fenetre_FormClosed;

                Fenetre_Ajouter_Element.ShowDialog();
            }
        }

        /// <summary>
        /// Permets de supprimer un élément ( article, famille, sous-famille ou marque ).
        /// </summary>
        public void Supprimer_Element()
        {
            string Nom_2eme_Colonne = ListView1.Columns[1].Text;
            string Nom_Noeud = TreeView1.SelectedNode.Text;

            // On regarde si la listView est remplie d'articles.
            if (Nom_2eme_Colonne == "Description")
            {
                Supprimer_Article();
            }

            else
            {
                // On regarde si la listView est remplie de familles.
                if (Obtenir_Type_Noeud(Nom_Noeud) == "Famille")
                {
                    Supprimer_Famille();
                }

                // On regarde si la listView est remplie de sous-familles.
                else if (Obtenir_Type_Noeud(Nom_Noeud) == "Sous_Famille")
                {
                    Supprimer_Sous_Famille();
                }

                // On regarde si la listView est remplie de marque.
                else if (Obtenir_Type_Noeud(Nom_Noeud) == "Marque")
                {
                    Supprimer_Marque();
                }
            }
        }

        /// <summary>
        /// Permet de savoir si un article possède la marque sélectionné comme marque.
        /// </summary>
        /// <returns> bool, indique si la marque est utilisé par un article ou non. </returns>
        public bool Is_Article_Avec_Marque()
        {
            foreach (Article Article in Base_de_Donnees.Lire_Liste_Article())
            {
                if (Article.Lire_Marque().Lire_Ref_Marque() == Convert.ToInt32(ListView1.SelectedItems[0].Text))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permet de savoir si un article possède la sous-famille sélectionné comme sous-famille.
        /// </summary>
        /// <returns> bool, indique si la sous-famille est utilisé par un article ou non. </returns>
        public bool Is_Article_Avec_Sous_Famille()
        {
            foreach (Article Article in Base_de_Donnees.Lire_Liste_Article())
            {
                if (Article.Lire_Sous_Famille().Lire_Ref_Sous_Famille() == Convert.ToInt32(ListView1.SelectedItems[0].Text))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permet de savoir si un article possède la famille sélectionné comme famille.
        /// </summary>
        /// <returns> bool, indique si la famille est utilisé par un article ou non. </returns>
        public bool Is_Article_Avec_Famille()
        {
            foreach (Article Article in Base_de_Donnees.Lire_Liste_Article())
            {
                if (Article.Lire_Sous_Famille().Lire_Famille().Lire_Ref_Famille() == Convert.ToInt32(ListView1.SelectedItems[0].Text))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permets de supprimer un article.
        /// </summary>
        public void Supprimer_Article()
        {
            DialogResult Choix = MessageBox.Show("Voulez-vous vraiment supprimer cette article ?", "Confirmation suppression article.", MessageBoxButtons.YesNo);

            // On vérifie la réponse de l'utilisateur.
            if (Choix == DialogResult.Yes)
            {
                // On supprime l'article de la BDD.
                Base_de_Donnees.Supprimer_Article_BDD(ListView1.SelectedItems[0].Text);

                MessageBox.Show("L'article a été supprimé avec succès.", "Supression article effectué", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Actualiser(false);
            }
        }

        /// <summary>
        /// Permets de supprimer une famille.
        /// </summary>
        public void Supprimer_Famille()
        {
            // On vérifie qu'aucun article n'a la famille sélectionnée comme famille.
            if (!Is_Article_Avec_Famille())
            {
                DialogResult Choix = MessageBox.Show("Voulez-vous vraiment supprimer cette famille ?", "Confirmation suppression famille.", MessageBoxButtons.YesNo);

                // On vérifie la réponse de l'utilisateur.
                if (Choix == DialogResult.Yes)
                {
                    // On supprime la marque de la BDD.
                    Base_de_Donnees.Supprimer_Famille_BDD(Convert.ToInt32(ListView1.SelectedItems[0].Text));

                    MessageBox.Show("La famille a été supprimé avec succès.", "Supression famille effectué", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Actualiser(false);
                }
            }

            // Si la marque est utilisé par un article.
            else
            {
                MessageBox.Show("Vous ne pouvez pas supprimer cette famille car des articles appartiennent à cette famille.", "Erreur : famille utilisé par un / des article(s).", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Permets de supprimer une sous-famille.
        /// </summary>
        public void Supprimer_Sous_Famille()
        {
            // On vérifie qu'aucun article n'a la sous-famille sélectionnée comme sous-famille.
            if (!Is_Article_Avec_Sous_Famille())
            {
                DialogResult Choix = MessageBox.Show("Voulez-vous vraiment supprimer cette sous-famille ?", "Confirmation suppression sous-famille.", MessageBoxButtons.YesNo);

                // On vérifie la réponse de l'utilisateur.
                if (Choix == DialogResult.Yes)
                {
                    // On supprime la marque de la BDD.
                    Base_de_Donnees.Supprimer_Sous_Famille_BDD(Convert.ToInt32(ListView1.SelectedItems[0].Text));

                    MessageBox.Show("La sous-famille a été supprimé avec succès.", "Supression sous-famille effectué", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Actualiser(false);
                }
            }

            // Si la marque est utilisé par un article.
            else
            {
                MessageBox.Show("Vous ne pouvez pas supprimer cette sous-famille car des articles appartiennent à cette sous-famille.", "Erreur : Sous-famille utilisé par un / des article(s).", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Permets de supprimer une marque.
        /// </summary>
        public void Supprimer_Marque()
        {
            // On vérifie qu'aucun article n'a la marque sélectionnée comme marque.
            if (!Is_Article_Avec_Marque())
            {
                DialogResult Choix = MessageBox.Show("Voulez-vous vraiment supprimer cette marque ?", "Confirmation suppression marque.", MessageBoxButtons.YesNo);

                // On vérifie la réponse de l'utilisateur.
                if (Choix == DialogResult.Yes)
                {
                    // On supprime la marque de la BDD.
                    Base_de_Donnees.Supprimer_Marque_BDD(Convert.ToInt32(ListView1.SelectedItems[0].Text));

                    MessageBox.Show("La marque a été supprimé avec succès.", "Supression marque effectué", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Actualiser(false);
                }
            }

            // Si la marque est utilisé par un article.
            else
            {
                MessageBox.Show("Vous ne pouvez pas supprimer cette marque car des articles appartiennent à cette marque.", "Erreur : Marque utilisé par un / des article(s).", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Permets de demander la confirmation pour la suppression d'un élément.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Contextuel_Supprimer_Click(object sender, EventArgs e)
        {
            Supprimer_Element();
        }

        /// <summary>
        /// Permets d'ouvrir la fenetre pour modifier l'élément sélectionné.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Contextuel_Modifier_Click(object sender, EventArgs e)
        {
            Ouvrir_Fenetre_Modifier();
        }

        /// <summary>
        /// Permets d'ouvrir la fenêtre modifié adéquate à l'élément sélectionné.
        /// </summary>
        public void Ouvrir_Fenetre_Modifier()
        {
            string Nom_2eme_Colonne = ListView1.Columns[1].Text;
            string Valeur_Noeud = TreeView1.SelectedNode.Text;
            string Type_Noeud = Obtenir_Type_Noeud(TreeView1.SelectedNode.Text);

            // On regarde si la listView est remplie d'articles.
            if (Nom_2eme_Colonne == "Description")
            {
                Article Article_Selected = Base_de_Donnees.Obtenir_Article_Par_Ref(ListView1.SelectedItems[0].Text);
                FormModifierArticle Fenetre_Modifier_Article = new FormModifierArticle(Base_de_Donnees, Article_Selected);

                // On centre la fenêtre d'importation par rapport au centre de la fenêtre de l'application.
                Fenetre_Modifier_Article.StartPosition = FormStartPosition.Manual;
                Fenetre_Modifier_Article.Location = new Point(
                    Location.X + (Width - Fenetre_Modifier_Article.Width) / 2,
                    Location.Y + ((Height - Fenetre_Modifier_Article.Height) / 2)
                );

                // Ajout du gestionnaire d'événements pour lorsque l'on ferme la fenetre de modification.
                Fenetre_Modifier_Article.FormClosed += Fenetre_FormClosed;

                // Afficher la FormImporter en tant que fenêtre modale.
                Fenetre_Modifier_Article.ShowDialog();
            }

            // Si la listeView est remplie d'autres éléments.
            else
            {
                FormModifierElement Fenetre_Modifier_Element = new FormModifierElement(Convert.ToInt32(ListView1.SelectedItems[0].Text), Type_Noeud, Base_de_Donnees);

                // On centre la fenêtre d'importation par rapport au centre de la fenêtre de l'application.
                Fenetre_Modifier_Element.StartPosition = FormStartPosition.Manual;
                Fenetre_Modifier_Element.Location = new Point(
                    Location.X + (Width - Fenetre_Modifier_Element.Width) / 2,
                    Location.Y + ((Height - Fenetre_Modifier_Element.Height) / 2)
                );

                // Ajout du gestionnaire d'événements pour lorsque l'on ferme la fenetre de modification.
                Fenetre_Modifier_Element.FormClosed += Fenetre_FormClosed;

                // Afficher la FormImporter en tant que fenêtre modale.
                Fenetre_Modifier_Element.ShowDialog();
            }
        }

        /// <summary>
        /// Permets de gérer les doubles clics sur la liste view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Ouvrir_Fenetre_Modifier();
            }
        }

        /// <summary>
        /// Permets de gérer la pression de touches sur la liste view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                Ouvrir_Fenetre_Modifier();
            }
        }

        /// <summary>
        /// Permets de sauvegarder les informations de la fenêtre lors de la fermeture de celle-ci.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // On modifie et sauvegarde les paramètres
            Properties.Settings.Default.TailleX = this.Width;
            Properties.Settings.Default.TailleY = this.Height;

            // La fenetre n'est pas en plein écran.
            if(this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.PleinEcran = false;

                // Si la fenetre est correctement placé.
                if (this.Location.X > 0 && this.Location.X < Screen.PrimaryScreen.Bounds.Width && this.Location.Y > 0 && this.Location.Y < Screen.PrimaryScreen.Bounds.Height)
                {
                    Properties.Settings.Default.PositionX = this.Location.X;
                    Properties.Settings.Default.PositionY = this.Location.Y;
                }

                // Si la fenêtre dépasse à gauche de l'écran.
                else if (this.Location.X < 0)
                {
                    Properties.Settings.Default.PositionX = 0;
                    
                    // Si la fenêtre dépasse en bas de l'écran.
                    if (this.Location.Y < 0)
                    {
                        Properties.Settings.Default.PositionY = 0;
                    }

                    // Si la fenêtre ne dépasse pasa en bas de l'écran.
                    else
                    {
                        Properties.Settings.Default.PositionY = this.Location.Y;
                    }

                    // Si la fenêtre dépasse en haut de l'écran.
                    if (this.Location.Y > Screen.PrimaryScreen.Bounds.Height)
                    {
                        Properties.Settings.Default.PositionY = Screen.PrimaryScreen.Bounds.Height;
                    }

                    // Si la fenêtre ne dépasse pas en haut de l'écran.
                    else
                    {
                        Properties.Settings.Default.PositionY = this.Location.Y;
                    }
                }

                // Si la fenêtre dépasse à droite de l'écran.
                else if (this.Location.X > Screen.PrimaryScreen.Bounds.Width)
                {
                    Properties.Settings.Default.PositionX = Screen.PrimaryScreen.Bounds.Width - this.Width;

                    // Si la fenêtre dépasse en bas de l'écran.
                    if (this.Location.Y < 0)
                    {
                        Properties.Settings.Default.PositionY = 0;
                    }

                    // Si la fenêtre ne dépasse pasa en bas de l'écran.
                    else
                    {
                        Properties.Settings.Default.PositionY = this.Location.Y;
                    }

                    // Si la fenêtre dépasse en haut de l'écran.
                    if (this.Location.Y > Screen.PrimaryScreen.Bounds.Height)
                    {
                        Properties.Settings.Default.PositionY = Screen.PrimaryScreen.Bounds.Height;
                    }

                    // Si la fenêtre ne dépasse pas en haut de l'écran.
                    else
                    {
                        Properties.Settings.Default.PositionY = this.Location.Y;
                    }
                }
            }

            // La fenetre est en plein écran.
            else
            {
                Properties.Settings.Default.PleinEcran = true;
            }
            Properties.Settings.Default.Save();
        }
    }
}
