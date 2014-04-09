using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SellSword.Gameplay.PartitionTree;

namespace SellSword.Gameplay.Sprites
{
    public class PlayerSprite : Sprite, IPartitionNode
    {
        private const float m_speed = 3.0f;
        private Vector2 m_velocity;

        public PlayerSprite(Texture2D texture, Vector2 position)
            : base(texture, position)
        {
            m_velocity = Vector2.Zero;
            m_moveable = true;
        }

        public override void Update()
        {
            //Handle user inputs
            HandleInputs();

            //Update our player, as well as his respective bounding rectangle, and the player center
            m_position.X += (float)Math.Round(m_velocity.X * m_speed);
            m_position.Y += (float)Math.Round(m_velocity.Y * m_speed);
            m_spriteRectangle.X = (int)m_position.X;
            m_spriteRectangle.Y = (int)m_position.Y;
            m_center.X = m_position.X + m_texture.Width / 2;
            m_center.Y = m_position.Y + m_texture.Height / 2;

            //Set velocity back to 0
            m_velocity.X = 0.0f;
            m_velocity.Y = 0.0f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_spriteRectangle, Color.White);
        }

        public void HandleCollision() { }

        private void HandleInputs()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                m_velocity.X = -1.0f;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                m_velocity.X = 1.0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                m_velocity.Y = -1.0f;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                m_velocity.Y = 1.0f;

            if (m_velocity.X != 0 && m_velocity.Y != 0)
                m_velocity = Vector2.Normalize(m_velocity);
        }

        //Properties
        public Rectangle BoundingRectangle { get { return m_spriteRectangle; } }
        public bool Moveable { get { return m_moveable; } }
    }
}
