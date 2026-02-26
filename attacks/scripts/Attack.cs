using System;
using System.ComponentModel;
using Godot;

public partial class Attack: Area3D
{
    [Export]
    public float speed;
    [Export]
    public float maxDistance;
    [Export]
    public float gravity = 1f;
    [Export]
    public Vector3 startOffset; // offset when the object is initially loaded, used for the pull out animation
    [Export]
    public Vector3 offset; // offset for idle position, idle is the state after the pull out animation
    [Export]
    public float blendFactorIdleRotation;
    [Export]
    public Vector3 offsetIdleRotation;
    [Export]
    public float idleSpeed;

    protected Character character; 
    public Vector3 idlePosition;
    public Vector3 startPos;
    public Vector3 endPos;
    public Vector3 direction;
    public Vector3 velocity;

    // 0 -> idle
    // 1 -> fired
    public int state;

    public virtual void Enter(Character newCharacter)
    {
        character = newCharacter ?? throw new InvalidProgramException("Failed Owner assignment to Player");
        state = 0;
    }

    public virtual void Trigger(){ }
    
    public virtual void Idle(float delta)
    {
        Vector3 cameraEuler = character.camera.GlobalTransform.Basis.GetEuler();
        Vector3 currentEuler = GlobalRotation;
        currentEuler.X = Mathf.LerpAngle(currentEuler.X, cameraEuler.X, blendFactorIdleRotation);
        currentEuler.Y = Mathf.LerpAngle(currentEuler.Y, cameraEuler.Y, blendFactorIdleRotation);
        currentEuler.Z = Mathf.LerpAngle(currentEuler.Z, cameraEuler.Z, blendFactorIdleRotation);
        currentEuler.X += offsetIdleRotation.X * delta;
        currentEuler.Y += offsetIdleRotation.Y * delta;
        currentEuler.Z += offsetIdleRotation.Z * delta;

        GlobalRotation = currentEuler;
        GlobalPosition = GlobalPosition.Lerp(idlePosition, 1 - Mathf.Exp(-idleSpeed * delta));
    }

    public virtual void Move(float delta){}
    
    public virtual void Collide(Node3D body)
    {
        if (state == 1 && character != null && character != body)
        { 
            Delete();
        }
    }

    public virtual void Delete()
    {
        QueueFree();
    }

    public override void _Ready()
    {
        Vector3 rotatedOffset = character.camera.GlobalTransform.Basis * startOffset;
        GlobalPosition = character.GlobalPosition + rotatedOffset;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 rotatedOffset = character.camera.GlobalTransform.Basis * offset;
        idlePosition = character.GlobalPosition + rotatedOffset;

        Move((float)delta);
    }
}