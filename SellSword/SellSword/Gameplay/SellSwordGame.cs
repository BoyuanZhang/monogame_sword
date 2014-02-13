using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using SellSword.Screens;
using SellSword.Gameplay.Sprites;
using SellSword.Gameplay.Camera;

namespace SellSword.Gameplay
{
    public class SellSwordGame
    {
        private GameScreen m_gameScreenHandle;

        //player
        private PlayerSprite m_player;

        //camera
        private PlayerCamera m_playerCamera;

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
            //init player
            m_player = new PlayerSprite(content.Load<Texture2D>("MonoGameContent/Sprites/MarioSprite"), new Vector2(0.0f, 0.0f));

            //init camera
            m_playerCamera = new PlayerCamera(Main.Instance.GraphicsDevice.Viewport, 1.0f);
        }

        public void Update(GameTime gameTime)
        {
            m_player.Update();
            m_playerCamera.Update(m_player);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, m_playerCamera.TransformMatrix);

            m_player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
