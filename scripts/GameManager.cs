using Godot;

public partial class GameManager : Node
{
    [Export] float maxFPS = 60f;

	[Export] public int score = 0;

	[Export] PackedScene enemy = ResourceLoader.Load<PackedScene>("res://scenes/enemy.tscn");
	[Export] Node2D enemyHolder;
	[Export] public Node2D bulletHolder;

	[Export] Label scoreText;

	public Node2D? player = null;

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

		while(enemyPos.X < 640f && enemyPos.X > -80f || enemyPos.Y < 360f && enemyPos.Y > -45f)
		{	
			enemyPos = new Vector2((float)GD.RandRange(-160f, 670f), (float)GD.RandRange(-90f, 390f));
		}

		InstanceNode(enemy, enemyPos,  enemyHolder);
	}

    public override void _Process(double delta)
    {
        scoreText.Text = score.ToString();
    }
}
