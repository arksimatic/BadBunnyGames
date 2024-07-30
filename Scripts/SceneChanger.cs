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
				case "BunThree":
					LoadBunThree();
					break;
				case "BunInvaders":
					LoadBunInvaders();
					break;
				case "BunPong":
					LoadBunPong();
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
		private void LoadBunThree()
		{
			GetTree().ChangeSceneToFile($"{GlobalPaths.BunThree}/Scenes/main_scene.tscn");
		}
		private void LoadBunInvaders()
		{
			GetTree().ChangeSceneToFile($"{GlobalPaths.BunInvaders}/Scenes/main_scene.tscn");
		}
		private void LoadBunPong()
		{
			GetTree().ChangeSceneToFile($"{GlobalPaths.BunPong}/Scenes/main_scene.tscn");
		}
	}
}
