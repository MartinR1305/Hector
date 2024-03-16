using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    class Famille
    {
        private static int Derniere_Ref = 0;
        private int Ref_Famille;
        private string Nom_Famille;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public Famille()
        {
            Ref_Famille = 0;
            Nom_Famille = "";
        }

        /// <summary>
        /// Constructeur de confort.
        /// </summary>
        /// <param name="Nom_Famille"> Nom que l'on veut donner à la famille. </param>
        /// <exception cref="Exception"> Nom vide. </exception>
        public Famille(string Nom)
        {
            // On vérifie que le nom n'est pas vide.
            if (Nom_Famille == "")
            {
                throw new Exception("Vous ne pouvez pas donner de nom vide à une famille.");
            }

            // On associe les valeurs d'attribut.
            Derniere_Ref++;
            Ref_Famille = Derniere_Ref;
            Nom_Famille = Nom;
        }

        /// <summary>
        /// Getter de la référence de la famille.
        /// </summary>
        /// <returns> La référence de la famille. </returns>
        public int Lire_Ref_Famille()
        {
            return Ref_Famille;
        }

        /// <summary>
        /// Getter du nom de la famille.
        /// </summary>
        /// <exception cref="Exception"> Le nom de la famille est vide. </exception>
        /// <returns> Le nom de la famille. </returns>
        public string Lire_Nom_Famille()
        {
            // On vérifie que le nom de la famille n'est pas vide.
            if (Nom_Famille == "")
            {
                throw new Exception("Vous essayez de lire un nom de famille qui est vide.");
            }

            return Nom_Famille;
        }

        /// <summary>
        /// Setter du nom de la famille.
        /// </summary>
        /// <param name="Nom"> Le nom que l'on veut donner à la famille. </param>
        /// <exception cref="Exception"> Nom vide. </exception>
        public void Modifier_Nom_Famille(string Nom)
        {
            // On vérifie que le nom n'est pas vide.
            if (Nom == "")
            {
                throw new Exception("Vous essayez de donner un nom de famille qui est vide.");
            }
            Nom_Famille = Nom;
        }
    }
}
