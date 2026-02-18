using Godot;
using Godot.Collections;
using System;

public partial class PlayerState : State
{
	[Signal]
	public delegate void TransitionedEventHandler(PlayerState state, string newStateName);

	public Dictionary<int, PackedScene> attackMap = new Dictionary<int, PackedScene>();

	public Attack attack;

	protected Player player;

    public override void _Ready()
    {
        player = GetTree().GetFirstNodeInGroup("Player") as Player ?? throw new InvalidProgramException("Player was null during PlayerState type cast");
    }   
}
