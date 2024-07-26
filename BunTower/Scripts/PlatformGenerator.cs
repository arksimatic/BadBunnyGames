using Godot;
using System;
using System.Collections.Generic;

namespace BadBunnyGames.BunTower
{
	public partial class PlatformGenerator : Node2D
	{
		private PackedScene _platformScene;
		private Random _random;
		private List<Node2D> _platforms = new List<Node2D>();
		private Single _lastPlatformY = 0;
		private CharacterBody2D _player;
		public override void _Ready()
		{
			_player = GetNode<CharacterBody2D>("%Player");
			_random = new Random();
			_platformScene = GD.Load<PackedScene>($"{GlobalPaths.BunTower}/Scenes/platform.tscn");
		}
		public override void _Process(double delta)
		{
			if (_player.Position.Y - _lastPlatformY < 250)
				SpawnNextPlatform();
		}

		public void GeneratePlatform(Vector2 position)
		{
			Node2D platform = _platformScene.Instantiate() as Node2D;
			platform.Position = position;
			AddChild(platform);
		}

		private void SpawnNextPlatform()
		{
			var platform = _platformScene.Instantiate() as Node2D;
			var posX = _random.Next(-175, 175);
			platform.Position = new Vector2(posX, _lastPlatformY);
			_lastPlatformY -= 50;
			platform.Name = $"Platform{_platforms.Count}";
			AddChild(platform);
			_platforms.Add(platform);
		}
	}
}
