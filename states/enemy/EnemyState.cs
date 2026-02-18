using Godot;
using Godot.Collections;
using System;

public partial class EnemyState : State
{
	[Signal]
	public delegate void TransitionedEventHandler(EnemyState state, string newStateName);

	public Dictionary<int, PackedScene> attackMap = new Dictionary<int, PackedScene>();

	public Attack attack;

	protected EnemyPlayer player;

    public override void _Ready()
    {
        player = GetTree().GetFirstNodeInGroup("EnemyPlayer") as EnemyPlayer ?? throw new InvalidProgramException("Player was null during PlayerState type cast");
    }   
}
