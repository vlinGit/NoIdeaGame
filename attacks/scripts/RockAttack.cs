using Godot;
using System;

// TODO:
// - Attack cooldown
// - Damage -> create an enemy
public partial class RockAttack : Attack
{
    Vector3 direction;
    Vector3 velocity;
    
    public override void Enter (Player newPlayer)
    {
        player = newPlayer ?? throw new InvalidProgramException("Failed Owner assignment to Player");
        direction = -player.camera.GlobalTransform.Basis.Z.Normalized();
        startPos = GlobalPosition;
        velocity = direction * speed;
    }

    public override void Move(float delta)
    {
        velocity.Y += -gravity * delta;
        GlobalPosition += velocity * delta;
        if (GlobalPosition.DistanceTo(startPos) > maxDistance)
        {
            Delete();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Move((float)delta);
    }
}