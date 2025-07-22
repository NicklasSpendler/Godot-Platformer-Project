using Godot;
using System;

[GlobalClass]
public partial class DamageComponent : Component
{
    [Export] public float DamageAmount;

    [Export] public float KnockbackHorizontalStrength;
    [Export] public float KnockbackVerticalStrength;
}
