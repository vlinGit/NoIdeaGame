using Godot;
using System;

public partial class ui_Crosshair : Control
{
	[Export]
	public float strokeWidth;
	[Export]
	public float radius;
	[Export]
	public Color color;
	[Export]
	public bool centerDot;
	[Export]
	public float centerDotRadius;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		QueueRedraw();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Draw()
    {
		Vector2 center = GetViewport().GetVisibleRect().Size / 2;
        DrawCircle(center, radius, color, false, strokeWidth, true);
		if (centerDot)
		{
			DrawCircle(center, centerDotRadius, color, true, antialiased: true);
		}
    }   
}
