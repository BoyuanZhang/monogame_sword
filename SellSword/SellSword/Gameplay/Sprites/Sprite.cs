using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SellSword.Gameplay.Sprites
{
    public class Sprite
    {
        protected Texture2D m_texture;
        protected Vector2 m_position;
        protected Rectangle m_spriteRectangle;
        protected Vector2 m_center;

        protected Sprite(Texture2D texture, Vector2 position)
        {
            m_position = position;
            m_texture = texture;

            m_center = new Vector2(m_position.X + m_texture.Width / 2, m_position.Y + m_texture.Height / 2);
            m_spriteRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public Vector2 Center { get { return m_center; } }

        public virtual void Update() { }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
