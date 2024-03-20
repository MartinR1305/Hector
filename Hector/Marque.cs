using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    public class Marque
    {
        private int Ref_Marque;
        private string Nom_Marque;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public Marque()
        {
            Ref_Marque = 0;
            Nom_Marque = "";
        }

        /// <summary>
        /// Constructeur de confort.
        /// </summary>
        /// <param name="Nom_Marque"> Nom que l'on veut donner à la marque. </param>
        /// <param name="Ref"> Référence de la marque. </param>
        /// <exception cref="Exception"> Nom vide. </exception>
        public Marque(int Ref, string Nom)
        {
            // On vérifie que le nom n'est pas vide.
            if (Nom_Marque == "")
            {
                throw new Exception("Vous ne pouvez pas donner de nom vide à une marque.");
            }

            // On associe les valeurs d'attribut.
            Ref_Marque = Ref;
            Nom_Marque = Nom;
        }

        /// <summary>
        /// Getter de la référence de la marque.
        /// </summary>
        /// <returns> La référence de la marque. </returns>
        public int Lire_Ref_Marque()
        {
            return Ref_Marque;
        }

        /// <summary>
        /// Setter de l'attribut de référence.
        /// </summary>
        /// <param name="Ref"> La référence que l'on veut attribuer. </param>
        public void Modifier_Ref_Marque(int Ref)
        {
            Ref_Marque = Ref;
        }

        /// <summary>
        /// Getter du nom de la marque.
        /// </summary>
        /// <exception cref="Exception"> Le nom de la marque est vide. </exception>
        /// <returns> Le nom de la marque. </returns>
        public string Lire_Nom_Marque()
        {
            // On vérifie que le nom de la marque n'est pas vide.
            if (Nom_Marque == "")
            {
                throw new Exception("Vous essayez de lire un nom de marque qui est vide.");
            }

            return Nom_Marque;
        }

        /// <summary>
        /// Setter du nom de la marque.
        /// </summary>
        /// <param name="Nom"> Le nom que l'on veut donner à la marque. </param>
        /// <exception cref="Exception"> Nom vide. </exception>
        public void Modifier_Nom_Marque(string Nom)
        {
            // On vérifie que le nom n'est pas vide.
            if (Nom == "")
            {
                throw new Exception("Vous essayez de donner un nom de marque qui est vide.");
            }
            Nom_Marque = Nom;
        }
    }
}
