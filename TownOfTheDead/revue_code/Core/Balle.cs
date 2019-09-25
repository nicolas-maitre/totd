using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD.Core
{
    class Balle:GameObject
    {
        #region Autre objets
        GameManager gameManager;
        Player player;
        #endregion
        #region Propriétés
        private int positionX;
        private int positionY;
        private Direction direction;
        private Type type;
        private int distance;
        private int vitesse;
        private int degats;
        #endregion
        #region Constantes
        private const int VITESSEBASE = 20;
        private const int VITESSEDOUBLE = 40;
        private const int DEGATSBASE = 50;
        private const int DEGATSUPGRADED = 200;
        private const int DISTANCEMAX = ((GameManager.TILEHEIGHT + GameManager.TILEWIDTH) / 2) * 6;
        #endregion
        #region Méthodes
        public void Déplacer()
        {
            switch (direction)
            {
                case Direction.Haut:
                    if(gameManager.IsMovePossible(positionX,positionY,vitesse,direction))
                        positionY -= vitesse;
                    else
                        player.DespawnBalle();
                    break;
                case Direction.Droite:
                    if (gameManager.IsMovePossible(positionX, positionY, vitesse, direction))
                        positionX += vitesse;
                    else
                        player.DespawnBalle();
                    break;
                case Direction.Bas:
                    if (gameManager.IsMovePossible(positionX, positionY, vitesse, direction))
                        positionY += vitesse;
                    else
                        player.DespawnBalle();
                    break;
                case Direction.Gauche:
                    if (gameManager.IsMovePossible(positionX, positionY, vitesse, direction))
                        positionX -= vitesse;
                    else
                        player.DespawnBalle();
                    break;
            }
        }
        public void GestTimeout()
        {
            distance += vitesse;
            if (distance == DISTANCEMAX)
            {
                player.DespawnBalle();
            }
        }
        public void GestEtatVisible()
        {
            int directionInt=0;
            int typeInt=0;

            switch (direction)
            {
                case Direction.Haut:
                    directionInt = 0;
                    break;
                case Direction.Droite:
                    directionInt = 2;
                    break;
                case Direction.Bas:
                    directionInt = 4;
                    break;
                case Direction.Gauche:
                    directionInt = 6;
                    break;
            }
            switch (type)
            {
                case Type.Basic:
                    typeInt = 0;
                    break;
                case Type.Upgraded:
                    typeInt = 1;
                    break;
            }
            frameIndex=directionInt+typeInt;
        }
        public void Update()
        {
            Déplacer();
            UpdateFrame();
            GestTimeout();
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
        public int Distance
        {
            get { return distance; }
        }
        public Direction Direction
        {
            get { return direction; }
        }
        public int Dégats
        {
            get { return degats; }
        }
        public Type Type
        {
            get { return type; }
        }
        #endregion
        #region Constructeur
        public Balle(int totalFrames, int frameWidth, int frameHeight,int xPositionX,int xPositionY,Direction xDirection,Type xType, GameManager xGameManager)
        : base(totalFrames, frameWidth, frameHeight)
        {
            //transfers de variables
            gameManager = xGameManager;
            player = gameManager.getPlayer;
            type = xType;
            direction = xDirection;
            positionX = xPositionX;
            positionY = xPositionY;
            //
            GestEtatVisible();
            distance = 0;
            #region dégats
            if (type == Type.Basic)
                degats = DEGATSBASE;
            else
                degats = DEGATSUPGRADED;
            #endregion
            #region vitesse
            if (player.Atouts.Damage)
                vitesse = VITESSEDOUBLE;
            else
                vitesse = VITESSEBASE;
            #endregion
            
        }
        #endregion
    }
}
