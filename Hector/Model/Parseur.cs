using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Hector
{
    class Parseur
    {
        private string Chemin_Fichier_CSV;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public Parseur()
        {
            Chemin_Fichier_CSV = "";
        }

        /// <summary>
        /// Constructeur de confort.
        /// </summary>
        /// <param name="Chemin_Fichier"> Chemin du fichier CSV. </param>
        public Parseur(string Chemin_Fichier)
        {
            Chemin_Fichier_CSV = Chemin_Fichier;
        }

        /// <summary>
        /// Permets de remplir la liste de marque à partir d'un fichier CSV.
        ///<param name="Liste_Marque_CSV"> Liste que l'on veut remplir. </param>
        /// </summary>
        public void Remplir_Liste_Marque(List<Marque> Liste_Marque)
        {
            // On ouvre le fichier en mode lecture
            using (TextFieldParser TFParseur = new TextFieldParser(Chemin_Fichier_CSV, Encoding.GetEncoding(1252)))
            {
                TFParseur.SetDelimiters(new string[] { ";" });

                // Ignorer la première ligne ( en-tête du tableau ).
                TFParseur.ReadLine();

                // On va lire chaque lire jusqu'à la fin du fichier.
                while (!TFParseur.EndOfData)
                {
                    string[] Ligne = TFParseur.ReadFields();

                    // Récupérer la troisième valeur.
                    string Nom_Marque = Ligne[2];

                    if (!Is_Nom_Marque_Present(Liste_Marque, Nom_Marque))
                    {
                        // On l'ajoute à la liste.
                        Marque Marque_Temp = new Marque(0, Nom_Marque); // On mets 0 comme référence car la marque n'a pas encore été ajouté à la BDD.
                        Liste_Marque.Add(Marque_Temp);
                    }
                }
            }
        }

        /// <summary>
        /// Permets de remplir la liste de famille à partir d'un fichier CSV.
        /// </summary>
        /// <param name="Liste_Famille"> Liste que l'on veut remplir. </param>
        public void Remplir_Liste_Famille(List<Famille> Liste_Famille)
        {
            // On ouvre le fichier en mode lecture
            using (TextFieldParser TFParseur = new TextFieldParser(Chemin_Fichier_CSV, Encoding.GetEncoding(1252)))
            {
                TFParseur.SetDelimiters(new string[] { ";" });

                // Ignorer la première ligne ( en-tête du tableau ).
                TFParseur.ReadLine();

                // On va lire chaque lire jusqu'à la fin du fichier.
                while (!TFParseur.EndOfData)
                {
                    string[] Ligne = TFParseur.ReadFields();

                    // Récupérer la quatrième valeur.
                    string Nom_Famille = Ligne[3];

                    if (!Is_Nom_Famille_Present(Liste_Famille, Nom_Famille))
                    {
                        // On l'ajoute à la liste.
                        Famille Famille_Temp = new Famille(0, Nom_Famille); // On mets 0 comme référence car la famille n'a pas encore été ajouté à la BDD.
                        Liste_Famille.Add(Famille_Temp);
                    }
                }
            }
        }

        /// <summary>
        /// Permets de remplir la liste de sous-famille à partir d'un fichier CSV.
        /// </summary>
        /// <param name="Liste_Sous_Famille"> Liste que l'on veut remplir. </param>
        /// <param name="Liste_Famille"> Liste des familles dont on va avoir besoin. </param>
        public void Remplir_Liste_Sous_Famille(List<SousFamille> Liste_Sous_Famille, List<Famille> Liste_Famille)
        {
            // On ouvre le fichier en mode lecture
            using (TextFieldParser TFParseur = new TextFieldParser(Chemin_Fichier_CSV, Encoding.GetEncoding(1252)))
            {
                TFParseur.SetDelimiters(new string[] { ";" });

                // Ignorer la première ligne ( en-tête du tableau ).
                TFParseur.ReadLine();

                // On va lire chaque ligne jusqu'à la fin du fichier.
                while (!TFParseur.EndOfData)
                {
                    string[] Ligne = TFParseur.ReadFields();

                    // Récupérer la troisième et quatrième valeur.
                    string Nom_Famille = Ligne[3];
                    string Nom_Sous_Famille = Ligne[4];

                    if (!Is_Nom_Sous_Famille_Present(Liste_Sous_Famille, Nom_Sous_Famille))
                    {
                        // On cherche la famille associé à cette sous-famille.
                        Famille Famille_De_la_Sous_Famille = Obtenir_Famille(Liste_Famille, Nom_Famille);

                        // On l'ajoute à la liste.
                        SousFamille Sous_Famille_Temp = new SousFamille(0, Nom_Sous_Famille, Famille_De_la_Sous_Famille); // On mets 0 comme référence car la sous-famille n'a pas encore été ajouté à la BDD.
                        Liste_Sous_Famille.Add(Sous_Famille_Temp);
                    }
                }
            }
        }
        /// <summary>
        /// Permets de remplir la liste d'article.
        /// </summary>
        /// <param name="Liste_Article"> La liste d'article que l'on veut remplir. </param>
        /// <param name="Liste_Marque"> La liste de marque dont on va avoir besoin. </param>
        /// <param name="Liste_Sous_Famille"> La liste de sous-famille dont on va avoir besoin. </param>
        public void Remplir_Liste_Article(List<Article> Liste_Article, List<Marque> Liste_Marque, List<SousFamille> Liste_Sous_Famille)
        {
            // On ouvre le fichier en mode lecture
            using (TextFieldParser TFParseur = new TextFieldParser(Chemin_Fichier_CSV, Encoding.GetEncoding(1252)))
            {
                TFParseur.SetDelimiters(new string[] { ";" });

                // Ignorer la première ligne ( en-tête du tableau ).
                TFParseur.ReadLine();

                // On va lire chaque linge jusqu'à la fin du fichier.
                while (!TFParseur.EndOfData)
                {
                    string[] Ligne = TFParseur.ReadFields();

                    // Récupérer toutes les valeurs.
                    string Description = Ligne[0];
                    string Référence_Article = Ligne[1];
                    string Nom_Marque = Ligne[2];
                    string Nom_Sous_Famille = Ligne[4];
                    string PrixHT = Ligne[5];

                    if (!Is_Description_Article_Present(Liste_Article, Description))
                    {
                        // On cherche la sous-famille.
                        SousFamille Sous_Famille = Obtenir_Sous_Famille(Liste_Sous_Famille, Nom_Sous_Famille);

                        // On cherche la marque.
                        Marque Marque = Obtenir_Marque(Liste_Marque, Nom_Marque);

                        // On l'ajoute à la liste.
                        Article Article_Temp = new Article(Référence_Article, Description, Sous_Famille, Marque, Convert.ToDouble(PrixHT));
                        Liste_Article.Add(Article_Temp);
                    }
                }
            }
        }

        /// <summary>
        /// Permets d'obtenir la marque à partir de son nom.
        /// </summary>
        /// <param name="Liste_Marque"> Liste de marque où l'on va chercher la marque. </param>
        /// <param name="Nom"> Nom de la marque que l'on recherche. </param>
        /// <returns> Marque, la marque que l'on cherche. </returns>
        public Marque Obtenir_Marque(List<Marque> Liste_Marque, string Nom)
        {
            // On regarde chaque marque de la liste.
            foreach (Marque Marque in Liste_Marque)
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
        public SousFamille Obtenir_Sous_Famille(List<SousFamille> Liste_Sous_Famille, string Nom)
        {
            // On regarde chaque sous-famille de la liste.
            foreach (SousFamille Sous_Famille in Liste_Sous_Famille)
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
        public Famille Obtenir_Famille(List<Famille> Liste_Famille, string Nom)
        {
            // On regarde chaque famille de la liste.
            foreach (Famille Famille in Liste_Famille)
            {
                // Si l'on trouve la famille voulue.
                if (Famille.Lire_Nom_Famille().ToUpper() == Nom.ToUpper())
                {
                    return Famille;
                }
            }
            return null;
        }

        /// <summary>
        /// Permets de savoir si un nom est déjà présent dans la liste de Marque.
        /// </summary>
        /// <param name="Liste_Sous_Famille"> Liste de sous-famille où l'on va vérifier. </param>
        /// <param name="Nom"> Nom que l'on veut vérifier.</param>
        /// <returns> Bool, indiquant si le nom est déjà présent ou non. </returns>
        public bool Is_Nom_Sous_Famille_Present(List<SousFamille> Liste_Sous_Famille, string Nom)
        {
            // Cas où la liste est vide.
            if (Liste_Sous_Famille == null)
            {
                return false;
            }

            // On regarde chaque sous-famille de la liste.
            foreach (SousFamille Sous_Famille in Liste_Sous_Famille)
            {
                // Si l'on trouve un nom identique.
                if (Sous_Famille.Lire_Nom_Sous_Famille().ToUpper() == Nom.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permets de savoir si une description est déjà présent dans la liste de Article.
        /// </summary>
        /// <param name="Liste_Article"> Liste d'article où l'on va vérifier. </param>
        /// <param name="Description"> Description que l'on veut vérifier.</param>
        /// <returns> Bool, indiquant si la description est déjà présente ou non. </returns>
        public bool Is_Description_Article_Present(List<Article> Liste_Article, string Description)
        {
            // Cas où la liste est vide.
            if (Liste_Article == null)
            {
                return false;
            }

            // On regarde chaque article de la liste.
            foreach (Article Article in Liste_Article)
            {
                // Si l'on trouve une description identique.
                if (Article.Lire_Description().ToUpper() == Description.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Permets de savoir si un nom est déjà présent dans la liste de Marque.
        /// </summary>
        /// <param name="Liste_Marque"> Liste où l'on va vérifier si la marque est présente. </param>
        /// <param name="Nom"> Nom que l'on veut vérifier.</param>
        /// <returns> Bool, indiquant si le nom est déjà présent ou non. </returns>
        public bool Is_Nom_Marque_Present(List<Marque> Liste_Marque, string Nom)
        {
            // Cas où la liste est vide.
            if (Liste_Marque == null)
            {
                return false;
            }

            // On regarde chaque marque de la liste.
            foreach (Marque Marque in Liste_Marque)
            {
                // Si l'on trouve un nom identique.
                if (Marque.Lire_Nom_Marque().ToUpper() == Nom.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permets de savoir si un nom est déjà présent dans la liste de Famille.
        /// </summary>
        /// <param name="Liste_Famille"> Liste de famille où l'on va vérifier. </param>
        /// <param name="Nom"> Nom que l'on veut vérifier.</param>
        /// <returns> Bool, indiquant si le nom est déjà présent ou non. </returns>
        public bool Is_Nom_Famille_Present(List<Famille> Liste_Famille, string Nom)
        {
            // Cas où la liste est vide.
            if (Liste_Famille == null)
            {
                return false;
            }

            // On regarde chaque famille de la liste.
            foreach (Famille Famille in Liste_Famille)
            {
                // Si l'on trouve un nom identique.
                if (Famille.Lire_Nom_Famille().ToUpper() == Nom.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Permets de remplir toutes les tables dans le code.
        /// </summary>
        /// <param name="Base_de_Donnees"> Base de données dans le code que l'on veut remplir. </param>
        public void Remplir_Toutes_Les_Tables_BDD(BDD Base_de_Donnees)
        {
            Remplir_Liste_Marque(Base_de_Donnees.Lire_Liste_Marque());
            Remplir_Liste_Famille(Base_de_Donnees.Lire_Liste_Famille());
            Remplir_Liste_Sous_Famille(Base_de_Donnees.Lire_Liste_Sous_Famille(), Base_de_Donnees.Lire_Liste_Famille());
            Remplir_Liste_Article(Base_de_Donnees.Lire_Liste_Article(), Base_de_Donnees.Lire_Liste_Marque(), Base_de_Donnees.Lire_Liste_Sous_Famille());
        }
    }
}
