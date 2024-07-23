using Godot;
using System;
using System.Threading.Tasks;

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
