	using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[ExportCategory("Movement")]
	[Export]
	public float MaxSpeed = 300.0f;
	[Export]
	public float Acceleration = 800.0f;
	[Export]
	public float JumpVelocity = -400.0f;
	[Export]
	public float Friction = 1000.0f;

	[ExportCategory("Combat")]
	[Export] public float Damage = 10.0f;

	[ExportCategory("State Machine")]
	[Export] public StateMachine StateMachine;
	
	[ExportCategory("Nodes")]
	[Export] public AnimationPlayer AnimationPlayer;
	
	[Export] public AnimationTree AnimationTree;

	public AnimationNodeStateMachinePlayback AnimationStateMachine;
		
	[Export] public Sprite2D Sprite2D;
	
	[Export] public CollisionShape2D Hurtbox;
	
	Vector2 velocity = Vector2.Zero;

	public override void _Ready()
	{
		base._Ready();
		if (StateMachine == null)
		{
			GD.PrintErr("No state machine found!");
			return;
		}

		if (AnimationPlayer == null)
		{
			GD.PrintErr("No animation player found!");
			return;
		}
		AnimationStateMachine = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");
		
		StateMachine.Parent = this;

		if (IsOnFloor())
		{
			StateMachine.ChangeState(StateType.PlayerIdle);
		}
		else
		{
			StateMachine.ChangeState(StateType.PlayerFall);
		}
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		StateMachine.CurrentState.Update(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		
		velocity = Velocity;
		
		StateMachine.CurrentState.PhysicsUpdate(delta);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
		if (@event.IsActionPressed("quit"))
		{
			GetTree().Quit();
		}
		
		StateMachine.CurrentState.HandleInput(@event);
	}

	public void HandleMovement(double delta)
	{
		MoveAndSlide();
	}

	public void HandleMovementInput(double delta)
	{
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			//velocity.X = direction.X * HandleSpeed;
			velocity.X = Mathf.MoveToward(velocity.X, direction.X * MaxSpeed, Acceleration * (float)delta);
		}
		else
		{
			velocity.X = Mathf.MoveToward(velocity.X, 0, Friction * (float)delta);
		}

		Velocity = velocity;
	}

	public void HandleJump()
	{
		velocity.Y = JumpVelocity;
		Velocity = velocity;
	}
}
