using Godot;
using System;

public class BattleSpace : BaseScene//Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    
    readonly int SCREEN_HIGHT_WIDTH = 10000;
    readonly new int FAR_STAR_COUNT = 10000;
    readonly new int CLOSE_STAR_COUNT = 5000;
    readonly int NEBULA_COUNT = 70;
    //BattleStarShip battleStar;

    public void SetBattleStarPosition(int sectorX, int sectorY)
    { 
      var battleStar = (BattleStarShip)this.GetNode("BattleStar");//res://BattleStarShip/BattleStarShip.tscn");    
      
      float deltaX = CommonFunctions.GetRandomValueI(0,Game.FIELD_HEIGHT)/sectorX;
      float deltaY = CommonFunctions.GetRandomValueI(0,Game.FIELD_HEIGHT)/sectorY;
      
      CommonFunctions.GetRandomValueI(0,Game.FIELD_WIDTH);

      battleStar.SetPosition(new Vector2(sectorX*deltaX,sectorY*deltaY));
    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {   
        DateTime time = new DateTime();

        var mainCamera = (Camera2D)GetNode("BattleStar").GetNode("MainCamera");
        mainCamera.LimitLeft   = 0;
        mainCamera.LimitTop    = 0;
        mainCamera.LimitRight  = Game.FIELD_WIDTH;
        mainCamera.LimitBottom = Game.FIELD_HEIGHT;

        //var shipUI = (Control)GetNode("GUILayer").GetNode("ShipUI");
        //Console.WriteLine(shipUI.RectSize);
        //shipUI.RectSize = GetViewportRect().Size;
        //Console.WriteLine(shipUI.RectSize);
        

        MakeStarsBackground();
        MakeNebulaObjects(NEBULA_COUNT, Game.FIELD_WIDTH, Game.FIELD_HEIGHT);

        var ui = (Control)GetNode("GUILayer").GetNode("ShipUI");
        ui.SetSize(GetViewportRect().Size);
        
        Console.WriteLine("Loaded " + time.Ticks);

        var enemy      = (AIShip)this.GetNode("AIShip");//res://BattleStarShip/BattleStarShip.tscn");        

        // SetBattleStarPosition(
        //                       CommonFunctions.GetRandomValueI(1,4),
        //                       CommonFunctions.GetRandomValueI(1,4)
        //                       );

    }

    void _on_ShipUI_resized()
    {
      //var ui = GetNode("ShipUI");
      Console.WriteLine("Resized");
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  // public override void _Process(float delta)
  // {
  // }


}
