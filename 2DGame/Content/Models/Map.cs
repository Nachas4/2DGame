using _2DGame.Content.Globals;
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

        public Map(int num/*, bool rnd = false*/)
        {
            Rows = 12;
            Columns = 12;
            TileSize = 64;

            GridPositions = new Vector2[Rows, Columns];

            //if (rnd) CreateRndMap(); else
            CreateMap(num);

            CreateEnemies();
        }

        public void MoveEnemies()
        {

        }

        private void CreateMap(int num)
        {
            string mapFile = "map" + num + ".txt";

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

        private void CreateEnemies()
        {
            Random rnd = new();

            //int numOfEnemies = rnd.Next(3, 7);
            int numOfEnemies = 6;
            int keyHolder = rnd.Next(numOfEnemies + 1);
            int boss;

            // Making sure the boss isn't the keyholder
            do boss = rnd.Next(numOfEnemies + 1);
            while (boss == keyHolder);

            for (int i = 0; i < numOfEnemies; i++)
            {
                Enemies.Add(new Enemy(SpawnPoints[i],
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
