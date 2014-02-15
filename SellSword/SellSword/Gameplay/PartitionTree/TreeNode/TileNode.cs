using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace SellSword.Gameplay.PartitionTree.TreeNode
{
    public class TileNode : TreeNode
    {
        public TileNode(bool collidable, Rectangle tileRect)
            : base(collidable, tileRect)
        {

        }
    }
}
