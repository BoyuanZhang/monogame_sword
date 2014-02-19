using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TileEngine.Map;
using TileEngine.Layer;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SellSword.Gameplay.Managers;

namespace SellSword.Gameplay.Levels
{
    public class Level
    {
        //Each game location will have it's own content manager for easier handling of game content
        protected ContentManager m_contentManager;
        protected LevelManager.Levels m_levelEnum;
        protected Rectangle m_levelRectangle;
        protected TileMap m_levelMap;

        protected Level( ContentManager contentManager, LevelManager.Levels level )
        {
            m_contentManager = contentManager;
            m_levelEnum = level;
            m_levelRectangle = Rectangle.Empty;
            m_levelMap = new TileMap();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //for drawing the entire map (not optimized)
            m_levelMap.Draw(spriteBatch);
        }

        public virtual void UnloadContent() { }
        public virtual void LoadContent() { }

        //Properties

        //Get layer list of map
        public List<GameTileLayer> LevelLayerList { get { return m_levelMap.TileLayerList; } }

        //Get level Rectangle
        public Rectangle LevelBoundingBox { get { return m_levelRectangle; } }
    }
}
