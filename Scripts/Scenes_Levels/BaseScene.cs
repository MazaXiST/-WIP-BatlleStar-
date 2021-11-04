using Godot;
using System;

public class BaseScene : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    	protected readonly int CLOSE_STAR_COUNT = 30;
    	protected readonly int FAR_STAR_COUNT = 120;
		
		protected Vector2 CLOSE_STAR_SCALE = new Vector2(0.018f, 0.018f);
		protected Vector2 FAR_STAR_SCALE = new Vector2(0.01f, 0.01f);

	protected void MakeFarStarsByDrawing()
	{
		//var farStarsTexture = (Texture)GD.Load("res://Resourses/Sprites/StarsSamples.png");
		var farStarsTexture = (Texture)GD.Load("res://Resourses/Sprites/FarStar.png");
		var middleStarsTexture = (Texture)GD.Load("res://Resourses/Sprites/MiddleStar.png");
		var closeStarsTexture = (Texture)GD.Load("res://Resourses/Sprites/CloseStar.png");
	

		for(int j = 0; j < 5000; j++)
		{
		Sprite farStarSprite = new Sprite();
		farStarSprite.SetTexture(farStarsTexture);
		farStarSprite.Scale = new Vector2(0.1f, 0.1f);
		farStarSprite.ZIndex = -10;
		farStarSprite.Position = CommonFunctions.GetRandomVector2(0, Game.FIELD_WIDTH, 0, Game.FIELD_HEIGHT);
		AddChild(farStarSprite);
		}	



		for(int j = 0; j < 5000; j++)
		{		
		Sprite middleStarSprite = new Sprite();
		middleStarSprite.SetTexture(middleStarsTexture);
		middleStarSprite.Scale = new Vector2(0.1f, 0.1f);
		middleStarSprite.ZIndex = -10;
		middleStarSprite.Position = CommonFunctions.GetRandomVector2(0, Game.FIELD_WIDTH, 0, Game.FIELD_HEIGHT);
		AddChild(middleStarSprite);
		}


		
		for(int j = 0; j < 5000; j++)
		{
		Sprite closeStarSprite = new Sprite();
		closeStarSprite.SetTexture(closeStarsTexture);
		closeStarSprite.Scale = new Vector2(0.1f, 0.1f);
		closeStarSprite.ZIndex = -10;
		closeStarSprite.Position = CommonFunctions.GetRandomVector2(0, Game.FIELD_WIDTH, 0, Game.FIELD_HEIGHT);
		AddChild(closeStarSprite);
		}

	}


	protected void MakeStarsBackground()//int count, int screenHeight = 10000, int screenWidth = 10000)
	{
		var farStarsTexture = (Texture)GD.Load("res://Resourses/Sprites/FarStars back.png");

		var starsTextureWidth = farStarsTexture.GetWidth();
		var starsTextureHeight = farStarsTexture.GetHeight();

		var textureCountToWidth  = Game.FIELD_WIDTH  / starsTextureWidth+1;
		var textureCountToHeight = Game.FIELD_HEIGHT / starsTextureHeight+1;

		Console.WriteLine(textureCountToWidth);
		Console.WriteLine(textureCountToHeight);

		for(int i = 0; i < textureCountToWidth; i++)
		for(int j = 0; j < textureCountToHeight; j++)
		{
		Sprite farStarsSprite = new Sprite();
		farStarsSprite.SetTexture(farStarsTexture);
		farStarsSprite.ZIndex = -5;

		int rand = CommonFunctions.GetRandomValueI(0,3);
		farStarsSprite.Rotate((float)(Math.PI/2f*rand));

		//farStarsSprite.RegionRect = new Rect2(0, 0, textureCountToWidth/2, textureCountToHeight/2);
		//farStarsSprite.RegionRect = new Rect2(0, 0, starsTextureWidth, starsTextureHeight);
		
		//farStarsSprite.Position = new Vector2(i*(Game.FIELD_WIDTH/textureToWidth), j*(Game.FIELD_HEIGHT/textureToHeight));//CommonFunctions.GetRandomVector2(0, Game.FIELD_WIDTH, 0, Game.FIELD_HEIGHT);
		farStarsSprite.Position = new Vector2(i*starsTextureWidth, j*starsTextureHeight);//CommonFunctions.GetRandomVector2(0, Game.FIELD_WIDTH, 0, Game.FIELD_HEIGHT);
		
		AddChild(farStarsSprite);
		}	
	}
	protected enum STAR_TYPE {STANDING_STAR = 0, MOVING_STAR = 1}
	// protected void MakeStars(Vector2 scale,
	// 						 int count,
	// 						 STAR_TYPE starType = STAR_TYPE.STANDING_STAR,
	// 						 int screenWidth = 800,
	// 						 int screenHeight = 600
	// 						 )//, int speed = 100, Vector2 direction)
	// {
	// 	PackedScene star;
	// 		switch(starType)
	// 		{
	// 			case STAR_TYPE.STANDING_STAR :
	// 			star = (PackedScene)GD.Load("res://Scenes/BackGrounds/Stars/Star.tscn");
	// 										   break;
	// 			case STAR_TYPE.MOVING_STAR :
	// 			star = (PackedScene)GD.Load("res://Scenes/BackGrounds/Stars/MovingStar.tscn");
	// 										   break;
	// 			default: return;
	// 		}

	// 	for (int i = 0; i< count; i++)
	// 			{
					
	// 			var localStar = (Star)star.Instance();
                      
    //             localStar.Scale = scale;	
				
	// 			localStar.Position = CommonFunctions.GetRandomVector2(0, screenWidth, 0, screenHeight);//new Vector2(x, y); // = new Vector2(0 + i *20, 0 + i *20);
				
	// 			AddChild(localStar);
	// 			}
	// 	}

	protected void MakeNebulaObjects(int count,
							   int screenWidth,
							   int screenHeight
							   )

	{
		for(int i = 0; i < count; i++)
			{
				var nebulaInGroup = CommonFunctions.GetRandomValueI(3,10);

				var position = CommonFunctions.GetRandomVector2(0, Game.FIELD_WIDTH, 0, Game.FIELD_HEIGHT);

				AddChild(new NebulaGroup(nebulaInGroup, position));
			}

			//AddChild(new Nebula());
							   }

	protected void MakeNebulas(int count,
							   int screenWidth,
							   int screenHeight
							   )
	{

     	int mimNebulaInGroup = 5;
    	int maxNebulaInGroup = 10;
    	float alpha = 0.5f;

   		var nebulaTexture1 = (Texture) GD.Load("res://Resourses/Sprites/Nebula_1.png");
        var nebulaTexture2 = (Texture) GD.Load("res://Resourses/Sprites/Nebula_2.png");
        var nebulaTexture3 = (Texture) GD.Load("res://Resourses/Sprites/Nebula_3.png");
        var nebulaTexture4 = (Texture) GD.Load("res://Resourses/Sprites/Nebula_4.png");
        

        for(int i = 0; i<count; i++)
        {
			var nebulasGroupPosition = CommonFunctions.GetRandomVector2(0, screenWidth, 0, screenHeight);
			var nebulasInGroup = CommonFunctions.GetRandomValueI(mimNebulaInGroup, maxNebulaInGroup);

			for(int j = mimNebulaInGroup; j<nebulasInGroup+1; j++)
            {
			var nebulaSprite = new Sprite();

            var nebulaTextureNum = CommonFunctions.GetRandomValueI(0,3);

            switch(nebulaTextureNum)
            {
                case 0: nebulaSprite.SetTexture(nebulaTexture1);
                        break;
                case 1: nebulaSprite.SetTexture(nebulaTexture2);
                        break;
                case 2: nebulaSprite.SetTexture(nebulaTexture3);
                        break;
                case 3: nebulaSprite.SetTexture(nebulaTexture4);
                        break;
            }
       

	   		var offsetPos = CommonFunctions.GetRandomVector2(-1*nebulaSprite. Texture.GetWidth(),
                                                                	nebulaSprite.Texture.GetWidth(),
                                                                	-1*nebulaSprite.Texture.GetHeight(),
                                                                	nebulaSprite.Texture.GetHeight());

            nebulaSprite.Position = nebulasGroupPosition + offsetPos;
            
			int rand = CommonFunctions.GetRandomValueI(0,3);
			nebulaSprite.Rotate((float)(Math.PI/2f*rand));
            
            nebulaSprite.SetScale(new Vector2(0.25f, 0.25f));

        	var r =  CommonFunctions.GetRandomValueI(0,2);
    	    var g =  CommonFunctions.GetRandomValueI(0,2);
	        var b =  CommonFunctions.GetRandomValueI(1,2);


            nebulaSprite.SetModulate(new Color(r,g,b, alpha));
            nebulaSprite.SetSelfModulate(new Color(r,g,b, alpha));
            nebulaSprite.ZIndex = -4;
		    
			AddChild(nebulaSprite);     
			}
        }

	}

    // Called when the node enters the scene tree for the first time.
    //public override void _Ready()
    //{
    //    
    //}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
