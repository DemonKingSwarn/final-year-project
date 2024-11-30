using Godot;

public partial class PauseMenu : Control
{
	public void Resume()
	{
		Visible = false;
		GetTree().Paused = false;
	}

	void Quit()
	{
		GetTree().Quit();
	}

	public void Pause()
	{
		Visible = true;
		GetTree().Paused = true;
	}
}
