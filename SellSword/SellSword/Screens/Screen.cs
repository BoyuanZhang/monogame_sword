using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SellSword.Screens
{
    public class Screen
    {
        public Screen() { }

        public virtual void LoadContent( ContentManager content) { }

        public virtual void UnloadContent() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
