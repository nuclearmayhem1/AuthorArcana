using AUTHORARCANA.ECS;
using Godot;
using System;
using Friflo.Engine.ECS;
using AUTHORARCANA.CONTENT;

public partial class EcsTester : Node
{
	public override void _Ready()
	{
		EcsManager.Instance.World.CreateEntity(new WorldObject());
	}
	public override void _Process(double delta)
	{
	}
}
