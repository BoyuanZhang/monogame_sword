using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SellSword.Gameplay.Managers;

namespace SellSword.Gameplay.Levels
{
    public class Level
    {
        //Each game location will have it's own content manager for easier handling of game content
        protected ContentManager m_contentManager;
        protected LevelManager.Levels m_level;
        protected Rectangle m_levelRectangle;

        protected Level( ContentManager contentManager, LevelManager.Levels level )
        {
            m_contentManager = contentManager;
            m_level = level;
            m_levelRectangle = Rectangle.Empty;
        }

        public virtual void UnloadContent() { }
        public virtual void LoadContent() { }
    }
}
