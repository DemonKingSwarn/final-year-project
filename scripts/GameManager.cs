using System.Windows.Markup;
using Godot;

public partial class GameManager : Node
{
    [Export] float maxFPS = 60f;

	[Export] public int score = 0;

	[Export] PackedScene enemy = ResourceLoader.Load<PackedScene>("res://scenes/enemy.tscn");
	[Export] Node2D enemyHolder;
	[Export] public Node2D bulletHolder;
	[Export] public Node2D pickupHolder;

	[Export] Label scoreText;

	public Node2D? player = null;

    string saveFilePath = "user://score.tres";


    public override void _Ready()
    {
        Engine.MaxFps = (int)maxFPS;
    }

    public Node2D InstanceNode(PackedScene node, Vector2 location, Node parent)
    {
        Node2D nodeInstance = node.Instantiate<Node2D>();
        parent.AddChild(nodeInstance);
        nodeInstance.GlobalPosition = location;
        return nodeInstance;
    }

	void OnEnemySpawnTimeout()
	{
		Vector2 enemyPos = new Vector2((float)GD.RandRange(-160f, 670f), (float)GD.RandRange(-90f, 390f));

		while(enemyPos.X < 640f && enemyPos.X > -80f && enemyPos.Y < 360f && enemyPos.Y > -45f)
		{	
			enemyPos = new Vector2((float)GD.RandRange(-160f, 670f), (float)GD.RandRange(-90f, 390f));
		}

		InstanceNode(enemy, enemyPos,  enemyHolder);
	}

    public override void _Process(double delta)
    {
        scoreText.Text = score.ToString();
    }


    public void SaveScore()
    {
        var res = (SaveGame)GD.Load("res://scripts/SaveGame.cs").Call("new");
        
        if(score > (int)res.Get("highscore")){
            res.Set("highScore", score);
            ResourceSaver.Save(res, saveFilePath);
        }
    }

    /*
    void LoadScore()
    {
        var res = GD.Load(saveFilePath);
        int highScore = (int)res.Get("highScore");
        GD.Print(highScore);
    }
    */

}
