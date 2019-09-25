using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD.Core
{
    class GameWin
    {
        #region Autres objets
        GameManager gameManager;
        Player player;
        #endregion

        #region Propriétés
        private int positionX;
        private int positionY;
        #endregion

        #region Constructeur
        public GameWin(GameManager xGameManager)
        {
            //Initialisation
            positionX = 0;
            positionY = 0;
            //Transfers d'objets
            gameManager = xGameManager;
            player = gameManager.getPlayer;
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

        #region Methodes
        public void GestPosPlayer()
        {
            positionX = player.PositionX - Player.posInterfaceX;
            positionY = player.PositionY - Player.posInterfaceY;
        }
        public void Update()
        {
            GestPosPlayer();
        }
        #endregion
    }
}
