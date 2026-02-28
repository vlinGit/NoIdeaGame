using Godot;
using System;

public partial class ui_Health : Control
{
	[Export]
	public Player player;
	[Export]
	public ColorRect healthBar;
	[Export]
	public Label healthText;

	private float maxWidth;
	private float maxHeight;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		maxWidth = healthBar.Size.X;
		maxHeight = healthBar.Size.Y;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		QueueRedraw();
	}

    public override void _Draw()
    {
        float progress = (float)player.Health / player.MaxHealth;

		healthBar.Size = new Vector2(maxWidth * progress, maxHeight);
		healthText.Text = player.Health + "/" + player.MaxHealth;
    }
}
