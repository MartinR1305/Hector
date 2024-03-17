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
            /*            Parseur parseur = new Parseur("C://Users//reche//Documents//FOD//4A//S8//.NET//Projet//Données à intégrer.csv");
                        parseur.Remplir_Liste_Famille();
                        parseur.Remplir_Liste_Sous_Famille();

                        BDD bdd = new BDD();
                        bdd.Obtenir_Chemin_Base_de_Donnees();
                        bdd.Ajouter_Familles(parseur.Lire_Liste_Famille());
                        bdd.Ajouter_Sous_Familles(parseur.Lire_Liste_Sous_Famille());*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
