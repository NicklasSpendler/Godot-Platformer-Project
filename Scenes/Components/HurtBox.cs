using Godot;
using System;

public partial class HurtBox : Area2D
{
	public Entity Parent;
	public HealthComponent HealthComponent;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}

	public void Initialize(HealthComponent healthComponent, Entity parent)
	{
		Parent = parent;
		HealthComponent = healthComponent;
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area is HitBox hitBox)
		{
			float Damage = hitBox.DamageComponent.DamageAmount;
			
			HealthComponent.TakeDamage(Damage);
			
			
			KnockbackState knockbackState = (KnockbackState)Parent.StateMachine.GetState(StateType.Knockback);
			if (Parent.StateMachine.CurrentState == knockbackState)
			{
				knockbackState.RepeatKnockback();
				return;
			}
			knockbackState.InitializeKnockback(hitBox, hitBox.DamageComponent);
			Parent.StateMachine.ChangeState(StateType.Knockback);
		}
	}
}
