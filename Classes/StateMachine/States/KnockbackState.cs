using Godot;
using System;

[GlobalClass]
public partial class KnockbackState : State
{
    [Export] public string AnimationName;

    private Area2D _damageSource;
    private DamageComponent _damageComponent;
    
    public override void Enter(State PreviousState = null)
    {
        if (Parent is Enemy enemy)
        {
            enemy.AnimationStateMachine.Travel("idle");
        }
        else if (Parent is Player player)
        {
            player.AnimationStateMachine.Travel("idle");
        }
    }

    public override void Exit(State NextState = null)
    {
        base.Exit(NextState);
        _damageSource = null;
        _damageComponent = null;
    }

    public void InitializeKnockback(Area2D damageSource, DamageComponent damageComponent)
    {
        _damageSource = damageSource;
        _damageComponent = damageComponent;
    }
    
    public void Knockback()
    {
        //Make knockback logic here, or maybe I have to actually implement Physics logic... A method like this does not work
    }
}
