using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD
{
    /// <summary>
    /// Cette classe représente le joueur
    /// </summary>
    class Player : Entité
    {
        #region Constantes

        
        private const int COOLDOWNREGEN = 240;//Temps avant la régénération de la vie
        public const int TEMPSHIT = 25;//Temps entre 2 coups recus
        private const int TirTime = 10;//Temps durant lequel l'animation de tir est affichée
        private const int VITESSEREGEN = 5;
        public const int posInterfaceX = GameManager.TILEWIDTH * 7 / 2;//position x par rapport à la fenêtre
        public const int posInterfaceY = GameManager.TILEHEIGHT * 5 / 2;//position y par rapport à la fenêtre

        private const int COOLDOWNTIR_BASE = 30;//Temps entre les tirs de base
        private const int COOLDOWNTIR_UP = 15;//Temps entre les tirs avec l'atout "Damage"
        private const int PVBASE = 100;//Points de vie de base
        private const int PVUP = 200;//Points de vie avec l'atout "Health"
        private const int VITESSEBASE = 5;//Vitesse de déplacement de base
        private const int VITESSEUPGRADED = 8;//Vitesse de déplacement avec l'atout "Speed"

        public const int PRIXDAMAGE = 4000;//Prix d'achat de l'atout "Damage"
        public const int PRIXHEALTH = 6000;//Prix d'achat de l'atout "Health"
        public const int PRIXSPEED = 3000;//Prix d'achat de l'atout "Speed"
        public const int PRIXREVIVE = 4000;//Prix d'achat de l'atout "Revive"
        public const int PRIXUPGRADE = 10000;//Prix d'achat de l'amélioration

        
        #endregion

        #region Objets
        Balle balle;
        #endregion

        #region Autres objets
        GameManager gameManager;
        World world;
        Wall wall;
        #endregion

        #region Propriétés
        private int tirTimeOut;//Temps durant lequel l'animation de tir a été affichée
        private Atouts lesAtouts;//Atouts du joueur
        private Direction directionAffichée;//Direction visible
        private int coolDownTir;//Temps écoulé depuis le dernier Tir
        private bool tirPossible;//Pret à Tirer
        public int hitTime;//Temps depuis le dernier coup prit(Pour Tirer)
        private int regenWait;//Temps  depuis le dernier coup prit (Pour régénérer la vie)
        private int timeRegen;//Temps depuis le dernier point de vie rendu
        #endregion

        #region Méthodes
        /// <summary>
        /// Permet de déplacer le joueur
        /// </summary>
        /// <param name="xDirection">Direction dans laquelle le joueur doit se déplacer</param>
        public override void Déplacer(Direction xDirection)
        {
            if (etat != Etat.Mort && etat != Etat.MortUp)
            {
                directionAffichée = xDirection;
                Direction direction = xDirection;
                switch (xDirection)
                {
                    case Direction.Haut:
                        if (gameManager.IsMovePossible(positionX,positionY,vitesse,direction))
                        {
                            positionY += -vitesse;
                            //Temp:
                            //world.positionRealY -= -vitesse;
                        }
                        break;
                    case Direction.Gauche:
                        if (gameManager.IsMovePossible(positionX, positionY,vitesse, direction))
                        {
                            positionX += -vitesse;
                            //Temp:
                            //world.positionRealX -= -vitesse;
                        }
                        break;
                    case Direction.Bas:
                        if (gameManager.IsMovePossible(positionX, positionY,vitesse, direction))
                        {
                            positionY += vitesse;
                            //Temp:
                            //world.positionRealY -= vitesse;
                        }
                        break;
                    case Direction.Droite:
                        if (gameManager.IsMovePossible(positionX, positionY,vitesse, direction))
                        {
                            positionX += vitesse;
                            //Temp:
                            //world.positionRealX -= vitesse;
                        }
                        break;
            }
            
            }
        }
        /// <summary>
        /// Permet de tirer une balle
        /// </summary>
        /// <param name="xAngle">Direction du Tir</param>
        public void Tirer(Direction xAngle)
        {
            directionAffichée = xAngle;
            direction = xAngle;
            
            if (etat == Etat.Blessé || etat == Etat.Tir || etat == Etat.Vivant)
            {
                if (etat == Etat.Tir&&tirTimeOut>=TirTime)
                {
                    etat = Etat.Vivant;
                }
                else if (balle == null&&tirPossible)
                {
                    if (Atouts.Damage)
                        coolDownTir = COOLDOWNTIR_UP;
                    else
                        coolDownTir = COOLDOWNTIR_BASE;
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
        /// <summary>
        /// Gère le delai entre 2 tirs
        /// </summary>
        private void GestTir()
        {
            if (coolDownTir == 0)
            {
                tirPossible = true;    
            }
            else
            {
                coolDownTir--;
                tirPossible = false;
            }
            
        }
        /// <summary>
        /// Gère l'achat des atouts
        /// </summary>
        public void Acheter()
        {
            switch (gameManager.GameMap[(positionY / GameManager.TILEHEIGHT), (positionX / GameManager.TILEWIDTH)])
            {
                case TileType.Damage:
                    if (gameManager.points >= PRIXDAMAGE && !lesAtouts.Damage)
                    {
                        gameManager.points -= PRIXDAMAGE;
                        lesAtouts.Damage = true;
                    }
                    break;
                case TileType.Health:
                    if (gameManager.points >= PRIXHEALTH && !lesAtouts.Health)
                    {
                        gameManager.points -= PRIXHEALTH;
                        lesAtouts.Health = true;
                    }
                    break;
                case TileType.Revive:
                    if (gameManager.points >= PRIXREVIVE && !lesAtouts.Revive)
                    {
                        gameManager.points -= PRIXREVIVE;
                        lesAtouts.Revive = true;
                    }
                    break;
                case TileType.Speed:
                    if (gameManager.points >= PRIXSPEED && !lesAtouts.Speed)
                    {
                        gameManager.points -= PRIXSPEED;
                        lesAtouts.Speed = true;
                    }
                    break;
                case TileType.Upgrade:
                    if (gameManager.points >= PRIXUPGRADE &&
                        etat != Etat.BlesséUp &&
                        etat != Etat.MortUp &&
                        etat != Etat.TirUp &&
                        etat != Etat.VivantUp &&
                        wall.Open
                        )
                    {
                        etat = Etat.VivantUp;
                    }
                    break;
                default:
                    break;
            }
            //Gestion de la porte
            if(lesAtouts.Damage && lesAtouts.Health && lesAtouts.Revive && lesAtouts.Speed && !wall.Open)
            {
                wall.Open = true;
            }
        }
        /// <summary>
        /// Gère le rammassage des Bonus
        /// </summary>
        /// <param name="xType"></param>
        private void Ramasser(typeBonus xType)
        {

        }
        /// <summary>
        /// Gère l'action réalisée à la mort du joueur
        /// </summary>
        protected override void Mourir()
        {
            etat = Etat.Mort;
            if (lesAtouts.Revive)
            {
                pointsDeVie = 100;
                for (int indPoints = 0; indPoints < 1000; indPoints++)
                {
                    if (gameManager.points > 0)
                    {
                        gameManager.points -= 1;
                    }
                }
                lesAtouts.Damage = false;
                lesAtouts.Health = false;
                lesAtouts.Revive = false;
                lesAtouts.Speed = false;
                etat = Etat.Vivant;
            }
            else
            {
                gameManager.FinPartie();
            }
        }
        /// <summary>
        /// Gère la régénération et le temps avant d'être re-touché
        /// </summary>
        protected override void GestVie()
        {
            if (hitTime<TEMPSHIT)
            {
                hitTime++;
                if (etat == Etat.Vivant || etat == Etat.Tir)
                {
                    etat = Etat.Blessé;
                }
                else if (etat == Etat.VivantUp || etat == Etat.TirUp)
                {
                    etat = Etat.BlesséUp;
                }
                regenWait = COOLDOWNREGEN; 
            }
            if (hitTime == TEMPSHIT)
            {
                if (etat == Etat.Blessé)
                {
                    etat = Etat.Vivant;
                }
                else if (etat == Etat.BlesséUp)
                {
                    etat = Etat.VivantUp;
                }
            }
            if (regenWait == 0)
            {
                if (timeRegen == 0)
                {
                    timeRegen = VITESSEREGEN;
                    if (lesAtouts.Health&&pointsDeVie<PVUP)
                    {
                        pointsDeVie++;
                    }
                    else if (pointsDeVie < PVBASE)
                    {
                        pointsDeVie++;
                    }

                }
                timeRegen--;
            }
            if (regenWait > 0)
            {
                regenWait--;
            }
            
                
        }
        /// <summary>
        /// Vérifie si le personnage est vivant
        /// </summary>
        private void GestMort()
        {
            if (pointsDeVie <= 0)
            {
                Mourir();
            }
        }
        /// <summary>
        /// Gère la sprite à afficher en fonction des différents etats du joueur
        /// </summary>
        protected override void GestEtatVisible()
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
        /// <summary>
        /// Gère la vitesse du personnage (atout vitesse)
        /// </summary>
        private void GestVitesse()
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
        /// <summary>
        /// Désintancie la balle
        /// </summary>
        public void DespawnBalle()
        {
            balle = null;
        }
        /// <summary>
        /// Enlève la sprite de tir du joueur
        /// </summary>
        public void TirClear()
        {
            if (etat == Etat.Tir)
                etat = Etat.Vivant;
            if (etat == Etat.TirUp)
                etat = Etat.VivantUp;
        }
        /// <summary>
        /// Fonction Update
        /// </summary>
        public override void Update()
        {
            GestTir();
            //GestDirection();
            GestVitesse();
            GestEtatVisible();
            GestVie();
            GestMort();
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
                return balle;
            }
        }

        public int PointsDeVie
        {
            get { return pointsDeVie; }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de player
        /// </summary>
        /// <param name="totalFrames">total de sprites dans l'image de player</param>
        /// <param name="frameWidth">largeur d'une sprite</param>
        /// <param name="frameHeight">hauteur d'une sprite</param>
        /// <param name="xPosX">position x de départ</param>
        /// <param name="xPosY">position y de départ</param>
        /// <param name="xGameManager"></param>
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
            coolDownTir = 0;
            hitTime = TEMPSHIT;
            //transfers de variables
            gameManager = xGameManager;
            world = gameManager.getWorld;
            wall = gameManager.getWall;
            //
            positionX = xPosX;
            positionY = xPosY;
        }

        #endregion
    }

}
