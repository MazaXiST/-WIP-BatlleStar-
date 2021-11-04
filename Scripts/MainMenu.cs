using Godot;
using System;

public class MainMenu : BaseScene
{ 
	//Label pressAnyKeyLabel;
		

    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
	
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here

	
			//MakeStars(CLOSE_STAR_SCALE, CLOSE_STAR_COUNT,STAR_TYPE.MOVING_STAR);
			//MakeStars(FAR_STAR_SCALE, FAR_STAR_COUNT,STAR_TYPE.MOVING_STAR);
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.

	

        if(Input.IsActionPressed("Fire") && GetTree().GetCurrentScene().GetName() == this.Name)//this.Name)//this.GetName())
		{
			
			GetTree().ChangeScene("res://Scenes/BattleSpace/BattleSpace.tscn");

			QueueFree();
			Console.WriteLine("<Scene is still here!>");
		}  
		 
    }

    public override void _PhysicsProcess(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.
        
    }
}
