using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    public class SousFamille
    {
        private int Ref_Sous_Famille;
        private string Nom_Sous_Famille;
        private Famille Famille;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public SousFamille()
        {
            Ref_Sous_Famille = 0;
            Nom_Sous_Famille = "";
        }

        /// <summary>
        /// Constructeur de confort.
        /// </summary>
        /// <param name="Ref"> Référence de la sous-famille. </param>
        /// <param name="Nom"> Nom que l'on veut donner à la sous-famille. </param>
        /// <param name="Fam"> Famille à laquelle la sous-famille appartient. </param>
        /// <exception cref="Exception"> Nom vide. </exception>
        public SousFamille(int Ref, string Nom, Famille Fam)
        {
            // On vérifie que le nom n'est pas vide.
            if (Nom_Sous_Famille == "")
            {
                throw new Exception("Vous ne pouvez pas donner de nom vide à une sous_famille.");
            }

            // On associe les valeurs d'attribut.
            Ref_Sous_Famille = Ref;
            Nom_Sous_Famille = Nom;
            Famille = Fam;
        }

        /// <summary>
        /// Getter de la référence de la sous-famille.
        /// </summary>
        /// <returns> La référence de la sous-famille. </returns>
        public int Lire_Ref_Sous_Famille()
        {
            return Ref_Sous_Famille;
        }

        /// <summary>
        /// Setter de l'attribut de référence.
        /// </summary>
        /// <param name="Ref"> La référence que l'on veut attribuer. </param>
        public void Modifier_Ref_Sous_Famille(int Ref)
        {
            Ref_Sous_Famille = Ref;
        }

        /// <summary>
        /// Getter du nom de la sous-famille.
        /// </summary>
        /// <exception cref="Exception"> Le nom de la sous-famille est vide. </exception>
        /// <returns> Le nom de la sous-famille. </returns>
        public string Lire_Nom_Sous_Famille()
        {
            // On vérifie que le nom de la sous-famille n'est pas vide.
            if (Nom_Sous_Famille == "")
            {
                throw new Exception("Vous essayez de lire un nom de sous-famille qui est vide.");
            }

            return Nom_Sous_Famille;
        }

        /// <summary>
        /// Setter du nom de la sous-famille.
        /// </summary>
        /// <param name="Nom"> Le nom que l'on veut donner à la sous-famille. </param>
        /// <exception cref="Exception"> Nom vide. </exception>
        public void Modifier_Nom_Sous_Famille(string Nom)
        {
            // On vérifie que le nom n'est pas vide.
            if (Nom == "")
            {
                throw new Exception("Vous essayez de donner un nom de sous-famille qui est vide.");
            }
            Nom_Sous_Famille = Nom;
        }

        /// <summary>
        /// Getter de l'attribut de famille.
        /// </summary>
        /// <returns> La famille à laquelle appartient la sous-famille. </returns>
        public Famille Lire_Famille()
        {
            return Famille;
        }

        /// <summary>
        /// setter de l'attribut de famille.
        /// </summary>
        /// <param name="Fam"> Famille que l'on veut associer à cette sous-famille. </param>
        public void Modifier_Famille(Famille Fam)
        {
            Famille = Fam;
        }
    }
}
