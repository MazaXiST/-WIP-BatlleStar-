using Godot;
using System;

public class ShipUI : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    Label shipSpeedLabel;
    Label positionLabel;
    TextureRect shipModel;
    TextureRect thrustVector;
    //Control radarRect;
    Radar radar;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        shipSpeedLabel = (Label) GetNode("shipSpeedLabel");
        positionLabel  = (Label) GetNode("positionLabel");
        shipModel       = (TextureRect) GetNode("naviBall/ShipModel");
        thrustVector       = (TextureRect) GetNode("naviBall/ThrustVector");
        radar   = (Radar) GetNode("Radar");
    }
    private float Vector2ToAngle(Vector2 vec)
    {
        return (float)(vec.Angle() - 1.5*Math.PI);
    }
    public void _on_BattleStar_PositionUpdated(Vector2 position, Vector2 velocity, string className)
    {
      positionLabel.Text = position.ToString();
        double speed =  CommonFunctions.VectorToSpeed(velocity);
        shipSpeedLabel.Text  = "Ship speed = " + ((int)speed).ToString() + " m/sec";   
    }
    public void _on_BattleStar_TorqueUpdated(Vector2 shipDirection)//float angle)
    {
        shipModel.SetRotation(Vector2ToAngle(shipDirection));
    }

    public void _on_BattleStar_ThrustUpdated(Vector2 shipThrust)//, Vector2 position, Vector2 [] engineTrace, string className)
    {
        thrustVector.SetRotation(Vector2ToAngle(shipThrust));
    }

    void _on_UI_resized()
    {
      //if(shipSpeedLabel != null)
      //shipSpeedLabel.RectPosition = GetViewportRect().Position;
      
      //this.RectSize = GetViewport().Size;
      Console.WriteLine("viewport size " + GetViewport().Size);
      Console.WriteLine("ui rect " + this.RectSize);
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
