using Godot;
using System;
public class Nebula : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    //readonly int nebulaAreaHW = 150;
    // Called when the node enters the scene tree for the first time.
    //readonly int mimNebulaInGroup = 5;
    //readonly int maxNebulaInGroup = 10;
    
    readonly float alpha = 0.65f;
    readonly float scale = 0.25f;

    public Nebula()
    {
        this.Position = CommonFunctions.GetRandomVector2(0, Game.FIELD_WIDTH, 0, Game.FIELD_HEIGHT);
    
        SetScale(new Vector2(scale, scale));   
            
        var r =  CommonFunctions.GetRandomValueI(0,3);
        var g =  CommonFunctions.GetRandomValueI(0,3);
	    var b =  CommonFunctions.GetRandomValueI(1,3);
        SetModulate(new Color(r,g,b, alpha));
    }

    public Nebula(Vector2 position, float scale, Color color)
    {
        this.Position = position;
        SetScale(new Vector2(scale, scale));   
        SetModulate(color);
    }

    public override void _Ready()
    {
            var nebulaTextureNum = CommonFunctions.GetRandomValueI(0,3);

            switch(nebulaTextureNum)
            {
                case 0: Texture = (Texture) GD.Load("res://Resourses/Sprites/Nebula_1_smooted.png");
                        break;

                case 1: Texture = (Texture) GD.Load("res://Resourses/Sprites/Nebula_2_smooted.png");
                        break;

                case 2: Texture = (Texture) GD.Load("res://Resourses/Sprites/Nebula_3_smooted.png");
                        break;

                case 3: Texture = (Texture) GD.Load("res://Resourses/Sprites/Nebula_4_smooted.png");
                        break;
            }

         
    int rand = CommonFunctions.GetRandomValueI(0,3);
			Rotate((float)(Math.PI/2f*rand));


            //SetSelfModulate(new Color(r,g,b, alpha));
            ZIndex = -4;    
    }
}

public class NebulaGroup : Nebula
{  
    private Vector2 position;
    public int nebulasCount {get;set;} = 10;
    public float nebulaScale = 1.0f;

    public NebulaGroup(int nebulasCount, Vector2 approxPosition)
    {
        this.nebulasCount = nebulasCount;
        position = approxPosition;
    }

    public override void _Ready()
    {

        var texture = (Texture) GD.Load("res://Resourses/Sprites/Nebula_1_smooted.png");
        var textureWidth  = texture.GetWidth();
        var textureHeight = texture.GetWidth();

            var r =  CommonFunctions.GetRandomValueI(1,2);
            var g =  CommonFunctions.GetRandomValueI(1,2);
	        var b =  CommonFunctions.GetRandomValueI(1,2);

            if(r == b) r--;
            else
            if(g == b) g--;  

            var alpha = 0.4f;
          
        for(int i = 0; i < nebulasCount; i++)
        {
            var scale = CommonFunctions.GetRandomValueF(5,10, 1);//0.6f;//CommonFunctions.GetRandomValueF(0,1,1);
            
            var newNebulaPositinon = position + CommonFunctions.GetRandomVector2(-1* textureWidth*scale/2,
                                                                	            textureWidth*scale/2,
                                                                	            -1*textureHeight*scale/2,
                                                                	            textureHeight*scale/2);

            var nebula = new Nebula(newNebulaPositinon, scale, new Color(r,g,b,alpha));

            AddChild(nebula);
        }
    }

}



