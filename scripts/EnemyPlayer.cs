using Godot;
using Godot.Collections;
using System;

public partial class EnemyPlayer : Character	{

	[Export]
	public int MaxDash = 2;
	[Export]
	public float DashCooldown = 2.0f;
	[Export]
	public EnemyStateMachine stateMachine;
	[Export]
	public float attack_0Cooldown;
	[Export]
	public float attack_1Cooldown;

	public Dictionary<int, PackedScene> attackMap;
	public Dictionary<int, float> attackCooldownMap;
	public Attack attack;
	public int curAttack;
	public float attackTimer;
	public bool canAttack;
	public int dashCount;
	public float dashTimer;


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

	public void switchAttack()
	{
		canAttack = true;
		attackTimer = 0;
		attack.Delete();
		initAttack();
	}

	public void computeCanAttack(float delta)
	{
		if (canAttack)
		{
			return;
		}

		if (attackTimer <= 0)
		{
			canAttack = true;
			attackTimer = attackCooldownMap[curAttack];
		}
		else
		{
			attackTimer -= delta;
		}
	}

	public override void _Ready()
	{
		base._Ready();
		Input.MouseMode = Input.MouseModeEnum.Captured; //locks to window
		dashCount = MaxDash;
		dashTimer = DashCooldown;
		attackTimer = 0;
		attackMap = [];
		attackCooldownMap = [];
		canAttack = true;
		curAttack = 1;

		attackMap.Add(0, GD.Load<PackedScene>("res://attacks/rock.tscn"));
		attackMap.Add(1, GD.Load<PackedScene>("res://attacks/boulder.tscn"));
		attackCooldownMap.Add(0, attack_0Cooldown);
		attackCooldownMap.Add(1, attack_1Cooldown);
		initAttack();
	}

	public override void _PhysicsProcess(double delta)
	{
		computeCanAttack((float) delta);

		// if (canAttack && attack.Trigger())
		// {	
		// 	attackTimer = attackCooldownMap[curAttack];
		// 	canAttack = false;
		// 	initAttack();
		// }
	}
}
