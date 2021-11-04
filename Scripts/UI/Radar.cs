using Godot;
using System;
using System.Collections.Generic;


public class Radar : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Vector2 positionOffset = new Vector2(1,1);
    private Vector2 engineTraceSize = new Vector2(1,1);
    public List<DataForRadar> unitsOnRadar { get; private set; } = new List<DataForRadar>();

    private TextureRect radarField; 
    public void _on_Unit_add(DataForRadar unit)
    {
        unitsOnRadar.Add(unit);
    }
    public void _on_Unit_data_update(DataForRadar unit)
    {
        Update();
    }
    public void _on_Unit_remove(int index)
    {
        unitsOnRadar.RemoveAt(index);
    }

    public Vector2 GlobalPositionToRadarPosition(Vector2 originPosition)
    {
        originPosition.x = ((float)originPosition.x/(float)Game.FIELD_WIDTH) * radarField.RectSize.x;//*this.RectScale.x;//this.RectSize.x;///(float)Math.Pow(this.RectScale.x,2);
        originPosition.y = ((float)originPosition.y/(float)Game.FIELD_HEIGHT) * radarField.RectSize.y;//*this.RectScale.y;//this.RectSize.y;///(float)Math.Pow(this.RectScale.y,2);
        
        return originPosition;
    }

    private void DrawEngineTrace(DataForRadar unit)
    {
        foreach(Vector2 vector in unit.EngineTrace)
        {
            DrawRect(new Rect2(GlobalPositionToRadarPosition(vector),engineTraceSize), unit.Color);
        }  
    }

    public void DrawPosition(DataForRadar unit)
    {
        DrawRect(new Rect2(GlobalPositionToRadarPosition(unit.Position)-positionOffset,
                                        unit.LabelSize), unit.Color);
    }

    public override void _Draw()
    {
        foreach(DataForRadar unit in unitsOnRadar)
        {
             DrawPosition(unit);
             DrawEngineTrace(unit);
        }
    }

    public override void _Ready()
    {
       radarField = (TextureRect) GetChild(0);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
