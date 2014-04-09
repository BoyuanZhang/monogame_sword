using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SellSword.Utility;

namespace SellSword.Gameplay.PartitionTree
{
    public class QuadPartitionTree<T> where T : IPartitionNode
    {
        //Partition section
        public readonly Rectangle partitionBox;

        //Parent partition, when partitioned contains four child partitions for each different section
        public QuadPartitionTree<T> parentPartition;
        public QuadPartitionTree<T> leftTopPartition;
        public QuadPartitionTree<T> rightTopPartition;
        public QuadPartitionTree<T> leftBottomPartition;
        public QuadPartitionTree<T> rightBottomPartition;
        //objects contained within current partition
        public readonly LinkedList<T> m_itemList;

        //keep track of moveable objects for easier moving of items throughout partition tree, this is shared across all partition trees
        public static Dictionary<T, QuadPartitionTree<T>> m_moveableDict = new Dictionary<T,QuadPartitionTree<T>>();

        private static List<LinkedList<T>> m_itemsInsideBound = new List<LinkedList<T>>();
        private int maxItems = 3;

        //FOR VISUALIZATION OF QUADTREE ONLY to store unique quads. For actually running of game this should be commented
       // private static Dictionary<Rectangle, Rectangle> m_quadRectangleMap = new Dictionary<Rectangle, Rectangle>();
        //-------------------------------------------------------------------------------------------------------------

        //Creates partition tree with the "master" bounding box, there is no parent partition for this one
        public QuadPartitionTree( Rectangle boundingBox)
        {
            partitionBox = boundingBox;
            m_itemList = new LinkedList<T>();  
            parentPartition = null;
        }

        //Private constructor to create the child partition trees, that will have a parent
        private QuadPartitionTree(Rectangle boundingBox, QuadPartitionTree<T> parent)
        {
            partitionBox = boundingBox;
            m_itemList = new LinkedList<T>();
            parentPartition = parent;
        }

        //Attempt to add an item into the correct partition tree
        public bool Add(T item)
        {
            //is item contained within this partition's bounding box?
            if (RectangleUtility.ContainedWithin( item.BoundingRectangle, partitionBox))
            {
                if (leftTopPartition == null)
                {
                    if (m_itemList.Count < maxItems)
                    {
                        //No children and max items haven't been reached yet, add another item to this trees item list
                        m_itemList.AddLast(item);
                        //if the item is moveable, we add it to the moveable dictionary for referencing
                        if (item.Moveable)
                        {
                            //if dictionary doesn't contain item we add it
                            if (!m_moveableDict.ContainsKey(item))
                                m_moveableDict.Add(item, this);
                        }
                        return true;
                    }
                    else
                        Split();
                }

                if (!leftTopPartition.Add(item))
                    if (!rightTopPartition.Add(item))
                        if (!leftBottomPartition.Add(item))
                            if (!rightBottomPartition.Add(item))
                            {
                                //If no children wholly contain this item, add the item to the parent, and increase the max number of items allowed
                                //to keep max items above the current number of items
                                m_itemList.AddLast(item);
                                if (m_itemList.Count >= maxItems)
                                    maxItems++;

                                if (item.Moveable)
                                {
                                    if (!m_moveableDict.ContainsKey(item))
                                        m_moveableDict.Add(item, this);
                                }
                            }

                return true;
            }
            else
                return false;
        }

        public void RemoveItem( T item, Rectangle containingRectangle)
        {
            if (leftTopPartition != null)
            {
                if (RectangleUtility.ContainedWithin(containingRectangle, leftTopPartition.partitionBox))
                    leftTopPartition.RemoveItem(item, containingRectangle);
                if (RectangleUtility.ContainedWithin(containingRectangle, rightTopPartition.partitionBox))
                    rightTopPartition.RemoveItem(item, containingRectangle);
                if (RectangleUtility.ContainedWithin(containingRectangle, leftBottomPartition.partitionBox))
                    leftBottomPartition.RemoveItem(item, containingRectangle);
                if (RectangleUtility.ContainedWithin(containingRectangle, rightBottomPartition.partitionBox))
                    rightBottomPartition.RemoveItem(item, containingRectangle);
                else
                {
                    if (m_itemList.Contains(item))
                    {
                        m_itemList.Remove(item);
                        if (m_moveableDict.ContainsKey(item))
                            m_moveableDict.Remove(item);
                    }
                }
            }
            else
            {
                if (m_itemList.Contains(item))
                {
                    m_itemList.Remove(item);
                    if (m_moveableDict.ContainsKey(item))
                        m_moveableDict.Remove(item);
                }
            }
        }

