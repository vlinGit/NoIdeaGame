using Godot;

public partial class Idle : PlayerState
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
    }
    public override void PhysicsUpdate(float delta)
    {
        Vector2 direction = player.getInputVector();

        if (!player.IsOnFloor())
        {
            player.stateMachine.OnPlayerStateTransition(player.stateMachine.CurState, "Air");
        }

        if (direction.LengthSquared() > 0.0001f)
        {
            player.stateMachine.OnPlayerStateTransition(player.stateMachine.CurState, "Move");
        }
        else
        {
            player.MoveZeroGround();
        }
        // GD.Print(player.dashCount);
        player.computeDash(delta);
        player.updateVelocity();
    }
}