using Godot;
using System;
using System.Linq;

namespace BadBunnyGames.BunThree
{
	public partial class FallingTile : Node2D
	{
		[Export] public FallingTileType Type;
		private Int32 _internalRotation = 0;
		public Tile[] Tiles => GetChildren().Select(child => (Tile)child).ToArray();
		public void Rotate()
		{
			switch (Type)
			{
				case FallingTileType.I:
					RotateI();
					break;
				case FallingTileType.L:
					RotateL();
					break;
			}
		}
		private void RotateL()
		{
			Node2D head = GetNode("%Head") as Node2D;
			Node2D body = GetNode("%Body") as Node2D;
			Node2D legs = GetNode("%Legs") as Node2D;

			switch (_internalRotation)
			{
				case 0:
					head.Position = new Vector2(head.Position.X - Tile.TileSize, head.Position.Y);
					body.Position = new Vector2(body.Position.X, body.Position.Y - Tile.TileSize);
					legs.Position = new Vector2(legs.Position.X + Tile.TileSize, legs.Position.Y);
					break;
				case 90:
					head.Position = new Vector2(head.Position.X, head.Position.Y + Tile.TileSize);
					body.Position = new Vector2(body.Position.X - Tile.TileSize, body.Position.Y);
					legs.Position = new Vector2(legs.Position.X, legs.Position.Y - Tile.TileSize);
					break;
				case 180:
					head.Position = new Vector2(head.Position.X + Tile.TileSize, head.Position.Y);
					body.Position = new Vector2(body.Position.X, body.Position.Y + Tile.TileSize);
					legs.Position = new Vector2(legs.Position.X - Tile.TileSize, legs.Position.Y);
					break;
				case 270:
					head.Position = new Vector2(head.Position.X, head.Position.Y - Tile.TileSize);
					body.Position = new Vector2(body.Position.X + Tile.TileSize, body.Position.Y);
					legs.Position = new Vector2(legs.Position.X, legs.Position.Y + Tile.TileSize);
					break;
			}

			head.Rotate(-(Single)Math.PI / 2);
			body.Rotate(-(Single)Math.PI / 2);
			legs.Rotate(-(Single)Math.PI / 2);

			_internalRotation += 90;
			if (_internalRotation == 360)
				_internalRotation = 0;
		}
		private void RotateI()
		{
			Node2D head = GetNode("%Head") as Node2D;
			Node2D body = GetNode("%Body") as Node2D;
			Node2D legs = GetNode("%Legs") as Node2D;

			switch (_internalRotation)
			{
				case 0:
					head.Position = new Vector2(head.Position.X + Tile.TileSize, head.Position.Y + Tile.TileSize);
					legs.Position = new Vector2(legs.Position.X - Tile.TileSize, legs.Position.Y - Tile.TileSize);
					break;
				case 90:
					head.Position = new Vector2(head.Position.X - Tile.TileSize, head.Position.Y + Tile.TileSize);
					legs.Position = new Vector2(legs.Position.X + Tile.TileSize, legs.Position.Y - Tile.TileSize);
					break;
				case 180:
					head.Position = new Vector2(head.Position.X - Tile.TileSize, head.Position.Y - Tile.TileSize);
					legs.Position = new Vector2(legs.Position.X + Tile.TileSize, legs.Position.Y + Tile.TileSize);
					break;
				case 270:
					head.Position = new Vector2(head.Position.X + Tile.TileSize, head.Position.Y - Tile.TileSize);
					legs.Position = new Vector2(legs.Position.X - Tile.TileSize, legs.Position.Y + Tile.TileSize);
					break;
			}

			head.Rotate((Single)Math.PI / 2);
			body.Rotate((Single)Math.PI / 2);
			legs.Rotate((Single)Math.PI / 2);
			_internalRotation += 90;
			if (_internalRotation == 360)
				_internalRotation = 0;
		}
		public Vector2[] GetTilesRotatedLike()
		{
			switch(Type)
			{
				case FallingTileType.I:
					return GetTilesRotatedLikeI();
				case FallingTileType.L:
					return GetTilesRotatedLikeL();
				default:
					return new Vector2[] { };
			}
		}
		private Vector2[] GetTilesRotatedLikeL()
		{
			Node2D head = GetNode("%Head") as Node2D;
			Node2D body = GetNode("%Body") as Node2D;
			Node2D legs = GetNode("%Legs") as Node2D;

			switch (_internalRotation)
			{
				case 0:
					return new Vector2[]
					{
						new Vector2(head.Position.X - Tile.TileSize, head.Position.Y),
						new Vector2(body.Position.X, body.Position.Y - Tile.TileSize),
						new Vector2(legs.Position.X + Tile.TileSize, legs.Position.Y)
					};
				case 90:
					return new Vector2[]
					{
						new Vector2(head.Position.X, head.Position.Y + Tile.TileSize),
						new Vector2(body.Position.X - Tile.TileSize, body.Position.Y),
						new Vector2(legs.Position.X, legs.Position.Y - Tile.TileSize),
					};
				case 180:
					return new Vector2[]
					{
						new Vector2(head.Position.X + Tile.TileSize, head.Position.Y),
						new Vector2(body.Position.X, body.Position.Y + Tile.TileSize),
						new Vector2(legs.Position.X - Tile.TileSize, legs.Position.Y)
					};
				case 270:
					return new Vector2[]
					{
						new Vector2(head.Position.X, head.Position.Y - Tile.TileSize),
						new Vector2(body.Position.X + Tile.TileSize, body.Position.Y),
						new Vector2(legs.Position.X, legs.Position.Y + Tile.TileSize)
					};
				default:
					return new Vector2[] { };
			}
		}
		private Vector2[] GetTilesRotatedLikeI()
		{
			Node2D head = GetNode("%Head") as Node2D;
			Node2D body = GetNode("%Body") as Node2D;
			Node2D legs = GetNode("%Legs") as Node2D;

			switch (_internalRotation)
			{
				case 0:
					return new Vector2[] 
					{ 
						new Vector2(head.Position.X + Tile.TileSize, head.Position.Y + Tile.TileSize), 
						new Vector2(body.Position.X, body.Position.Y), 
						new Vector2(legs.Position.X - Tile.TileSize, legs.Position.Y - Tile.TileSize)
					};
				case 90:
					return new Vector2[] 
					{ 
						new Vector2(head.Position.X - Tile.TileSize, head.Position.Y + Tile.TileSize), 
						new Vector2(body.Position.X, body.Position.Y), 
						new Vector2(legs.Position.X + Tile.TileSize, legs.Position.Y - Tile.TileSize)
					};
				case 180:
					return new Vector2[] 
					{ 
						new Vector2(head.Position.X - Tile.TileSize, head.Position.Y - Tile.TileSize), 
						new Vector2(body.Position.X, body.Position.Y), 
						new Vector2(legs.Position.X + Tile.TileSize, legs.Position.Y + Tile.TileSize)
					};
				case 270:
					return new Vector2[] 
					{ 
						new Vector2(head.Position.X + Tile.TileSize, head.Position.Y - Tile.TileSize), 
						new Vector2(body.Position.X, body.Position.Y), 
						new Vector2(legs.Position.X - Tile.TileSize, legs.Position.Y + Tile.TileSize)
					};
				default:
					return new Vector2[] { };
			}
		}
		public void PaintTiles(Color color)
		{
			foreach(var tile in Tiles)
				tile.GetNode<Sprite2D>("Sprite2D").Modulate = color;
		}
	}
}
