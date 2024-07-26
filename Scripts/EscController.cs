using Godot;

namespace BadBunnyGames
{
	public partial class EscController : Node2D
	{
		public override void _Process(double delta)
		{
			if (Input.IsActionPressed(KeyCode.Esc))
			{
				GetChild<Panel>(0).Visible = true;
			}
		}
	}
}
