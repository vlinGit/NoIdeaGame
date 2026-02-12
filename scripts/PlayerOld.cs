using Godot;
using System;

public partial class PlayerOld : Character
{
    [Export]
    public int MaxDash = 2;
    [Export]
    public float DashCooldown = 2.0f;

    private int dashCount;
    private float dashTimer;
    private double delta;

    public Vector2 getInputVector()
    {
        return Input.GetVector("move_left", "move_right", "move_forward", "move_back");
    }

    public bool canDash()
    {
        if (dashCount > 0)
        {
            dashCount -= 1;
            return true;
        }
        return false;
    }
    public void computeDash()
    {
        if (dashTimer <= 0)
        {
            dashCount = Math.Min(dashCount+=1, MaxDash);
            dashTimer = DashCooldown;
        }
        else
        {
            dashTimer -= (float)delta;
        }
    }

    public override void _Ready()
    {
        base._Ready();
        Input.MouseMode = Input.MouseModeEnum.Captured; //locks to window
        dashCount = MaxDash;
        dashTimer = DashCooldown;
    }

    public override void _Input(InputEvent @event)
    {
        // if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
        // {
        //     moveCamera(mouseMotion);
        // }

        // if (@event.IsActionPressed("ui_cancel"))
        // {
        //     if (Input.MouseMode == Input.MouseModeEnum.Captured)
        //     {
        //         Input.MouseMode = Input.MouseModeEnum.Visible;
        //     }
        //     else
        //     {
        //         Input.MouseMode = Input.MouseModeEnum.Captured;
        //     }
        // }

        // if (@event.IsActionPressed("move_jump"))
        // {
        //     Jump();
        // }

        // if (@event.IsActionPressed("move_dash"))
        // {
        //     if (canDash())
        //     {
        //         Dash(getInputVector());
        //     }
        // }

        // if (@event.IsActionPressed("move_slam"))
        // {
        //     slam();
        // }
    }

    public override void _PhysicsProcess(double delta)
    {
        // this.delta = delta;

        // HandlePhysics(delta);
        // Move(getInputVector());
        // computeDash();
    }

}
