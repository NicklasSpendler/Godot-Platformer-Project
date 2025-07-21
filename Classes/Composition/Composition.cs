using Godot;
using System;
using Godot.Collections;

public enum ComponentName
{
    HealthComponent,
    DamageComponent,
};

[GlobalClass]
public partial class Composition : Resource
{
    [Export]
    private Dictionary<ComponentName, Component> Components;

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

    public Component GetComponent(ComponentName name)
    {
        return Components[name];
    }

    public bool IsComponentActive(ComponentName componentName)
    {
        return Components.ContainsKey(componentName);
    }
}
