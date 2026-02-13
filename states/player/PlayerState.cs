using Godot;
using Godot.Collections;
using System;

public partial class PlayerState : State
{
	[Signal]
	public delegate void TransitionedEventHandler(PlayerState state, string newStateName);

	public Dictionary<int, PackedScene> attackMap = new Dictionary<int, PackedScene>();

	public int curAttack = 0;
	public Attack attack;

	protected Player player;

	void initAttack()
	{
		attack = attackMap[curAttack].Instantiate() as Attack;
		attack.Enter(player);
	}

    public override void HandleInputs(InputEvent @event)
    {
		if (@event.IsActionPressed("move_jump"))
		{
		    player.Jump();
		}

        if (@event.IsActionPressed("attack"))
		{	
			attack.Trigger();
			initAttack();
		}
    }

    public override void _Ready()
    {
        player = GetTree().GetFirstNodeInGroup("Player") as Player ?? throw new InvalidProgramException("Player was null during PlayerState type cast");

		attackMap.Add(0, GD.Load<PackedScene>("res://attacks/rock.tscn"));
		initAttack();
    }   
}
