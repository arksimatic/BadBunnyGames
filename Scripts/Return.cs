using Godot;

namespace BadBunnyGames
{
	public partial class Return : Button
	{
		private void _on_pressed()
		{
			GetTree().ChangeSceneToFile($"{GlobalPaths.MainMenu}/Scenes/main_scene.tscn");
		}
	}
}
