using Godot;

public partial class Player : Sprite2D
{
	[Export] GameManager gameManager;

	[Export] float speed = 150f;

	Vector2 velocity;

	[Export] PackedScene bullet = ResourceLoader.Load<PackedScene>("res://scenes/bullet.tscn");
	[Export] Node2D bulletHolder;

	public override void _Process(double delta)
	{
		
		velocity.X = (Input.IsActionPressed("right") ? 1 : 0) - (Input.IsActionPressed("left") ? 1 : 0);
		velocity.Y = (Input.IsActionPressed("backward") ? 1 : 0) - (Input.IsActionPressed("forward") ? 1 : 0);

		velocity.Normalized();

		this.GlobalPosition += speed * velocity * (float)delta;

		if(Input.IsActionJustPressed("fire"))
		{
			gameManager.InstanceNode(bullet, this.GlobalPosition, bulletHolder);
		}
	}
}