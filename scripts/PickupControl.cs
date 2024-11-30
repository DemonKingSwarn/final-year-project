using Godot;

public partial class PickupControl : Sprite2D
{
	void OnPickTimeout()
	{
		QueueFree();
	}
}
