using System;
using Godot;

public partial class Move : PlayerState
{
    public override void HandleInputs(InputEvent @event)
    {
        base.HandleInputs(@event);

        if (@event.IsActionPressed("move_dash"))
		{
		    if (player.canDash())
		    {
		        player.Dash(player.getInputVector());
                player.dashCount -= 1;
                
                player.updateVelocity();
		    }
		}

        if (@event.IsActionPressed("move_jump"))
		{
		    player.Jump();
		}
    }

    public override void PhysicsUpdate(float delta)
    {
        if (!player.IsOnFloor())
        {
            player.stateMachine.OnPlayerStateTransition(player.stateMachine.CurState, "Air");
        }

        Vector2 direction = player.getInputVector();
        if (!(direction.LengthSquared() > 0.0001f))
        {
            player.stateMachine.OnPlayerStateTransition(player.stateMachine.CurState, "Idle");
        }
        else
        {
            player.MoveGround(direction);
        }
        
        player.computeDash(delta);
        player.updateVelocity();
    }
}