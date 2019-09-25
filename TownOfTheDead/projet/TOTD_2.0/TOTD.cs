using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TOTD
{
    /// <summary>
    /// Classe principale de gestion des elements principalements graphiques 
    /// </summary>
    public class TOTD : Game
    {
        #region Constantes
        public const int WINDOW_WIDTH = 800;//Largeur de la fenêtre
        public const int WINDOW_HEIGHT = 600;//Hauteur de la fenêtre
        #endregion

        #region Base
        //Gestion graphique
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        #endregion

        #region Déclaration
        World world;//Carte de jeu
        Player player;//Joueur
        GameManager gameManager;//Gestionnaire de jeu
        Balle balle;//Balle de pistolet
        Zombie[] zombies;//Tableau de zombies
        Wall wall;//Mur de l'amélioration
        //Fond
        Fond fond;
        //HUD
        SpriteFont font;//Texte
        DamageIcon damageIcon;
        HealthIcon healthIcon;
        ReviveIcon reviveIcon;
        SpeedIcon speedIcon;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public TOTD()
        {
            //Base
            graphics = new GraphicsDeviceManager(this);//Gestionnaire des graphismes
            Content.RootDirectory = "Content";//Dossier de fichiers
            //Résolution
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;

            //Initialize
            gameManager = new GameManager(this);
            world = gameManager.getWorld;
            player = gameManager.getPlayer;
            zombies = gameManager.getZombies;
            wall = gameManager.getWall;
            //ArrièrePlan
            fond = new Fond();
            //HUD
            damageIcon = new DamageIcon();
            healthIcon = new HealthIcon();
            reviveIcon = new ReviveIcon();
            speedIcon = new SpeedIcon();
            //DEBUG
        }
        #endregion

        #region Initialise
        protected override void Initialize()
        {
            //Maintenant dans le constructeur car problèmes d'ordre de Load
            base.Initialize();
        }
        #endregion

        #region Load Content
        /// <summary>
        /// Gère le chargement des textures et autres
        /// </summary>
        protected override void LoadContent()
        {
            //Création du SpriteBatch utilisé pour afficher les textures
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //Chargement des textures et positions fixes
            //Fond
            fond.Texture = Content.Load<Texture2D>("bonjour.png");
            fond.Position = new Vector2(0, 0);
            //world
            world.Texture = Content.Load<Texture2D>("Worldv2.png");
            world.Position = new Vector2(0, 0);
            //player
            player.Texture = Content.Load<Texture2D>("SpritePlayer.png");
            //Position fixe pour ne pas surcharger le processeur - Possible de faire le rendu par rapport au world
            player.Position = new Vector2(Player.posInterfaceX,Player.posInterfaceY);
            //Wall
            wall.Texture = Content.Load<Texture2D>("RemovableWall.png");
            //Zombie
            for(int indZombie = 0; indZombie < GameManager.MAXZOMBIES; indZombie++)
            {
                zombies[indZombie].Texture = Content.Load<Texture2D>("SpriteZombie.png");
            }
            //HUD
            font = Content.Load<SpriteFont>("font");//texte
            damageIcon.Texture = Content.Load<Texture2D>("Damage.png");
            healthIcon.Texture = Content.Load<Texture2D>("Health.png");
            reviveIcon.Texture = Content.Load<Texture2D>("Revive.png");
            speedIcon.Texture = Content.Load<Texture2D>("Speed.png");
            damageIcon.Position = new Vector2(100, 0);
            healthIcon.Position = new Vector2(175, 0);
            reviveIcon.Position = new Vector2(250, 0);
            speedIcon.Position = new Vector2(325, 0);
            //Plein écran
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            //Visibilité de la souris
            IsMouseVisible = true;

            /*btnPlay = new cButton(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(350, 300));*/ 
        }
        /// <summary>
        /// Décharge le contenu
        /// </summary>
        protected override void UnloadContent()
        {

        }
        /// <summary>
        /// Charge le contenu de manière répétée
        /// </summary>
        private void UpdateContent()
        {
            //balle
            balle = player.getBalle;
            if (balle != null)
            {
                balle.Texture = Content.Load<Texture2D>("SpriteBalle.png");
                balle.Position = new Vector2
                    (
                    gameManager.PosXWorldToWindow(balle.PositionX), 
                    gameManager.PosYWorldToWindow(balle.PositionY)
                    );
            }
            //wall
            wall.Position = new Vector2
                (
                gameManager.PosXWorldToWindow(wall.PositionX), 
                gameManager.PosYWorldToWindow(wall.PositionY)
                );
            //zombies
            for(int indZombie = 0; indZombie < GameManager.MAXZOMBIES; indZombie++)
            {
                zombies[indZombie].Position = new Vector2
                    (
                        gameManager.PosXWorldToWindow(zombies[indZombie].PositionX), 
                        gameManager.PosYWorldToWindow(zombies[indZombie].PositionY)
                    );
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// S'execute 60 fois par seconde en execute la fonction Update de GameManager
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            world.Position = new Vector2(world.PositionRealX, world.PositionRealY);
            // TODO: Add your update logic here
            gameManager.Update();
            UpdateContent();
            base.Update(gameTime);
        }
        #endregion

        #region Draw
        /// <summary>
        /// Execute toutes les fonctions Draw() des objets à afficher
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //Fond
            fond.Draw(spriteBatch);
            //world
            world.Draw(spriteBatch);
            //Wall
            if (!wall.Open)
                wall.Draw(spriteBatch);
            //Zombies
            for (int indZombie = 0; indZombie < GameManager.MAXZOMBIES; indZombie++)
            {
                if (zombies[indZombie].Etat != Etat.Invisible)
                {
                    zombies[indZombie].DrawAnimation(spriteBatch);
                }
            }
            //balle
            if (balle != null)
                balle.DrawAnimation(spriteBatch);
            //player
            player.DrawAnimation(spriteBatch);
            //TEXTE
                //spriteBatch.DrawString(font, gameManager.nombreZombies.ToString(), new Vector2(10, 250), Color.Black);//nombre de zombies vivants
                //spriteBatch.DrawString(font, gameManager.zombiesRound.ToString(), new Vector2(10, 270), Color.Black);//nombre de zombies apparus dans le round
            spriteBatch.DrawString(font, gameManager.Round.ToString(), new Vector2(10, 10), Color.Black);//Round en cours
            spriteBatch.DrawString(font, gameManager.points.ToString(), new Vector2(600, 10), Color.Black);//nombre de points
            spriteBatch.DrawString(font, player.PointsDeVie.ToString(), new Vector2(600, 400), Color.DarkRed);//points de vie du joueur

            spriteBatch.DrawString(font, Player.PRIXHEALTH.ToString(), new Vector2(gameManager.PosXWorldToWindow(790), gameManager.PosYWorldToWindow(220)), Color.DarkRed);//Prix de Health
            spriteBatch.DrawString(font, Player.PRIXDAMAGE.ToString(), new Vector2(gameManager.PosXWorldToWindow(2040), gameManager.PosYWorldToWindow(3120)), Color.Yellow);//Prix de damage
            spriteBatch.DrawString(font, Player.PRIXREVIVE.ToString(), new Vector2(gameManager.PosXWorldToWindow(3200), gameManager.PosYWorldToWindow(630)), Color.Blue);//Prix de Revive
            spriteBatch.DrawString(font, Player.PRIXSPEED.ToString(), new Vector2(gameManager.PosXWorldToWindow(500), gameManager.PosYWorldToWindow(2780)), Color.Green);//Prix de speed
            //HUD
            if (player.Atouts.Damage) damageIcon.Draw(spriteBatch);
            if (player.Atouts.Health) healthIcon.Draw(spriteBatch);
            if (player.Atouts.Revive) reviveIcon.Draw(spriteBatch);
            if (player.Atouts.Speed) speedIcon.Draw(spriteBatch);
            //
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}
