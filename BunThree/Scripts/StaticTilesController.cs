using Godot;
using System;
using System.Linq;

namespace BadBunnyGames.BunThree
{
	public partial class StaticTilesController : Node
	{
		public Tile[] Tiles => GetChildren().Select(child => (Tile)child).ToArray();
		public Boolean DoesCollideWithTiles(Vector2 position)
		{
			//GD.Print(String.Join(",", tiles.Select(tile => $"Checking against: ({tile.Position.X}, {tile.Position.Y})")));
			return Tiles.Select(tile => tile.Position).Contains(position);
		}
	}
}
