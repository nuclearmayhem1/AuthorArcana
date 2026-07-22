using Friflo;
using Friflo.Engine.ECS;
using Friflo.Engine.ECS.Systems;
using Godot;
using System;
using System.Linq;
using System.Reflection;
using AUTHORARCANA.ECS;

public partial class EcsManager : Node
{
	public static EcsManager Instance { get; private set; }

	public static EntityStore World { get; private set; }

	public static SystemRoot Root { get; private set; }

	public override void _Ready()
	{
		Instance = this;

		CreateWorld();
		InitializeSystems();
	}

	public override void _Process(double delta)
	{

	}

	private void CreateWorld()
	{
		World = new EntityStore();
	}

	private void InitializeSystems()
	{
		Root = new SystemRoot();
	}

}
