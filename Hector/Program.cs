using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hector
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*            BDD bdd = new BDD();
                        bdd.Obtenir_Chemin_Base_de_Donnees();

                        Parseur parseur = new Parseur("C://Users//reche//Documents//FOD//4A//S8//.NET//Projet//Données à intégrer.csv"); 
                        //Parseur parseur = new Parseur("C://Users//Martin//Documents//Martin//Polytech//4A//S8//.NET//Données à intégrer.csv");
                        parseur.Remplir_Liste_Marque(bdd.Lire_Liste_Marque());
                        parseur.Remplir_Liste_Famille(bdd.Lire_Liste_Famille());
                        parseur.Remplir_Liste_Sous_Famille(bdd.Lire_Liste_Sous_Famille(), bdd.Lire_Liste_Famille());
                        parseur.Remplir_Liste_Article(bdd.Lire_Liste_Article(), bdd.Lire_Liste_Marque(), bdd.Lire_Liste_Sous_Famille());

                        bdd.Ajouter_Toutes_Les_Marques_BDD();
                        bdd.Ajouter_Toutes_Les_Familles_BDD();
                        bdd.Ajouter_Toutes_Les_Sous_Familles_BDD();
                        bdd.Ajouter_Tout_Les_Articles_BDD();*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
