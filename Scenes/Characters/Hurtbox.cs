using Godot;
using System;

public partial class Hurtbox : Area2D
{
	[Export]
	private Node2D _parent;

	private float _damage;
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void OnBodyEntered(Node body)
	{
		if (body is Enemy enemy)
		{
			GD.Print(enemy.Components.IsComponentActive(ComponentName.HealthComponent));
		}
		GD.Print(body.Name);
	}
}
