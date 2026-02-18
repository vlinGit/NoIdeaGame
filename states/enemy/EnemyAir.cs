using Godot;

public partial class EnemyAir : EnemyState
{

    public override void PhysicsUpdate(float delta)
    {
        if (player.IsOnFloor())
        {
            player.stateMachine.OnPlayerStateTransition(player.stateMachine.CurState, "Move");
        }

        player.HandlePhysics(delta);

        // Vector2 direction = player.getInputVector();
        // player.MoveAir(direction);

        player.updateVelocity();
    }
}