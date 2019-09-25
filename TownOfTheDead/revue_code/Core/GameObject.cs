using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TOTD.Core
{
    class GameObject
    {
        #region Propriétés
        public Rectangle Source;

        //Image en cours
        public int frameIndex;

        private int totalFrames;
        private int frameWidth;
        private int frameHeight;

        public Vector2 Position;
        public Texture2D Texture;
        #endregion

        #region Méthodes
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Source, Color.White);
        }
        //Test
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
        public GameObject() { }
        public GameObject(int xTotalFrames,int xFrameWidth,int xFrameHeight)
        {
            totalFrames = xTotalFrames;
            frameWidth = xFrameWidth;
            frameHeight = xFrameHeight;
        }
        #endregion
    }
}
