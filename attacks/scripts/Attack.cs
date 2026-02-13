using System;
using Godot;

public partial class Attack: Area3D
{
    [Export]
    public float speed;
    [Export]
    public float maxDistance;
    [Export]
    public float gravity = 1.0f;
    [Export]
    Vector3 offset;

    protected Player player; 
    public Vector3 startPos;
    public bool triggered = false;
    public Vector3 direction;
    public Vector3 velocity;

    public virtual void Enter(Player newPlayer)
    {
        player = newPlayer ?? throw new InvalidProgramException("Failed Owner assignment to Player");
    }

    public virtual void Trigger()
    {
        triggered = true;
        startPos = GlobalPosition;
    }

    public virtual void Move(float delta)
    {
        Vector3 rotatedOffset = player.camera.GlobalTransform.Basis * offset;
        GlobalPosition = player.GlobalPosition + rotatedOffset;
    }
    
    public virtual void Collide(Node3D body)
    {
        if (triggered && player != null && player != body)
        { 
            Delete();
        }
    }

    public virtual void Delete()
    {
        QueueFree();
    }

    public override void _PhysicsProcess(double delta)
    {
        Move((float)delta);
    }
}