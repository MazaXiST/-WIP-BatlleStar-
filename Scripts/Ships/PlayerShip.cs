using Godot;
using System;

public class PlayerShip : BattleStarShip
{
  
    public void TurnLeft(float torqueSpeed)
    {
        AddCentralForce(new Vector2(0,0));
        ApplyTorqueImpulse(torqueSpeed *-1);
    }
    public void TurnRight(float torqueSpeed)
    {
        AddCentralForce(new Vector2(0,0));
        ApplyTorqueImpulse(torqueSpeed);
    }
    protected override void SelfDestroy()
    {
        //var gameOverMotherFucker = GD.Print("GG");//  (Label) scene.Instance();
        var gameOverMotherFucker = new Label();
        gameOverMotherFucker.Text = "GAME OVER";
        gameOverMotherFucker.RectSize = new Vector2(100f, 100f);
        gameOverMotherFucker.ShowBehindParent = false;
        gameOverMotherFucker.RectPosition = Position;
        GetParent().AddChild(gameOverMotherFucker);
        Console.WriteLine(gameOverMotherFucker.GetParent());
        Console.WriteLine(Position);

        QueueFree();
    }

    protected void _on_BurnTrailAnim_animation_finished()
    {
        Console.WriteLine(1);
    }
  public override void _PhysicsProcess(float delta)
  {
      UpdateRotationUI();
      UpdateVelocityAndPosition();

      if(!IsInTheField()) return;
    //   if(CommonFunctions.VectorToSpeed(LinearVelocity) != 0.0)
    //   {
    //         UpdatePosition();
    //         //EmitSignal(nameof(RadarDataUpdate), dataForRadar);
    //         EmitSignal(nameof(PositionUpdated), Position, LinearVelocity, this.ToString());
            
    //         if(Position.x < 0 || Position.y < 0 || 
    //            Position.x > Game.FIELD_WIDTH || Position.y > Game.FIELD_HEIGHT)
    //         {
    //            BackToField();
    //            return;
    //         }
    //   }

    //   if(!IsInTheField())
    //   {
    //       BackToField();
    //       return;
    //   }

      if(Input.IsActionJustPressed("LeftBoardFire"))
      {      
            foreach (var cannon in leftBoardCannons)
            cannon.Shot();  
      }

      if(Input.IsActionJustPressed("RightBoardFire"))
      {         
            foreach (var cannon in rightBoardCannons)
            cannon.Shot();
      }
      
      if(Input.IsActionPressed("FullSpeedAhead"))
      {         
          FullSpeedAhead();
      }

      if(Input.IsActionPressed("SlowDown"))
      {         
          FullSlowDown();
          return;
      }

      if(Input.IsActionPressed("TurnLeft"))
      {
          TurnLeft(torqueAcceleration);
      }
        
      if(Input.IsActionPressed("TurnRight"))
      {
          TurnRight(torqueAcceleration);
      }

  }

}
