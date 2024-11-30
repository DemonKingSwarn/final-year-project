using Godot;

public partial class Enemy : Sprite2D
{
	[Export] GameManager gameManager;

	[Export] Timer stunTimer;

	[Export] PackedScene playerPickUp = ResourceLoader.Load<PackedScene>("res://scenes/pickup.tscn");
	[Export] PackedScene bloodScene = ResourceLoader.Load<PackedScene>("res://scenes/blood.tscn");

	[Export] float speed = 100f;

	float hp = 3f;

	Vector2 velocity;

	bool isStun = false;

    public override void _Ready()
    {
		gameManager = (GameManager)GetTree().Root.GetChild(0);
    }

    public override void _Process(double delta)
    {
        if(gameManager.player != null && isStun == false)
		{
			velocity = this.GlobalPosition.DirectionTo(gameManager.player.GlobalPosition);
		} 
		else if(isStun)
		{
			velocity = velocity.Lerp(new Vector2(0f, 0f), 0.3f);
		}

		this.GlobalPosition += velocity * speed * (float)delta;

		if(hp <= 0f)
		{
			if(gameManager.camera != null)
			{
				gameManager.camera.ScreenShake(80f, 0.2d);
			}

			if(gameManager.bloodHolder != null)
			{
				Node2D blood = gameManager.InstanceNode(bloodScene, GlobalPosition, gameManager.bloodHolder);
				blood.Rotation = velocity.Angle();
			}

			gameManager.score += 10;
			gameManager.InstanceNode(playerPickUp, GlobalPosition, gameManager.pickupHolder);
			QueueFree();
		}
    }

	void OnHitBoxEntered(Area2D area)
	{
		if(area.IsInGroup("EnemyDamager") && isStun == false)
		{
			Modulate = new Color(1f, 1f, 1f);
			velocity = -velocity * 4f;
			hp -= 1f;
			isStun = true;
			stunTimer.Start();
			area.GetParent().QueueFree();
		}
	}

	void OnStunTimeout()
	{
		Modulate = new Color("ff4e49");
		isStun = false;
	}
}
