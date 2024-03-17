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
        /// <param name="Liste_Marque"> Liste des marques que l'on veut ajouter à la BDD. </param>
        public void Ajouter_Marques()
        {
            // On va lire toutes les marques de la liste.
            foreach (Marque Marque in Liste_Marque)
            {
                using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
                {
                    Connection.Open();

                    // Commande SQL permets d'ajoute une marque dans la BDD.
                    string SQL_Query_Ajout_Marque = "INSERT INTO Marques (Nom) VALUES (@Nom)";

                    using (SQLiteCommand Commande_Ajout_Marque = new SQLiteCommand(SQL_Query_Ajout_Marque, Connection))
                    {
                        string Nom_Marque = Marque.Lire_Nom_Marque();

                        // Ajouter les paramètres à la commande.
                        Commande_Ajout_Marque.Parameters.AddWithValue("@Nom", Nom_Marque);

                        // Exécuter la commande.
                        int Rows_Affected = Commande_Ajout_Marque.ExecuteNonQuery();

                        // On va rechercher la référence donnée dans la BDD pour l'associer dans le code.
                        string SQL_Query_Recherche_Ref = "SELECT RefMarque FROM Marques WHERE Nom = @Nom";

                        using (SQLiteCommand Commande_Recherche_Ref = new SQLiteCommand(SQL_Query_Recherche_Ref, Connection))
                        {
                            // Ajouter le paramètre Nom avec sa valeur
                            Commande_Recherche_Ref.Parameters.AddWithValue("@Nom", Nom_Marque);

                            // Exécuter la commande et obtenir le résultat
                            object Ref = Commande_Recherche_Ref.ExecuteScalar();

                            // On modifie la référence dans le code pour qu'elles correspondent à celle de la BDD.
                            Marque.Modifier_Ref_Marque(Convert.ToInt32(Ref));
                        }
                    }
                    Connection.Close();
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
        /// <param name="Liste_Famille"> Liste des familles que l'on veut ajouter à la BDD. </param>
        public void Ajouter_Familles()
        {
            // On va lire toutes les familles de la liste.
            foreach (Famille Famille in Liste_Famille)
            {
                using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
                {
                    Connection.Open();

                    // Commande SQL permets d'ajoute une marque dans la BDD.
                    string SQL_Query_Ajout_Famille = "INSERT INTO Familles (Nom) VALUES (@Nom)";

                    using (SQLiteCommand Commande_Ajout_Famille = new SQLiteCommand(SQL_Query_Ajout_Famille, Connection))
                    {
                        string Nom_Famille = Famille.Lire_Nom_Famille();

                        // Ajouter les paramètres à la commande.
                        Commande_Ajout_Famille.Parameters.AddWithValue("@Nom", Nom_Famille);

                        // Exécuter la commande.
                        int Rows_Affected = Commande_Ajout_Famille.ExecuteNonQuery();

                        // On va rechercher la référence donnée dans la BDD pour l'associer dans le code.
                        string SQL_Query_Recherche_Ref = "SELECT RefFamille FROM Familles WHERE Nom = @Nom";

                        using (SQLiteCommand Commande_Recherche_Ref = new SQLiteCommand(SQL_Query_Recherche_Ref, Connection))
                        {
                            // Ajouter le paramètre Nom avec sa valeur
                            Commande_Recherche_Ref.Parameters.AddWithValue("@Nom", Nom_Famille);

                            // Exécuter la commande et obtenir le résultat
                            object Ref = Commande_Recherche_Ref.ExecuteScalar();

                            // On modifie la référence dans le code pour qu'elles correspondent à celle de la BDD.
                            Famille.Modifier_Ref_Famille(Convert.ToInt32(Ref));
                        }
                        Connection.Close();
                    }
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
        /// <param name="Liste_Sous_Famille"> Liste des sous-familles que l'on veut ajouter à la BDD. </param>
        public void Ajouter_Sous_Familles()
        {
            // On va lire toutes les sous-familles de la liste.
            foreach (SousFamille Sous_Famille in Liste_Sous_Famille)
            {
                using (SQLiteConnection Connection = new SQLiteConnection(Connection_String))
                {
                    Connection.Open();

                    // Commande SQL permets d'ajoute une marque dans la BDD.
                    string SQL_Query_Ajout_Sous_Famille = "INSERT INTO SousFamilles (RefFamille, Nom) VALUES (@RefFamille, @Nom)";

                    using (SQLiteCommand Commande_Ajout_Sous_Famille = new SQLiteCommand(SQL_Query_Ajout_Sous_Famille, Connection))
                    {
                        string Nom_Sous_Famille = Sous_Famille.Lire_Nom_Sous_Famille();
                        int Ref_Famille = Sous_Famille.Lire_Famille().Lire_Ref_Famille();

                        // Ajouter les paramètres à la commande.
                        Commande_Ajout_Sous_Famille.Parameters.AddWithValue("@RefFamille", Ref_Famille);
                        Commande_Ajout_Sous_Famille.Parameters.AddWithValue("@Nom", Nom_Sous_Famille);

                        // Exécuter la commande.
                        int Rows_Affected = Commande_Ajout_Sous_Famille.ExecuteNonQuery();

                        // On va rechercher la référence donnée dans la BDD pour l'associer dans le code.
                        string SQL_Query_Recherche_Ref = "SELECT RefMarque FROM Marques WHERE Nom = @Nom";

                        using (SQLiteCommand Commande_Recherche_Ref = new SQLiteCommand(SQL_Query_Recherche_Ref, Connection))
                        {
                            // Ajouter le paramètre Nom avec sa valeur
                            Commande_Recherche_Ref.Parameters.AddWithValue("@Nom", Nom_Sous_Famille);

                            // Exécuter la commande et obtenir le résultat
                            object Ref = Commande_Recherche_Ref.ExecuteScalar();

                            // On modifie la référence dans le code pour qu'elles correspondent à celle de la BDD.
                            Sous_Famille.Modifier_Ref_Sous_Famille(Convert.ToInt32(Ref));
                        }
                    }
                    Connection.Close();
                }
            }
        }
    }
}
