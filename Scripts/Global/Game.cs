using Godot;
using System;


public class Game : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Node CurrentScene { get; set; }

    public static int FIELD_WIDTH  { get; private set; } = 10000;  //ширина игрового поля
    public static int FIELD_HEIGHT { get; private set; } = 10000;  //высота игрового поля
    public static readonly int engineTraceCapacity = 1000;
    public static Vector2 ZERO_POINT { get; private set; } = new Vector2(0,0);  //начало координат игрового поля

    public static UInt32 OUTLINE_FIELD_WIDTH { get; private set; } = 100;   //ширина отступа за игровым полем куда прорисывывается бэкграунд

    public override void _Ready()
    {
      
        //Viewport root = GetTree().GetRoot();
        //CurrentScene = root.GetChild(root.GetChildCount() - 1);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
