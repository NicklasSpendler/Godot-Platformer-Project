using Godot;
using System;

public partial class HurtBox : Area2D
{
	public HealthComponent HealthComponent;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}

	public void Initialize(HealthComponent healthComponent)
	{
		HealthComponent = healthComponent;
	}

	private void OnAreaEntered(Area2D area)
	{
		Node Test = GetOwner();
		GD.Print(area.Name);
		if (area is HitBox hitBox)
		{
			float Damage = hitBox.DamageComponent.DamageAmount;
			
			HealthComponent.TakeDamage(Damage);
		}
	}
}
