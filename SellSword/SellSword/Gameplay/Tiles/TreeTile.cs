using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SellSword.Gameplay.PartitionTree;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TileEngine.Layer.Tiles;

namespace SellSword.Gameplay.Tiles
{
    public delegate Texture2D GetTileTexture(int textureIndex);

    public class TreeTile : IPartitionNode
    {
        private GameTile m_gameTile;
        private int m_textureIndex;
        private float m_alphaChannel;
        private GetTileTexture m_tileTextureDelegate;

        public TreeTile(GameTile tile, int textureIndex, float alphaChannel, GetTileTexture getTileTexture)
        {
            m_gameTile = tile;
            m_textureIndex = textureIndex;
            m_alphaChannel = alphaChannel;

            m_tileTextureDelegate = getTileTexture;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            Texture2D texture = m_tileTextureDelegate(m_textureIndex);
            m_gameTile.Draw(spriteBatch, texture, m_alphaChannel);
        }

        public Rectangle BoundingRectangle { get { return m_gameTile.TileRectangle; } }
        public int TextureIndex { get { return m_textureIndex; } }

        public void HandleCollision() { }
    }
}
