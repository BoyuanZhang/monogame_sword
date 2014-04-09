using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using SellSword.Screens;
using SellSword.Gameplay.Sprites;
using SellSword.Gameplay.Camera;
using SellSword.Gameplay.Managers;
using SellSword.Gameplay.Levels;
using SellSword.Gameplay.PartitionTree;
using SellSword.Gameplay.Tiles;

using TileEngine.Layer;
using TileEngine.Layer.Tiles;

namespace SellSword.Gameplay
{
    public class SellSwordGame
    {
        public static int VIEWPORTWIDTH = (int)(Main.Instance.GraphicsDevice.Viewport.Width/1.5);
        public static int VIEWPORTHEIGHT = (int)(Main.Instance.GraphicsDevice.Viewport.Height/1.5);

        private GameScreen m_gameScreenHandle;
        //player
        private PlayerSprite m_player;
        //camera
        private PlayerCamera m_playerCamera;
        //Collision and Drawing partition trees, when the player enters new worlds the trees are populated
        private QuadPartitionTree<IPartitionNode> m_drawTree;
        private QuadPartitionTree<IPartitionNode> m_collisionTree;

        //We only draw items inside this rectangle
        private Rectangle m_graphicsRectangle;

        //handle to the level manager
        private LevelManager m_levelManager;

        public SellSwordGame( GameScreen gameScreen)
        {
            //game screen handle, to inform screen of pauses / game ending events (game over etc...)
            m_gameScreenHandle = gameScreen;
        }

        public void UnloadContent()
        {
        }

        public void LoadContent(ContentManager content)
        {
            //init player
            m_player = new PlayerSprite(content.Load<Texture2D>("MonoGameContent/Sprites/MarioSprite"), new Vector2(0.0f, 0.0f));

            //init camera
            m_playerCamera = new PlayerCamera(Main.Instance.GraphicsDevice.Viewport, 1.0f);

            m_levelManager = new LevelManager(LevelManager.Levels.JungTown, new JungTown());

            //after everything has been loaded in, we initialize our partition trees, and send them to the current level to be populated
            m_drawTree = new QuadPartitionTree<IPartitionNode>(m_levelManager.LevelRectangle);
            m_collisionTree = new QuadPartitionTree<IPartitionNode>(m_levelManager.LevelRectangle);

            //load elements into partition trees
            PopulateTrees();

            //Set the graphics rectangle
            m_graphicsRectangle = new Rectangle((int)m_player.Center.X - VIEWPORTWIDTH, (int)m_player.Center.Y - VIEWPORTHEIGHT, VIEWPORTWIDTH * 2, VIEWPORTHEIGHT * 2);
        }

        public void PopulateTrees()
        {
            //Add the level objects to the draw and collision tree
            //first retrieve all tiles into the corresponding lists. If the layer containing the tiles is a collision layer
            //we add the tiles in that layer to both the collision and draw tree
            List<GameTileLayer> layerList = m_levelManager.CurrentLevel.LevelLayerList;
            foreach( GameTileLayer layer in layerList ) 
            {
                List<GameTile> tileList = layer.GetAllTiles();
                if (layer.LayerLayoutType == LayerType.LayerTypesEnum.Collision)
                {
                    foreach (GameTile tile in tileList)
                    {
                        if (tile.TextureIndex != -1)
                        {
                            TreeTile treeTile = new TreeTile(tile, tile.TextureIndex, layer.Alpha, layer.GetTileIndexTexture);
                            m_drawTree.Add(treeTile);
                            m_collisionTree.Add(treeTile);
                        }
                    }
                }
                else
                    foreach (GameTile tile in tileList)
                    {
                        if (tile.TextureIndex != -1)
                        {
                            TreeTile treeTile = new TreeTile(tile, tile.TextureIndex, layer.Alpha, layer.GetTileIndexTexture);
                            m_drawTree.Add(treeTile);
                        }
                    }
            }

            //Add the player to the draw and collision tree
            m_drawTree.Add(m_player);
            m_collisionTree.Add(m_player);
        }

        public void Update(GameTime gameTime)
        {
            //Update player
            m_player.Update();
            m_playerCamera.Update(m_player);

            //Update everything in current level
            m_levelManager.UpdateCurrentLevel();

            //Handle all collisions

            //Update scene Rectangle
            m_graphicsRectangle.X = (int)m_player.Center.X - VIEWPORTWIDTH;
            m_graphicsRectangle.Y = (int)m_player.Center.Y - VIEWPORTHEIGHT;

            //Upate all moveable game objects in the tree that contains all items
            m_drawTree.UpdateMoveableItems();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, m_playerCamera.TransformMatrix);

            //Get contained scene objects from partition tree and draw them
            List<LinkedList<IPartitionNode>> sceneItemList = m_drawTree.GetPartitionItems(m_graphicsRectangle);
            List<IPartitionNode> moveableItems = new List<IPartitionNode>();
            foreach (LinkedList<IPartitionNode> sceneItems in sceneItemList)
            {
                foreach (IPartitionNode item in sceneItems)
                {
                    //if item is a moveable type we append it to a list of items we will draw afterwards
                    if (item.Moveable == true)
                        moveableItems.Add(item);
                    else
                        item.Draw(spriteBatch);
                }
            }
            //Draw all moveable items on top now
            foreach (IPartitionNode item in moveableItems)
            {
                item.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
