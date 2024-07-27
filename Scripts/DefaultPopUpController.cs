using Godot;

namespace BadBunnyGames
{
	public partial class DefaultPopUpController : CanvasLayer
	{
		public override void _Ready()
		{
			ProcessMode = Node.ProcessModeEnum.Always;
		}
		public override void _Process(double delta)
		{
			if (Input.IsActionJustPressed(KeyCode.Esc) && Name == "Pause")
			{
				if(GetTree().Paused)
				{
					GetTree().Paused = false;
					GetChild<PanelContainer>(0).Hide();
				}
				else
				{
					GetTree().Paused = true;
					GetChild<PanelContainer>(0).Show();
				}
			}
		}

		private void _on_back_pressed()
		{
			GetTree().Paused = false;
			GetChild<PanelContainer>(0).Hide();
		}

		private void _on_restart_pressed()
		{
			GetTree().Paused = false;
			GetTree().ReloadCurrentScene();
		}

		private void _on_menu_pressed()
		{
			GetTree().Paused = false;
			GetTree().ChangeSceneToFile($"{GlobalPaths.MainMenu}/Scenes/main_scene.tscn");
		}
	}
}
