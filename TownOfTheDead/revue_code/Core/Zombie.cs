using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD.Core
{
    class Zombie:Entité
    {
        #region Constantes
        private const int VIEROUND = 100;//Vie ajoutée par manche
        private const int TEMPSHIT = 25;//Temps durant lequel le zombie n'est plus touchable
        private const int ZOMBIESPAWNDIST_MIN = 5;//Distance minimum d'apparition des zombies
        private const int ZOMBIESPAWNDIST_MAX = 11;//Distance maximum d'apparition des zombies
        private const int VITESSEBASE = 2;
        #endregion

        #region Propriétés
        private int id;//Id du zombie
        private int hitTime;//Temps durant le quel le zombie est resté intouchable
        #endregion

        #region Autres objets
        GameManager gameManager;
        Player player;
        Balle balle;
        Random random;
        #endregion

        #region Méthodes
        public void GestAttaquer()
        {
            
        }
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
        private void Déplacer(Direction xDirection/*,int xVitesse*/)
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
        private void GestEtatVisible()
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
                    
                    player.DespawnBalle();
                }
            }
            if (hitTime < TEMPSHIT&&etat!=Etat.Mort&&Etat!=Etat.MortUp)
            {
                hitTime++;
            }
        }
        private void Mourir()
        {
            bool Upgraded = false;
            if (balle != null && balle.Type == Type.Upgraded)
                Upgraded = true;
            if ( Upgraded == true )
                etat = Etat.MortUp;
            else
                etat = Etat.Mort;
            gameManager.nombreZombies--;
            
        }
        private void GestPointsDeVie()
        {
            if (pointsDeVie <= 0 && etat!=Etat.Mort && etat!=Etat.Invisible && etat!=Etat.MortUp)
            {
                //throw new Exception("José");
                Mourir();
            }
        }
        public void Spawn_Old()
        {
            int randPosX=0;
            int randPosY=0;
            int randY=0;
            int randX=0;
            do
            {
                //Randomistation
                randPosX = random.Next(ZOMBIESPAWNDIST_MIN, ZOMBIESPAWNDIST_MAX);
                randPosY = random.Next(ZOMBIESPAWNDIST_MIN, ZOMBIESPAWNDIST_MAX);
                randX = random.Next(0, 1);
                randY = random.Next(0, 1);
                //Determination de la direction
                //X
                if (randX == 1)
                    randPosX = (player.PositionX / GameManager.TILEWIDTH) - randPosX;
                else
                    randPosX = (player.PositionX / GameManager.TILEWIDTH) + randPosX;
                //Y
                if (randPosY == 1)
                    randPosY = (player.PositionY / GameManager.TILEHEIGHT)- randPosY;
                else
                    randPosY = (player.PositionY / GameManager.TILEHEIGHT)+ randPosY;
            }
            while (gameManager.IsWalkable(randPosX, randPosY));
            positionX = (randPosX*GameManager.TILEWIDTH);
            positionY = (randPosY*GameManager.TILEHEIGHT);
            etat = Etat.Vivant;
        }
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

                randX = randX+(player.PositionX/GameManager.TILEWIDTH);
                randY = randY+(player.PositionY/GameManager.TILEHEIGHT);
            } while (gameManager.IsWalkable(randX, randY)==false);
            //Determination Position réelle
            positionX = randX * GameManager.TILEWIDTH;
            positionY = randY * GameManager.TILEHEIGHT;
            gameManager.zombiesRound++;
            gameManager.nombreZombies++;
            etat = Etat.Vivant;
            pointsDeVie = VIEROUND * gameManager.Round;
            
        }
        //Update
        public void Update()
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
            GestPointsDeVie();
            GestEtatVisible();
            UpdateFrame();
        }
        #endregion

        #region Accesseurs
        public Etat Etat
        {
            get { return etat; }
        }
        #endregion

        #region Constructeur
        public Zombie(GameManager xGameManager,int xId,int xTotalFrames,int xFrameWidth,int xFrameHeight):base(xTotalFrames,xFrameWidth,xFrameHeight)
        {
            //getObjects
            gameManager = xGameManager;
            player = gameManager.getPlayer;
            random = gameManager.getRandom;
            id = xId;
            //
            etat = Etat.Invisible;
            hitTime = TEMPSHIT;
            positionX = 100;
            positionY = 100;
            vitesse = VITESSEBASE;
        }
        #endregion
    }
}
