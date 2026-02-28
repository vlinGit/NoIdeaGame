using Godot;

[GlobalClass]
public partial class AttackModel : Resource
{
    [Export]
    public string name;
    [Export]
    public float cooldown;
    [Export]
    public Texture2D thumbnail;
    [Export]
    public PackedScene packedScene;
}