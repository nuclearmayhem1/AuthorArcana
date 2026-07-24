using Friflo.Engine.ECS;
using Godot;
using System;
using System.Collections.Generic;

namespace AUTHORARCANA.BRIDGE;

public partial class GodotBridge : Node
{
    public static GodotBridge Instance { get; private set; }
    private Node root;

    public override void _EnterTree()
    {
        if (Instance == null || Instance != this) Instance = this;
        root = GetParent();
    }

    private Dictionary<Entity, Node3D> entityNodes = new Dictionary<Entity, Node3D>();

    public bool CreateWorldObject(Entity entity)
    {
        if (entityNodes.ContainsKey(entity)) return false;
        Node3D node = new Node3D();
        entityNodes[entity] = node;
        root.AddChild(node);
        GD.Print($"Instatiated worldobject: {entity}");
        return true;
    }

    public bool DestroyWorldObject(Entity entity)
    {
        if (!entityNodes.ContainsKey(entity)) return false;
        entityNodes[entity].QueueFree();
        entityNodes.Remove(entity);
        return true;
    }

}
