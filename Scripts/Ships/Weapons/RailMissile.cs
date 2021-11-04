using Godot;
using System;

public class RailMissile : RigidBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private int damage = 0;//-10;
    private Line2D trail;
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Ready()
    {
      trail = (Line2D)GetNode("Trail");
      //var signalList = GetParent().GetSignalConnectionList("body_entered");// GetSignalList();
      //GetParent().
      //Console.WriteLine(GetParent().GetName());
      //foreach (var element in signalList)
      //  Console.WriteLine(element["body_entered"]);
      //var err = this.Connect(nameof(body_entered), GetNode("../GUILayer/ShipUI/Radar"), nameof(Radar._on_Unit_add));//, _params);//,ff);//, 10);
      // Console.WriteLine("RailMissiles connection " + err);
    }

  public override void _PhysicsProcess(float delta)
  {
      	//if(!GetViewportRect().HasPoint(GlobalPosition))
	  	  if(!new Rect2(0, 0, Game.FIELD_WIDTH,  Game.FIELD_HEIGHT).HasPoint(GlobalPosition))
        {
          QueueFree();
        }
        
        if(trail.HasMethod("DrawTrail"))
        trail.Call("DrawTrail", GlobalPosition);

        return;

        var collisions = GetCollidingBodies(); 
       
        if(collisions.Count > 0)
        //    foreach(var obj in collisions)
        //    if(BattleStarShip.IsInstanceValid((BattleStarShip)obj))
        //    {
        //       QueueFree();
        //    } 
        //  }
        try
        {
          //if(collisions.Contains(BattleStarShip))
          {
            foreach(var obj in collisions)
            if(BattleStarShip.IsInstanceValid((Godot.Object)obj))
            {
              //if(HasMethod("UpdateHitPoint"))


              QueueFree();
            }
          } 
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Casting exception: {ex}", ex.Message);
            QueueFree();
        }

  }

protected void _on_RailMissile_body_entered(Godot.Object body)
{
  Console.WriteLine("Collision!");

	if(body.HasMethod("UpdateHitPoint"))
	{
		body.Call("UpdateHitPoint",damage);
	}

	if(body.HasMethod("UpdateAppliedForce"))
	{
		body.Call("UpdateAppliedForce", AppliedForce);
	}

  SelfKill();
	//Console.WriteLine("Bullet collide");
    // Replace with function body
}

//для внешних вызовов
  private void SelfKill()
  {
     QueueFree();
  }

}
