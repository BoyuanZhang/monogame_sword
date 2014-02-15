using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SellSword.Gameplay.Sprites
{
    public class PlayerSprite : Sprite
    {
        private const float m_speed = 2.0f;
        private Vector2 m_velocity;

        public PlayerSprite(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            m_velocity = Vector2.Zero;
        }

        public override void Update()
        {
            //Update our player, as well as his respective bounding rectangle, and the player center
            m_position.X += m_velocity.X;
            m_position.Y += m_velocity.Y;
            m_spriteRectangle.X = (int)m_position.X;
            m_spriteRectangle.Y = (int)m_position.Y;
            m_center.X = m_position.X + m_texture.Width / 2;
            m_center.Y = m_position.Y + m_texture.Height / 2;

            //Set velocity back to 0
            m_velocity.X = 0.0f;
            m_velocity.Y = 0.0f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_spriteRectangle, Color.White);
        }

        private void HandleInputs()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                m_velocity.X = m_speed * -1;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                m_velocity.X = m_speed;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                m_velocity.Y = m_speed * -1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                m_velocity.Y = m_speed;

            if (m_velocity.X != 0 && m_velocity.Y != 0)
                m_velocity = Vector2.Normalize(m_velocity);
        }
    }
}
