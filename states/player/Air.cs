using Godot;

public partial class Air : PlayerState
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
    
        if (@event.IsActionPressed("move_slam"))
		{
		    player.slam();
		}
    }

    public override void PhysicsUpdate(float delta)
    {
        if (player.IsOnFloor())
        {
            player.stateMachine.OnPlayerStateTransition(player.stateMachine.CurState, "Move");
        }

        player.HandlePhysics(delta);

        Vector2 direction = player.getInputVector();
        player.MoveAir(direction);

        player.updateVelocity();
    }
}