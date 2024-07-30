using Godot;
using System;

namespace BadBunnyGames.BunInvaders
{
	public partial class Projectile : Area2D, IHittable
	{
		[Export] public Single Speed;
		[Export] public String[] TagsToDamage;
		[Export] public Vector2 Direction;

		public override void _PhysicsProcess(Double delta)
		{
			Position += Direction * Speed * (Single)delta;
		}
		private void _on_area_2d_area_entered(Area2D area)
		{
			if (area is IHittable)
			{
				if (area.IsInAnyGroup(TagsToDamage))
				{
					(area as IHittable).TakeDamage();
					TakeDamage();
				}
			}

			if (area.IsInGroup("projectile_destructor"))
				TakeDamage();
		}
		private void _on_area_2d_body_entered(Node2D body)
		{
			if (body is IHittable)
			{
				if (body.IsInAnyGroup(TagsToDamage))
				{
					(body as IHittable).TakeDamage();
					TakeDamage();
				}
			}
		}
		public void TakeDamage()
		{
			QueueFree();
		}
	}
}
