using Godot;

namespace BadBunnyGames.BomBun
{
	public partial class Restart : Node
	{
		private void _on_pressed()
		{
			GetTree().ReloadCurrentScene();
		}
	}
}