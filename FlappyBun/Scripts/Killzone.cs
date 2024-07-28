using Godot;

namespace BadBunnyGames.FlappyBun
{
	public partial class Killzone : Node2D
	{
		private void _on_body_entered(Node2D body)
		{
			if (body.Name == "Player")
			{
				// It's quite a bad practice, but I have no better idea to escape from "same-scene only" unique nodes
				GetParent().GetNode<Addons>("%Addons")._on_game_over();
			}
		}
	}
}
