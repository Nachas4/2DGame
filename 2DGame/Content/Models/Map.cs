﻿using _2DGame.Content.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2DGame.Content.Models
{
    public class Map
    {
        public int Rows;
        public int Columns;
        public int TileSize;

        private readonly Vector2[,] GridPositions;
        public readonly List<Tile> Tiles = new();
        public readonly List<Enemy> Enemies = new();
        public readonly List<string[]> SpawnPoints = new();

        public Map(int lvl/*, bool rnd = false*/)
        {
            Rows = 12;
            Columns = 12;
            TileSize = 64;

            GridPositions = new Vector2[Rows, Columns];

            //if (rnd) CreateRndMap(); else
            CreateMap(lvl);
            CreateEnemies(lvl);
        }

        public void MoveEnemies()
        {
            foreach (Enemy enemy in Enemies.Where(x => x.Alive))
            {
                int finalDirection = int.MaxValue;

                // Calculate all potential directions
                int leftIndex = enemy.PositionIndex - 1;
                int rightIndex = enemy.PositionIndex + 1;
                int upIndex = enemy.PositionIndex - GVars.CurrentMap.Columns;
                int downIndex = enemy.PositionIndex + GVars.CurrentMap.Columns;

                List<int> legalDirections = new();

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
                        legalDirections.Add(leftIndex);

                    if (Tiles[rightIndex].Sprite == GVars.GrassTexture && !Enemies.Any(x => x.PositionIndex == rightIndex && x.Alive))
                        legalDirections.Add(rightIndex);

                    if (Tiles[upIndex].Sprite == GVars.GrassTexture && !Enemies.Any(x => x.PositionIndex == upIndex && x.Alive))
                        legalDirections.Add(upIndex);

                    if (Tiles[downIndex].Sprite == GVars.GrassTexture && !Enemies.Any(x => x.PositionIndex == downIndex && x.Alive))
                        legalDirections.Add(downIndex);

                    // Randomly select a legal direction
                    if (legalDirections.Any())
                        finalDirection = legalDirections[new Random().Next(legalDirections.Count)];
                }

                //Make movement
                if (finalDirection == leftIndex)
                {
                    enemy.VecPosition.X = Math.Max(0, enemy.VecPosition.X - GVars.CurrentMap.TileSize);

                    enemy.YPos -= 1;
                    enemy.EnemySprite.Play("left"); //boss sprite doesn't change
                }
                else if (finalDirection == rightIndex)
                {
                    enemy.VecPosition.X = Math.Min((GVars.CurrentMap.Columns - 1) * GVars.CurrentMap.TileSize, enemy.VecPosition.X + GVars.CurrentMap.TileSize);

                    enemy.YPos += 1;
                    enemy.EnemySprite.Play("right");
                }
                else if (finalDirection == upIndex)
                {
                    enemy.VecPosition.Y = Math.Max(0, enemy.VecPosition.Y - GVars.CurrentMap.TileSize);

                    enemy.XPos -= 1;
                    enemy.EnemySprite.Play("up");
                }
                else if (finalDirection == downIndex)
                {
                    enemy.VecPosition.Y = Math.Min((GVars.CurrentMap.Rows - 1) * GVars.CurrentMap.TileSize, enemy.VecPosition.Y + GVars.CurrentMap.TileSize);

                    enemy.XPos += 1;
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

            //int numOfEnemies = rnd.Next(3, 7);
            int numOfEnemies = 6;
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


        public void DrawMap(SpriteBatch spriteBatch)
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
