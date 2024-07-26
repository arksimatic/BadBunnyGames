using Godot;
using System.Threading.Tasks;

namespace BadBunnyGames.FlappyBun
{
	public partial class TimeFreezer : Node2D
	{
		public override void _Ready()
		{
			Freeze();
		}
		async void Freeze()
		{
			Engine.TimeScale = 0f;
			await Task.Delay(1000);
			Engine.TimeScale = 1f;
		}
	}
}
