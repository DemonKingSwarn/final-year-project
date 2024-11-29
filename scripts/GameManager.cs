using Godot;

public partial class GameManager : Node
{
	[Export] float maxFPS = 60f;

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
}
