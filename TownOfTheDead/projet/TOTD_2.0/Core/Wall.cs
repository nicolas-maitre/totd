using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD
{
    /// <summary>
    /// Cette classe représente le mur ouvrable pour accéder à l'arme améliorée
    /// </summary>
    class Wall:GameObject
    {
        private int positionX;//position x
        private int positionY;//position y
        public bool Open;//Mur ouvert?

        public int PositionX
        {
            get { return positionX; }
        }
        public int PositionY
        {
            get { return positionY; }
        }
        /// <summary>
        /// Constructeur de Wall
        /// </summary>
        public Wall()
        {
            positionX = 29 * GameManager.TILEWIDTH + 2;
            positionY = 20 * GameManager.TILEHEIGHT + 8;
        }
    }
}
