using Godot;

namespace BadBunnyGames
{
	public partial class Back : Button
	{
		public void _on_pressed()
		{
			GetParent<Panel>().Hide();
			// get parent 

			// close menu
		}
	}
}
