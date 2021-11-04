using Godot;
using System;

public class RailCannon : Node2D, IArmory
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    private PackedScene railMissile = (PackedScene)GD.Load("res://Scenes/BattleStarShip/Weapons/RailMissile.tscn");//(PackedScene)ResourceLoader.Load("res://Bullet.tscn");
    private float shotPower = 100000.0f;

    public void Shot()
    {
        var missile = (RailMissile)railMissile.Instance();

        missile.GlobalPosition = Position *new Vector2(1,1.5f);
        missile.GlobalRotation = Rotation;
        GetParent().AddChild(missile);

        var x = Math.Cos(missile.GlobalRotation);
        var y = Math.Sin(missile.GlobalRotation);

        var vec =  new Vector2((float)x,(float)y).Normalized();
        
        //missile.ApplyCentralImpulse(shotPower*vec*10000000);
        missile.AddCentralForce(vec * shotPower*missile.Mass);
    }

}
