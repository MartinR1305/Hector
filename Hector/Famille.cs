using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    public class Famille
    {
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
        /// <param name="Ref"> Référence de la famille. </param>
        /// <param name="Nom_Famille"> Nom que l'on veut donner à la famille. </param>
        /// <exception cref="Exception"> Nom vide. </exception>
        public Famille(int Ref, string Nom)
        {
            // On vérifie que le nom n'est pas vide.
            if (Nom_Famille == "")
            {
                throw new Exception("Vous ne pouvez pas donner de nom vide à une famille.");
            }

            // On associe les valeurs d'attribut.
            Ref_Famille = Ref;
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
        /// Setter de l'attribut de référence.
        /// </summary>
        /// <param name="Ref"> La référence que l'on veut attribuer. </param>
        public void Modifier_Ref_Famille(int Ref)
        {
            Ref_Famille = Ref;
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
