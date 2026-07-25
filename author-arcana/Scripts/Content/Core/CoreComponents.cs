using Friflo.Engine.ECS;
using Friflo.Engine.ECS.Systems;
using Godot;
using System;
using System.ComponentModel;
using IComponent = Friflo.Engine.ECS.IComponent;

namespace AUTHORARCANA.CONTENT;

public struct WorldObject : IComponent
{
    /// <summary>
    /// The Godot Node3D representation of this entity.
    /// </summary>
    /// <remarks>
    /// Do not access directly. Use WorldObject methods instead to maintain ECS synchronization.
    /// </remarks>
    public Node3D node3D;
}

[Update]
public class WorldObjectSystem : GameSystem<WorldObject>
{
    protected override void OnMatch(ref WorldObject worldObject, Entity entity)
    {
        worldObject.node3D = GodotBridge.Instance.GetOrCreateWorldObject(entity);
    }
    protected override void OnUnmatch(ComponentType componentType, Entity entity)
    {
        GodotBridge.Instance.DestroyWorldObject(entity);
    }
    protected override void OnDelete(Entity entity)
    {
        GodotBridge.Instance.DestroyWorldObject(entity);
    }
}

public struct WorldTransform : IComponent
{

    public Authority authority;

    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public WorldTransform()
    {
        position = default;
        rotation = default;
        scale = Vector3.One;
    }

    public WorldTransform(Authority authority, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        this.authority = authority;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
    }
}

[Update]
public class TransformSystem : GameSystem<WorldTransform, WorldObject>
{
    protected override void Update(ref WorldTransform transform, ref WorldObject worldObject, Entity entity)
    {
        Node3D node = worldObject.node3D;

        if (transform.authority == Authority.Godot)
        {
            transform.position = node.Position;
            transform.rotation = node.Quaternion;
            transform.scale = node.Scale;
        }
        else if (transform.authority == Authority.ECS)
        {
            node.Position = transform.position;
            node.Quaternion = transform.rotation;
            node.Scale = transform.scale;
        }
    }
}

public enum Authority
{
    Godot,
    ECS
}

public struct Model : IComponent
{
    public ModelAsset asset;
    /// <summary>
    /// The Godot Node3D representation of this entity's model.
    /// </summary>
    /// <remarks>
    /// Do not access directly. Use ModelSystems methods instead to maintain ECS synchronization.
    /// </remarks>
    public Node3D node3D;

    public Model(ModelAsset asset)
    {
        this.asset = asset;
    }
    public Model(string name)
    {
        this.asset = new ModelAsset(name);
    }
}

[Update]
public class ModelSystem : GameSystem<Model,WorldObject>
{
    protected override void OnMatch(ref Model model, ref WorldObject worldObject, Entity entity)
    {
        model.node3D = GodotBridge.Instance.GetOrCreateModel(entity, model.asset);
    }
    protected override void OnUnmatch(ComponentType componentType, Entity entity)
    {
        GodotBridge.Instance.DestroyModel(entity);
    }
    protected override void OnDelete(Entity entity)
    {
        GodotBridge.Instance.DestroyModel(entity);
    }
}