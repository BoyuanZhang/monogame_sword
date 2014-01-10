using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using SellSword.Gameplay;
using SellSword.Managers;

namespace SellSword.Screens
{
    public class GameScreen : Screen
    {
        //Handle to the gameplay object. Currently the game screen will call the gameplay object to display the world onto the screen.
        //Screen class used to abstract away screen logic from game logic.
        private SellSwordGame m_game;

        public GameScreen()
        {
            m_game = new SellSwordGame(this);
        }

        public override void UnloadContent()
        {
        }

        //Loads necessary game starting content
        public override void LoadContent( ContentManager content)
        {
            m_game.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            m_game.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            m_game.Draw(spriteBatch);
        }

        //Once game is running, the three ways out are, exit, pause, and lose the game. Pause and losing state changes will be called from here
        public void PauseGame()
        {
            ScreenManager.Instance.ChangeState(ScreenManager.GameState.Pause);
        }

        public void GameOver()
        {
            ScreenManager.Instance.ChangeState(ScreenManager.GameState.GameOver);
        }
    }
}
