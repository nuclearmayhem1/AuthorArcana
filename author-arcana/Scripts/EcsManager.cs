using Friflo;
using Friflo.Engine.ECS;
using Friflo.Engine.ECS.Systems;
using Godot;
using System;
using System.Linq;
using System.Reflection;
using AUTHORARCANA.ECS;
using AUTHORARCANA.Scripts.ECS;

public partial class EcsManager : Node
{
	public static EcsManager Instance { get; private set; }

	public static EntityStore World { get; private set; }
	public static SystemRoot Root { get; private set; }
	public static SystemGroup UpdateSystems { get; private set; }
    public static SystemGroup FixedUpdateSystems { get; private set; }

    public override void _Ready()
	{
		Instance = this;

		CreateWorld();
		InitializeSystems();
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
    }

    private void InitializeSystems()
	{
		Root = new SystemRoot();
		UpdateSystems = new SystemGroup("Update");
		FixedUpdateSystems = new SystemGroup("FixedUpdate");
		Root.Add(UpdateSystems);
		Root.Add(FixedUpdateSystems);

		var systemTypes = Assembly.GetExecutingAssembly()
			.GetTypes()
			.Where(t => t.IsClass && !t.IsAbstract)
			.Where(t => t.IsSubclassOf(typeof(GameSystem)));

        foreach (var systemType in systemTypes)
        {
            if (!typeof(BaseSystem).IsAssignableFrom(systemType)) continue;
			if (systemType.GetCustomAttribute<Update>() != null)
			{
				GameSystem gameSystem = Activator.CreateInstance(systemType) as GameSystem;

                UpdateSystems.Add(gameSystem);

            }
			else if (systemType.GetCustomAttribute<FixedUpdate>() != null)
			{
				FixedUpdateSystems.Add(Activator.CreateInstance(systemType) as GameSystem);
            }
        }
    }
}
