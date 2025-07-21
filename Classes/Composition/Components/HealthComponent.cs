using Godot;
using System;

[GlobalClass]
public partial class HealthComponent : Component
{
    [Signal]
    public delegate void HealthChangedEventHandler(float currentHealth);
    
    [Export]
    public float MaxHealth
    {
        get => maxHealth;
        private set
        {
            maxHealth = value;
            if (CurrentHealth > currentHealth)
            {
                CurrentHealth = maxHealth;
                EmitChanged();
            }
        }
    }

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            currentHealth = value;
            if (currentHealth <= 0)
            {
                hasDied = true;
            }
            EmitChanged();
        }
    }
    
    private float currentHealth;
    private float maxHealth;
    private bool hasDied = false;

    public override void Initialize()
    {
        base.Initialize();
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            
        }
        
        GD.Print(currentHealth);
        EmitSignal(SignalName.HealthChanged, currentHealth);
    }
}
