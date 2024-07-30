using Godot;
using System;

namespace BadBunnyGames.BunPong
{
	public partial class Ball : CharacterBody2D
	{
		public Single Speed = 150f;
		public Vector2 Direction;
		private RandomNumberGenerator rng;
		public override void _Ready()
		{
			Direction = new Vector2(-1, -1);
			rng = new RandomNumberGenerator();
			rng.Randomize();
		}
		public override void _PhysicsProcess(Double delta)
		{
			Velocity = Direction * Speed;
			MoveAndSlide();

			for(int i = 0; i < GetSlideCollisionCount(); i++)
			{
				var collision = GetSlideCollision(i);
				Node2D collider = collision.GetCollider() as Node2D;
				if (collider.IsInGroup("platform"))
					Direction = new Vector2((collider.GlobalPosition.DirectionTo(GlobalPosition).Normalized() - Velocity.Normalized()).Normalized().X, -Direction.Y);
				if (collider.IsInGroup("border"))
					Direction = new Vector2(Direction.X, -(collider.GlobalPosition.DirectionTo(GlobalPosition).Normalized() - Velocity.Normalized()).Normalized().Y);

				Tween tween = CreateTween();
				tween.TweenProperty(this, "rotation", Rotation + rng.RandfRange(0, 2 * (Single)Math.PI), 5f);
			}
		}
	}
}
