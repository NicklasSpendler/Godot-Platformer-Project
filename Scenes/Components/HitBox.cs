using Godot;
using System;

public partial class HitBox : Area2D
{
	public Entity Parent;
	public DamageComponent DamageComponent;

	public void Initialize(DamageComponent damageComponent, Entity parent)
	{
		Parent = parent;
		DamageComponent = damageComponent;
	}
}
