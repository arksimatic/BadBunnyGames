using Godot;
using System;

public partial class Score : Label
{
	private CharacterBody2D player;
	public override void _Ready()
	{
		player = GetNode<CharacterBody2D>("%Player");
	}
	public override void _Process(double delta)
	{
		var score = Math.Floor(Math.Max(player.Position.X, 0) / 50);
		Text = $"{score}";
	}
}
