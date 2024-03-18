using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Hector
{
    class BDD
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

                            // On modifie la référence dans le code pour qu'elles correspondent à celle de la BDD.
                            Marque.Modifier_Ref_Marque(Convert.ToInt32(Obtenir_Ref_Marque_BDD(Nom_Marque, Connection)));
                        }
                    }
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

                            // On modifie la référence dans le code pour qu'elles correspondent à celle de la BDD.
                            Famille.Modifier_Ref_Famille(Convert.ToInt32(Obtenir_Ref_Famille_BDD(Nom_Famille, Connection)));
                        }
                    }
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

                            // On modifie la référence dans le code pour qu'elles correspondent à celle de la BDD.
                            Sous_Famille.Modifier_Ref_Sous_Famille(Convert.ToInt32(Obtenir_Ref_Sous_Famille_BDD(Nom_Sous_Famille, Connection)));
                        }
                    }
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
        /// Permets d'ajouter tout les articles dans la BDD.
        /// </summary>
        public void Ajouter_Tout_Les_Articles_BDD()
        {
            // On va lire tout les articles de la liste.
            foreach (Article Article in Liste_Article)
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
    }
}