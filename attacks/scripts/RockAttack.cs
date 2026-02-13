using Godot;
using System;

public partial class RockAttack : Attack
{
    public override void Enter (Player newPlayer)
    {
        base.Enter(newPlayer);
        player.camera.CallDeferred("add_child", this);
    }

    public override void Trigger()
    {
        base.Trigger();

        direction = -player.camera.GlobalTransform.Basis.Z;
        velocity = direction * speed;
        startPos = GlobalPosition;
        
        GetParent().RemoveChild(this);
        player.GetParent().AddChild(this);
        GlobalPosition = startPos;
    }


    public override void Move(float delta)
    {
        if (triggered){
            velocity.Y += -gravity * delta;
            GlobalPosition += velocity * delta;
            if (GlobalPosition.DistanceTo(startPos) > maxDistance)
            {
                Delete();
            }
        }
        else
        {
            base.Move(delta);
        }
    }
}