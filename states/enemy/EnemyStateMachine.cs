using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;

public partial class EnemyStateMachine : Node
{
	[Export]
	public EnemyState InitialState;

	public EnemyPlayer player;
	public EnemyState CurState;
	public Dictionary<string, EnemyState> StateDict;

	public override void _Ready()
	{
		StateDict = new Dictionary<string, EnemyState>();
		foreach (Node node in GetChildren())
		{
			if (node is EnemyState)
			{
				EnemyState playerState = (EnemyState)node;
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
	
	public void OnPlayerStateTransition(EnemyState state, string newStateName)
	{
		if (state != CurState)
		{
			return;
		}

		EnemyState newState = StateDict[newStateName.ToLower()];
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
