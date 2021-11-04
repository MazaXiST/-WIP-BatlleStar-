using Godot;
using System;

public class BattleStarShip : RigidBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    protected readonly float SAME_DIRECTION_ANGLE = 0.075f;    
    //protected readonly double ROTATION_OFFSET = Math.PI/2;
    protected readonly float MAX_SPEED = 500.0f;
    protected readonly float MAX_THRUST_ACCELERATION = 1000.0f;

    protected readonly float MAX_TORQUE_ACCELERATION = 3000.0f;
    protected float torqueAcceleration = 3000.0f;
    protected float thrustAcceleration = 1000.0f;
    public Vector2 [] EngineTrace { get; private set;} = new Vector2[Game.engineTraceCapacity];
    private int currentIndexEngineTrace = 0;
    protected bool isOutsideField = false;
    protected Vector2 direction  { get; set; }
    protected int MAX_HIT_POINTS = 100;
    protected int MIN_HIT_POINTS = 0;
    protected int hitPoints = 100;
    protected int HitPoints { 
                                get{ 
                                    return hitPoints;
                                    } 
                                set{
                                    if (value >= 0 && value <= 100)
                                    hitPoints = value;
                                   }
                                }
    protected int ReloadTime { get; set; } = 100;
    protected DataForRadar dataForRadar;
    protected RailCannon[] leftBoardCannons = new RailCannon[3];
    protected RailCannon[] rightBoardCannons = new RailCannon[3];
    protected Line2D trail;
    protected AnimationPlayer burnAnim;
    [Signal]
    protected delegate void ThrustUpdated( Vector2 AppliedForce);//, Vector2 position, Vector2 [] engineTrace, string className);
    [Signal]
    protected delegate void TorqueUpdated(Vector2 shipDirection);
    [Signal]
    protected delegate void PositionUpdated(Vector2 position, Vector2 velocity, string className);
    
    //Сигнал который отпраляется после создания очередного объекта корабля
    //для того чтобы оповестить объект радара, что нужно добавть новый объект
    //для прорисовки и на интерфейсе пользователя
    [Signal]
    protected delegate void UnitAdd(DataForRadar unit);

    //Сигнал который отпраляется после уничтожения очередного объекта корабля
    //для того чтобы оповестить объект радара, что нужно убрать объект с индексом
    //из прорисовки и на интерфейсе пользователя
    [Signal]
    protected delegate void UnitRemove(int index);    
    [Signal]
    protected delegate void RadarDataUpdate(DataForRadar unitData);


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        hitPoints = 100;

        leftBoardCannons[0] = (RailCannon)GetNode("LeftBoardCannon1");
        leftBoardCannons[1] = (RailCannon)GetNode("LeftBoardCannon2");
        leftBoardCannons[2] = (RailCannon)GetNode("LeftBoardCannon3");
        
        rightBoardCannons[0] = (RailCannon)GetNode("RightBoardCannon1");
        rightBoardCannons[1] = (RailCannon)GetNode("RightBoardCannon2");
        rightBoardCannons[2] = (RailCannon)GetNode("RightBoardCannon3");

        burnAnim = (AnimationPlayer)GetNode("Trail").GetNode("BurnTrailAnim");

        var direction = GetDirection();
        AddCentralForce(direction*0);
        EmitSignal(nameof(PositionUpdated), Position, LinearVelocity, this.ToString());
        EmitSignal(nameof(TorqueUpdated), direction);
        EmitSignal(nameof(ThrustUpdated), AppliedForce);

        dataForRadar = new DataForRadar(
                                         Position, 
                                         EngineTrace,
                                         Game.engineTraceCapacity, 
                                         new Vector2(3,3), 
                                         new Color(255,255,255)
                                         );

        var err = this.Connect(nameof(UnitAdd), GetNode("../GUILayer/ShipUI/Radar"), nameof(Radar._on_Unit_add));//, _params);//,ff);//, 10);
        Console.WriteLine("Connection " + err);
        err = this.Connect(nameof(RadarDataUpdate), GetNode("../GUILayer/ShipUI/Radar"), nameof(Radar._on_Unit_data_update));//, _params);//,ff);//, 10);
        Console.WriteLine("Connection " + err);

        EmitSignal(nameof(UnitAdd), dataForRadar);
    }

 protected bool CheckSpeed(float speed)
 {
      if(CommonFunctions.VectorToSpeed(LinearVelocity) <= speed)
      return false;
      else return true;
 }
  protected bool CheckMaxSpeed()
  {
      return CheckSpeed(MAX_SPEED);
  }
  protected void UpdateEngineTrace()
    {
        EngineTrace[currentIndexEngineTrace] = Position;
        currentIndexEngineTrace++;
        if(currentIndexEngineTrace == Game.engineTraceCapacity) currentIndexEngineTrace = 0;
    }
 protected void BackToField()
 {
    if(!IsInTheField())
    if(SetDirectionToPoint(new Vector2(Game.FIELD_WIDTH/2,Game.FIELD_HEIGHT/2)))
    {
        //дать газу по вектору возврата на игровое поле
         FullSpeedAhead();
    }
 }

