using Godot;

namespace BadBunnyGames.BunPong
{
	public partial class ScoreArea : Node2D
	{
		private void _on_body_entered(Node2D area)
		{
			if(area.IsInGroup("ball"))
				GetNode<Addons>("%Addons")._on_game_over();
		}
	}
}
