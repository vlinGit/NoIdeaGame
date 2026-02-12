using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;

public partial class PlayerStateMachine : Node
{
	[Export]
	public PlayerState InitialState;

	public Player player;
	public PlayerState CurState;
	public Dictionary<string, PlayerState> StateDict;

	public override void _Ready()
	{
		StateDict = new Dictionary<string, PlayerState>();
		foreach (Node node in GetChildren())
		{
			if (node is PlayerState)
			{
				PlayerState playerState = (PlayerState)node;
				string stateName = node.Name.ToString().ToLower();
				StateDict.Add(node.Name.ToString().ToLower(), playerState);
				playerState.Transitioned += OnPlayerStateTransition;
			}
		}

		if (InitialState != null)
		{
			InitialState.Enter();
			CurState = InitialState;
		}
	}

    public override void _Input(InputEvent @event)
    {
        CurState.HandleInputs(@event);
    }

	public void OnPlayerStateTransition(PlayerState state, string newStateName)
	{
		if (state != CurState)
		{
			return;
		}

		PlayerState newState = StateDict[newStateName.ToLower()];
		if (newState == null)
		{
			return;
		}

		if (CurState != null)
		{
			CurState.Exit();
		}

		newState.Enter();
		CurState = newState;
	}

	public override void _Process(double delta)
	{
		CurState?.Update((float)delta);
	}

    public override void _PhysicsProcess(double delta)
    {
		CurState?.PhysicsUpdate((float)delta);
    }

}
