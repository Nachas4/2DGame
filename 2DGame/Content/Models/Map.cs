using _2DGame.Content.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2DGame.Content.Models
{
    internal class Map
    {
        internal int Rows;
        internal int Columns;
        internal int TileSize;

        private readonly Vector2[,] GridPositions;
        internal readonly List<Tile> Tiles = new();
        internal readonly List<Enemy> Enemies = new();
        internal readonly List<string[]> SpawnPoints = new();

        internal Map(int lvl/*, bool rnd = false*/)
        {
            Rows = 12;
            Columns = 12;
            TileSize = 64;

            GridPositions = new Vector2[Rows, Columns];

            //if (rnd) CreateRndMap(); else
            CreateMap(lvl);
            CreateEnemies(lvl);
        }

        internal void MoveEnemies()
        {
            foreach (Enemy enemy in Enemies.Where(x => x.Alive))
            {
                int finalDirection = int.MaxValue;

                // Calculate all potential directions
                int leftIndex = enemy.PositionIndex - 1;
                int rightIndex = enemy.PositionIndex + 1;
                int upIndex = enemy.PositionIndex - GVars.CurrentMap.Columns;
                int downIndex = enemy.PositionIndex + GVars.CurrentMap.Columns;

                // The bool indicates whether the given move was the last one
                Dictionary<int, bool> legalDirections = new();

                // Check if player is next to the enemy
                if (GVars.Player.PositionIndex == leftIndex ||
                    GVars.Player.PositionIndex == rightIndex ||
                    GVars.Player.PositionIndex == upIndex ||
                    GVars.Player.PositionIndex == downIndex)
                {
                    // Prioritize attacking if player is adjacent
                    finalDirection = GVars.Player.PositionIndex;
                }
                else
                {
                    // Check legality of directions
                    if (Tiles[leftIndex].Sprite == GVars.GrassTexture && !Enemies.Any(x => x.PositionIndex == leftIndex && x.Alive))
                        legalDirections.Add(leftIndex, enemy.LastMove == 1);

                    if (Tiles[rightIndex].Sprite == GVars.GrassTexture && !Enemies.Any(x => x.PositionIndex == rightIndex && x.Alive))
                        legalDirections.Add(rightIndex, enemy.LastMove == 0);

                    if (Tiles[upIndex].Sprite == GVars.GrassTexture && !Enemies.Any(x => x.PositionIndex == upIndex && x.Alive))
                        legalDirections.Add(upIndex, enemy.LastMove != 3);

                    if (Tiles[downIndex].Sprite == GVars.GrassTexture && !Enemies.Any(x => x.PositionIndex == downIndex && x.Alive))
                        legalDirections.Add(downIndex, enemy.LastMove != 2);

                    // If only one movement is available, AND it was the last move, we need to explicitly state that the enemy can move backwards THIS time, otherwise it's stuck
                    if (legalDirections.Count == 1 && legalDirections.ContainsValue(true)) 
                        finalDirection = legalDirections.First(x => x.Value).Key;
                    else if (legalDirections.Any()) // Randomly select a legal direction
                    {
                        try
                        {
                            // Remove the last move
                            legalDirections.Remove(legalDirections.First(x => x.Value).Key);
                        } 
                        catch { }

                        finalDirection = legalDirections.ElementAt(new Random().Next(legalDirections.Count)).Key;
                    }
                }

                // Make movement
                if (finalDirection == leftIndex)
                {
                    enemy.VecPosition.X = Math.Max(0, enemy.VecPosition.X - GVars.CurrentMap.TileSize);

                    enemy.YPos -= 1;
                    enemy.LastMove = 0;
                    enemy.EnemySprite.Play("left"); //boss sprite doesn't change
                }
                else if (finalDirection == rightIndex)
                {
                    enemy.VecPosition.X = Math.Min((GVars.CurrentMap.Columns - 1) * GVars.CurrentMap.TileSize, enemy.VecPosition.X + GVars.CurrentMap.TileSize);

                    enemy.YPos += 1;
                    enemy.LastMove = 1;
                    enemy.EnemySprite.Play("right");
                }
                else if (finalDirection == upIndex)
                {
                    enemy.VecPosition.Y = Math.Max(0, enemy.VecPosition.Y - GVars.CurrentMap.TileSize);

                    enemy.XPos -= 1;
                    enemy.LastMove = 2;
                    enemy.EnemySprite.Play("up");
                }
                else if (finalDirection == downIndex)
                {
                    enemy.VecPosition.Y = Math.Min((GVars.CurrentMap.Rows - 1) * GVars.CurrentMap.TileSize, enemy.VecPosition.Y + GVars.CurrentMap.TileSize);

                    enemy.XPos += 1;
                    enemy.LastMove = 3;
                    enemy.EnemySprite.Play("down");
                }
                else
                    continue;
            }
        }

        private void CreateMap(int lvl)
        {
            string mapFile = $"map{lvl}.txt";

            //Enemy SpawnPoints
            string[] map = File.ReadAllLines(mapFile);

            foreach (var item in map.First().Split(';'))
                SpawnPoints.Add(item.Split(','));


            //Tile symbols into one string line
            string text = "";

            foreach (var item in map.Skip(1))
                text += item;


            //Tile symbols into string List
            List<string> tiles = new();

            foreach (char i in text)
                tiles.Add(i.ToString());


            //Tile symbols to Grid map
            int listnum = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (tiles[listnum] == "X")
                        Tiles.Add(new Tile(GVars.WallTexture, Tiles.Count));
                    else
                        Tiles.Add(new Tile(GVars.GrassTexture, Tiles.Count));

                    GridPositions[i, j] = new Vector2(j * TileSize, i * TileSize);
                    listnum++;
                }
            }
        }

        private void CreateEnemies(int lvl)
        {
            Random rnd = new();

            int numOfEnemies = rnd.Next(3, 7);
            //int numOfEnemies = 6;
            int keyHolder = rnd.Next(numOfEnemies);
            int boss;

            // Making sure the boss isn't the keyholder
            do boss = rnd.Next(numOfEnemies);
            while (boss == keyHolder);

            for (int i = 0; i < numOfEnemies; i++)
            {
                Enemies.Add(new Enemy(SpawnPoints[i], lvl,
                    isBoss: i == boss,
                    hasKey: i == keyHolder));

                Enemies[i].EnemySprite.Play("downStand");
            }
        }


        internal void DrawMap(SpriteBatch spriteBatch)
        {
            int gridNum = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    spriteBatch.Draw(Tiles[gridNum].Sprite, GridPositions[i, j], Color.White);
                    gridNum++;
                }
            }
        }


        //private void CreateRndMap()
        //{
        //    Random rnd = new();

        //    for (int i = 0; i < Rows; i++)
        //    {
        //        for (int j = 0; j < Columns; j++)
        //        {
        //            if (i == 0 || i == 11 || j == 0 || j == 11)
        //            {
        //                Tiles.Add(new Tile(GVars.WallTexture, Tiles.Count));
        //            }
        //            else
        //            {
        //                Tiles.Add(new Tile(rnd.Next(1, 6) == 1 ? GVars.WallTexture : GVars.GrassTexture,
        //                        Tiles.Count));
        //            }
        //            GridPositions[i, j] = new Vector2(j * TileSize, i * TileSize);
        //        }
        //    }
        //}
    }
}
