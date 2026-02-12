using Godot;

public partial class Attack: Area3D
{
    [Export]
    public float speed;
    [Export]
    public float maxDistance;

    protected Player player; 
    public Vector3 startPos;

    public virtual void Enter(Player newPlayer){}

    public virtual void Move(float delta){}
    
    public virtual void Collide(Node3D body)
    {
        if (player != body)
        { 
            Delete();
        }
    }

    public virtual void Delete()
    {
        QueueFree();
    }
}