        public void UpdateMoveableItems()
        {
            Dictionary<T, QuadPartitionTree<T>> temporaryDict = new Dictionary<T, QuadPartitionTree<T>>(m_moveableDict);
            foreach (KeyValuePair<T, QuadPartitionTree<T>> pair in temporaryDict)
            {
                if (!RectangleUtility.ContainedWithin(pair.Key.BoundingRectangle, pair.Value.partitionBox))
                {
                    //item is no longer contained within this quad
                    pair.Value.m_itemList.Remove(pair.Key);
                    //make sure there is a parent partition (There should always be a parent partition)
                    if (pair.Value.parentPartition != null)
                        MoveItemUpQuad(pair.Key, pair.Value.parentPartition);
                }
                else
                {
                    //otherwise see if the item has been moved to a lower quad
                    if (pair.Value.leftTopPartition != null)
                    {
                        //check if item can be contained with lower partitions
                        MoveItemDownQuad(pair.Key, pair.Value);
                    }
                }
            }
        }

        private void MoveItemDownQuad(T item, QuadPartitionTree<T> quad)
        {
            if (RectangleUtility.ContainedWithin(item.BoundingRectangle, quad.leftTopPartition.partitionBox))
            {
                m_moveableDict.Remove(item);
                quad.m_itemList.Remove(item);
                quad.leftTopPartition.Add(item);
            }
            else if (RectangleUtility.ContainedWithin(item.BoundingRectangle, quad.rightTopPartition.partitionBox))
            {
                m_moveableDict.Remove(item);
                quad.m_itemList.Remove(item);
                quad.rightTopPartition.Add(item);
            }
            else if (RectangleUtility.ContainedWithin(item.BoundingRectangle, quad.leftBottomPartition.partitionBox))
            {
                m_moveableDict.Remove(item);
                quad.m_itemList.Remove(item);
                quad.leftBottomPartition.Add(item);
            }
            else if (RectangleUtility.ContainedWithin(item.BoundingRectangle, quad.rightBottomPartition.partitionBox))
            {
                m_moveableDict.Remove(item);
                quad.m_itemList.Remove(item);
                quad.rightBottomPartition.Add(item);
            }
        }

        private void MoveItemUpQuad(T item, QuadPartitionTree<T> quad)
        {
            if (RectangleUtility.ContainedWithin(item.BoundingRectangle, quad.partitionBox))
            {
                //To reach here the key must exist
                //remove the current dictionary entry
                m_moveableDict.Remove(item);
                quad.Add(item);
            }
            else
                if (quad.parentPartition != null)
                    MoveItemUpQuad(item, quad.parentPartition);
        }



        //For visualization of quadtree only, remove or comment out this method when running game for non-test / debug pruposes
        //--------------------------------------------------------------------
      /*  public List<Rectangle> GetQuadRectangles()
        {
            return m_quadRectangleMap.Values.ToList();
        }*/
        //--------------------------------------------------------------------

        //Find the partition / partitions that contain this rectangle, and return all objects within the partition / partitions
        public List<LinkedList<T>> GetPartitionItems( Rectangle box )
        {
            m_itemsInsideBound.Clear();
            AddItemsToBoundPartition(box);
            return m_itemsInsideBound;
        }

