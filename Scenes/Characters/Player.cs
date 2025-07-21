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
	public float Friction = 1000.0f;
	
	[Export]
	public float JumpVelocity = -400.0f;

	[ExportCategory("Combat")]
	[Export] public float Damage = 10.0f;

	[ExportCategory("State Machine")]
	[Export] public StateMachine StateMachine;
	
	[ExportCategory("Components")]
	[Export] public Composition Components;
	
	[ExportCategory("Nodes")]
	[Export] public AnimationPlayer AnimationPlayer;
	
	[Export] public AnimationTree AnimationTree;

	public AnimationNodeStateMachinePlayback AnimationStateMachine;
		
	[Export] public Sprite2D Sprite2D;
	
	Vector2 velocity = Vector2.Zero;

	public override void _Ready()
	{
		base._Ready();
		if (StateMachine == null)
		{
			GD.Print("No state machine found!");
		}

		if (AnimationPlayer == null)
		{
			GD.Print("No animation player found!");
		}

		if (Components == null)
		{
			GD.Print(Name ,"No components found!");
		}
		
		Components.InitializeComponents(this);
		
		AnimationStateMachine = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");
		
		StateMachine.InitializeStateMachine(this);

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
	
	public void HandleJump()
	{
		velocity.Y = JumpVelocity;
		Velocity = velocity;
	}
}
