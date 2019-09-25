using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD.Core
{
    class Entité:GameObject
    {
        #region Constantes
        //Différence position /centre
        public const int DIFFX = 50;
        public const int DIFFY = 50;
        #endregion

        #region Propriétés
        protected int positionX;
        protected int positionY;
        public int vitesse;
        protected int pointsDeVie;
        protected int vieMax;
        protected int attaque;
        protected Etat etat;
        protected Direction direction;
        #endregion

        #region Méthodes
        public void Touché(int xNbDégats)
        {
            pointsDeVie -= xNbDégats;
        }
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
