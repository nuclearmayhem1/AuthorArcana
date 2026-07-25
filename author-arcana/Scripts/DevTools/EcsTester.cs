using AUTHORARCANA.ECS;
using Godot;
using System;
using Friflo.Engine.ECS;
using AUTHORARCANA.CONTENT;

public partial class EcsTester : Node
{

	private Entity entity;
    private Entity entity2;
    private Entity entity3;
    private Entity entity4;

    public override void _Ready()
	{
		entity = EcsManager.Instance.World.CreateEntity(new WorldObject(), new WorldTransform());
        entity2 = EcsManager.Instance.World.CreateEntity(new WorldObject(), new WorldTransform()
        {
            position = new Vector3(0, 0, 0),
            rotation = Quaternion.Identity,
            scale = new(2, 2, 2),
            authority = Authority.ECS
        });
        entity3 = EcsManager.Instance.World.CreateEntity(new WorldObject(), new WorldTransform(), new AnimatedModel("Crate", true));
        entity4 = EcsManager.Instance.World.CreateEntity(new WorldObject(), new WorldTransform(), new AnimatedModel("Crate"));
    }

    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.K))
        {
            entity3.DeleteEntity();
            entity4.DeleteEntity();
        }
        if (Input.IsKeyPressed(Key.L))
        {
            AnimatedModelSystem.Play(entity4, "Animation2");
        }
    }
}
