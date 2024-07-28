using Godot;
using System;
using System.Linq;

namespace BadBunnyGames.BunThree
{
	public partial class FallingTilesController : Node2D
	{
		[Export] public Vector2 SpawnPosition;
		[Export] public PackedScene[] FallingTileScenes;

		private FeltTilesController _feltTilesController;
		private StaticTilesController _borderController;
		private FallingTile _fallingTile;
		private Addons _addons;
		
		public override void _Ready()
		{
			_feltTilesController = GetNode<FeltTilesController>("%FeltTiles");
			_borderController = GetNode<StaticTilesController>("%Border");
			_addons = GetNode<Addons>("%Addons");

			SpawnFallingTile();
		}

		public override void _PhysicsProcess(Double delta)
		{
			if (Input.IsActionJustPressed(KeyCode.Right))
				TryMoveRight();
			if (Input.IsActionJustPressed(KeyCode.Left))
				TryMoveLeft();
			if (Input.IsActionJustPressed(KeyCode.Up))
				TryRotate();
			if(Input.IsActionJustPressed(KeyCode.Down))
				ProcessMoveDown();
		}
		private void SpawnFallingTile()
		{
			_fallingTile = FallingTileScenes.GetRandomItem().Instantiate() as FallingTile;

			foreach(var tile in _fallingTile.Tiles)
				tile.Position += SpawnPosition;

			AddChild(_fallingTile);
			_fallingTile.PaintTiles(Helper.GetRandomColor());

			if(DoesPositionsCollideWithAnything(_fallingTile.Tiles.Select(tile => tile.Position).ToArray()))
				_addons._on_game_over();
		}
		private void _on_timer_timeout()
		{
			ProcessMoveDown();
		}
		private void ProcessMoveDown()
		{
			if (!TryFall())
			{
				foreach (var tile in _fallingTile.Tiles)
					tile.Reparent(_feltTilesController);

				_fallingTile.QueueFree();
				_feltTilesController.RemoveRows();
				SpawnFallingTile();
			}
		}
		private Boolean TryFall()
		{
			Vector2[] destinatePositions = _fallingTile.Tiles.Select(tile => new Vector2(tile.Position.X, tile.Position.Y + Tile.TileSize)).ToArray();
			if (!DoesPositionsCollideWithAnything(destinatePositions))
			{
				foreach (var tile in _fallingTile.Tiles)
					tile.Position += new Vector2(0, Tile.TileSize);
				return true;
			}

			return false;
		}
		private void TryMoveLeft()
		{
			Vector2[] destinatePositions = _fallingTile.Tiles.Select(tile => new Vector2(tile.Position.X - Tile.TileSize, tile.Position.Y)).ToArray();
			if (!DoesPositionsCollideWithAnything(destinatePositions))
			{
				foreach (var tile in _fallingTile.Tiles)
					tile.Position += new Vector2(-Tile.TileSize, 0);
			}
		}
		private void TryMoveRight()
		{
			Vector2[] destinatePositions = _fallingTile.Tiles.Select(tile => new Vector2(tile.Position.X + Tile.TileSize, tile.Position.Y)).ToArray();
			if (!DoesPositionsCollideWithAnything(destinatePositions))
			{
				foreach (var tile in _fallingTile.Tiles)
					tile.Position += new Vector2(Tile.TileSize, 0);
			}
		}
		private void TryRotate()
		{
			Vector2[] destinatePositions = _fallingTile.GetTilesRotatedLike();
			if (!DoesPositionsCollideWithAnything(destinatePositions))
				_fallingTile.Rotate();
		}

		private Boolean DoesPositionsCollideWithAnything(Vector2[] destinatePositions)
		{
			//GD.Print(String.Join(",", destinatePositions.Select(pos => $"({pos.X}, {pos.Y})")));
			foreach (var position in destinatePositions)
			{
				if (DoesPositionCollideWithAnything(position))
				{
					//GD.Print($"{position.X}, {position.Y} COLLIDE!");
					return true;
				}
			}

			return false;
		}

		private Boolean DoesPositionCollideWithAnything(Vector2 destinatePosition)
		{
			return _borderController.DoesCollideWithTiles(destinatePosition) || _feltTilesController.DoesCollideWithTiles(destinatePosition);
		}
	}
}
