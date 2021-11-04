using Godot;
using System;

public class DataForRadar : Node
{
    public Vector2 Position { get; set; } = new Vector2();
    public Vector2 [] EngineTrace {get; private set;} = new Vector2[Game.engineTraceCapacity];
    public Vector2 LabelSize {get; private set;} = new Vector2(3,3);
    public Color Color {get; private set;} = new Color(255,255,255);
    private static int unitId = -1;
    private DataForRadar() //конструктор поумолчанию объявлен явно тк моно требовал чтобы он был
    {}

    public DataForRadar(Vector2 position, Vector2[] engineTrace, int engineTraceCapacity, Vector2 labelSize, Color color)
    {
        this.Position = position; 
        this.EngineTrace = new Vector2[engineTraceCapacity];
        this.EngineTrace = engineTrace;

        for(int i =0; i<EngineTrace.Length; i++)
        EngineTrace[i] = Position;

        this.LabelSize = labelSize;
        this.Color = color;
        unitId++;
    }

    ~DataForRadar()
    {
        unitId--;
    }
    
}
