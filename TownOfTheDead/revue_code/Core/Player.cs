using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD.Core
{
    class Player : Entité
    {
        #region Constantes
        public const int VITESSEBASE = 5;
        public const int VITESSEUPGRADED=8;
        private const int PVBASE=100;
        private const int PVUP = 200;
        public const int posInterfaceX = GameManager.TILEWIDTH * 7 / 2;
        public const int posInterfaceY = GameManager.TILEHEIGHT * 5 / 2;
        #endregion

        #region Objets
        Balle balle;
        #endregion

        #region Autres objets
        GameManager gameManager;
        World world;
        #endregion

        #region Propriétés
        private const int TirTime=10;
        private int tirTimeOut;
        private Atouts lesAtouts;
        private Direction directionAffichée;
        private Direction directionDéplacement;
        #endregion

        #region Méthodes
		//Revue JPM: Enlever code mort.
        public void Déplacer(Direction xDirection)
        {
            directionDéplacement = xDirection;
            switch (xDirection)
            {
                case Direction.Haut:
                    if (gameManager.IsMovePossible(positionX,positionY,vitesse,directionDéplacement))
                    {
                        positionY += -vitesse;
                        //Temp:
                        //world.positionRealY -= -vitesse;
                    }
                    break;
                case Direction.Gauche:
                    if (gameManager.IsMovePossible(positionX, positionY,vitesse, directionDéplacement))
                    {
                        positionX += -vitesse;
                        //Temp:
                        //world.positionRealX -= -vitesse;
                    }
                    break;
                case Direction.Bas:
                    if (gameManager.IsMovePossible(positionX, positionY,vitesse, directionDéplacement))
                    {
                        positionY += vitesse;
                        //Temp:
                        //world.positionRealY -= vitesse;
                    }
                    break;
                case Direction.Droite:
                    if (gameManager.IsMovePossible(positionX, positionY,vitesse, directionDéplacement))
                    {
                        positionX += vitesse;
                        //Temp:
                        //world.positionRealX -= vitesse;
                    }
                    break;
            }
        }
        private void GestDirection()
        {
            if (balle != null /*&& balle.Distance < 300*/)
            {
                directionAffichée = balle.Direction;
            }
            else
            {
                directionAffichée = directionDéplacement;
            }
        }
        public void Tirer(Direction xAngle)
        {
            direction = xAngle;
            
            if (etat == Etat.Blessé || etat == Etat.Tir || etat == Etat.Vivant)
            {
                if (etat == Etat.Tir&&tirTimeOut>=TirTime)
                {
                    etat = Etat.Vivant;
                }
                else if (balle == null)
                {
                    balle = new Balle(8,100,100,positionX,positionY,direction, Type.Basic,gameManager);
                    etat = Etat.Tir;
                    tirTimeOut = 0;
                }
                tirTimeOut++;
            }
            else if (etat == Etat.BlesséUp || etat == Etat.TirUp || etat == Etat.VivantUp)
            {
                if (etat == Etat.TirUp && tirTimeOut >= TirTime)
                {
                    etat = Etat.VivantUp;
                }
                else if (balle == null)
                {
                    balle = new Balle(8,100,100, positionX, positionY, direction, Type.Upgraded,gameManager);
                    etat = Etat.TirUp;
                    tirTimeOut = 0;
                }
                tirTimeOut++;
            }
            
        }
        public void Acheter()
        {

        }
        public void Ramasser(typeBonus xType)
        {

        }
        public void GestSoin()
        {

        }
        private void Mourir()
        {
            etat = Etat.Mort;
            gameManager.nombreZombies--;
        }
        private void GestPointsDeVie()
        {
            if (pointsDeVie <= 0)
            {
                Mourir();
            }
        }
        private void GestEtatVisible()
        {
            //variables
            int directionInt=0;
            int etatInt = 0;
            //translation
            switch (etat)
            {
                case Etat.Vivant:   etatInt = 0;    break;
                case Etat.Tir:      etatInt = 1;    break;
                case Etat.Blessé:   etatInt = 2;    break;
                case Etat.VivantUp: etatInt = 3;    break;
                case Etat.TirUp:    etatInt = 4;    break;
                case Etat.BlesséUp: etatInt = 5;    break;
                case Etat.Mort:     etatInt = 6;    break;    
            }
            switch (directionAffichée)
            {
                case Direction.Haut:    directionInt = 0;   break;
                case Direction.Droite:  directionInt = 7;   break;
                case Direction.Bas:     directionInt = 14;   break;
                case Direction.Gauche:  directionInt = 21;   break;
            }

            frameIndex = directionInt + etatInt;
        }
        public void GestVitesse()
        {
            if (lesAtouts.Speed)
            {
                vitesse = VITESSEUPGRADED;
            }
            else
            {
                vitesse = VITESSEBASE;
            }
        }
        public void DespawnBalle()
        {
            balle = null;
        }
        public void TirClear()
        {
            if (etat == Etat.Tir)
                etat = Etat.Vivant;
            if (etat == Etat.TirUp)
                etat = Etat.VivantUp;
        }
        //Update
        public void Update()
        {
            GestDirection();
            GestVitesse();
            GestEtatVisible();
            GestSoin();
            GestPointsDeVie();
            UpdateFrame();
            //Balle
            if (balle != null)
            {
                balle.Update();  
            }
        }
        #endregion

        #region Accesseurs
        public Atouts Atouts
        {
            get { return lesAtouts; }
        }

        public Balle getBalle
        {
            get
            {
                //if (balle != null)
                return balle;
            }
        }
        #endregion

        #region Constructeur
        public Player(int totalFrames, int frameWidth, int frameHeight,int xPosX,int xPosY, GameManager xGameManager)
        : base(totalFrames, frameWidth, frameHeight)
        {
            //default
            pointsDeVie = PVBASE;
            etat = Etat.Vivant;
            lesAtouts.Damage = false;
            lesAtouts.Health = false;
            lesAtouts.Revive = false;
            lesAtouts.Speed = false;
            vitesse = VITESSEBASE;
            //transfers de variables
            gameManager = xGameManager;
            world = gameManager.getWorld;
            //
            positionX = xPosX;
            positionY = xPosY;
        }

        #endregion
    }

}
