using Godot;

public partial class EnemyIdle : EnemyState
{

    
    public override void PhysicsUpdate(float delta)
    {

        if (!player.IsOnFloor())
        {
            player.stateMachine.OnPlayerStateTransition(player.stateMachine.CurState, "Air");
        }

        // Vector2 direction = player.getInputVector();
        // if (direction.LengthSquared() > 0.0001f)
        // {
        //     player.stateMachine.OnPlayerStateTransition(player.stateMachine.CurState, "Move");
        // }
        // else
        // {
        //     player.MoveZeroGround();
        // }
        // GD.Print(player.dashCount);
        player.computeDash(delta);
        player.updateVelocity();
    }
}