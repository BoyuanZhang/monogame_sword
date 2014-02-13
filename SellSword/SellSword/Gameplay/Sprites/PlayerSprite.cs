using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SellSword.Gameplay.Sprites
{
    public class PlayerSprite : Sprite
    {
        public PlayerSprite(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
        }

        public override void Update()
        {
            //Update our player, as well as his respective bounding rectangle, and the player center
            m_spriteRectangle.X = (int)m_position.X;
            m_spriteRectangle.Y = (int)m_position.Y;

            m_center.X = m_position.X + m_texture.Width / 2;
            m_center.Y = m_position.Y + m_texture.Height / 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_spriteRectangle, Color.White);
        }
    }
}
