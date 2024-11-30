using Godot;

public partial class MainMenu : Control
{
	[Export] Label highscoreText;

	void Play()
	{
		GetTree().ChangeSceneToFile("res://scenes/bootstrap.tscn");
	}

	void Credits()
	{

	}

	void Quit()
	{
		GetTree().Quit();
	}

	public override void _Process(double delta)
    {
        var res = GD.Load("user://score.tres");
		int highscore = (int)res.Get("highScore");
        highscoreText.Text = "highscore: " + highscore;
    }
}
