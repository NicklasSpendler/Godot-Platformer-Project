using Godot;
using System;
using Godot.Collections;

public enum ComponentName
{
    HealthComponent
};

[GlobalClass]
public partial class Composition : Resource
{
    [Export]
    public Dictionary<ComponentName, Component> Components;

    private Node Parent;
    
    public void InitializeComponents(Node parent)
    {
        Parent = parent;
        foreach (var component in Components)
        {
            if (component.Value == null)
                return;
            
            if (component.Value is Component curComponent)
            {
                curComponent.Parent = parent;
                curComponent.Initialize();
            }
        }
    }

    public bool IsComponentActive(ComponentName componentName)
    {
        return Components.ContainsKey(componentName);
    }
}
