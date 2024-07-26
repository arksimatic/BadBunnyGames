using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BadBunnyGames.FlappyBun
{
	public partial class WoodGenerator : Node2D
	{
		private Int32 _initialWoodCount = 10;
		private Single _spacing = 50;
		private Random _random;
		private CharacterBody2D _player;
		private PackedScene _woodScene;
		private List<Node2D> _woods = new List<Node2D>();
		public override void _Ready()
		{
			_player = GetNode<CharacterBody2D>("%Player");

			_random = new Random();
			_woodScene = GD.Load<PackedScene>($"{GlobalPaths.FlappyBun}/Scenes/wood.tscn");

			for (int i = 0; i < _initialWoodCount; i++)
			{
				SpawnNextWood();
			}
		}
		public override void _Process(double delta)
		{
			if (_woods.Last().Position.X - _player.Position.X < 200)
			{
				DeleteFirstWood();
				SpawnNextWood();
			}
		}

		private void DeleteFirstWood()
		{
			_woods[0].QueueFree();
			_woods.Remove(_woods[0]);
		}
		private void SpawnNextWood()
		{
			var wood = _woodScene.Instantiate() as Node2D;
			var shiftY = _random.Next(-50, 50);
			wood.Position = new Vector2(GetNextX(), shiftY);
			wood.Name = $"Wood{_woods.Count}";
			AddChild(wood);
			_woods.Add(wood);
		}

		private Single GetNextX()
		{
			if (_woods.Count == 0)
				return _spacing;
			else
				return _woods.Last().Position.X + _spacing;
		}
	}
}
