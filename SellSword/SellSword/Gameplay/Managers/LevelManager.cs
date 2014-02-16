using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

using SellSword.Gameplay.Levels;
using Microsoft.Xna.Framework;

namespace SellSword.Gameplay.Managers
{
    public class LevelManager
    {
        public enum Levels { JungTown };
        private Levels m_currentLevel;
        private Dictionary<Levels, Level> m_levelDictionary;

        public LevelManager( Levels startingLevel, Level levelObj)
        { 
            m_levelDictionary = new Dictionary<Levels,Level>();

            m_currentLevel = startingLevel;
            m_levelDictionary.Add( Levels.JungTown, levelObj );
        }

        public void AddNewLevel() { }
        public void ChangeLevel() { }

        public Rectangle LevelRectangle { get { return Rectangle.Empty; } }

        
        //Level getter
        public Level CurrentLevel
        {
            get
            {
                if (m_levelDictionary.ContainsKey(m_currentLevel))
                    return m_levelDictionary[m_currentLevel];
                return null;
            }
        }

        //Draw current level
        public void DrawCurrentLevel(SpriteBatch spriteBatch)
        {
            CurrentLevel.Draw(spriteBatch);
        }
    }
}
