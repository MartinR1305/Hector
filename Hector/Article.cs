using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hector
{
    public class Article
    {
        private string Ref_Article;
        private string Description;
        private SousFamille Sous_Famille;
        private Marque Marque;
        private double PrixHT;
        private int Quantite;

        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public Article()
        {
            Ref_Article = "";
            Description = "";
            PrixHT = 0D;
            Quantite = 0;
        }

        /// <summary>
        /// Constructeur de confort sans la quantité.
        /// </summary>
        /// <param name="Ref"> Référence de l'article. </param>
        /// <param name="Descrip"> Description de l'article. </param>
        /// <param name="Ss_Famille"> Sous-famille de l'article. </param>
        /// <param name="Marq"> Marque de l'article. </param>
        /// <param name="Prix"> PrixHT de l'article. </param>
        /// <exception cref="Exception"> Description vide. </exception>
        /// <exception cref="Exception"> Prix négatif. </exception>
        public Article(string Ref, string Descrip, SousFamille Ss_Famille, Marque Marq, double Prix)
        {
            // On vérifie que la description n'est pas vide.
            if (Descrip == "")
            {
                throw new Exception("Vous ne pouvez pas donner de description vide à un article.");
            }

            // On vérifie que le prix est positif.
            else if (Prix < 0D)
            {
                throw new Exception("Vous ne pouvez pas donner de prix négatif à un article.");
            }

            // On associe les valeurs d'attribut.
            Ref_Article = Ref;
            Description = Descrip;
            Sous_Famille = Ss_Famille;
            Marque = Marq;
            PrixHT = Prix;
        }

        /// <summary>
        /// Constructeur de confort avec quantité.
        /// </summary>
        /// <param name="Ref"> Référence de l'article. </param>
        /// <param name="Descrip"> Description de l'article. </param>
        /// <param name="Ss_Famille"> Sous-famille de l'article. </param>
        /// <param name="Marq"> Marque de l'article. </param>
        /// <param name="Prix"> Prix de l'article. </param>
        /// <param name="Qte"> Quantité de l'article. </param>
        /// <exception cref="Exception"> Description vide. </exception>
        /// <exception cref="Exception"> Prix négatif. </exception>
        /// <exception cref="Exception"> Quantité négatif. </exception>
        public Article(string Ref, string Descrip, SousFamille Ss_Famille, Marque Marq, double Prix, int Qte)
        {
            // On vérifie que la description n'est pas vide.
            if (Descrip == "")
            {
                throw new Exception("Vous ne pouvez pas donner de description vide à un article.");
            }

            // On vérifie que le prix est positif.
            else if (Prix < 0D)
            {
                throw new Exception("Vous ne pouvez pas donner de prix négatif à un article.");
            }

            // On vérifie que la quantité est positive.
            else if (Qte < 0)
            {
                throw new Exception("Vous ne pouvez pas donner de quantité négatif à un article.");
            }

            // On associe les valeurs d'attribut.
            Ref_Article = Ref;
            Description = Descrip;
            Sous_Famille = Ss_Famille;
            Marque = Marq;
            PrixHT = Prix;
            Quantite = Qte;
        }

        /// <summary>
        /// Getter de la référence de l'article.
        /// </summary>
        /// <returns> La référence de l'article. </returns>
        public string Lire_Ref_Article()
        {
            return Ref_Article;
        }

        /// <summary>
        /// Setter de l'attribut de référence.
        /// </summary>
        /// <param name="Ref"> La référence que l'on veut attribuer. </param>
        public void Modifier_Ref_Article(string Ref)
        {
            Ref_Article = Ref;
        }

        /// <summary>
        /// Getter de la description de l'article.
        /// </summary>
        /// <returns> La description de l'article. </returns>
        /// <exception cref="Exception"> Description vide. </exception>
        public string Lire_Description()
        {
            // On vérifie que la description n'est pas vide.
            if (Description == "")
            {
                throw new Exception("Vous ne pouvez pas lire une description vide.");
            }
            return Description;
        }

        /// <summary>
        /// Setter de la description de l'article.
        /// </summary>
        /// <param name="Descrip"> Description que l'on veut donner à l'article. </param>
        /// <exception cref="Exception"> Description vide. </exception>
        public void Modifier_Description(string Descrip)
        {
            // On vérifie que la description n'est pas vide.
            if (Descrip == "")
            {
                throw new Exception("Vous ne pouvez pas donner une description vide à un article.");
            }
            Description = Descrip;
        }

        /// <summary>
        /// Getter de la sous-famille de l'article.
        /// </summary>
        /// <returns> La sous-famille de l'article. </returns>
        public SousFamille Lire_Sous_Famille()
        {
            return Sous_Famille;
        }

        /// <summary>
        /// Setter de la sous-famille de l'article.
        /// </summary>
        /// <param name="Ss_Famille"> La sous-famille que l'on veut donner à l'article. </param>
        public void Modifier_Sous_Famille(SousFamille Ss_Famille)
        {
            Sous_Famille = Ss_Famille;
        }

        /// <summary>
        /// Getter de la marque de l'article.
        /// </summary>
        /// <returns> La marque de l'article. </returns>
        public Marque Lire_Marque()
        {
            return Marque;
        }

        /// <summary>
        /// Setter de la marque de l'article.
        /// </summary>
        /// <param name="Marq"> La marque que l'on veut donner à l'article. </param>
        public void Modifier_Marque(Marque Marq)
        {
            Marque = Marq;
        }

        /// <summary>
        /// Getter du prix HT de l'article.
        /// </summary>
        /// <returns> Le prix HT de l'article. </returns>
        public double Lire_PrixHT()
        {
            return PrixHT;
        }

        /// <summary>
        /// Setter du prix HT de l'article.
        /// </summary>
        /// <param name="Prix"> Le prix que l'on veut donner à l'article. </param>
        /// <exception cref="Exception"> Prix négatif. </exception>
        public void Modifier_PrixHT(double Prix)
        {
            // On vérifie que le prix est positif.
            if (Prix < 0D)
            {
                throw new Exception("Vous ne pouvez pas donner de prix négatif à un article.");
            }
            PrixHT = Prix;
        }

        /// <summary>
        /// Getter de la quantité de l'article.
        /// </summary>
        /// <returns> La quantité de l'article. </returns>
        public int Lire_Quantite()
        {
            return Quantite;
        }

        /// <summary>
        /// Setter de la quantité de l'article.
        /// </summary>
        /// <param name="Qte"> La quantité que l'on veut associer à l'article. </param>
        /// <exception cref="Exception"> Quantité négatif. </exception>
        public void Modifier_Quantite(int Qte)
        {
            // On vérifie que la quantité est positive.
            if (Qte < 0)
            {
                throw new Exception("Vous ne pouvez pas donner de quantité négatif à un article.");
            }
            Quantite = Qte;
        }
    }
}
