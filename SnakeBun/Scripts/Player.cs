using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BadBunnyGames.SnakeBun
{
	public partial class Player : Node2D
	{
		[Export] public Texture2D HeadTexture;
		[Export] public Texture2D BodyTexture;
		[Export] public Texture2D BodyLeftTexture;
		[Export] public Texture2D BodyRightTexture;
		[Export] public Texture2D LegsTexture;

		private Node2D _bunBody;
		private Sprite2D[] _bodyChilds;
		private Sprite2D _head => _bodyChilds.First();
		private Sprite2D _legs => _bodyChilds.Last();

		private Double _timer = 0.0f;
		private Single _movesPerSecond => 1 / Statics.Speed;
		private Vector2 _moveDirection = new Vector2(1, 0);

		private Sprite2D _food;
		private Single _eightyShift = 80;
		private Single _eightShift = 8;
		public override void _Ready()
		{
			_bunBody = GetNode<Node2D>("Body");
			_food = GetNode<Sprite2D>("%Food");
		}

		public override void _Process(Double delta)
		{
			UpdateInputVector();

			_timer += delta;

			if (_timer > _movesPerSecond)
			{
				_timer -= _movesPerSecond;
				UpdateBodyChilds();

				if (GetFreePositions().Count() == 0)
				{
					GD.Print("YOU WON");
					return;
				}

				if (DoesHeadCollideWithBody() || DoesHeadCollideWithWalls())
				{
					GetNode<Addons>("%Addons")._on_game_over();
					return;
				}

				MoveParts();
			}
		}

		private void UpdateBodyChilds()
		{
			_bodyChilds = _bunBody.GetChildren().Select(child => child as Sprite2D).ToArray();
		}
		private Boolean DoesHeadCollideWithWalls()
		{
			Vector2 headNewPosition = CalculateNewGlobalHeadPosition();
			return headNewPosition.X < -_eightyShift || headNewPosition.X >= Statics.MapWidth * Statics.TileSize - _eightyShift || headNewPosition.Y < -_eightyShift || headNewPosition.Y >= Statics.MapHeight * Statics.TileSize - _eightyShift;
		}
		private Boolean DoesHeadCollideWithBody()
		{
			Vector2 headNewPosition = CalculateNewHeadPosition();
			for (int i = 1; i < _bodyChilds.Length - 1; i++) // skip head and legs
			{
				if (headNewPosition == _bodyChilds[i].Position)
					return true;
			}
			return false;
		}
		private void AddNewBodyPart()
		{
			Sprite2D legs = _bodyChilds.Last();
			Sprite2D newBodyPart = new Sprite2D();
			newBodyPart.Texture = BodyTexture;
			newBodyPart.Position = legs.Position;
			newBodyPart.RotationDegrees = legs.RotationDegrees;
			_bunBody.RemoveChild(legs);
			_bunBody.AddChild(newBodyPart);
			_bunBody.AddChild(legs);
			UpdateBodyChilds();
		}
		private void MoveParts()
		{
			Boolean willEatCarrot = CalculateNewGlobalHeadPosition() == _food.GlobalPosition;

			if (willEatCarrot)
				AddNewBodyPart();
			else
				_bodyChilds[_bodyChilds.Length - 1].Position = _bodyChilds[_bodyChilds.Length - 2].Position; // legs movement

			// BODY
			for (int i = _bodyChilds.Length - 2; i > 0; i--)
			{
				{
					Single oneBeforeDegrees = _bodyChilds[i - 1].RotationDegrees;
					Single twoBeforeDegrees = i == 1 ? Helper.GetDegreesFromVector(_moveDirection) : _bodyChilds[i - 2].RotationDegrees;

					// to be honest, the sprites were breaking and this appears to solve the issue so I don't really want to mess with it
					if (Helper.GetKeyCodeFromDegrees(oneBeforeDegrees) == KeyCode.Up && Helper.GetKeyCodeFromDegrees(twoBeforeDegrees) == KeyCode.Left)
						oneBeforeDegrees = 360;
					else if (Helper.GetKeyCodeFromDegrees(oneBeforeDegrees) == KeyCode.Left && Helper.GetKeyCodeFromDegrees(twoBeforeDegrees) == KeyCode.Up)
						twoBeforeDegrees = 360;

					if (oneBeforeDegrees > twoBeforeDegrees)
						_bodyChilds[i].Texture = BodyLeftTexture;
					else if (oneBeforeDegrees < twoBeforeDegrees)
						_bodyChilds[i].Texture = BodyRightTexture;
					else
						_bodyChilds[i].Texture = BodyTexture;
				}

				_bodyChilds[i].Position = _bodyChilds[i - 1].Position;
				_bodyChilds[i].RotationDegrees = _bodyChilds[i - 1].RotationDegrees;
			}

			if (willEatCarrot)
			{
				Vector2[] freePositions = GetFreePositions();
				Int32 rand = GD.RandRange(0, freePositions.Count() - 1);

				Vector2 newPosition = freePositions[rand];
				_food.Position = new Vector2(newPosition.X + _eightShift, newPosition.Y + _eightShift);
			}
			else
			{
				// legs
				_bodyChilds[_bodyChilds.Length - 1].Texture = LegsTexture;
				_bodyChilds[_bodyChilds.Length - 1].RotationDegrees = _bodyChilds[_bodyChilds.Length - 2].RotationDegrees;
			}

			// head
			_bodyChilds[0].Texture = HeadTexture;
			_bodyChilds[0].Position = CalculateNewHeadPosition();
			_bodyChilds[0].RotationDegrees = Helper.GetDegreesFromVector(_moveDirection);

		}

		private Vector2[] GetFreePositions()
		{
			List<Vector2> takenPositions = new List<Vector2>();
			takenPositions = _bodyChilds.Select(child => new Vector2(child.Position.X + _eightyShift, child.Position.Y + _eightyShift)).ToList();
			takenPositions.RemoveAt(takenPositions.Count() - 1);

			List<Vector2> allMapPositions = new List<Vector2>();
			for (int i = 1; i < Statics.MapWidth - 1; i++)
			{
				for (int j = 1; j < Statics.MapHeight - 1; j++)
				{
					allMapPositions.Add(new Vector2(i * Statics.TileSize, j * Statics.TileSize));
				}
			}

			return allMapPositions.Except(takenPositions).ToArray();
		}
		private Vector2 CalculateNewHeadPosition()
		{
			return new Vector2(_bodyChilds[0].Position.X + _moveDirection.X * Statics.TileSize, _bodyChilds[0].Position.Y + _moveDirection.Y * Statics.TileSize);
		}
		private Vector2 CalculateNewGlobalHeadPosition()
		{
			return new Vector2(_bodyChilds[0].GlobalPosition.X + _moveDirection.X * Statics.TileSize, _bodyChilds[0].GlobalPosition.Y + _moveDirection.Y * Statics.TileSize);
		}
		public void UpdateInputVector()
		{
			Vector2 vector2 = Vector2.Zero;

			if (Input.IsActionPressed(KeyCode.Right))
				vector2 = new Vector2(vector2.X + 1, vector2.Y);
			else if (Input.IsActionPressed(KeyCode.Left))
				vector2 = new Vector2(vector2.X - 1, vector2.Y);
			else if (Input.IsActionPressed(KeyCode.Down))
				vector2 = new Vector2(vector2.X, vector2.Y + 1);
			else if (Input.IsActionPressed(KeyCode.Up))
				vector2 = new Vector2(vector2.X, vector2.Y - 1);

			// _moveDirection is direction where I want to go NOT THE ONE I'M HEADING so it's need to be fixed
			if (vector2 != Vector2.Zero && vector2 + _moveDirection != Vector2.Zero)
				_moveDirection = vector2;
		}
	}
}
