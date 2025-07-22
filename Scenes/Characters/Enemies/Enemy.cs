using Godot;
using System;

[GlobalClass]
public partial class Enemy : Entity
{

	[ExportCategory("Movement")]
	[Export] public float MaxSpeed = 100.0f;
	[Export] public float Acceleration = 800.0f;
	[Export] public float Friction = 1000.0f;

	[Export] public float JumpVelocity = -400.0f;
	
	[ExportCategory("Nodes")]
	[Export] public Sprite2D Sprite2D;
	[Export] public AnimationTree AnimationTree;
	[Export] public HurtBox HurtBox;
	[Export] public HitBox HitBox;
	
	public AnimationNodeStateMachinePlayback AnimationStateMachine;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		if (StateMachine == null)
		{
			GD.PrintErr("No StateMachine found!");
		}

		if (Composition == null)
		{
			GD.PrintErr(Name ,"No Composition found!");
		}
		
		Composition?.InitializeComponents(this);
		
		HurtBox?.Initialize((HealthComponent)Composition?.GetComponent(ComponentName.HealthComponent), this);
		HitBox?.Initialize((DamageComponent)Composition?.GetComponent(ComponentName.DamageComponent), this);
		
		
		AnimationStateMachine = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");
		
		StateMachine.InitializeStateMachine(this);
		
		Composition.InitializeComponents(this);
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
