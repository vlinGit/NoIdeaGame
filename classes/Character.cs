using Godot;
using System;

public partial class Character : CharacterBody3D
{
	[Export]
	public int Health = 100;
	[Export]
	public int MaxHealth = 100;

	[Export]
	public float Speed = 5.0f;
	[Export]
	public float JumpVelocity = 4.5f;
	[Export]
	public float SlamVelocity = 20.0f;
	[Export]
	public float DashDistanceBase = 10.0f;

	[Export]
	public float groundFriction = 0.5f;
	[Export]
	public float airFriction = 0.05f;
	
	[Export]
	public float mouseSense = 0.003f;
	
	[Export]
	public Camera3D camera;

	[Export]
	public PackedScene scene;

	[Export]
	public RayCast3D ray;

	private float pitch = 0.0f;
	private float minPitch = -90.0f;
	private float maxPitch = 90.0f;
	private Vector3 velocity;
	
	private Vector3 calcDirectionVector(Vector2 inputDir)
	{
		return (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
	}

	public void Dash(Vector2 inputDir)
	{
		Vector3 direction = calcDirectionVector(inputDir).Normalized();
		float dashDist = IsOnFloor() ? DashDistanceBase * 1.5f : DashDistanceBase;

		if (direction.LengthSquared() < 0.0001f)
		{
			direction = -camera.GlobalTransform.Basis.Z;
			velocity.X = direction.X * dashDist;
			velocity.Z = direction.Z * dashDist;
		}
		else
		{
			velocity.X = direction.X * dashDist;
			velocity.Z = direction.Z * dashDist;
		}
	}

	public void moveCamera(InputEventMouseMotion mouseMotion)
	{
		// horizontal
		RotateY(-mouseMotion.Relative.X * mouseSense);
		
		// vertical
		pitch -= mouseMotion.Relative.Y * mouseSense;
		pitch = Mathf.Clamp(pitch, Mathf.DegToRad(minPitch), Mathf.DegToRad(maxPitch));
		camera.Rotation = new Vector3(pitch, 0, 0);
	}

	public void Jump()
	{
		velocity.Y = JumpVelocity;
		updateVelocity();
	}

	public void MoveGround(Vector2 inputDir)
	{
		Vector3 direction = calcDirectionVector(inputDir);
		velocity.X = Mathf.MoveToward(Velocity.X, direction.X * Speed, groundFriction);
		velocity.Z = Mathf.MoveToward(Velocity.Z, direction.Z * Speed, groundFriction);
	}

	public void MoveAir(Vector2 inputDir)
	{
		Vector3 direction = calcDirectionVector(inputDir);
		velocity.X = Mathf.MoveToward(Velocity.X, direction.X * Speed, airFriction*2);
		velocity.Z = Mathf.MoveToward(Velocity.Z, direction.Z * Speed, airFriction*2);
	}

	public void MoveZeroGround()
	{
		velocity.X = Mathf.MoveToward(Velocity.X, 0, groundFriction);
		velocity.Z = Mathf.MoveToward(Velocity.Z, 0, groundFriction);
	}

	public void MoveZeroAir()
	{
		velocity.X = Mathf.MoveToward(Velocity.X, 0, airFriction);
		velocity.Z = Mathf.MoveToward(Velocity.Z, 0, airFriction);
	}

	public void HandlePhysics(float delta)
	{
		if (!IsOnFloor())
		{
			velocity += GetGravity() * delta;
		}
	}

	public void updateVelocity()
	{
		Velocity = velocity;
		MoveAndSlide();
	}

	public void slam()
	{
		velocity.Y = -SlamVelocity;
	}

	public override void _Ready()
	{
		velocity = Velocity;
	}
}
