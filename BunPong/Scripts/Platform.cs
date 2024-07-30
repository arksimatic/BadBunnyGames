using Godot;
using System;

namespace BadBunnyGames.BunPong
{
	public partial class Platform : Node2D
	{
		[Export] public String KeyCodeUp;
		[Export] public String KeyCodeDown;
		public Single Speed = 3f;
		public override void _PhysicsProcess(Double delta)
		{
			if(Input.IsActionPressed(KeyCodeUp))
				Position += new Vector2(0, -1) * Speed;
			if(Input.IsActionPressed(KeyCodeDown))
				Position += new Vector2(0, 1) * Speed;

			if(Position.Y > 120)
				Position = new Vector2(Position.X, 120);
			if(Position.Y < -120)
				Position = new Vector2(Position.X, -120);
		}
	}   
}

