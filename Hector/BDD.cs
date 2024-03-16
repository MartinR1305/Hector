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

        public BDD()
        {

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
        }

        /// <summary>
        /// Permets d'ajouter les marques d'une liste à la BDD.
        /// </summary>
        /// <param name="Liste_Marque"> Liste des marques que l'on veut ajouter à la BDD. </param>
        public void Ajouter_Marques(List<Marque> Liste_Marque)
        {
            string connectionString = $"Data Source={Lire_Chemin_Base_de_Donnees()};Version=3;";

            // On va lire toutes les marques de la liste.
            foreach (Marque Marque in Liste_Marque)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Commande SQL permets d'ajoute une marque dans la BDD.
                    string sqlQuery = "INSERT INTO Marques (Nom) VALUES (@Nom)";

                    using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                    {
                        // Ajouter les paramètres à la commande.
                        command.Parameters.AddWithValue("@Nom", Marque.Lire_Nom_Marque());

                        // Exécuter la commande.
                        int rowsAffected = command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Permets d'ajouter les familles d'une liste à la BDD.
        /// </summary>
        /// <param name="Liste_Famille"> Liste des familles que l'on veut ajouter à la BDD. </param>
        public void Ajouter_Familles(List<Famille> Liste_Famille)
        {
            string connectionString = $"Data Source={Lire_Chemin_Base_de_Donnees()};Version=3;";

            // On va lire toutes les familles de la liste.
            foreach (Famille Famille in Liste_Famille)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Commande SQL permets d'ajoute une marque dans la BDD.
                    string sqlQuery = "INSERT INTO Familles (Nom) VALUES (@Nom)";

                    using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                    {
                        // Ajouter les paramètres à la commande.
                        command.Parameters.AddWithValue("@Nom", Famille.Lire_Nom_Famille());

                        // Exécuter la commande.
                        int rowsAffected = command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Permets d'ajouter les sous-familles d'une liste à la BDD.
        /// </summary>
        /// <param name="Liste_Sous_Famille"> Liste des sous-familles que l'on veut ajouter à la BDD. </param>
        public void Ajouter_Sous_Familles(List<SousFamille> Liste_Sous_Famille)
        {
            string connectionString = $"Data Source={Lire_Chemin_Base_de_Donnees()};Version=3;";

            // On va lire toutes les sous-familles de la liste.
            foreach (SousFamille Sous_Famille in Liste_Sous_Famille)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Commande SQL permets d'ajoute une marque dans la BDD.
                    string sqlQuery = "INSERT INTO SousFamilles (RefFamille, Nom) VALUES (@RefFamille, @Nom)";

                    using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                    {
                        // Ajouter les paramètres à la commande.
                        command.Parameters.AddWithValue("@RefFamille", Sous_Famille.Lire_Famille().Lire_Ref_Famille());
                        command.Parameters.AddWithValue("@Nom", Sous_Famille.Lire_Nom_Sous_Famille());

                        // Exécuter la commande.
                        int rowsAffected = command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
        }
    }
}