//получить текущее направление корабля (куда смотрит нос)
protected Vector2 GetDirection()
{
    var x = Math.Cos(Rotation);
    var y = Math.Sin(Rotation);
    
    return new Vector2((float)x,(float)y).Normalized();
}

//получить угол между двумя векторами
protected float GetAngleBetweenVectors(Vector2 vec1, Vector2 vec2)
{
    var lengthVec1 = CommonFunctions.VectorToSpeed(vec1);
    var lengthVec2 = CommonFunctions.VectorToSpeed(vec2);
    var scalarMultVecToVec = vec1.x * vec2.x + vec1.y *vec2.y;

    var cosVecToVec = scalarMultVecToVec/(lengthVec1 * lengthVec2);
    var angleACos = Math.PI - Math.Acos(cosVecToVec);
    //var angleACos = Math.Acos(cosVecToVec);

    return (float)angleACos;
}

protected bool SetDirection(Vector2 newDir, double accuracy = SAME_DIRECTION_ANGLE)
{   
        var shipDirection = GetDirection();
        var newDirNorm    = newDir.Normalized();
        var angleACos     = Math.PI - GetAngleBetweenVectors( shipDirection,newDirNorm);    
        
        if(angleACos >= accuracy)
        {
        var scalarMulti = shipDirection.x *newDirNorm.y -  shipDirection.y *newDirNorm.x; 
        ApplyTorqueImpulse((float)angleACos*Math.Sign(scalarMulti)*torqueAcceleration+0.0001f);

        return false;
        }
        else return true;
}

protected bool SetDirectionToPoint(Vector2 point)
{
   return SetDirection(point - Position);
}

public bool FullSlowDown()
{
    return SlowDown(this.MAX_THRUST_ACCELERATION, Math.PI/16);
}
public bool SlowDown(float breakingVelocity, double accuracy = SAME_DIRECTION_ANGLE)
{
    //if(AppliedForce == new Vector2(0f,0f)) return true;
    //если скорость корабля меньше 1, то приравниваем скорость нулю, делаем полную остановку
    if(CommonFunctions.VectorToSpeed(LinearVelocity) <= 1f) 
    {
        AddCentralForce(AppliedForce*-1);
        return true;
    }

    if(SetDirection(AppliedForce*-1, accuracy))
    {
        SpeedAhead(breakingVelocity);
    }
    return false;
}
public void FullSpeedAhead()
{
    SpeedAhead(this.MAX_THRUST_ACCELERATION);
}

public void SpeedAhead(float acceleration)
{
          direction = GetDirection();
             if(CheckMaxSpeed())
                if(GetAngleBetweenVectors(AppliedForce * -1, direction) <= Math.PI/2)
                //if(direction.AngleTo( AppliedForce*-1) <= SAME_DIRECTION_ANGLE)
                return;

          burnAnim.Play("BurnTrailAnimation");
          AddCentralForce(direction*acceleration);
          UpdateEngineTrace();
          EmitSignal(nameof(ThrustUpdated), AppliedForce);
}

protected void UpdateRotationUI()
{
      if(this.AngularVelocity != 0.0)
        {
            EmitSignal(nameof(TorqueUpdated), GetDirection());
        }
}

protected bool IsInTheField()
{
    var gameField = new Rect2(Game.ZERO_POINT, Game.FIELD_WIDTH,Game.FIELD_HEIGHT);
    
    if(gameField.HasPoint(Position))
        return true;
    else 
        return false;
}

protected void UpdateVelocityAndPosition()
{
      if(CommonFunctions.VectorToSpeed(LinearVelocity) != 0.0)
      {            
            dataForRadar.Position = Position;
            EmitSignal(nameof(RadarDataUpdate), dataForRadar);
            EmitSignal(nameof(PositionUpdated), Position, LinearVelocity, this.ToString());

            if(!IsInTheField()) BackToField();
      }
}

protected virtual void SelfDestroy()
{
    //animation
    QueueFree();
}
protected void UpdateHitPoint(int delta)
{
    hitPoints += delta;
    Console.WriteLine(hitPoints);
    if(hitPoints < 1)
    SelfDestroy();
}

protected void UpdateAppliedForce(Vector2 force)
{
    AddCentralForce(force);
}

protected void _on_BurnTrailAnim_animation_finished()
{
    Console.WriteLine("BurnTrailAnim_animation_finished");
    //burnAnim.Stop();
    //if(body.HasMethod("SelfKill"))
    //    body.Call("SelfKill");

    //Console.WriteLine("Collision!");
}
  // Called every frame. 'delta' is the elapsed time since the previous frame.

}
