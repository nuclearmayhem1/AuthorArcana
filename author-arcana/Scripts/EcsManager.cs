using Friflo;
using Friflo.Engine.ECS;
using Friflo.Engine.ECS.Systems;
using Godot;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Friflo.Json.Fliox.Transform.Query.Parser;

namespace AUTHORARCANA.ECS;

public partial class EcsManager : Node
{
	public static EcsManager Instance { get; private set; }

	public EntityStore World { get; private set; }
	public SystemRoot Root { get; private set; }
	public SystemGroup UpdateSystems { get; private set; }
	public SystemGroup FixedUpdateSystems { get; private set; }

	public Dictionary<Entity, List<IEntityDeleteListener>> interestList = new Dictionary<Entity, List<IEntityDeleteListener>>();

	public override void _EnterTree()
	{
		if (Instance == null || Instance != this) Instance = this;

		CreateWorld();
		InitializeSystems();
		InitializeEvents();
	}
	
	private void InitializeEvents()
	{
		World.OnEntityDelete += HandleEntityDeleteEvent;
	}

	private void HandleEntityDeleteEvent(EntityDelete entityDelete)
	{
		Entity entity = entityDelete.Entity;
		if (!interestList.TryGetValue(entity, out var listeners)) return;

		foreach (IEntityDeleteListener listener in listeners)
		{
			listener.OnDeleteEntity(entity);
		}

		interestList.Remove(entity);
	}

	public override void _Process(double delta)
	{
		UpdateSystems.Update(new UpdateTick((float)delta, Time.GetTicksMsec()));
	}

	public override void _PhysicsProcess(double delta)
	{
		FixedUpdateSystems.Update(new UpdateTick((float)delta, Time.GetTicksMsec()));
	}

	private void CreateWorld()
	{
		World = new EntityStore();
		World.EventRecorder.Enabled = true;
	}

	private static bool IsGameSystem(Type type)
	{
		while (type != null)
		{
			if (type.IsGenericType)
			{
				var generic = type.GetGenericTypeDefinition();

				if (generic == typeof(GameSystem<>)
				 || generic == typeof(GameSystem<,>)
				 || generic == typeof(GameSystem<,,>)
				 || generic == typeof(GameSystem<,,,>)
				 || generic == typeof(GameSystem<,,,,>))
				{
					return true;
				}
			}

			type = type.BaseType;
		}

		return false;
	}

	private void InitializeSystems()
	{
		Root = new SystemRoot(World);
		UpdateSystems = new SystemGroup("Update");
		FixedUpdateSystems = new SystemGroup("FixedUpdate");
		Root.Add(UpdateSystems);
		Root.Add(FixedUpdateSystems);
		UpdateSystems.Enabled = true;
		FixedUpdateSystems.Enabled = true;

		var systemTypes = typeof(WorldObjectSystem).Assembly.GetTypes()
			.Where(t => t.IsClass && !t.IsAbstract)
			.Where(IsGameSystem);
		GD.Print("Initializing systems");

		foreach (var item in systemTypes)
		{
			GD.Print(item.FullName);
		}

		foreach (var systemType in systemTypes)
		{
			if (systemType.GetCustomAttribute<Update>() != null)
			{
				BaseSystem gameSystem = Activator.CreateInstance(systemType) as BaseSystem;

				UpdateSystems.Add(gameSystem);
				GD.Print($"Added system: {systemType.FullName} to Update");

			}
			else if (systemType.GetCustomAttribute<FixedUpdate>() != null)
			{
				BaseSystem gameSystem = Activator.CreateInstance(systemType) as BaseSystem;

				FixedUpdateSystems.Add(gameSystem);
				GD.Print($"Added system: {systemType.FullName} to FixedUpdate");
			}
		}
		GD.Print("Finished initializing systems");
	}

	public void SubscribeDeleteEvent(IEntityDeleteListener system, Entity entity)
	{
		Dictionary<Entity, List<IEntityDeleteListener>> interests = EcsManager.Instance.interestList;

		if (!interests.ContainsKey(entity))
		{
			interests.Add(entity, new List<IEntityDeleteListener>());
		}

		if (!interests[entity].Contains(system))
		{
			interests[entity].Add(system);
		}

	}
		
}
