using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD
{
    /// <summary>
    /// Cette classe est la classe mère de player et de zombies
    /// </summary>
    abstract class Entité:GameObject
    {
        #region Constantes
        //Différence position de la sprite par rapport au centre de la sprite
        public const int DIFFX = 50;
        public const int DIFFY = 50;
        #endregion

        #region Propriétés
        protected int positionX;//position X
        protected int positionY;//position Y
        public int vitesse;//vitesse de l'entité
        protected int pointsDeVie;//Points de vie de l'entité
        protected int attaque;//points d'attaque de l'entité
        protected Etat etat;//etat de l'entité
        protected Direction direction;
        #endregion

        #region Méthodes
        /// <summary>
        /// lorque l'entité est touchée, Lui enlève un nombre de points de vie
        /// </summary>
        /// <param name="xNbDégats">Nombre de points de vie à enlever</param>
        public void Touché(int xNbDégats)
        {
            if (pointsDeVie > 0)
            {
                pointsDeVie -= xNbDégats;
            }
            
        }
        /// <summary>
        /// Déplace l'entité dans la direction souhaitée
        /// </summary>
        /// <param name="xDirection">Direction</param>
        public abstract void Déplacer(Direction xDirection);
        /// <summary>
        /// Gère la sprite affichée
        /// </summary>
        protected abstract void GestEtatVisible();
        /// <summary>
        /// Gère l'action à réaliser à la mort de l'entité
        /// </summary>
        protected abstract void Mourir();
        /// <summary>
        /// Gère le nombre de points de vie de l'entité
        /// </summary>
        protected abstract void GestVie();
        /// <summary>
        /// Fonction Update
        /// </summary>
        public abstract void Update();
        #endregion

        #region Accesseurs
        public int PositionX
        {
            get { return positionX; }
        }
        public int PositionY
        {
            get { return positionY; }
        }
        #endregion

        #region Constructeurs
        public Entité(int totalFrames, int frameWidth, int frameHeight)
        : base(totalFrames, frameWidth, frameHeight)
        {

        }
        public Entité() { }

        #endregion
    }
}
