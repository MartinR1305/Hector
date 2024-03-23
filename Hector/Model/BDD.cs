using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Hector
{
    public class BDD
    {
        private string Chemin_Base_de_Donnees;
        private string Connection_String;

        private List<Marque> Liste_Marque;
        private List<Famille> Liste_Famille;
        private List<SousFamille> Liste_Sous_Famille;
        private List<Article> Liste_Article;

        public BDD()
        {
            Chemin_Base_de_Donnees = "";
            Connection_String = "";
            Liste_Marque = new List<Marque>();
            Liste_Famille = new List<Famille>();
            Liste_Sous_Famille = new List<SousFamille>();
            Liste_Article = new List<Article>();
        }

        /// <summary>
        /// Getter permettant de lire le chemin de la base de données.
        /// </summary>
        /// <returns> Le chemin de la base de données </returns>
        public string Lire_Chemin_Base_de_Donnees()
        {
            // On vérifie que le chemin n'est pas null.
            if (Chemin_Base_de_Donnees == null)
            {
                // S'il est null, cela signifie que l'on ne l'a pas encore obtenu.
                Obtenir_Chemin_Base_de_Donnees();
            }
            return Chemin_Base_de_Donnees;
        }

        /// <summary>
        /// Permets d'obtenir le chemin de la base de données et de l'associer à l'attribut de l'objet base de données.
        /// </summary>
        /// <exception cref="FileNotFoundException">Le fichier de base de données n'a pas été trouvé.</exception>
        public void Obtenir_Chemin_Base_de_Donnees()
        {
            // Chemin absolu du répertoire de l'exécutable.
            string Chemin_Executable = AppDomain.CurrentDomain.BaseDirectory;

            string Nom_Base_de_Donnees = "Hector.SQLite";

            // Chemin complet de la base de données en concaténant le chemin de l'exécutable avec le nom du fichier.
            string Chemin_Trouver = Path.Combine(Chemin_Executable, Nom_Base_de_Donnees);

            // Vérifie si le fichier existe, sinon lance une exception
            if (!File.Exists(Chemin_Trouver))
            {
                throw new FileNotFoundException("Le fichier de base de données n'a pas été trouvé.", Chemin_Trouver);
            }

            this.Chemin_Base_de_Donnees = Chemin_Trouver;
            Connection_String = $"Data Source={Chemin_Trouver};Version=3;";
        }

        /// <summary>
        /// Getter permettant de lire le chemin de la base de données.
        /// </summary>
        /// <returns> Le string de connection à la BDD. </returns>
        public string Lire_Connection_String()
        {
            // On vérifie que le string n'est pas null.
            if (Connection_String == null)
            {
                throw new Exception("Le fichier de base de données n'a pas été trouvé.");
            }
            return Connection_String;
        }

        /// <summary>
        /// Setter du string du connection.
        /// </summary>
        /// <param name="String"> String que l'on veut associer. </param>
        public void Modifier_Connection_String(string String)
        {
            Connection_String = String;
        }

        /// <summary>
        /// Setter de la liste de marques.
        /// </summary>
        /// <returns> La Liste de marque</returns>
        public List<Marque> Lire_Liste_Marque()
        {
            return Liste_Marque;
        }

        /// <summary>
        /// Permets d'ajouter les marques d'une liste à la BDD.
        /// </summary>
        public void Ajouter_Toutes_Les_Marques_BDD()
        {
            // On va lire toutes les marques de la liste.
            foreach (Marque Marque in Liste_Marque)
            {
                using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
                {
                    Connection.Open();

                    string Nom_Marque = Marque.Lire_Nom_Marque();

                    // On vérifie que la marque n'est pas déjà présente dans la BDD.
                    if (!Is_Nom_Marque_Present_BDD(Nom_Marque, Connection))
                    {
                        // Commande SQL permets d'ajoute une marque dans la BDD.
                        string SQL_Query_Ajout_Marque = "INSERT INTO Marques (Nom) VALUES (@Nom)";

                        using (SQLiteCommand Commande_Ajout_Marque = new SQLiteCommand(SQL_Query_Ajout_Marque, Connection))
                        {
                            // Ajouter les paramètres à la commande.
                            Commande_Ajout_Marque.Parameters.AddWithValue("@Nom", Nom_Marque);

                            // Exécuter la commande.
                            int Rows_Affected = Commande_Ajout_Marque.ExecuteNonQuery();
                        }
                    }
                    // On modifie la référence dans le code pour qu'elles correspondent à celle de la BDD.
                    Marque.Modifier_Ref_Marque(Obtenir_Ref_Marque_BDD(Nom_Marque, Connection));

                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Permets d'obtenir la référence d'une marque dans la BDD à partir de son nom.
        /// </summary>
        /// <param name="Nom_Marque"> Nom de la marque que l'on cherche. </param>
        /// <param name="Connection"> Connection à la BDD.</param>
        /// <returns> int, Référence de la marque. </returns>
        public int Obtenir_Ref_Marque_BDD(string Nom_Marque, SQLiteConnection Connection)
        {
            int Ref_Marque_BDD = 0;

            // On va rechercher la référence donnée dans la BDD pour l'associer dans le code.
            string SQL_Query_Recherche_Ref = "SELECT RefMarque FROM Marques WHERE Nom = @Nom";

            using (SQLiteCommand Commande_Recherche_Ref = new SQLiteCommand(SQL_Query_Recherche_Ref, Connection))
            {
                // Ajouter le paramètre Nom avec sa valeur.
                Commande_Recherche_Ref.Parameters.AddWithValue("@Nom", Nom_Marque);

                // Exécuter la commande et obtenir le résultat.
                object Ref = Commande_Recherche_Ref.ExecuteScalar();

                Ref_Marque_BDD = Convert.ToInt32(Ref);
            }
            return Ref_Marque_BDD;
        }

        /// <summary>
        /// Permets de savoir si la marque est déjà présente dans la BDD ou non.
        /// </summary>
        /// <param name="Nom_Marque"> Nom de la marque que l'on va chercher. </param>
        /// <param name="Connection"> Connection à la BDD.</param>
        /// <returns>bool, indique si la marque déjà présente dans la BDD ou non. </returns>
        public bool Is_Nom_Marque_Present_BDD(string Nom_Marque, SQLiteConnection Connection)
        {
            // Requête SQL pour savoir si le nom est présent dans la BDD.
            string SQL_Query_Count_Marque = "SELECT COUNT(*) FROM Marques WHERE Nom = @Nom";

            using (SQLiteCommand Commande_Count_Marque = new SQLiteCommand(SQL_Query_Count_Marque, Connection))
            {
                // Ajouter le paramètre Nom avec sa valeur
                Commande_Count_Marque.Parameters.AddWithValue("@Nom", Nom_Marque);

                // Exécuter la commande et obtenir le nombre de lignes correspondantes
                int Nombre_Lignes = Convert.ToInt32(Commande_Count_Marque.ExecuteScalar());

                if (Nombre_Lignes > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Setter de la liste de familles.
        /// </summary>
        /// <returns> Liste de familles. </returns>
        public List<Famille> Lire_Liste_Famille()
        {
            return Liste_Famille;
        }

        /// <summary>
        /// Permets d'ajouter les familles d'une liste à la BDD.
        /// </summary>
        public void Ajouter_Toutes_Les_Familles_BDD()
        {
            // On va lire toutes les familles de la liste.
            foreach (Famille Famille in Liste_Famille)
            {
                using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
                {
                    Connection.Open();

                    string Nom_Famille = Famille.Lire_Nom_Famille();

                    // On vérifie que la famille n'est pas déjà présente dans la BDD.
                    if (!Is_Nom_Famille_Present_BDD(Nom_Famille, Connection))
                    {
                        // Commande SQL permets d'ajoute une marque dans la BDD.
                        string SQL_Query_Ajout_Famille = "INSERT INTO Familles (Nom) VALUES (@Nom)";

                        using (SQLiteCommand Commande_Ajout_Famille = new SQLiteCommand(SQL_Query_Ajout_Famille, Connection))
                        {
                            // Ajouter les paramètres à la commande.
                            Commande_Ajout_Famille.Parameters.AddWithValue("@Nom", Nom_Famille);

                            // Exécuter la commande.
                            int Rows_Affected = Commande_Ajout_Famille.ExecuteNonQuery();
                        }
                    }
                    // On modifie la référence dans le code pour qu'elles correspondent à celle de la BDD.
                    Famille.Modifier_Ref_Famille(Obtenir_Ref_Famille_BDD(Nom_Famille, Connection));

                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Permets d'obtenir la référence d'une famille dans la BDD à partir de son nom.
        /// </summary>
        /// <param name="Nom_Famille"> Nom de la famille que l'on cherche. </param>
        /// <param name="Connection"> Connection à la BDD.</param>
        /// <returns> int, Référence de la famille. </returns>
        public int Obtenir_Ref_Famille_BDD(string Nom_Famille, SQLiteConnection Connection)
        {
            int Ref_Famille_BDD = 0;

            // On va rechercher la référence donnée dans la BDD pour l'associer dans le code.
            string SQL_Query_Recherche_Ref = "SELECT RefFamille FROM Familles WHERE Nom = @Nom";

            using (SQLiteCommand Commande_Recherche_Ref = new SQLiteCommand(SQL_Query_Recherche_Ref, Connection))
            {
                // Ajouter le paramètre Nom avec sa valeur.
                Commande_Recherche_Ref.Parameters.AddWithValue("@Nom", Nom_Famille);

                // Exécuter la commande et obtenir le résultat.
                object Ref = Commande_Recherche_Ref.ExecuteScalar();

                Ref_Famille_BDD = Convert.ToInt32(Ref);
            }
            return Ref_Famille_BDD;
        }

        /// <summary>
        /// Permets de savoir si la famille est déjà présente dans la BDD ou non.
        /// </summary>
        /// <param name="Nom_Famille"> Nom de la famille que l'on va chercher. </param>
        /// <param name="Connection"> Connection à la BDD.</param>
        /// <returns>bool, indique si la famille déjà présente dans la BDD ou non. </returns>
        public bool Is_Nom_Famille_Present_BDD(string Nom_Famille, SQLiteConnection Connection)
        {
            // Requête SQL pour savoir si le nom est présent dans la BDD.
            string SQL_Query_Count_Famille = "SELECT COUNT(*) FROM Familles WHERE Nom = @Nom";

            using (SQLiteCommand Commande_Count_Famille = new SQLiteCommand(SQL_Query_Count_Famille, Connection))
            {
                // Ajouter le paramètre Nom avec sa valeur
                Commande_Count_Famille.Parameters.AddWithValue("@Nom", Nom_Famille);

                // Exécuter la commande et obtenir le nombre de lignes correspondantes
                int Nombre_Lignes = Convert.ToInt32(Commande_Count_Famille.ExecuteScalar());

                if (Nombre_Lignes > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Setter de la liste de sous-familles.
        /// </summary>
        /// <returns> Liste de sous-familles. </returns>
        public List<SousFamille> Lire_Liste_Sous_Famille()
        {
            return Liste_Sous_Famille;
        }

        /// <summary>
        /// Permets d'ajouter les sous-familles d'une liste à la BDD.
        /// </summary>
        public void Ajouter_Toutes_Les_Sous_Familles_BDD()
        {
            // On va lire toutes les sous-familles de la liste.
            foreach (SousFamille Sous_Famille in Liste_Sous_Famille)
            {
                using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
                {
                    Connection.Open();

                    string Nom_Sous_Famille = Sous_Famille.Lire_Nom_Sous_Famille();

                    // On vérifie que la famille n'est pas déjà présente dans la BDD.
                    if (!Is_Nom_Sous_Famille_Present_BDD(Nom_Sous_Famille, Connection))
                    {
                        // Commande SQL permets d'ajoute une marque dans la BDD.
                        string SQL_Query_Ajout_Sous_Famille = "INSERT INTO SousFamilles (RefFamille, Nom) VALUES (@RefFamille, @Nom)";

                        using (SQLiteCommand Commande_Ajout_Sous_Famille = new SQLiteCommand(SQL_Query_Ajout_Sous_Famille, Connection))
                        {
                            int Ref_Famille = Obtenir_Ref_Famille_BDD(Sous_Famille.Lire_Famille().Lire_Nom_Famille(), Connection);

                            // Ajouter les paramètres à la commande.
                            Commande_Ajout_Sous_Famille.Parameters.AddWithValue("@RefFamille", Ref_Famille);
                            Commande_Ajout_Sous_Famille.Parameters.AddWithValue("@Nom", Nom_Sous_Famille);

                            // Exécuter la commande.
                            int Rows_Affected = Commande_Ajout_Sous_Famille.ExecuteNonQuery();
                        }
                    }
                    // On modifie la référence dans le code pour qu'elles correspondent à celle de la BDD.
                    Sous_Famille.Modifier_Ref_Sous_Famille(Obtenir_Ref_Sous_Famille_BDD(Nom_Sous_Famille, Connection));

                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Permets d'obtenir la référence d'une sous-famille dans la BDD à partir de son nom.
        /// </summary>
        /// <param name="Nom_Sous_Famille"> Nom de la sous-famille que l'on cherche. </param>
        /// <param name="Connection"> Connection à la BDD.</param>
        /// <returns> int, Référence de la sous-famille. </returns>
        public int Obtenir_Ref_Sous_Famille_BDD(string Nom_Sous_Famille, SQLiteConnection Connection)
        {
            int Ref_Sous_Famille_BDD = 0;

            // On va rechercher la référence donnée dans la BDD pour l'associer dans le code.
            string SQL_Query_Recherche_Ref = "SELECT RefSousFamille FROM SousFamilles WHERE Nom = @Nom";

            using (SQLiteCommand Commande_Recherche_Ref = new SQLiteCommand(SQL_Query_Recherche_Ref, Connection))
            {
                // Ajouter le paramètre Nom avec sa valeur.
                Commande_Recherche_Ref.Parameters.AddWithValue("@Nom", Nom_Sous_Famille);

                // Exécuter la commande et obtenir le résultat.
                object Ref = Commande_Recherche_Ref.ExecuteScalar();

                Ref_Sous_Famille_BDD = Convert.ToInt32(Ref);
            }
            return Ref_Sous_Famille_BDD;
        }

        /// <summary>
        /// Permets de savoir si la sous-famille est déjà présente dans la BDD ou non.
        /// </summary>
        /// <param name="Nom_Sous_Famille"> Nom de la sous-famille que l'on va chercher. </param>
        /// <param name="Connection"> Connection à la BDD.</param>
        /// <returns>bool, indique si la sous-famille est déjà présente dans la BDD ou non. </returns>
        public bool Is_Nom_Sous_Famille_Present_BDD(string Nom_Sous_Famille, SQLiteConnection Connection)
        {
            // Requête SQL pour savoir si le nom est présent dans la BDD.
            string SQL_Query_Count_Sous_Famille = "SELECT COUNT(*) FROM SousFamilles WHERE Nom = @Nom";

            using (SQLiteCommand Commande_Count_Sous_Famille = new SQLiteCommand(SQL_Query_Count_Sous_Famille, Connection))
            {
                // Ajouter le paramètre Nom avec sa valeur
                Commande_Count_Sous_Famille.Parameters.AddWithValue("@Nom", Nom_Sous_Famille);

                // Exécuter la commande et obtenir le nombre de lignes correspondantes
                int Nombre_Lignes = Convert.ToInt32(Commande_Count_Sous_Famille.ExecuteScalar());

                if (Nombre_Lignes > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Getter de la liste d'article.
        /// </summary>
        /// <returns></returns>
        public List<Article> Lire_Liste_Article()
        {
            return Liste_Article;
        }

        /// <summary>
        /// Permets d'ajouter un article dans la BDD.
        /// </summary>
        /// <param name="Article"></param>
        public void Ajouter_Un_Article_BDD(Article Article)
        {
            using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
            {
                Connection.Open();

                string Description_Article = Article.Lire_Description();

                // On vérifie que la description n'est pas déjà présente dans la BDD.
                if (!Is_Description_Article_Present_BDD(Description_Article, Connection))
                {
                    // Commande SQL permets d'ajoute une marque dans la BDD.
                    string SQL_Query_Ajout_Article = "INSERT INTO Articles (RefArticle, Description, RefSousFamille, RefMarque, PrixHT, Quantite) VALUES (@RefArticle, @Description, @RefSousFamille, @RefMarque, @PrixHT, @Quantite)";

                    using (SQLiteCommand Commande_Ajout_Article = new SQLiteCommand(SQL_Query_Ajout_Article, Connection))
                    {
                        int Ref_Sous_Famille = Obtenir_Ref_Sous_Famille_BDD(Article.Lire_Sous_Famille().Lire_Nom_Sous_Famille(), Connection);
                        int Ref_Marque = Obtenir_Ref_Marque_BDD(Article.Lire_Marque().Lire_Nom_Marque(), Connection);

                        // Ajouter les paramètres à la commande.
                        Commande_Ajout_Article.Parameters.AddWithValue("@RefArticle", Article.Lire_Ref_Article());
                        Commande_Ajout_Article.Parameters.AddWithValue("@Description", Description_Article);
                        Commande_Ajout_Article.Parameters.AddWithValue("@RefSousFamille", Ref_Sous_Famille);
                        Commande_Ajout_Article.Parameters.AddWithValue("@RefMarque", Ref_Marque);
                        Commande_Ajout_Article.Parameters.AddWithValue("@PrixHT", Article.Lire_PrixHT());
                        Commande_Ajout_Article.Parameters.AddWithValue("@Quantite", 0);

                        // Exécuter la commande.
                        int Rows_Affected = Commande_Ajout_Article.ExecuteNonQuery();
                    }
                }
                Connection.Close();
            }
        }

        /// <summary>
        /// Permets d'ajouter tout les articles dans la BDD.
        /// </summary>
        public void Ajouter_Tout_Les_Articles_BDD()
        {
            // On va lire tout les articles de la liste.
            foreach (Article Article in Liste_Article)
            {
                Ajouter_Un_Article_BDD(Article);
            }
        }

        /// <summary>
        /// Permets de savoir si la sous-famille est déjà présente dans la BDD ou non.
        /// </summary>
        /// <param name="Nom_Sous_Famille"> Nom de la sous-famille que l'on va chercher. </param>
        /// <param name="Connection"> Connection à la BDD.</param>
        /// <returns>bool, indique si la sous-famille est déjà présente dans la BDD ou non. </returns>
        public bool Is_Description_Article_Present_BDD(string Description_Article, SQLiteConnection Connection)
        {
            // Requête SQL pour savoir si le nom est présent dans la BDD.
            string SQL_Query_Count_Article = "SELECT COUNT(*) FROM Articles WHERE Description = @Description";

            using (SQLiteCommand Commande_Count_Article = new SQLiteCommand(SQL_Query_Count_Article, Connection))
            {
                // Ajouter le paramètre Description avec sa valeur
                Commande_Count_Article.Parameters.AddWithValue("@Description", Description_Article);

                // Exécuter la commande et obtenir le nombre de lignes correspondantes.
                int Nombre_Lignes = Convert.ToInt32(Commande_Count_Article.ExecuteScalar());

                if (Nombre_Lignes > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Permets d'ajouter toutes les tables dans la BDD.
        /// </summary>
        public void Ajouter_Toutes_Les_Tables()
        {
            Ajouter_Toutes_Les_Marques_BDD();
            Ajouter_Toutes_Les_Familles_BDD();
            Ajouter_Toutes_Les_Sous_Familles_BDD();
            Ajouter_Tout_Les_Articles_BDD();
        }

        /// <summary>
        /// Permets de vider toutes les tables de la BDD. ( Article, Famille, SousFamille et Marque )
        /// </summary>
        public void Vider_Toutes_Les_Tables()
        {
            using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
            {
                Connection.Open();

                string[] Nom_Tables = { "Articles", "SousFamilles", "Familles", "Marques" };

                foreach (string Table in Nom_Tables)
                {
                    string SQL_Query_Vider_Tables = $"DELETE FROM {Table};";

                    using (SQLiteCommand Commande_Vider_Tables = new SQLiteCommand(SQL_Query_Vider_Tables, Connection))
                    {
                        Commande_Vider_Tables.ExecuteNonQuery();
                    }
                }
                Connection.Close();
            }
        }

        /// <summary>
        /// Permets de lire le nombre d'articles présent dans la BDD.
        /// </summary>
        /// <returns></returns>
        public int Lire_Nombre_Article_BDD()
        {
            int Nombre_Articles = 0;

            using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
            {
                Connection.Open();
                string SQL_Query_Compter = "SELECT COUNT(*) AS NombreArticles FROM Articles";

                using (SQLiteCommand Commande_Compter = new SQLiteCommand(SQL_Query_Compter, Connection))
                {
                    object Resultat_Requete = Commande_Compter.ExecuteScalar();

                    // Vérifie si le résultat est nul et convertit en int
                    if (Resultat_Requete != null && Resultat_Requete != DBNull.Value)
                    {
                        Nombre_Articles = Convert.ToInt32(Resultat_Requete);
                    }
                }
                Connection.Close();
            }
            return Nombre_Articles;
        }

        /// <summary>
        /// Permets de remplir la liste de marque à partir de la BDD.
        /// </summary>
        public void Remplir_Liste_Marque()
        {
            Liste_Marque.Clear();

            using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
            {
                Connection.Open();

                string SQL_Query_Recherche_Marques = "SELECT RefMarque, Nom FROM Marques";

                using (SQLiteCommand Commande_Query_Recherche_Marques = new SQLiteCommand(SQL_Query_Recherche_Marques, Connection))
                {
                    using (SQLiteDataReader Reader = Commande_Query_Recherche_Marques.ExecuteReader())
                    {
                        // On lit le résultat de la recherche.
                        while (Reader.Read())
                        {
                            // On crée la marque et on l'ajoute à la liste.
                            Marque Marque = new Marque(Convert.ToInt32(Reader["RefMarque"]), Convert.ToString(Reader["Nom"]));
                            Liste_Marque.Add(Marque);
                        }
                    }
                }
                Connection.Close();
            }
        }

        /// <summary>
        /// Permets d'obtenir l'objet marque en fonction de sa référence.
        /// </summary>
        /// <param name="Ref"> Référence de la marque. </param>
        /// <returns> Marque portant cette référence. </returns>
        public Marque Obtenir_Marque(int Ref)
        {
            // On regarde chaque Marque de la liste.
            foreach (Marque Marque in Liste_Marque)
            {
                // Si l'on trouve la marque voulue.
                if (Marque.Lire_Ref_Marque() == Ref)
                {
                    return Marque;
                }
            }
            return null;
        }

        /// <summary>
        /// Permets de remplir la liste de famille à partir de la BDD.
        /// </summary>
        public void Remplir_Liste_Famille()
        {
            Liste_Famille.Clear();

            using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
            {
                Connection.Open();

                string SQL_Query_Recherche_Familles = "SELECT RefFamille, Nom FROM Familles";

                using (SQLiteCommand Commande_Query_Recherche_Familles = new SQLiteCommand(SQL_Query_Recherche_Familles, Connection))
                {
                    using (SQLiteDataReader Reader = Commande_Query_Recherche_Familles.ExecuteReader())
                    {
                        // On lit le résultat de la recherche.
                        while (Reader.Read())
                        {
                            // On crée la famille et on l'ajoute à la liste.
                            Famille Famille = new Famille(Convert.ToInt32(Reader["RefFamille"]), Convert.ToString(Reader["Nom"]));
                            Liste_Famille.Add(Famille);
                        }
                    }
                }
                Connection.Close();
            }
        }

        /// <summary>
        /// Permets d'obtenir l'objet famille en fonction de sa référence.
        /// </summary>
        /// <param name="Ref"> Référence de la famille. </param>
        /// <returns> Famille portant cette référence. </returns>
        public Famille Obtenir_Famille(int Ref)
        {
            // On regarde chaque famille de la liste.
            foreach (Famille Famille in Liste_Famille)
            {
                // Si l'on trouve la famille voulue.
                if (Famille.Lire_Ref_Famille() == Ref)
                {
                    return Famille;
                }
            }
            return null;
        }

        /// <summary>
        /// Permets de remplir la liste de sous-famille à partir de la BDD.
        /// </summary>
        public void Remplir_Liste_Sous_Famille()
        {
            Liste_Sous_Famille.Clear();

            using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
            {
                Connection.Open();

                string SQL_Query_Recherche_Sous_Familles = "SELECT RefSousFamille, RefFamille, Nom FROM SousFamilles";

                using (SQLiteCommand Commande_Query_Recherche_Sous_Familles = new SQLiteCommand(SQL_Query_Recherche_Sous_Familles, Connection))
                {
                    using (SQLiteDataReader Reader = Commande_Query_Recherche_Sous_Familles.ExecuteReader())
                    {
                        // On lit le résultat de la recherche.
                        while (Reader.Read())
                        {
                            // On cherche la famille associé à la sous-famille.
                            Famille Famille = Obtenir_Famille(Convert.ToInt32(Reader["RefFamille"]));

                            // On crée la sous-famille et on l'ajoute à la liste.
                            SousFamille Sous_Famille = new SousFamille(Convert.ToInt32(Reader["RefSousFamille"]), Convert.ToString(Reader["Nom"]), Famille);
                            Liste_Sous_Famille.Add(Sous_Famille);
                        }
                    }
                }
                Connection.Close();
            }
        }

        /// <summary>
        /// Permets d'obtenir l'objet sous-famille en fonction de sa référence.
        /// </summary>
        /// <param name="Ref"> Référence de la sous-famille. </param>
        /// <returns> Sous-Famille portant cette référence. </returns>
        public SousFamille Obtenir_Sous_Famille(int Ref)
        {
            // On regarde chaque sous-famille de la liste.
            foreach (SousFamille Sous_Famille in Liste_Sous_Famille)
            {
                // Si l'on trouve la sous-famille voulue.
                if (Sous_Famille.Lire_Ref_Sous_Famille() == Ref)
                {
                    return Sous_Famille;
                }
            }
            return null;
        }

        /// <summary>
        /// Permets de remplir la liste d'article à partir de la BDD.
        /// </summary>
        public void Remplir_Liste_Article()
        {
            Liste_Article.Clear();

            using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
            {
                Connection.Open();

                string SQL_Query_Recherche_Articles = "SELECT RefArticle, Description, RefSousFamille, RefMarque, PrixHT, Quantite FROM Articles";

                using (SQLiteCommand Commande_Query_Recherche_Articles = new SQLiteCommand(SQL_Query_Recherche_Articles, Connection))
                {
                    using (SQLiteDataReader Reader = Commande_Query_Recherche_Articles.ExecuteReader())
                    {
                        // On lit le résultat de la recherche.
                        while (Reader.Read())
                        {
                            // On cherche la sous-famille associé à l'article.
                            SousFamille Sous_Famille = Obtenir_Sous_Famille(Convert.ToInt32(Reader["RefSousFamille"]));

                            // On cherche la marque associé à l'article.
                            Marque Marque = Obtenir_Marque(Convert.ToInt32(Reader["RefMarque"]));

                            // On crée l'article et on l'ajoute à la liste.
                            Article Article = new Article(Convert.ToString(Reader["RefArticle"]), Convert.ToString(Reader["Description"]), Sous_Famille, Marque, Convert.ToDouble(Reader["PrixHT"]), Convert.ToInt32(Reader["Quantite"]));
                            Liste_Article.Add(Article);
                        }
                    }
                }
                Connection.Close();
            }
        }

        /// <summary>
        /// Permets de supprimer un article de la BDD à partir de sa référence.
        /// </summary>
        /// <param name="Ref"></param>
        public void Supprimer_Article_BDD(string Ref)
        {
            using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
            {
                Connection.Open();

                string SQL_Query_Suppr_Article = "DELETE FROM Articles WHERE RefArticle = @Ref";

                using (SQLiteCommand Commande_Query_Suppr_Article = new SQLiteCommand(SQL_Query_Suppr_Article, Connection))
                {
                    Commande_Query_Suppr_Article.Parameters.AddWithValue("@Ref", Ref);
                    int rowsAffected = Commande_Query_Suppr_Article.ExecuteNonQuery();
                }
                Connection.Close();
            }
        }
    }
}