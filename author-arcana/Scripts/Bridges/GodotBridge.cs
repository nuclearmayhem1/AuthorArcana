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

    public Node3D GetOrCreateWorldObject(Entity entity)
    {
        if (entityNodes.ContainsKey(entity)) return entityNodes[entity];
        Node3D node = new Node3D();
        entityNodes[entity] = node;
        root.AddChild(node);
        GD.Print($"Instatiated worldobject: {entity}");
        return node;
    }

    public bool DestroyWorldObject(Entity entity)
    {
        if (!entityNodes.ContainsKey(entity)) return false;
        entityNodes[entity].QueueFree();
        entityNodes.Remove(entity);
        return true;
    }

    private Dictionary<Entity, Node3D> entityModels = new Dictionary<Entity, Node3D>();

    public Node3D GetOrCreateModel(Entity entity, ModelAsset asset)
    {
        if (entityModels.ContainsKey(entity)) return entityModels[entity];

        Node3D newModel = asset.GetModel().Instantiate<Node3D>();
        entityNodes[entity].AddChild(newModel);
        entityModels[entity] = newModel;
        GD.Print($"Instatiated model: {entity}");
        return newModel;
    }

    public bool DestroyModel(Entity entity)
    {
        if (!entityModels.ContainsKey(entity)) return false;
        entityModels[entity].QueueFree();
        entityModels.Remove(entity);
        return true;
    }

    public Node3D GetOrCreateModel(Entity entity, AnimatedModelAsset asset)
    {
        if (entityModels.ContainsKey(entity)) return entityModels[entity];

        Node3D newModel = asset.GetModel().Instantiate<Node3D>();
        entityNodes[entity].AddChild(newModel);
        entityModels[entity] = newModel;
        GD.Print($"Instatiated model: {entity}");
        return newModel;
    }
}
