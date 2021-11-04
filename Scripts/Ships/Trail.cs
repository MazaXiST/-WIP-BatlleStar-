using Godot;
using System;

public class Trail : Line2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private Vector2 currentPosition = new Vector2(0f,0f);
    private Node2D target;
    [Export]
    NodePath targetNode;
    [Export]
    private int trailLeght = 0;

    public override void _Ready()
    {
        target = (Node2D)GetNode(targetNode);
    }

    public void DrawTrail(Vector2 point)
    {
      GlobalPosition = new Vector2(0f,0f);
      GlobalRotation = 0;
      currentPosition = point;
      AddPoint(currentPosition);

      while(GetPointCount() > trailLeght)
          RemovePoint(0);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  //public override void _Process(float delta)
  //{
    //DrawTrail(target.GlobalPosition);
  //}
}