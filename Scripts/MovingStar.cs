using Godot;
using System;

public class MovingStar : Star
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

   public override void _Process(float delta)
    {

		if(!GetViewportRect().HasPoint(Position) )
        {
			var newX = CommonFunctions.GetRandomValueI(0,(int)GetViewportRect().Size.x);//Texture.GetWidth()*Scale.x));//
			
			Position = new Vector2(newX, 0);//newY*(-1));
			
			return;

		}
				Position += new Vector2(0f,1f);
		// Called every frame. Delta is time since last frame.
        // Update game logic here.
        
    }
}
