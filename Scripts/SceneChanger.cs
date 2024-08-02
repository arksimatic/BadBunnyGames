using Godot;
using System;

namespace BadBunnyGames
{
	public partial class SceneChanger : Node
	{
		private void _on_pressed()
		{
			String pathToScene;

			switch (Name)
			{
				case "FlappyBun":
					pathToScene = GlobalPaths.FlappyBun;
					break;
				case "SnakeBun":
					pathToScene = GlobalPaths.SnakeBun;
					break;
				case "BunTower":
					pathToScene = GlobalPaths.BunTower;
					break;
				case "BoomBun":
					pathToScene = GlobalPaths.BoomBun;
					break;
				case "BunThree":
					pathToScene = GlobalPaths.BunThree;
					break;
				case "BunInvaders":
					pathToScene = GlobalPaths.BunInvaders;
					break;
				case "BunPong":
					pathToScene = GlobalPaths.BunPong;
					break;
				default:
					pathToScene = null;
					break;
			}

			if (pathToScene != null)
				GetTree().ChangeSceneToFile($"{pathToScene}/Scenes/main_scene.tscn");
		}
	}
}
