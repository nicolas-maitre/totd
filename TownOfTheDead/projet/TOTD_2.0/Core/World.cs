using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD
{
    /// <summary>
    /// Cette carte représente la carte de jeu (Visible)
    /// </summary>
    class World:GameObject
    {
        #region Autres objets
        Player player;
        GameManager gameManager;
        GameWin gameWindow;
        #endregion
        #region Propriétés
        private int positionRealX;//position réelle x
        private int positionRealY;//position réelle y
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
        /// <summary>
        /// Constructeur du World
        /// </summary>
        /// <param name="xGameManager">référence de GameManager</param>
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
        /// <summary>
        /// Gère la position du monde par rapport à la fenêtre
        /// </summary>
        public void GestCentrWindow()
        {
            positionRealX = -(gameWindow.PositionX);
            positionRealY = -(gameWindow.PositionY);
        }
        /// <summary>
        /// Fonction Update
        /// </summary>
        public void Update()
        {
            GestCentrWindow();
        }
        #endregion
    }
}
