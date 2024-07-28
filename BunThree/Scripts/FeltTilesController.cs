using Godot;
using System;
using System.Linq;

namespace BadBunnyGames.BunThree
{
    public partial class FeltTilesController : StaticTilesController
    {
        private Single MaxRowSize = 6;
        public void RemoveRows()
        {
            Single[] allRows = Tiles.Select(tile => tile.Position.Y).Distinct().OrderBy(y => y).ToArray();

            foreach (Single row in allRows)
            {
                if (DoesRowContainFullTiles(row))
                {
                    RemoveRow(row);
                    UpdateRowsAbove(row);
                    // add score or something
                }
            }
        }
        private Boolean DoesRowContainFullTiles(Single row)
        {
            return Tiles.Where(tile => tile.Position.Y == row).Count() == MaxRowSize;
        }
        private void RemoveRow(Single row)
        {
            foreach (Tile tile in Tiles.Where(tile => tile.Position.Y == row))
                tile.QueueFree();
        }
        private void UpdateRowsAbove(Single row)
        {
            foreach (Tile tile in Tiles)
            {
                if (tile.Position.Y < row)
                    tile.Position += new Vector2(0, Tile.TileSize);
            }
        }
    }
}