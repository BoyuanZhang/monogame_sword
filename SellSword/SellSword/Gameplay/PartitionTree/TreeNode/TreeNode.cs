using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using SellSword.Gameplay.Collision;

namespace SellSword.Gameplay.PartitionTree.TreeNode
{
    public class TreeNode : ICollidable, IPartitionNode
    {
        protected bool m_collidable;
        protected Rectangle m_nodeRect;

        protected TreeNode( bool collidable, Rectangle nodeRect)
        {
            m_collidable = collidable;
            m_nodeRect = nodeRect;
        }

        public Rectangle BoundingRectangle { get { return m_nodeRect; } }
        public bool Collidable { get { return m_collidable; } }
    }
}
