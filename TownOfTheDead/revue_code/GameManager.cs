﻿//Revue JPM: Manque en tête de fichier.
//Revue JPM: Manque de commentaires
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TOTD.Core;
namespace TOTD
{
    #region Types Perso
	//Revue JPM: Commenter types
    enum Direction { Haut,Gauche,Bas,Droite,Fixe}
    enum typeBonus { Pt500,Bombe,DoublePt,Insta,Shield}
    enum Type { Basic,Upgraded}
    enum Etat { Vivant,Blessé,Mort,Tir,VivantUp,BlesséUp,MortUp,TirUp,Invisible}
    enum GameState { EnCours,Pause,Arreté,EntreRound}
    enum TileType { Walkable,Wall,Door,Revive,Health,Speed,Damage,Upgrade}
    struct Atouts
    {
        public bool Revive;
        public bool Health;
        public bool Speed;
        public bool Damage;
    }
    #endregion Types
    class GameManager
    {
        #region Constantes
        public const int MAXZOMBIES = 24;//Nombre de zombies max durant la manche
        private const int TEMPSZOMBIE=60;
        public const int TEMPSROUNDS = 360;//intervalle entre 2 rounds
        public const int TILEHEIGHT = 100;//Hauteur d'une tuile
        public const int TILEWIDTH = 100;//Largeur d'une tuile
        public const double LIMITETHUMBSTICK = 0.5;//Valeur à partir de laquelle le joystick détecte le mouvement
        private const int MAPHEIGHT = 35;//Hauteur de la carte
        private const int MAPWIDTH = 35;//Largeur de la carte
        private const int POS_X_DEPARTPLAYER = 15 * TILEWIDTH;//Position X de départ du joueur
        private const int POS_Y_DEPARTPLAYER = 21 * TILEHEIGHT;//Position Y de départ du joueur
        #endregion

        #region Déclaration objets 
        GameWin gameWindow; //Fenetre de jeu
        World world;//Carte de jeu
        Zombie[] zombies;
        Player player;
        TileType[,] map;//Carte physique de jeu
        Wall wall;//Mur Amélioration
        TOTD TOTD;
        Random random;
        #endregion

        #region Autres Variables
        public bool nWasPressed;//flag permettant d'activer le noclip
        public int nombreZombies;//nombre de zombies vivants sur la carte
        public int timerRound;//compteur du temps entre rounds
        private int timerZombieRound;//compteur du temps entre l'apparition des zombies
        public int zombiesRound;//nombre de zombies apparus durant la manche//PRIVATE
        #endregion Autres Variables

        #region Propriétés
        private bool noclip;
        private int round;
        private GameState gameState;
        #endregion Propriétés

