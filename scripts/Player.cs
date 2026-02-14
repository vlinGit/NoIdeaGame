using Godot;
using Godot.Collections;
using System;

public partial class Player : Character	{

	[Export]
	public int MaxDash = 2;
	[Export]
	public float DashCooldown = 2.0f;
	[Export]
	public PlayerStateMachine stateMachine;

	public Dictionary<int, PackedScene> attackMap = new Dictionary<int, PackedScene>();
	public Attack attack;
	public int curAttack;
	public int dashCount;
	public float dashTimer;
	public double delta;

	public Vector2 getInputVector()
	{
		return Input.GetVector("move_left", "move_right", "move_forward", "move_back");
	}

	public bool canDash()
	{
		if (dashCount > 0)
		{
			return true;
		}
		return false;
	}
	public void computeDash(float delta)
	{
		if (dashTimer <= 0)
		{
			dashCount = Math.Min(dashCount+=1, MaxDash);
			dashTimer = DashCooldown;
			// GD.Print(dashCount);
		}
		else
		{
			dashTimer -= delta;
		}
	}

	public void initAttack()
	{
		attack = attackMap[curAttack].Instantiate() as Attack;
		attack.Enter(this);
	}

	public override void _Ready()
	{
		base._Ready();
		Input.MouseMode = Input.MouseModeEnum.Captured; //locks to window
		dashCount = MaxDash;
		dashTimer = DashCooldown;

		attackMap.Add(0, GD.Load<PackedScene>("res://attacks/rock.tscn"));
		initAttack();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
		{
			moveCamera(mouseMotion);
		}

		if (@event.IsActionPressed("ui_cancel"))
		{
			if (Input.MouseMode == Input.MouseModeEnum.Captured)
			{
				Input.MouseMode = Input.MouseModeEnum.Visible;
			}
			else
			{
				Input.MouseMode = Input.MouseModeEnum.Captured;
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		// this.delta = delta;

		// HandlePhysics(delta);
		// Move(getInputVector());
		// computeDash();
	}
}
