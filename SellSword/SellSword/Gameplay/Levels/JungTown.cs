using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TileEngine.Filing;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SellSword.Gameplay.Managers;

namespace SellSword.Gameplay.Levels
{
    public class JungTown : Level
    {
        public JungTown()
            : base(new ContentManager(Main.Instance.Services, "Content"), LevelManager.Levels.JungTown)
        {
            LoadContent();
        }

        public override void UnloadContent()
        {
        }

        public override void LoadContent()
        {
            //Load in Jungtown map (normal layer, and collision layer)
            m_levelMap.AddLayer( EngineFileHandler.OpenGameLayer( m_contentManager, "Content/Levels/jungtown_normal.layer" ));
            m_levelMap.AddLayer( EngineFileHandler.OpenGameLayer( m_contentManager, "Content/Levels/jungtown_collision.layer" ));
           
            //set the level rectangle here
            m_levelRectangle = new Rectangle( 0, 0, m_levelMap.GetMapPixelWidth(), m_levelMap.GetMapPixelHeight() );
        }
    }
}
