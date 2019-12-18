using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD
{
    class Zombie:Entité
    {
        #region Constantes
        private const int VIEROUND = 100;//Vie ajoutée par manche
        private const int TEMPSHIT = 10;//Temps durant lequel le zombie n'est plus touchable
        private const int ZOMBIESPAWNDIST_MIN = 5;//Distance minimum d'apparition des zombies
        private const int ZOMBIESPAWNDIST_MAX = 11;//Distance maximum d'apparition des zombies
        private const int VITESSEBASE_MIN = 1;//Vitesse du zombie de base min
        private const int VITESSEBASE_MAX = 2;//Vitesse du zombie de base max
        private const int DISTANCE_ATTAQUE = 50;//Distance à laquelle le distance attaque
        private const int ATTAQUEZOMBIE = 60;//points de vie en moins par attaque
        #endregion

        #region Propriétés
        private int id;//Id du zombie
        private int hitTime;//Temps durant le quel le zombie est resté intouchable
        //TEST
        public int diffPosX;
        public int diffPosY;
        #endregion

        #region Autres objets
        GameManager gameManager;
        Player player;
        Balle balle;
        Random random;
        #endregion

        #region Méthodes
        /// <summary>
        /// Gère si le zombie peut attaquer le joueur 
        /// </summary>
        public void GestAttaquer()
        {
            //Valeur Absolue de distance entre le joueur et le zombie
            if (player.hitTime == Player.TEMPSHIT)
            {
                diffPosX = positionX - player.PositionX;
                if (diffPosX < 0)
                {
                    diffPosX = -diffPosX;
                }
                diffPosY = positionY - player.PositionY;
                if (diffPosY < 0)
                {
                    diffPosY = -diffPosY;
                }
                //HitRegister
                if (diffPosX < DISTANCE_ATTAQUE && diffPosY < DISTANCE_ATTAQUE)
                {
                    player.Touché(attaque);
                    player.hitTime = 0;
                }
            }
        }
        /// <summary>
        /// Gère les déplacements du zombie
        /// </summary>
        public void GestDéplacement()
        {
            int diffPosX = 0;
            int diffPosY = 0;

            diffPosX = player.PositionX - positionX;
            diffPosY = player.PositionY - positionY;
            if (diffPosX > 1)
            {
                Déplacer(Direction.Droite/*,(diffPosX*vitesse/diffPosY)*/);
            }
                
            if (diffPosX < -1)
            {
                Déplacer(Direction.Gauche/*,(diffPosY * vitesse / diffPosX)*/);
            }
                
            if (diffPosY > 1)
            {
                Déplacer(Direction.Bas/*, (diffPosY * vitesse / diffPosX)*/);
            }
               
            if (diffPosY < -1)
            {
                Déplacer(Direction.Haut/*, (diffPosX * vitesse / diffPosY)*/);
            }
                
        }
        /// <summary>
        /// Déplace le zombie sans collisions
        /// </summary>
        /// <param name="xDirection">Direction du déplacement</param>
        public override void Déplacer(Direction xDirection)
        {
            //Test
            int xVitesse = vitesse;
            //
            direction = xDirection;
            switch (xDirection)
            {
                case Direction.Haut:
                    if (gameManager.IsMovePossible(positionX, positionY, xVitesse, xDirection))
                    {
                        positionY += -xVitesse;
                    }
                    break;
                case Direction.Gauche:
                    if (gameManager.IsMovePossible(positionX, positionY, xVitesse, xDirection))
                    {
                        positionX += -xVitesse;
                    }
                    break;
                case Direction.Bas:
                    if (gameManager.IsMovePossible(positionX, positionY, xVitesse, xDirection))
                    {
                        positionY += xVitesse;
                    }
                    break;
                case Direction.Droite:
                    if (gameManager.IsMovePossible(positionX, positionY, xVitesse, xDirection))
                    {
                        positionX += xVitesse;
                    }
                    break;
            }
        }
        /// <summary>
        /// Gère quelle image le zombie doit afficher
        /// </summary>
        protected override void GestEtatVisible()
        {
            int directionInt=0;
            int etatInt=0;

            switch (direction)
            {
                case Direction.Haut:    directionInt = 0;   break;
                case Direction.Droite:  directionInt = 5;   break;
                case Direction.Bas:     directionInt = 10;   break;
                case Direction.Gauche:  directionInt = 15;   break;
            }
            switch (etat)
            {
                case Etat.Vivant:   etatInt = 0;    break;
                case Etat.Blessé:   etatInt = 1;    break;
                case Etat.BlesséUp: etatInt = 2;    break;
                case Etat.Mort:     etatInt = 3;    break;
                case Etat.MortUp:   etatInt = 4;    break;
            }

            frameIndex = directionInt + etatInt;
        }
        /// <summary>
        /// Gère si le zombie est touché par la balle
        /// </summary>
        private void GestHit()
        {
            if (hitTime == TEMPSHIT)
            {
                //Centre Sprite
                
                //
                etat = Etat.Vivant;
                if (
                    balle!=null
                    &&
                    balle.PositionX > positionX - DIFFX &&
                    balle.PositionX < positionX + DIFFX
                    &&
                    balle.PositionY > positionY - DIFFY &&
                    balle.PositionY < positionY + DIFFY
                    )
                {
                    if (balle.Type == Type.Basic)
                        etat = Etat.Blessé;
                    else if (balle.Type == Type.Upgraded)
                        etat = Etat.BlesséUp;
                    Touché(balle.Dégats);
                    hitTime = 0;
                    gameManager.points += GameManager.POINTSTIR;
                    
                    player.DespawnBalle();
                }
            }
            if (hitTime < TEMPSHIT&&etat!=Etat.Mort&&Etat!=Etat.MortUp)
            {
                hitTime++;
            }
        }
        /// <summary>
        /// Fait mourir le zombie
        /// </summary>
        protected override void Mourir()
        {
            bool upgraded = false;
            if (balle != null && balle.Type == Type.Upgraded)
                upgraded = true;
            if ( upgraded == true )
                etat = Etat.MortUp;
            else
                etat = Etat.Mort;
            gameManager.nombreZombies--;
            gameManager.points += GameManager.POINTSMORT;
            
        }
        /// <summary>
        /// Gère le nombre de points de vie du zombie ainsi que le moment de sa mort
        /// </summary>
        protected override void GestVie()
        {
            if (pointsDeVie <= 0 && etat!=Etat.Mort && etat!=Etat.Invisible && etat!=Etat.MortUp)
            {
                //throw new Exception("José");
                Mourir();
            }
        }
        /// <summary>
        /// Gère l'apparition du zombie (Points de vie,position,etc.)
        /// </summary>
        public void Spawn()
        {
            //Initialisation
            int indError = 0;
            int randX = 0;
            int randY = 0;
            int randDirX = 0;
            int randDirY = 0;
            do
            {
                indError++;
                if (indError > 10000)
                {
                    //throw new Exception("Boucle Fonction Bug");
                    return;
                }
                    
                //Determination direction
                randDirX = random.Next(0, 2);
                randDirY = random.Next(0, 2);
                //Determination de la distance
                randX = random.Next(ZOMBIESPAWNDIST_MIN, ZOMBIESPAWNDIST_MAX);
                randY = random.Next(ZOMBIESPAWNDIST_MIN, ZOMBIESPAWNDIST_MAX);
                //Determination Position
                if (randDirX == 1)
                    randX = -randX;
                if (randDirY == 1)
                    randY = -randY;

                //translates to player position
                randX = randX+(player.PositionX/GameManager.TILEWIDTH);
                randY = randY+(player.PositionY/GameManager.TILEHEIGHT);
            } while (gameManager.IsSpawnable(randX, randY)==false);
            //Determination Position réelle
            positionX = randX * GameManager.TILEWIDTH;
            positionY = randY * GameManager.TILEHEIGHT;
            gameManager.zombiesRound++;
            gameManager.nombreZombies++;
            etat = Etat.Vivant;
            pointsDeVie = VIEROUND * gameManager.Round;
            
        }
        /// <summary>
        /// Fonction Update du zombie
        /// </summary>
        public override void Update()
        {
            //Load
            balle = player.getBalle;
            //Si Vivant
            if (etat != Etat.Invisible && Etat != Etat.Mort && etat != Etat.MortUp)
            {
                GestHit();
                GestAttaquer();
                GestDéplacement();
            }
            //
            GestVie();
            GestEtatVisible();
            UpdateFrame();
        }
        #endregion

        #region Accesseurs
        /// <summary>
        /// Accesseur de l'etat
        /// </summary>
        public Etat Etat
        {
            get { return etat; }
            set { etat = value; }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur du Zombie
        /// </summary>
        /// <param name="xGameManager">référence de gameManager</param>
        /// <param name="xId">Id du zombie (dans zombies[id])</param>
        /// <param name="xTotalFrames">Total de frames dans l'image du zombie</param>
        /// <param name="xFrameWidth">Largeur d'une frame</param>
        /// <param name="xFrameHeight">Hauteur d'une frame</param>
        public Zombie(GameManager xGameManager,int xId,int xTotalFrames,int xFrameWidth,int xFrameHeight):base(xTotalFrames,xFrameWidth,xFrameHeight)
        {
            //getObjects
            gameManager = xGameManager;
            player = gameManager.getPlayer;
            random = gameManager.getRandom;
            id = xId;
            //
            attaque = ATTAQUEZOMBIE;
            etat = Etat.Invisible;
            hitTime = TEMPSHIT;
            positionX = 100;
            positionY = 100;
            //Vitesse
            vitesse = random.Next(VITESSEBASE_MIN, VITESSEBASE_MAX + 1);
        }
        #endregion
    }
}
