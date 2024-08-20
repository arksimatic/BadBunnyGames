using Godot;

namespace BadBunnyGames
{
	public partial class Addons : CanvasLayer
	{
		public void _on_game_over()
		{
			GetNode<DefaultPopUpController>("GameOver").ShowPopUp();
		}
		public void _on_game_win()
		{
			GetNode<DefaultPopUpController>("Win").ShowPopUp();
		}
	}
}
