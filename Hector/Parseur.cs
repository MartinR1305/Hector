using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.Linq;
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
        /// <param name="Chemin_Fichier"> Chemi du fichier CSV. </param>
        public Parseur(string Chemin_Fichier)
        {
            Chemin_Fichier_CSV = Chemin_Fichier;
        }

        /// <summary>
        /// Permets de remplir la liste de marque à partir d'un fichier CSV.
        /// </summary>
        public void Remplir_Liste_Marque(List<Marque> Liste_Marque_CSV)
        {
            // On ouvre le fichier en mode lecture
            using (TextFieldParser TFParseur = new TextFieldParser(Chemin_Fichier_CSV, Encoding.UTF8))
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

                    if (!Is_Nom_Marque_Present(Liste_Marque_CSV, Nom_Marque))
                    {
                        // On l'ajoute à la liste.
                        Marque Marque_Temp = new Marque(0, Nom_Marque); // On mets 0 comme référence car la marque n'a pas encore été ajouté à la BDD.
                        Liste_Marque_CSV.Add(Marque_Temp);
                    }
                }
            }
        }

        /// <summary>
        /// Permets de remplir la liste de famille à partir d'un fichier CSV.
        /// </summary>
        public void Remplir_Liste_Famille(List<Famille> Liste_Famille_CSV)
        {
            // On ouvre le fichier en mode lecture
            using (TextFieldParser TFParseur = new TextFieldParser(Chemin_Fichier_CSV, Encoding.UTF8))
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

                    if (!Is_Nom_Famille_Present(Liste_Famille_CSV, Nom_Famille))
                    {
                        // On l'ajoute à la liste.
                        Famille Famille_Temp = new Famille(0, Nom_Famille); // On mets 0 comme référence car la famille n'a pas encore été ajouté à la BDD.
                        Liste_Famille_CSV.Add(Famille_Temp);
                    }
                }
            }
        }

        /// <summary>
        /// Permets de remplir la liste de sous-famille à partir d'un fichier CSV.
        /// </summary>
        public void Remplir_Liste_Sous_Famille(List<SousFamille> Liste_Sous_Famille_CSV, List<Famille> Liste_Famille_CSV)
        {
            // On ouvre le fichier en mode lecture
            using (TextFieldParser TFParseur = new TextFieldParser(Chemin_Fichier_CSV, Encoding.UTF8))
            {
                TFParseur.SetDelimiters(new string[] { ";" });

                // Ignorer la première ligne ( en-tête du tableau ).
                TFParseur.ReadLine();

                // On va lire chaque lire jusqu'à la fin du fichier.
                while (!TFParseur.EndOfData)
                {
                    string[] Ligne = TFParseur.ReadFields();

                    // Récupérer la troisième et quatrième valeur.
                    string Nom_Famille = Ligne[3];
                    string Nom_Sous_Famille = Ligne[4];

                    if (!Is_Nom_Sous_Famille_Present(Liste_Sous_Famille_CSV, Nom_Sous_Famille))
                    {
                        // On cherche la famille associé à cette sous-famille.
                        Famille Famille_De_la_Sous_Famille = Obtenir_Famille(Liste_Famille_CSV, Nom_Famille);

                        // On l'ajoute à la liste.
                        SousFamille Sous_Famille_Temp = new SousFamille(0, Nom_Sous_Famille, Famille_De_la_Sous_Famille); // On mets 0 comme référence car la sous-famille n'a pas encore été ajouté à la BDD.
                        Liste_Sous_Famille_CSV.Add(Sous_Famille_Temp);
                    }
                }
            }
        }

        /// <summary>
        /// Permets de savoir si un nom est déjà présent dans la liste de Marque.
        /// </summary>
        /// <param name="Nom"> Nom que l'on veut vérifier.</param>
        /// <returns> Bool, indiquant si le nom est déjà présent ou non. </returns>
        public bool Is_Nom_Marque_Present(List<Marque> Liste_Marque_CSV, string Nom)
        {
            // Cas où la liste est vide.
            if(Liste_Marque_CSV == null)
            {
                return false;
            }

            // On regarde chaque marque de la liste.
            foreach (Marque Marque in Liste_Marque_CSV)
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
        /// <param name="Nom"> Nom que l'on veut vérifier.</param>
        /// <returns> Bool, indiquant si le nom est déjà présent ou non. </returns>
        public bool Is_Nom_Famille_Present(List<Famille> Liste_Famille_CSV, string Nom)
        {
            // Cas où la liste est vide.
            if (Liste_Famille_CSV == null)
            {
                return false;
            }

            // On regarde chaque famille de la liste.
            foreach (Famille Famille in Liste_Famille_CSV)
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
        /// Permets d'obtenir la famille à partir de son nom.
        /// </summary>
        /// <param name="Nom"> Nom de la famille que l'on recherche. </param>
        /// <returns> Famille, la famille que l'on cherche. </returns>
        public Famille Obtenir_Famille(List<Famille> Liste_Famille_CSV, string Nom)
        {
            // On regarde chaque famille de la liste.
            foreach (Famille Famille in Liste_Famille_CSV)
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
        /// <param name="Nom"> Nom que l'on veut vérifier.</param>
        /// <returns> Bool, indiquant si le nom est déjà présent ou non. </returns>
        public bool Is_Nom_Sous_Famille_Present(List<SousFamille> Liste_Sous_Famille_CSV, string Nom)
        {
            // Cas où la liste est vide.
            if (Liste_Sous_Famille_CSV == null)
            {
                return false;
            }

            // On regarde chaque marque de la liste.
            foreach (SousFamille Sous_Famille in Liste_Sous_Famille_CSV)
            {
                // Si l'on trouve un nom identique.
                if (Sous_Famille.Lire_Nom_Sous_Famille().ToUpper() == Nom.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
