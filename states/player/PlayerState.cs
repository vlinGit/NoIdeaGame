using Godot;
using Godot.Collections;
using System;

public partial class PlayerState : State
{
	[Signal]
	public delegate void TransitionedEventHandler(PlayerState state, string newStateName);

	public Dictionary<int, PackedScene> attackMap = new Dictionary<int, PackedScene>();

	public int curAttack = 0;

	protected Player player;

    public override void HandleInputs(InputEvent @event)
    {
		if (@event.IsActionPressed("move_jump"))
		{
		    player.Jump();
		}

        if (@event.IsActionPressed("attack"))
		{	
			var attack = attackMap[curAttack].Instantiate() as Attack;
			GetTree().Root.AddChild(attack);
			attack.GlobalPosition = player.GlobalPosition;
			attack.Enter(player);
		}
    }


    public override void _Ready()
    {
        player = GetTree().GetFirstNodeInGroup("Player") as Player;

		attackMap.Add(0, GD.Load<PackedScene>("res://attacks/rock.tscn"));
		
		if (player == null)
		{
			throw new InvalidProgramException("Player was null during PlayerState type cast");
		}
    }   
}
