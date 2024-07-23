using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	public const float Speed = 50.0f;
	public const float JumpVelocity = -200.0f;

	private Boolean _isJumping = false;
	private AnimatedSprite2D _animatedSprite;

	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.Y += gravity * (float)delta;

		if (Input.IsActionJustPressed("ui_accept"))
		{
			velocity.Y = JumpVelocity;
			_isJumping = true;
			_animatedSprite.Play("jump");
		}

		Vector2 direction = Vector2.Right;
		if (direction != Vector2.Zero)
			velocity.X = direction.X * Speed;
		else
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);

		if(velocity.Y < 0 && _isJumping)
		{
			_isJumping = false;
			_animatedSprite.Play("fall");
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
