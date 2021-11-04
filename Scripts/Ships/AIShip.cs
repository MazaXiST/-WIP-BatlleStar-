using Godot;
using System;

public class AIShip : BattleStarShip
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    int[,] sectors {set;get;} = new int[Game.FIELD_HEIGHT/10, Game.FIELD_WIDTH/10];

    private float combatAcceleration = 500f;
    private float cruiseAcceleration = 250f;
    private Vector2 startPoint = new Vector2();
    public Vector2 [] playerEngineTrace {get;set;} = new Vector2[Game.engineTraceCapacity];
    public Vector2 playerPosition { get; set; } = new Vector2(0,0);
    private readonly Vector2 AREA_SIZE = new Vector2 (1000,1000);
    private bool startPointSaved = false;

    private void SaveStartPoint()
    {
        if(!startPointSaved)
        {
        startPointSaved = true;
        startPoint = Position;
        }
    }
    //public override void _Ready()
    //{
        
        //Эталонное подключение сигнала в коде
        //var errCode = this.Connect(nameof(PositionUpdated), GetNode("../GUILayer/ShipUI"), nameof(ShipUI._on_BattleStar_PositionUpdated));
        //Console.WriteLine("errCode = " + errCode);
    //}
    protected bool CheckPosition(Vector2 targetArea, Vector2 areaSize)
    {
        Rect2 area = new Rect2(targetArea - areaSize/2,areaSize);
        //Console.WriteLine(targetArea - AREA_SIZE/2);
        //Console.WriteLine(Position);
        if(area.HasPoint(Position))
        { 
            //Console.WriteLine("true");
            return true;
        
        }
        else 
        {
            //Console.WriteLine("false");
            return false;
        }
    }
    protected void MoveTo(Vector2 coordinate, float acceleration, float maxSpeed)
    {
        //if(Position >= coordinate)
        if(CheckPosition(coordinate, AREA_SIZE)) 
        {
            //if(SlowDown()) 
            return;
        }
        else
        {
            if(SetDirectionToPoint(coordinate))
            SpeedAhead(acceleration);
        }
        
    }

    private float GetDistanceToPoint(Vector2 point)
    {
        return CommonFunctions.VectorToSpeed(point - Position);
    }
    private float GetDistanceBeetweenPoints(Vector2 point1, Vector2 point2)
    {
     
        return point1.DistanceTo(point2);//
        //return CommonFunctions.VectorToSpeed(point1 - point2);
    }
    protected bool MoveTo(Vector2 targetCoordinate, float acceleration)//, float maxSpeed)
    {

        //targetCoordinate += new Vector2(acceleration,acceleration);
                  //  Console.WriteLine("Position "+Position);
        SaveStartPoint();
      
  
        var distanceToTarget = startPoint.DistanceTo(targetCoordinate);//-1000*(1-acceleration/MAX_THRUST_ACCELERATION);//GetDistanceBeetweenPoints(startPoint,targetCoordinate);
        //var speedLegth = CommonFunctions.VectorToSpeed(LinearVelocity)+1;
        //var breakWay   = Math.Sqrt(acceleration*distanceToTarget*1.2);//62.5;
        //var breakWay   = Math.Sqrt(acceleration*distanceToTarget); работает правильно, но конечное отклонение
        //слишком больше нужно меня параметры в корне

        //Console.WriteLine(distanceToTarget);
        //Console.WriteLine("SpeedPow          |  " + breakWay);
        //Console.WriteLine("HalfWayToTarget   |  " + distanceToTarget/2);
 

       // Console.WriteLine(Position +"   |  ");//+ (distanceToTarget/2+breakWay));
    
        // return false;

        if(CheckPosition(targetCoordinate, AREA_SIZE))
        {
            if(CommonFunctions.VectorToSpeed(LinearVelocity) > 0) FullSlowDown();
            else 
            startPointSaved = false;
            //SAME_DIRECTION_ANGLE = 0.01f;
            
            
            return true;

        }
        else
        {
            
            //if( GetDistanceToPoint(coordinate) <= GetDistanceBeetweenPoints(coordinate, startPoint)/1.5f)
            //Лучший вариант - if( GetDistanceToPoint(coordinate) <= (distanceToTarget/2+distanceToTarget/3.2*Math.Sqrt(acceleration/MAX_SPEED)))//(2 - MAX_SPEED/acceleration+0.25))
            
            //if( GetDistanceToPoint(coordinate) <= (distanceToTarget/2+distanceToTarget/2.5+acceleration*(acceleration/MAX_SPEED)))//Math.Sqrt(speedLegth)))//(2 - MAX_SPEED/acceleration+0.25))
            //if( Position.DistanceTo(targetCoordinate) <= (distanceToTarget/2 + distanceToTarget/4*(acceleration/MAX_SPEED)+(acceleration*acceleration/200) ))
            
            //if( Position.DistanceTo(targetCoordinate) <= (distanceToTarget/2 + distanceToTarget/4 ))
            //if( Position.DistanceTo(targetCoordinate) <= (distanceToTarget/2 + (distanceToTarget/3) * (acceleration/MAX_THRUST_ACCELERATION)))
            

            
            if( Position.DistanceTo(targetCoordinate) <= (distanceToTarget*0.7))//*(acceleration/MAX_THRUST_ACCELERATION)))
            {
                SetDirection(AppliedForce*-1);

                  //if(  Position.DistanceTo(targetCoordinate) <= (distanceToTarget/2 - (distanceToTarget/6)*(acceleration/MAX_SPEED)-(acceleration*acceleration/200)))
                    //if(  Position.DistanceTo(targetCoordinate) <= (distanceToTarget/2 - distanceToTarget/4));//+100*(acceleration/MAX_SPEED)))
                    if(  Position.DistanceTo(targetCoordinate) <= (distanceToTarget*0.3*(acceleration/MAX_THRUST_ACCELERATION)))
                    
                    SlowDown(acceleration, 0.05*(acceleration/(MAX_THRUST_ACCELERATION)));
                    //FullSlowDown();

                    //else
                    //SetDirection(AppliedForce*-1);

                return false;
            }
            
            if(SetDirectionToPoint(targetCoordinate))
            SpeedAhead(acceleration);
            
        }
        return false;
    }

    public void _on_BattleStar_ThrustUpdated(Vector2 shipSpeed, Vector2 position, Vector2[] trace, string className)
    {
        
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.

    public override void _PhysicsProcess(float delta)
    {   
        if(!IsInTheField()) BackToField();

        if(ReloadTime == 100)
        {    
        foreach (var cannon in leftBoardCannons)
        cannon.Shot();
        ReloadTime = 0;
        }
        else ReloadTime++;

        //if(flag) Position = new Vector2(9000,9000);
        
        //if(SetDirectionToPoint(new Vector2(5000,5000)))
        //if(SetDirection(new Vector2(5000,5000)))
        //{
           //FullSpeedAhead();
        //}
        return;
        //SetDirection(new Vector2(5000,10000));
        //SetDirection(new Vector2(-5000,-5000));
        
        MoveTo(new Vector2(5000,5000), 50);
        //Console.WriteLine(Position);

        //Move2(new Vector2(5000,5000), 500f);
        //if(SetDirectionToPoint(new Vector2(5000,5000)))
        //if(SetDirection(new Vector2(1,1)))
        
        //Console.WriteLine(GetDirection());
        //if(SetDirection(new Vector2(1,1)))
        //FullSpeedAhead();

       
        //Rotation =(float) Math.PI/4;
        //if(Rotation == Math.PI/4)
        //FullSpeedAhead();

        UpdateVelocityAndPosition();

        return;

        if(Input.IsActionPressed("TurnLeft"))
        {
           // TurnLeft(torqueSpeed);
           //Rotate((float)Math.PI/2);
           //SetGlobalRotation((float)Math.PI/2);
           GlobalRotationDegrees = -90;
        }

        if(Input.IsActionPressed("TurnRight"))
        {
           // TurnRight(torqueSpeed);
           //Rotate((float)Math.PI/2);
           //SetGlobalRotation((float)Math.PI/2);
           GlobalRotationDegrees = 90;
        }
    

 

        
        if(RotationDegrees > 90)
        {
            //AddTorque(torqueSpeed);
            //AngularDamp = 0.0f;
            //for(int i =0; i<100; i ++) 
            //SetDirection(new Vector2(0,1));
            //Console.WriteLine();
            //SetDirection(new Vector2(0,-1));
            //SetDirection(new Vector2(1,0));
            //SetDirection(new Vector2(-1,0));
        }
            //FullSpeedAhead();
            //dataForRadar.Position = Position;
            //EmitSignal(nameof(RadarDataUpdate), dataForRadar);
    }
}
