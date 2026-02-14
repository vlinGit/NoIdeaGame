using Godot;
using System;

public partial class RockAttack : Attack
{
    public override void Enter (Player newPlayer)
    {
        base.Enter(newPlayer);
        player.Owner.CallDeferred("add_child", this);
    }

    public override bool Trigger()
    {
        if (state == 1)
        {    
            state = 2;
            direction = -player.camera.GlobalTransform.Basis.Z;
            velocity = direction * speed;
            startPos = GlobalPosition;
            
            return true;
        }
        
        return false;
    }

    public override void Move(float delta)
    {
        switch (state)
        {
            case 0:
                Idle(delta);
                if (GlobalPosition.DistanceTo(idlePosition) < 1f)
                {
                    state = 1;
                }
                break;
            case 1:
                Idle(delta);
                break;
            case 2:
                RotateY(speed/2 * delta);
                RotateZ(speed/2 * delta);
                velocity.Y += -gravity * delta;
                GlobalPosition += velocity * delta;
                if (GlobalPosition.DistanceTo(startPos) > maxDistance)
                {
                    Delete();
                }
                break;
        }
    }
}