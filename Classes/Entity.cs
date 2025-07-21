using Godot;
using System;

public partial class Entity : CharacterBody2D
{
    [ExportCategory("State Machine")]
    [Export] public StateMachine StateMachine;
	
    [ExportCategory("Composition")]
    [Export] public Composition Composition;
}
