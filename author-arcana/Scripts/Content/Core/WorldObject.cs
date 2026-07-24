using AUTHORARCANA.BRIDGE;
using AUTHORARCANA.ECS;
using Friflo.Engine.ECS;
using Friflo.Engine.ECS.Systems;
using Godot;
using System.ComponentModel;
using IComponent = Friflo.Engine.ECS.IComponent;

namespace AUTHORARCANA.CONTENT;

public struct WorldObject : IComponent { }

[Update]
public class WorldObjectSystem : GameSystem<WorldObject>
{
    protected override void OnMatch(ref WorldObject component, Entity entity)
    {
        GodotBridge.Instance.CreateWorldObject(entity);
    }
    protected override void OnUnmatch(ComponentType componentType, Entity entity)
    {
        GodotBridge.Instance.DestroyWorldObject(entity);
    }
}