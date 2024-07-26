using Godot;
using System;

public partial class SceneChanger : Node
{
	private void _on_pressed()
	{
		switch(Name)
		{
			case "FlappyBun":
				LoadFlappyBun();
				break;
			case "SnakeBun":
				LoadSnakeBun();
				break;
			case "BunTower":
				LoadBunTower();
				break;
			case "BomBun":
				LoadBomBun();
				break;
		}
	}

	private void LoadFlappyBun()
	{
		GetTree().ChangeSceneToFile($"{GlobalPaths.FlappyBun}/Scenes/main_scene.tscn");
	}
	private void LoadSnakeBun()
	{
		GetTree().ChangeSceneToFile($"{GlobalPaths.SnakeBun}/Scenes/main_scene.tscn");
	}
	private void LoadBunTower()
	{
		GetTree().ChangeSceneToFile($"{GlobalPaths.BunTower}/Scenes/main_scene.tscn");
	}
	private void LoadBomBun()
	{
		GetTree().ChangeSceneToFile($"{GlobalPaths.BomBun}/Scenes/main_scene.tscn");
	}
}
