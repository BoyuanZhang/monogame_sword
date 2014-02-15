using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public override void LoadContent()
        {
           //set the level rectangle here
        }
    }
}
