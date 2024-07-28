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
					HidePopUp();
				else
					ShowPopUp();
			}
		}
		public void ShowPopUp()
		{
			GetTree().Paused = true;
			GetChild<PanelContainer>(0).Show();
		}
		public void HidePopUp()
		{
			GetTree().Paused = false;
			GetChild<PanelContainer>(0).Hide();
		}
		private void _on_back_pressed()
		{
			HidePopUp();
		}

		private void _on_restart_pressed()
		{
			HidePopUp();
			GetTree().ReloadCurrentScene();
		}

		private void _on_menu_pressed()
		{
			HidePopUp();
			GetTree().ChangeSceneToFile($"{GlobalPaths.MainMenu}/Scenes/main_scene.tscn");
		}
	}
}
