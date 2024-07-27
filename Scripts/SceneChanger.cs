using Godot;

namespace BadBunnyGames
{
	public partial class SceneChanger : Node
	{
		private void _on_pressed()
		{
			switch (Name)
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
				case "BoomBun":
					LoadBoomBun();
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
		private void LoadBoomBun()
		{
			GetTree().ChangeSceneToFile($"{GlobalPaths.BoomBun}/Scenes/main_scene.tscn");
		}
	}
}
