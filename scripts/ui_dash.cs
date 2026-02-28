using Godot;
using System;
using System.Collections.Generic;

public partial class ui_dash : Control
{
	[Export]
	public Player player;

	List<ColorRect> dashes;

	float maxHeight;
	float maxWidth;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		dashes = new List<ColorRect>();
		foreach (Control child in GetChildren())
		{
			if (child is ColorRect dash)
			{
				dashes.Add(dash);
			}
		}

		ColorRect dashRect = dashes[0];
		maxHeight = dashRect.Size.Y;
		maxWidth = dashRect.Size.X;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (player.dashCount != player.MaxDash)
		{
			QueueRedraw();
		}
	}

	public override void _Draw()
	{
		if (player.dashCount == player.MaxDash) return;

		for (int i = 0; i < dashes.Count; i++)
		{
			ColorRect dashRect = dashes[i];
			float progress = player.dashTimer / player.DashCooldown;
			DrawRect(new Rect2(dashRect.Position.X, dashRect.Position.Y, maxWidth, maxHeight), new Color(0, 0, 0, 0.5f));

			if (player.dashCount == i)
			{
				dashRect.Size = new Vector2(maxWidth * (1 - progress), maxHeight);	
			}else if (player.dashCount > i) 
			{
				dashRect.Size = new Vector2(maxWidth, maxHeight);
			}else if (player.dashCount < i) 
			{
				dashRect.Size = new Vector2(0, maxHeight);
			}

		}
	}
}
