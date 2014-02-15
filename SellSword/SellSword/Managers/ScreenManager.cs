#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using SellSword.Screens;

#endregion

namespace SellSword.Managers
{
    public class ScreenManager
    {
        //Singleton instance of the screenmanager
        private static ScreenManager instance;

        //Manages screen the user is interacting with, through the current game state. If game is not running manager menu screen,
        //if game is paused, pause screen action until user un-pauses game etc...
        public enum GameState { Menu, Load, Pause, Running, GameOver }

        private GraphicsDevice m_graphicsDevice;
        private GameState m_currentState;
        private Dictionary<GameState, Screen> m_screenDictionary;

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();

                return instance;
            }
        }

        private ScreenManager()
        {
            //Since there is no menu / loading functionality yet, just start game state with running
            //change this later when the menu, and load functionality are implemented.
            m_currentState = GameState.Running;
            //-----------------------------------

            m_graphicsDevice = Main.Instance.GraphicsDevice;

            //Initialize all screens
            InitializeScreens();
        }

        public void UnloadContent() { }

        public void LoadContent( ContentManager content ) 
        {
            //Load content for each screen in the dictionary, this will always be called after the screens are initialized
            Array gameStateValues = Enum.GetValues(typeof(GameState));

            foreach (GameState value in gameStateValues)
            {
                if (m_screenDictionary.ContainsKey(value))
                    m_screenDictionary[value].LoadContent(content);
            }
        }

        public void Update(GameTime gameTime)
        {
            //Update the screen according to the current state of the game application
            try
            {
                if (m_screenDictionary.ContainsKey(m_currentState))
                    m_screenDictionary[m_currentState].Update(gameTime);
            }
            catch(Exception e )
            {
                //Should never enter here but just in case...
                //Log the exception, for now just use console
                System.Console.WriteLine( e.Message );
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw the screen according to the state of the game application
            try
            {
                if (m_screenDictionary.ContainsKey(m_currentState))
                    m_screenDictionary[m_currentState].Draw(spriteBatch);
            }
            catch (Exception e)
            {
                //Should never enter here but log the exception if it does
                System.Console.WriteLine(e.Message);
            }
        }

        public void ChangeState(GameState newState)
        {
            m_currentState = newState;
        }

        private void InitializeScreens()
        {
            m_screenDictionary = new Dictionary<GameState, Screen>();

            //Initialize Game Screen, and add to screen dictionary
            m_screenDictionary.Add(GameState.Running, new GameScreen());
        }
    }
}
