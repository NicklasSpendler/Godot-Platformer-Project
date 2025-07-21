using Godot;
using System;

public partial class test : Area2D
{
	[Export] public Composition Composition;

	private HealthComponent HealthComponent;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		
		HealthComponent = (HealthComponent)Composition.Components[ComponentName.HealthComponent];
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Enemy enemy)
		{
			if (enemy.Components == null || enemy.Components?.Components[ComponentName.DamageComponent] == null)
			{
				return;
			}

			DamageComponent damageComponent = (DamageComponent)enemy.Components.Components[ComponentName.DamageComponent];
			HealthComponent.TakeDamage(damageComponent.DamageAmount);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
