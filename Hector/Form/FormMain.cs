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

        bool Is_Actualisation_Lancement = true;

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

            // Gestionnaire d'événement pour la pression de touches.
            this.KeyDown += Pression_Touche_Clavier;

            // Gestionnaire d'événement pour la pression de clics de la souris dans la listView.
            ListView1.MouseDown += ListView1_Clic_Souris;

            // Obtient le chemin de la base de données SQLite
            Base_de_Donnees.Obtenir_Chemin_Base_de_Donnees();

            Ajouter_Noeuds_Racine();

            // On mets la fenêtre en pleine écran au démarrage afin de faciliter la visibilité de la list view.
            this.WindowState = FormWindowState.Maximized;

            // On actualise l'application avec le contenu de la BDD.
            Actualiser();
            Is_Actualisation_Lancement = false;
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
        public string Is_Marque_or_Famille_or_Sous_Famille(string Nom)
        {
            // On regarde s'il s'agit d'un nom de marque.
            foreach (Marque Marque in Base_de_Donnees.Lire_Liste_Marque())
            {
                if (Marque.Lire_Nom_Marque() == Nom)
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

        /// <summary>
        /// Permets d'afficher dans la liste view les éléments en fonction du noeud cliqué.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string Selected_Node_Text = e.Node.Text;

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
            else if (Is_Marque_or_Famille_or_Sous_Famille(Selected_Node_Text) == "Famille")
            {
                ListView1.Clear();

                Ajouter_Colonnes_Sous_Familles_ListView();
                Ajouter_Sous_Familles_ListView(Selected_Node_Text);
                Ajuster_Largeur_Colonne_ListeView();

                ListView1.Groups.Clear();
            }

            // L'utilisateur a cliqué sur un Nœud "Sous-Famille".
            else if (Is_Marque_or_Famille_or_Sous_Famille(Selected_Node_Text) == "Sous_Famille")
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

            // L'utilisateur a cliqué sur un Nœud "Marque".
            else if (Is_Marque_or_Famille_or_Sous_Famille(Selected_Node_Text) == "Marque")
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
            Actualiser();
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
                Actualiser();
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
        public void Actualiser()
        {
            Base_de_Donnees.Remplir_Liste_Marque();
            Base_de_Donnees.Remplir_Liste_Famille();
            Base_de_Donnees.Remplir_Liste_Sous_Famille();
            Base_de_Donnees.Remplir_Liste_Article();
            Remplir_TreeView();

            // On affiche un message de succès à part lors de l'actualisation au lancement de l'application.
            if (!Is_Actualisation_Lancement)
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
            string Nom_1er_Item = ListView1.Items[0].SubItems[1].Text;
            string Valeur_Noeud = TreeView1.SelectedNode.Text;
            string Type_Noeud = Is_Marque_or_Famille_or_Sous_Famille(TreeView1.SelectedNode.Text);

            FormAjouterArticle Fenetre_Ajouter_Article = new FormAjouterArticle(Base_de_Donnees, Valeur_Noeud, Type_Noeud);

            // On regarde si la listView est remplie d'articles.
            if (Nom_2eme_Colonne == "Description")
            {
                // On centre la fenêtre d'importation par rapport au centre de la fenêtre de l'application.
                Fenetre_Ajouter_Article.StartPosition = FormStartPosition.Manual;
                Fenetre_Ajouter_Article.Location = new Point(
                    Location.X + (Width - Fenetre_Ajouter_Article.Width) / 2,
                    Location.Y + ((Height - Fenetre_Ajouter_Article.Height) / 2)
                );

                // Afficher la FormImporter en tant que fenêtre modale.
                Fenetre_Ajouter_Article.ShowDialog();
            }

            else
            {
                // On regarde si la listView est remplie de familles.
                if (Is_Marque_or_Famille_or_Sous_Famille(Nom_1er_Item) == "Famille")
                {

                }

                // On regarde si la listView est remplie de sous-familles.
                else if (Is_Marque_or_Famille_or_Sous_Famille(Nom_1er_Item) == "Sous_Famille")
                {

                }

                // On regarde si la listView est remplie de sous-familles.
                else if (Is_Marque_or_Famille_or_Sous_Famille(Nom_1er_Item) == "Marque")
                {

                }
            }
        }

        /// <summary>
        /// Permets de supprimer un élément ( article, famille, sous-famille ou marque ).
        /// </summary>
        public void Supprimer_Element()
        {
            string Nom_2eme_Colonne = ListView1.Columns[1].Text;
            string Nom_1er_Item = ListView1.Items[0].SubItems[1].Text;

            // On regarde si la listView est remplie d'articles.
            if (Nom_2eme_Colonne == "Description")
            {
                DialogResult Choix = MessageBox.Show("Voulez-vous vraiment supprimer cette article ?", "Confirmation suppression article.", MessageBoxButtons.YesNo);

                // Vérifier la réponse de l'utilisateur
                if (Choix == DialogResult.Yes)
                {
                    // On supprime l'article dans la liste.
                    foreach(Article Article in Base_de_Donnees.Lire_Liste_Article())
                    {
                        if(Article.Lire_Ref_Article() == ListView1.SelectedItems[0].Text)
                        {
                            Base_de_Donnees.Lire_Liste_Article().Remove(Article);
                            break;
                        }
                    }

                    // On supprime l'article de la BDD.
                    Base_de_Donnees.Supprimer_Article_BDD(ListView1.SelectedItems[0].Text);

                    MessageBox.Show("L'article a été supprimé.", "Supression article effectué", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else
            {
                // On regarde si la listView est remplie de familles.
                if (Is_Marque_or_Famille_or_Sous_Famille(Nom_1er_Item) == "Famille")
                {

                }

                // On regarde si la listView est remplie de sous-familles.
                else if (Is_Marque_or_Famille_or_Sous_Famille(Nom_1er_Item) == "Sous_Famille")
                {

                }

                // On regarde si la listView est remplie de sous-familles.
                else if (Is_Marque_or_Famille_or_Sous_Famille(Nom_1er_Item) == "Marque")
                {

                }
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
    }
}
