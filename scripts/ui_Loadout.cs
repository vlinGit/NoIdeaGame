using System;
using Godot;
using Godot.Collections;

public partial class ui_Loadout : Control
{
	[Export]
	public Player player;
	[Export]
	public Label name;
	[Export]
	public TextureRect thumbnail;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player.AttackChanged += OnAttackChanged;
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	private void OnAttackChanged(AttackModel attackModel)
    {
        name.Text = attackModel.name.ToUpper();
		thumbnail.Texture = attackModel.thumbnail;
    }
}
