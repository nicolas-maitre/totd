using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD
{
    /// <summary>
    /// Cette classe représente la balle tirée
    /// </summary>
    class Balle:GameObject
    {
        #region Autre objets
        GameManager gameManager;
        Player player;
        #endregion
        #region Propriétés
        private int positionX;//position x
        private int positionY;//position y
        private Direction direction;//direction
        private Type type;//type de la balle(normal,amélioré)
        private int distance;//Distance parcourue par la balle
        private int vitesse;//vitesse de la balle
        private int degats;//Dégats infligés au zombie
        #endregion
        #region Constantes
        private const int VITESSEBASE = 20;//Vitesse de base
        private const int VITESSEDOUBLE = 40;//Vitesse si atout "Damage"
        private const int DEGATSBASE = 100;//Dégats de base
        private const int DEGATSUPGRADED = 500;//Dégats si arme améliorée
        private const int DISTANCEMAX = ((GameManager.TILEHEIGHT + GameManager.TILEWIDTH) / 2) * 6;//Distance maximum de la balle avant de disparaitre
        #endregion
        #region Méthodes
        /// <summary>
        /// Déplace la balle
        /// </summary>
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
        /// <summary>
        /// Gère si la balle a atteint la distance maximum
        /// </summary>
        public void GestTimeout()
        {
            distance += vitesse;
            if (distance == DISTANCEMAX)
            {
                player.DespawnBalle();
            }
        }
        /// <summary>
        /// Gère la sprite affichée par la balle (Content\SpriteZombie.png)
        /// </summary>
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
        /// <summary>
        /// Methode Update de la balle
        /// </summary>
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
        /// <summary>
        /// Constructeur de la balle
        /// </summary>
        /// <param name="totalFrames">nombre de frames dans la sprite</param>
        /// <param name="frameWidth">largeur d'une frame</param>
        /// <param name="frameHeight">hauteur d'une frame</param>
        /// <param name="xPositionX">position x de départ</param>
        /// <param name="xPositionY">position y de départ</param>
        /// <param name="xDirection">direction de la balle</param>
        /// <param name="xType">type de la balle (normale, améliorée)</param>
        /// <param name="xGameManager">référence du gameManager</param>
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
