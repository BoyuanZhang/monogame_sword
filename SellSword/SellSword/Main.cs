#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

using SellSword.Managers;
#endregion

namespace SellSword
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Game
    {
        //Singleton instance of the Main
        private static Main instance;

        //Testing Variables -----------------------------------------------------------------------------------------------
        //time interval can be used for fps tracking through the console.
        static float g_timeInterval = 0;
        //Testing Variables -----------------------------------------------------------------------------------------------

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Main Instance { get { return instance; } }

        public Main()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Set singleton
            instance = this;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //Use singleton instance of the screen manager to load content for all necessary game objects
            ScreenManager.Instance.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            ScreenManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            ScreenManager.Instance.Draw(spriteBatch);

            //Uncomment for frame rate testing ----------------------------------------------------------------------------------
            double frameRate = 1 / gameTime.ElapsedGameTime.TotalSeconds;

            g_timeInterval += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (g_timeInterval > 5)
            {
                System.Console.WriteLine(frameRate);
                g_timeInterval = 0;
            }
            //Uncomment for frame rate testing ----------------------------------------------------------------------------------

            base.Draw(gameTime);
        }
    }
}
