using Godot;
using System;

namespace BadBunnyGames.BunInvaders
{
	public partial class Player : CharacterBody2D, IHittable
	{
		[Export] public PackedScene Projectile;
		[Export] public Node ProjectilesNode;
		private Projectile playerProjectile;
		private Int32 Speed = 200;
		public override void _Process(Double delta)
		{
			if (Input.IsActionPressed(KeyCode.Left))
				Move(Vector2.Left);
			if (Input.IsActionPressed(KeyCode.Right))
				Move(Vector2.Right);
			if (Input.IsActionJustPressed(KeyCode.Up))
				TryShoot();
		}
		private void Move(Vector2 velocity)
		{
			Velocity = velocity * Speed;
			MoveAndSlide();
		}
		private void TryShoot()
		{
			if (!IsInstanceValid(playerProjectile) || playerProjectile == null)
				Shoot();
		}
		private void Shoot()
		{
			playerProjectile = Projectile.Instantiate() as Projectile;
			playerProjectile.Name = "PlayerProjectile";
			playerProjectile.GlobalPosition = GlobalPosition;
			ProjectilesNode.AddChild(playerProjectile);
		}
		public void TakeDamage()
		{
			GetNode<Addons>("%Addons")._on_game_over();
		}
		private void _on_area_2d_body_entered(Node2D body)
		{
			if (body.IsInGroup("enemy"))
				TakeDamage();
		}
	}
}
