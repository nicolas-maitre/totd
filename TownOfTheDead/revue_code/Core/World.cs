using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD.Core
{
    class World:GameObject
    {
        #region Autres objets
        Player player;
        GameManager gameManager;
        GameWin gameWindow;
        #endregion
        #region Propriétés
        private int positionRealX;
        private int positionRealY;
        #endregion
        #region Accesseurs
        public int PositionRealX
        {
            get { return positionRealX; }
        }
        public int PositionRealY
        {
            get { return positionRealY; }
        }
        #endregion
        #region Constructeur
        public World(GameManager xGameManager)
        {
            //positionRealX = 0;
            //positionRealY = 0;
            //Transfers d'objets
            gameManager = xGameManager;
            player = gameManager.getPlayer;
            gameWindow = gameManager.getWindow;
        }
        #endregion
        #region Methodes
        public void GestCentrWindow()
        {
            positionRealX = -(gameWindow.PositionX);
            positionRealY = -(gameWindow.PositionY);
        }
        public void Update()
        {
            GestCentrWindow();
        }
        #endregion
    }
}
