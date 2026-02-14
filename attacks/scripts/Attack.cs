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
    public Vector3 startOffset; // offset when the object is initially loaded, used for the pull out animation
    [Export]
    public Vector3 offset; // offset for idle position, idle is the state after the pull out animation

    protected Player player; 
    public Vector3 idlePosition;
    public Vector3 startPos;
    public Vector3 direction;
    public Vector3 velocity;

    // 0 -> entering
    // 1 -> idle
    // 2 -> fired
    public int state = 0; 

    public virtual void Enter(Player newPlayer)
    {
        player = newPlayer ?? throw new InvalidProgramException("Failed Owner assignment to Player");
    }

    public virtual bool Trigger(){ return true; }
    
    public virtual void Idle()
    {
        Vector3 rotatedOffset = player.camera.GlobalTransform.Basis * offset;
        idlePosition = player.GlobalPosition + rotatedOffset;
    }

    public virtual void Move(float delta){}
    
    public virtual void Collide(Node3D body)
    {
        if (state == 2 && player != null && player != body)
        { 
            Delete();
        }
    }

    public virtual void Delete()
    {
        state = 3;
        QueueFree();
    }

    public override void _Ready()
    {
        Vector3 rotatedOffset = player.camera.GlobalTransform.Basis * startOffset;
        GlobalPosition = player.GlobalPosition + rotatedOffset;
    }

    public override void _PhysicsProcess(double delta)
    {
        Idle();
        Move((float)delta);
    }
}