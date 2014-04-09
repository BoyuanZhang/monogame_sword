using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SellSword.Gameplay.PartitionTree
{
    //Currently this interface is a little messy, we need to a find a better way of doing things.
    //The HandleCollision is only used by collision objects, and the Draw with the texture's is only
    //used by tiles.
    public interface IPartitionNode
    {
        Rectangle BoundingRectangle { get; }

        bool Moveable { get; }

        void Draw(SpriteBatch spriteBatch);

        void HandleCollision();
    }
}
