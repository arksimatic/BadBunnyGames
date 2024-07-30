using Godot;
using System;

namespace BadBunnyGames.BunInvaders
{
	public partial class InvadersSpawner : Node2D
	{
		[Export] public PackedScene InvaderScene;
		[Export] public Node ProjectilesNode;
		public Int32 MinX = -200;
		public Int32 MaxX = 200;
		public Int32 MinY = 0;
		public Int32 MaxY = 100;
		public Int32 Spacing = 50;

		public override void _Ready()
		{
			SpawnInvaders();
		}
		private void SpawnInvaders()
		{
			for(int x = MinX; x <= MaxX; x += Spacing)
			{
				for(int y = MinY; y <= MaxY; y += Spacing)
				{
					SpawnInvader(x, y);
				}
			}
		}
		private void SpawnInvader(Int32 x, Int32 y)
		{
			Invader invader = InvaderScene.Instantiate() as Invader;
			invader.GlobalPosition = new Vector2(x, y);
			invader.ProjectilesNode = ProjectilesNode;
			invader.MoveDirection = Vector2.Right;
			invader.OnDestroy += OnInvaderKilled;
			invader.OnHitBorderLeft += OnInvaderHitBorderLeft;
			invader.OnHitBorderRight += OnInvaderHitBorderRight;
			AddChild(invader);
		}
		public void OnInvaderKilled()
		{
			if (GetChildCount() <= 1)
				SpawnInvaders();
		}
		private void OnInvaderHitBorderLeft()
		{
			TweenInvadersForwardWithDirectionChange(Vector2.Right);
		}
		private void OnInvaderHitBorderRight()
		{
			TweenInvadersForwardWithDirectionChange(Vector2.Left);
		}
		private void TweenInvadersForwardWithDirectionChange(Vector2 newDirection)
		{
			foreach (var child in GetChildren())
			{
				Invader invader = child as Invader;
				invader.MoveDirection = newDirection;
				Tween tween = CreateTween();
				tween.TweenProperty(invader, "position", invader.Position + new Vector2(0, 10), 1);
			}
		}
	}
}
