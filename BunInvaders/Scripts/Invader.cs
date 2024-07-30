using Godot;
using System;

namespace BadBunnyGames.BunInvaders
{
	public partial class Invader : CharacterBody2D, IHittable
	{
		[Export] public PackedScene Projectile;
		[Export] public Node ProjectilesNode;
		public Action OnDestroy;
		public Action OnHitBorderLeft;
		public Action OnHitBorderRight;
		public Vector2 MoveDirection;
		public Single Speed = 10;
		private RandomNumberGenerator rng;
		public override void _Ready()
		{
			rng = new RandomNumberGenerator();
			var timer = GetTree().CreateTimer(rng.RandfRange(0, 5));
			timer.Timeout += ShootProjectileLoop;
		}
		public override void _Process(Double delta)
		{
			Velocity = MoveDirection * Speed;
			MoveAndSlide();
		}
		private void _on_area_2d_area_entered(Area2D area)
		{
			if (area.IsInGroup("border_left") && MoveDirection == Vector2.Left)
				OnHitBorderLeft?.Invoke();
			if (area.IsInGroup("border_right") && MoveDirection == Vector2.Right)
				OnHitBorderRight?.Invoke();
		}
		private void ShootProjectileLoop()
		{
			var timer = GetTree().CreateTimer(rng.RandfRange(3, 5));
			timer.Timeout += ShootProjectileLoop;
			ShootProjectile();
		}
		private void ShootProjectile()
		{
			Projectile projectile = Projectile.Instantiate() as Projectile;
			projectile.Name = "InvaderProjectile";
			projectile.GlobalPosition = GlobalPosition;
			ProjectilesNode.AddChild(projectile);
		}
		public void TakeDamage()
		{
			OnDestroy?.Invoke();
			QueueFree();
		}
	}
}
