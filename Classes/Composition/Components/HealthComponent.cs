using Godot;
using System;

[GlobalClass]
public partial class HealthComponent : Component
{
    [Export] private float _maxHealth;
    private float _health;

    public override void Initialize()
    {
        base.Initialize();
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, _maxHealth);

        if (_health <= 0)
        {
            
        }
    }
}
