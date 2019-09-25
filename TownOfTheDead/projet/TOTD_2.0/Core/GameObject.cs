using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TOTD
{
    /// <summary>
    /// Cette classe est la classe mêre de tous les objets affichables en jeu
    /// </summary>
    class GameObject
    {
        #region Propriétés
        public Rectangle Source;//Rectangle de séléction de la sprite

        //Image en cours
        public int frameIndex;//numéro d'image en cours d'utilisation

        private int totalFrames;//total d'images
        private int frameWidth;//largeur de la sprite
        private int frameHeight;//hauteur de la sprite

        public Vector2 Position;//position de la sprite par rapport à la fenêtre
        public Texture2D Texture;//Texture de la sprite
        #endregion

        #region Méthodes
        /// <summary>
        /// Permet d'afficher la sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
        /// <summary>
        /// Permet d'afficher une partie de l'image pour en faire une animation
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Source, Color.White);
        }
        /// <summary>
        /// Change la partie de l'image en fonction de frameIndex
        /// </summary>
        public void UpdateFrame()
        {
            Source = new Rectangle(frameIndex * frameWidth,0,frameWidth,frameHeight);
        }
        #endregion Méthodes

        #region Accesseurs
        public int TotalFrames
        {
            get { return totalFrames; }
        }
        public int FrameWidth
        {
            get { return frameWidth; }
        }
        public int FrameHeight
        {
            get { return frameHeight; }
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur de Base
        /// </summary>
        public GameObject() { }
        /// <summary>
        /// OverLoad du constructeur de base
        /// </summary>
        /// <param name="xTotalFrames">Nombre de sprites dans l'image</param>
        /// <param name="xFrameWidth">largeur d'une sprite</param>
        /// <param name="xFrameHeight">Hauteur d'une sprite</param>
        public GameObject(int xTotalFrames,int xFrameWidth,int xFrameHeight)
        {
            totalFrames = xTotalFrames;
            frameWidth = xFrameWidth;
            frameHeight = xFrameHeight;
        }
        #endregion
    }
}
