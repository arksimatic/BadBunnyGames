using Godot;

namespace BadBunnyGames
{
	public partial class DefaultPopUpController : CanvasLayer
	{
		[Export] public PanelContainer PopUpPanel;
		public override void _Ready()
		{
			ProcessMode = Node.ProcessModeEnum.Always;
		}
		public override void _Process(double delta)
		{
			if (Input.IsActionJustPressed(KeyCode.Esc) && Name == "Pause")
				ShowHidePopUp();
		}
		public void ShowHidePopUp()
		{
			if (GetTree().Paused)
				HidePopUp();
			else
				ShowPopUp();
		}
		public void ShowPopUp()
		{
			GetTree().Paused = true;
			PopUpPanel.Show();
		}
		public void HidePopUp()
		{
			GetTree().Paused = false;
			PopUpPanel.Hide();
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
			GetTree().ChangeSceneToFile($"{GlobalPaths.MainMenu}/main_scene.tscn");
		}
	}
}
