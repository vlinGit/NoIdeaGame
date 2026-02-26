using Godot;
using System;

public partial class RockAttack : Attack
{
    public override void Enter (Character newCharacter)
    {
        base.Enter(newCharacter);
        character.Owner.CallDeferred("add_child", this);
    }

    public override void Trigger()
    {
        state = 1;
        direction = -character.camera.GlobalTransform.Basis.Z;
        velocity = direction * speed;
        startPos = GlobalPosition;

        if (character.ray.IsColliding())
        {
            endPos = character.ray.GetCollisionPoint();
        }
        else
        {
            endPos = startPos + (direction * maxDistance);
        }
    }

    public override void Move(float delta)
    {   
        switch (state)
        {
            case 0:
                Idle(delta);
                break;
            case 1:
                shoot(delta);
                break;
        }
    }

    private void shoot(float delta)
    {
        RotateY(speed/2 * delta);
        RotateZ(speed/2 * delta);

        GlobalPosition = GlobalPosition.MoveToward(endPos, speed * delta);
        
        if (GlobalPosition.DistanceTo(startPos) > maxDistance || GlobalPosition.DistanceTo(endPos) <= 0.01f)
        {
            Delete();
        }
    }
}