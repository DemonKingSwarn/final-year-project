using System.Runtime.Serialization;
using Godot;

public partial class Player : Sprite2D
{
    [Export] GameManager gameManager;

    [Export] float speed = 150f;

    [Export] public float health = 10f;

    Vector2 velocity;

    [Export] PackedScene bullet = ResourceLoader.Load<PackedScene>("res://scenes/bullet.tscn");

    bool canShoot = true;

    [Export] Timer reloadTimer;

    public override void _Ready()
    {
        gameManager = (GameManager)GetTree().Root.GetChild(0);
        gameManager.player = this;
    }

    public override void _ExitTree()
    {
        gameManager.player = null;
    }

    public override void _Process(double delta)
    {

        velocity.X = (Input.IsActionPressed("right") ? 1 : 0) - (Input.IsActionPressed("left") ? 1 : 0);
        velocity.Y = (Input.IsActionPressed("backward") ? 1 : 0) - (Input.IsActionPressed("forward") ? 1 : 0);

        velocity.Normalized();

        Vector2 clampedPos = new Vector2(GlobalPosition.X, GlobalPosition.Y);
        clampedPos.X = Mathf.Clamp(clampedPos.X, 0f, 640f);
        clampedPos.Y = Mathf.Clamp(clampedPos.Y, 0f, 360f);
        this.GlobalPosition = clampedPos;

        this.GlobalPosition += speed * velocity * (float)delta;

        if (Input.IsActionPressed("fire") && canShoot)
        {
            gameManager.InstanceNode(bullet, this.GlobalPosition, gameManager.bulletHolder);
            reloadTimer.Start();
            canShoot = false;
        }

        if (health <= 0f)
        {
            gameManager.SaveScore();
            GetTree().ReloadCurrentScene();
        }

        //GD.Print($"health: {health}");

    }

    void OnReloadTimeOut()
    {
        canShoot = true;
    }

    void OnHitBoxEntered(Area2D area)
    {
        if (area.IsInGroup("PlayerPick"))
        {
            area.GetParent().QueueFree();
            health += 2f;
            health = Mathf.Clamp(health, 0f, 10f);
        }

        if (area.IsInGroup("Enemy"))
        {
            health -= 5f;
        }
    }
}
