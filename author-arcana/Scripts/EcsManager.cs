using Godot;
using System;

public partial class EcsManager : Node
{
	public static EcsManager Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}

	public override void _Process(double delta)
	{

	}
}
