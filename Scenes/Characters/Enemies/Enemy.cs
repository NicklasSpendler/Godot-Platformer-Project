using Godot;
using System;

[GlobalClass]
public partial class Enemy : CharacterBody2D
{

	[ExportCategory("Movement")]
	[Export] public float MaxSpeed = 100.0f;
	[Export] public float Acceleration = 800.0f;
	[Export] public float Friction = 1000.0f;

	[Export] public float JumpVelocity = -400.0f;
	
	[ExportCategory("StateMachine")]
	[Export] public StateMachine StateMachine;
	
	[ExportCategory("Nodes")]
	[Export] public Sprite2D Sprite2D;
	[Export] public AnimationTree AnimationTree;
	public AnimationNodeStateMachinePlayback AnimationStateMachine;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		if (StateMachine == null)
		{
			GD.PrintErr("No StateMachine found!");
			return;
		}
		
		AnimationStateMachine = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");
		
		StateMachine.Parent = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
		StateMachine.CurrentState?.Update(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		
		StateMachine.CurrentState?.PhysicsUpdate(delta);
	}
}
