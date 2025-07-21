using Godot;
using System;

public partial class HitBox : Area2D
{
	public DamageComponent DamageComponent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//AreaEntered += OnAreaEntered;
	}

	public void Initialize(DamageComponent damageComponent)
	{
		DamageComponent = damageComponent;
	}


	private void OnAreaEntered(Area2D area)
	{
		Node Test = GetOwner();
		GD.Print(area);
	}
}
