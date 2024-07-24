using Godot;
using System;

public partial class SceneChanger : Node
{
	private void _on_pressed()
	{
		if (Name == "FlappyBun")
		{
			LoadFlappyBun();
		}

		if (Name == "SnakeBun")
		{
			LoadSnakeBun();
		}
	}

	private void LoadSnakeBun()
	{
		GetTree().ChangeSceneToFile($"{GlobalPaths.SnakeBun}/Scenes/main_scene.tscn");
	}
	private void LoadFlappyBun()
	{
		GetTree().ChangeSceneToFile($"{GlobalPaths.FlappyBun}/Scenes/main_scene.tscn");
	}
}
