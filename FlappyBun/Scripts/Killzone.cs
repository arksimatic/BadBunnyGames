using Godot;
using System;

public partial class Killzone : Node2D
{
	private void _on_body_entered(Node2D body)
	{
		if(body.Name == "Player")
		{
			GD.Print("Lose");
			GetTree().ReloadCurrentScene();
		}
	}
}



