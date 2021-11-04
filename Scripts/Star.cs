using Godot;
using System;

public class Star : Sprite
{
public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
		//Vector2 width = new Vector2 (800,800);
		//Vector2 var1  = new Vector2(40,40);
		float size = 800.0f;
		float regionRectX = 40.0f;
		
		//ВОТ ТУТ НАДО СДЕЛАТЬ НОРМАЛЬНО!!!!
		int i = CommonFunctions.GetRandomValueI(0,4);
		
		switch(i)
		{
			case 0: regionRectX = 40.0f;
					break;
			case 1: regionRectX = 800 + 40.0f;
					break;
			case 2: regionRectX = 800.0f*2.0f + 40.0f;
					break;
			case 3: regionRectX = 800.0f*3.0f + 40.0f;
					break;
			case 4: regionRectX = 800.0f*4.0f + 40.0f;
					break;
			}
			
		// var modulate = this.Modulate;
		// modulate.a = 0.5f;
		// SetModulate(modulate);
		RegionRect = new Rect2(regionRectX, 40.0f, size, size);

		//Texture.GetWidth();
				        
    }
	

}