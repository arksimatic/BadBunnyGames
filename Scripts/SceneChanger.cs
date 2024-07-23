using Godot;
using System;

public partial class SceneChanger : Node
{
	private void _on_pressed()
	{
		GetTree().ChangeSceneToFile($"{GlobalPaths.FlappyBun}/Scenes/main_scene.tscn");
	}
}

