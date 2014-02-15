using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace SellSword.Gameplay.PartitionTree
{
    public interface IPartitionNode
    {
        Rectangle BoundingRectangle { get; }
    }
}