        public void AddItemsToBoundPartition(Rectangle box)
        {
            if (partitionBox.Intersects(box))
            {
                //for visualization of quadtree only, remove or comment when running game for non-test / debug purposes.
                //--------------------------------------------------------------------
               /* if( !m_quadRectangleMap.ContainsKey( partitionBox ) )
                    m_quadRectangleMap.Add(partitionBox, partitionBox);*/
                //--------------------------------------------------------------------

                if (leftTopPartition == null)
                {
                    if (m_itemList.Count > 0)
                        m_itemsInsideBound.Add(m_itemList);
                }
                else
                {
                    leftTopPartition.AddItemsToBoundPartition(box);
                    rightTopPartition.AddItemsToBoundPartition(box);
                    leftBottomPartition.AddItemsToBoundPartition(box);
                    rightBottomPartition.AddItemsToBoundPartition(box);

                    if (m_itemList.Count > 0)
                        m_itemsInsideBound.Add(m_itemList);
                }

            }
        }

        //Clear contents of tree recursively through the public clear call to root
        public void Clear()
        {
            ClearTree(this);
        }

        //Split tree into four partitions
        private void Split()
        {
            int midLength = (partitionBox.Right - partitionBox.Left) / 2;
            int midHeight = (partitionBox.Bottom - partitionBox.Top) / 2;

            leftTopPartition = new QuadPartitionTree<T>(new Rectangle(partitionBox.Left, partitionBox.Top, midLength, midHeight), this);
            rightTopPartition = new QuadPartitionTree<T>(new Rectangle(partitionBox.Left + midLength, partitionBox.Top, midLength, midHeight), this);
            leftBottomPartition = new QuadPartitionTree<T>(new Rectangle(partitionBox.Left, partitionBox.Bottom - midHeight, midLength, midHeight), this);
            rightBottomPartition = new QuadPartitionTree<T>(new Rectangle(partitionBox.Left + midLength, partitionBox.Bottom - midHeight, midLength, midHeight), this);

            //attempt to split items from parent into children
            SplitItems(m_itemList.First);
        }

        //After splitting a tree into four smaller partitions, re-distribute items from the parent tree into the children
        private void SplitItems(LinkedListNode<T> currentNode)
        {
            if (currentNode == null)
                return;

            //If item is unable to be split because an item is not wholly contained within the children then the parent
            //will hold onto this item
            bool isItemSplit = true;

            if (RectangleUtility.ContainedWithin(currentNode.Value.BoundingRectangle, leftTopPartition.partitionBox))
                leftTopPartition.Add(currentNode.Value);
            else if (RectangleUtility.ContainedWithin(currentNode.Value.BoundingRectangle, rightTopPartition.partitionBox))
                rightTopPartition.Add(currentNode.Value);
            else if (RectangleUtility.ContainedWithin(currentNode.Value.BoundingRectangle, leftBottomPartition.partitionBox))
                leftBottomPartition.Add(currentNode.Value);
            else if (RectangleUtility.ContainedWithin(currentNode.Value.BoundingRectangle, rightBottomPartition.partitionBox))
                rightBottomPartition.Add(currentNode.Value);
            else
                isItemSplit = false;
         
            SplitItems(currentNode.Next);

            //Only remove item, if it has been re-distributed to a child tree
            if( isItemSplit) 
                m_itemList.Remove(currentNode);
        }

        private void ClearTree(QuadPartitionTree<T> tree)
        {
            //Recursively empty this trees items and children, then set children to be null
            if (tree == null)
                return;

            if (m_itemList.Count > 0)
                m_itemList.Clear();
            if (m_itemsInsideBound.Count > 0)
                m_itemsInsideBound.Clear();

            if (tree.leftTopPartition != null)
            {
                ClearTree(tree.leftTopPartition);
                tree.leftTopPartition = null;
                ClearTree(tree.rightTopPartition);
                tree.rightTopPartition = null;
                ClearTree(tree.leftBottomPartition);
                tree.leftBottomPartition = null;
                ClearTree(tree.rightBottomPartition);
                tree.rightBottomPartition = null;
            }
        }
    }
}
