using Godot;
using System;

public partial class CameraControls : Camera2D
{
	[Export] GameManager gameManager;
	[Export] Timer screenShakeTimer;

	bool screenShakeStart;

	float shakeIntensity;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameManager = (GameManager)GetTree().Root.GetChild(0);
		gameManager.camera = this;
	}

    public override void _ExitTree()
    {
        gameManager.camera = null;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Zoom = Zoom.Lerp(new Vector2(1f, 1f), 0.3f);
		if(screenShakeStart)
		{
			this.GlobalPosition += new Vector2((int)GD.RandRange(-shakeIntensity, shakeIntensity), (int)GD.RandRange(-shakeIntensity, shakeIntensity)) * (float)delta;
		}
		else 
		{
			GlobalPosition = GlobalPosition.Lerp(new Vector2(320f, 180f), 0.3f);
		}

	}

	public void ScreenShake(float intensity, double time)
	{
		Zoom = new Vector2(1f, 1f) - new Vector2(intensity * 0.0015f, intensity * 0.0015f);
		shakeIntensity = intensity;

		screenShakeTimer.WaitTime = time;
		screenShakeTimer.Start();
		screenShakeStart = true;
	}

	void OnScreenShakeTimeout()
	{
		screenShakeStart = false;
	}
}