        #region Methodes
        private void LoadMap()//charge la map dans sa variable depuis un fichier
        {
            //déclaration
            string[] Text;
            string mapText="";
            map = new TileType[MAPHEIGHT, MAPWIDTH];
            //Transfers du fichier dans une variable
            Text = System.IO.File.ReadAllLines(@"Code.map");
            //Ligne par ligne
            for(int indString = 0; indString < MAPHEIGHT; indString++)
            {
                mapText += Text[indString];
            }
            //Remplissage du tableau à partir de la variable
            TileType tileType=TileType.Wall;
            for(int indLigne = 0; indLigne < MAPHEIGHT; indLigne++)
            {
                for(int indColonne = 0; indColonne < MAPWIDTH; indColonne++)
                {
					//Revue JPM: Manque default:
                    switch(mapText[indLigne * MAPWIDTH + indColonne])
                    {
                        case '0':   tileType = TileType.Walkable;   break;
                        case '1':   tileType = TileType.Wall;       break;
                        case '2':   tileType = TileType.Health;     break;
                        case '3':   tileType = TileType.Revive;     break;
                        case '4':   tileType = TileType.Damage;     break;
                        case '5':   tileType = TileType.Speed;      break;
                        case '6':   tileType = TileType.Upgrade;    break;
                        case '7':   tileType = TileType.Wall;       break;
                    }

                    map[indLigne, indColonne] = tileType;
                }
            }
        }
        public void Démarrer()//Démarrer la partie
        {
            DébutRound();
            round = 1;
        }
        public void Pause() //Mettre la partie en pause
        {
            gameState = GameState.Pause;
        }
        public void Quitter()//Quitter le jeu
        {
            gameState = GameState.Arreté;
            TOTD.Exit();
        }
        private void FinRound()//Terminer la manche en cours
        {
            gameState = GameState.EntreRound;
            timerRound = TEMPSROUNDS;
            zombiesRound = 0;
        }
        private void DébutRound()//Commencer une nouvelle manche
        {
            zombiesRound = 0;
            nombreZombies = 0;
            timerZombieRound = 0;
            round++;
            gameState = GameState.EnCours;
        }
        private void GestZombiesSpawn()//Gère l'apparition des zombies
        {
            if (gameState == GameState.EnCours)
            {
                if (zombiesRound < MAXZOMBIES)
                {
                    if (timerZombieRound >= TEMPSZOMBIE)
                    {
                        zombies[zombiesRound].Spawn();
                        timerZombieRound = 0;
                    }
                    timerZombieRound++;
                }
            }
        }
        private void GestRound()//Gère le changement de rounds
        {
			//Revue JPM: Tjs entouré les blocs de code d'accolades -> {}
			if (nombreZombies == 0 && zombiesRound == MAXZOMBIES)
                FinRound();
            if (gameState == GameState.EntreRound)
            {
                DébutRound();
            }
        }
        public void GestEntrée()//Gestion des Entrées
        {
        #region WASD
            bool dirD=false;    bool dirG = false;  bool dirH = false;  bool dirB = false;

            if (Keyboard.GetState().IsKeyDown(Keys.D)|| GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X>LIMITETHUMBSTICK)
                dirD = true;

            if (Keyboard.GetState().IsKeyDown(Keys.A)|| GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X<-LIMITETHUMBSTICK)
                dirG = true;

            if (Keyboard.GetState().IsKeyDown(Keys.S) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y <- LIMITETHUMBSTICK)
                dirB = true;

            if (Keyboard.GetState().IsKeyDown(Keys.W) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > LIMITETHUMBSTICK)
                dirH = true;

            //Gest vitesse player diagonale
#if (true)
            if ((dirH && dirD) || (dirH && dirG) || (dirB && dirG) || (dirB && dirD))
            {
                player.vitesse *= 4;
                player.vitesse /= 5;
            }          
#endif

            //Déplacement
            
            if (dirD) player.Déplacer(Direction.Droite);
            if (dirG) player.Déplacer(Direction.Gauche);
            if (dirB) player.Déplacer(Direction.Bas);
            if (dirH) player.Déplacer(Direction.Haut);
            #endregion

        #region Flèches
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y > LIMITETHUMBSTICK)
                player.Tirer(Direction.Haut);
            if (Keyboard.GetState().IsKeyDown(Keys.Left)|| GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X <- LIMITETHUMBSTICK)
                player.Tirer(Direction.Gauche);
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y <- LIMITETHUMBSTICK)
                player.Tirer(Direction.Bas);
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X > LIMITETHUMBSTICK)
                player.Tirer(Direction.Droite);
        #endregion

        #region Relaché
			// Revue JPM: Eviter les lignes trops longues
            if (Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right)&& GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X == 0&& GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y==0)
                player.TirClear();

        #endregion

        #region Autre
            if (Keyboard.GetState().IsKeyDown(Keys.Space)|| GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                player.Acheter();
            if (Keyboard.GetState().IsKeyDown(Keys.N))
                nWasPressed = true;
            if (Keyboard.GetState().IsKeyUp(Keys.N) && nWasPressed)
            {
                if (noclip) noclip = false; else noclip = true; //noclip toggle
                nWasPressed = false;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Quitter();



            #endregion
        }
        //Update
        public void Update()//S'execute 60 fois par seconde et execute toutes les fonction Update() des autres objets
        {
            
            if (gameState == GameState.EnCours || gameState == GameState.EntreRound)
            {
                
                //Player
                player.Update();
                //Zombies
                GestZombiesSpawn();
                //Zombie Update
                for (int i = 0; i < MAXZOMBIES; i++)
                {
                    zombies[i].Update();
                }
                //GameWindow
                gameWindow.Update();
                //World
                world.Update();
                //Entrée
                GestEntrée();
            }
            GestRound();

        }
        #endregion Methodes

        #region Fonctions
        //Conversions
        //Positions Monde > Positions Form
        public int PosXWorldToWindow(int xPosX) //Posistion X
        {
            int PosX=0;
            PosX=xPosX - gameWindow.PositionX;
            return PosX;
        }
        public int PosYWorldToWindow(int xPosY) //Position Y
        {
            int PosY=0;
            PosY=xPosY - gameWindow.PositionY;
            return PosY;
        }
        //Positions Form > Positions Monde
        public int PosXWindowToWorld(int xPosX) //Position X
        {
            int PosX=0;
            PosX=xPosX + gameWindow.PositionX;
            return PosX;
        }
        public int PosYWindowToWorld(int xPosY) //Position Y
        {
            int PosY=0;
            PosY=xPosY + gameWindow.PositionY;
            return PosY;
        }
        
        //Mouvement
        public bool IsMovePossible(int xPosX,int xPosY,int xVitesse,Direction xDirection)//Gère les collisions
        {
            if (noclip)//Ne fait rien en cas de noclip
                return true;
            //Déclaration
            bool possible=true;
            int posVerifX=0;
            int posVerifY=0;
            //Calcul du centre de la sprite
            xPosX += Entité.DIFFX;
            xPosY += Entité.DIFFY;
            //Calcul de la position à vérifier selon cas
            switch (xDirection)
            {
                case Direction.Haut:    posVerifX = xPosX;  posVerifY = xPosY-Entité.DIFFY-xVitesse;    break;//Haut
                case Direction.Droite:  posVerifY = xPosY;  posVerifX = xPosX + Entité.DIFFX+xVitesse;  break;//Droite
                case Direction.Bas:     posVerifX = xPosX;  posVerifY = xPosY + Entité.DIFFY+xVitesse;  break;//Bas
                case Direction.Gauche:  posVerifY = xPosY;  posVerifX = xPosX -Entité.DIFFX-xVitesse;   break;//Gauche
                case Direction.Fixe:    posVerifY = xPosX;  posVerifY = xPosY;  break;//Pas de mouvement
            } 
            //Détermination de sortie de la carte
            if (posVerifX < 0 || posVerifX >= MAPWIDTH*TILEWIDTH || posVerifY < 0 || posVerifY >= MAPHEIGHT*TILEHEIGHT)
                return false;
            //Diminution d'échelle
            posVerifX /= TILEWIDTH;
            posVerifY /= TILEHEIGHT;

            //Calcule si la position est Libre
            switch (map[posVerifY, posVerifX])
            {
                //true
                case TileType.Walkable: possible = true;    break;
                case TileType.Speed:    possible = true;    break;
                case TileType.Revive:   possible = true;    break;
                case TileType.Health:   possible = true;    break;
                case TileType.Damage:   possible = true;    break;
                //false
                case TileType.Wall:     possible = false;   break;
                //maybe
                case TileType.Door:
                    if (wall.Open)
                        possible = true;
                    else
                        possible = false;
                    break;
                case TileType.Upgrade:
                    if (wall.Open)
                        possible = true;
                    else
                        possible = false;
                    break;
            }
            //Retour de fonction
            return possible;
        }
        public bool IsWalkable(int xPosX, int xPosY)
        {   
            bool possible=false;
            //Out of range?
            if (xPosX < 0 || xPosX >= MAPWIDTH || xPosY < 0 || xPosY >= MAPHEIGHT)
                return false;

            switch (map[xPosX, xPosY])
            {   
                //true
                case TileType.Walkable: possible = true; break;
                case TileType.Speed: possible = true; break;
                case TileType.Revive: possible = true; break;
                case TileType.Health: possible = true; break;
                case TileType.Damage: possible = true; break;
                //false
                case TileType.Wall: possible = false; break;
                default: possible = false; break;
                //maybe
                /*case TileType.Door:
                    if (wall.Open)
                        possible = true;
                    else
                        possible = false;
                    break;
                case TileType.Upgrade:
                    if (wall.Open)
                        possible = true;
                    else
                        possible = false;
                    break;*/
            }
            if(possible&&
                IsMovePossible(xPosX*TILEWIDTH,xPosY*TILEHEIGHT,0,Direction.Haut)&&
                IsMovePossible(xPosX*TILEWIDTH,xPosY*TILEHEIGHT,0,Direction.Droite)&&
                IsMovePossible(xPosX*TILEWIDTH,xPosY*TILEHEIGHT,0,Direction.Bas)&&
                IsMovePossible(xPosX * TILEWIDTH, xPosY * TILEHEIGHT, 0, Direction.Gauche))
            {
                possible = true;
            }
            else
            {
                possible = false;
            }

            return possible;
        }
        #endregion

        #region Accesseurs
        //Propriétés
        public GameState GameState
        {
            get { return GameState; }
        }
        public int Round
        {
            get { return round; }
        }
        //Objets
        public Random getRandom
        {
            get { return random; }
        }
        public World getWorld
        {
            get { return world; }
        }
        public Player getPlayer
        {
            get { return player; }
        }
        public GameWin getWindow
        {
            get { return gameWindow; }
        }
        public TileType[,] GameMap
        {
            get { return map; }
        }
        public Zombie[] getZombies
        {
            get { return zombies; }
        }
        public Wall getWall
        {
            get { return wall; }
        }
        #endregion Accesseurs

        #region Constructeur
        public GameManager(TOTD xTOTD)
        {
            //Transfers d'objets
            TOTD = xTOTD;
            //Construction
            //Random
            random = new Random();
            //Game
            round = 1;
            //Map
            LoadMap();
            //Player
			//Revue JPM: Utiliser des constantes.
            player = new Player(28, 100, 100,POS_X_DEPARTPLAYER,POS_Y_DEPARTPLAYER,this);
            //Window
            gameWindow = new GameWin(this);
            //World
            world = new World(this);
            //Zombie
            zombies = new Zombie[MAXZOMBIES];
            for (int i = 0; i < MAXZOMBIES; i++)
            {
                zombies[i] = new Zombie(this, i, 20, 100, 100);
            }
            //Wall
            wall = new Wall();
            wall.Open = false;
            //Démarrage
            noclip = false;
            Démarrer();
        }
        #endregion
    }
}