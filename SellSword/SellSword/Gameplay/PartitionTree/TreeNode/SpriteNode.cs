using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace SellSword.Gameplay.PartitionTree.TreeNode
{
    public class SpriteNode : TreeNode
    {
        public SpriteNode(bool collidable, Rectangle spriteRect )
            : base(collidable, spriteRect)
        {
        }

        public void test() 
        {
        }
    }
}
