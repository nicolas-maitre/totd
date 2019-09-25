using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD
{
    /// <summary>
    /// Cette classe représente la fenetre de jeu
    /// </summary>
    class GameWin
    {
        #region Autres objets
        GameManager gameManager;//référence du gameManger
        Player player;//Référence du Player
        #endregion

        #region Propriétés
        private int positionX;//position x
        private int positionY;//position y
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
        /// <summary>
        /// Gère la position de la Fenêtre par rapport au joueur
        /// </summary>
        public void GestPosPlayer()
        {
            positionX = player.PositionX - Player.posInterfaceX;
            positionY = player.PositionY - Player.posInterfaceY;
        }
        /// <summary>
        /// Fonction Update
        /// </summary>
        public void Update()
        {
            GestPosPlayer();
        }
        #endregion
    }
}
