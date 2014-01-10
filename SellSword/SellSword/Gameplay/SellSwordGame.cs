using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using SellSword.Screens;

namespace SellSword.Gameplay
{
    public class SellSwordGame
    {
        private ContentManager m_contentManager;
        private GameScreen m_gameScreenHandle;

        public SellSwordGame( GameScreen gameScreen)
        {
            //game screen handle, to inform screen of pauses / game ending events (game over etc...)
            m_gameScreenHandle = gameScreen;
        }

        public void UnloadContent()
        {

        }

        public void LoadContent(ContentManager content)
        {
            m_contentManager = content;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
