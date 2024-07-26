using Godot;

public partial class Restart : Node
{
	private void _on_pressed()
	{
		GetTree().ReloadCurrentScene();
	}
}
