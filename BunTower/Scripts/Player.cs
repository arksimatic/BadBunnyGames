using Godot;
using System;

namespace BunTower
{
	public partial class Player : CharacterBody2D
	{
		public const Single Speed = 300.0f;
		public const Single JumpVelocity = -400.0f;

		public Single Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle(); // gravity from project settings

		public override void _PhysicsProcess(double delta)
		{
			Vector2 velocity = Velocity;

			// Add the gravity
			if (!IsOnFloor())
				velocity.Y += Gravity * (Single)delta;

			// Jump
			if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
				velocity.Y = JumpVelocity;

			// Change to keys
			Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
			if (direction != Vector2.Zero)
				velocity.X = direction.X * Speed;
			else
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

			Velocity = velocity;
			MoveAndSlide();
		}
	}
}
