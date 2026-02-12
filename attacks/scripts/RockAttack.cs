using Godot;
using System;

public partial class RockAttack : Attack
{
    Vector3 direction;
    
    public override void Enter (Player newPlayer)
    {
        player = newPlayer ?? throw new InvalidProgramException("Failed Owner assignment to Player");
        direction = -player.camera.GlobalTransform.Basis.Z.Normalized();
        startPos = GlobalPosition;
    }

    // TODO: add gravity
    public override void Move(float delta)
    {
        GlobalPosition += direction * speed * delta;
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