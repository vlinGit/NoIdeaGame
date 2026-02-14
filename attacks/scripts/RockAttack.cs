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

    // Find a way so that this also rotates like it would if it was parented to the camera isntead of the world
    void handleIdlePosition(float delta)
    {
        GD.Print(player.camera.GlobalTransform.Basis);
        Quaternion fromBasis = new(Basis);
        Quaternion toBasis = new(player.camera.GlobalTransform.Basis);
        Quaternion newBasis = fromBasis.Slerp(toBasis, 0.5f);
        Basis = new Basis(newBasis);

        GlobalPosition = GlobalPosition.Lerp(idlePosition, speed/2 * delta);
    }

    public override void Move(float delta)
    {
        switch (state)
        {
            case 0:
                handleIdlePosition(delta);
                if (GlobalPosition.DistanceTo(idlePosition) < 1f)
                {
                    state = 1;
                }
                break;
            case 1:
                handleIdlePosition(delta);
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