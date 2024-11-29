using Godot;

public partial class Bullet : Sprite2D
{
	Vector2 velocity = new Vector2(1f, 0f);

	float speed = 250f;

	bool lookOnce = true;

    public override void _Process(double delta)
    {
		if(lookOnce)
		{
			LookAt(GetGlobalMousePosition());
			lookOnce = false;
		}

		this.GlobalPosition += velocity.Rotated(Rotation) * speed * (float)delta;
    }
}
