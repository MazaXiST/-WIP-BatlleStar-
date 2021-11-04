using Godot;
using System;
	
	public static class CommonFunctions{	
		private static Random rand = new Random();
        //переделать на нормальный генератор рандома смотри на канале ExtremeCode
		public static int GetRandomValueI(int min, int max)
        {
            return rand.Next(min,max);
        }

        public static float GetRandomValueF(int min, int max, int symbolsAfterPoint)
        {
            if(min <1 || min >= max) throw new ArgumentException("Wrong MIN agument value!");
            var randInt = GetRandomValueI(min, max);
            var decimalPart = (float)(1.0/Math.Pow(10, symbolsAfterPoint));
            return (float)randInt * decimalPart;
        }

        public static Vector2 GetRandomVector2(int xmin, int xmax, int ymin, int ymax)
        {   
            var x = GetRandomValueI(xmin, xmax);
            var y = GetRandomValueI(ymin, ymax);
            return new Vector2(x,y);
        }

        public static Vector2 GetRandomVector2(float xmin, float xmax, float ymin, float ymax)
        {   
            var x = GetRandomValueI((int)xmin, (int)xmax);//F((int)xmin, (int)xmax, 1);
            var y = GetRandomValueI((int)ymin, (int)ymax);//(int)ymin, (int)ymax, 1);
            return new Vector2(x,y);
        }

        public static float VectorToSpeed(Vector2 vector)
        {    
            return Convert.ToSingle(Math.Sqrt(vector.x*vector.x + vector.y*vector.y));
        
            //return (float)Math.Sqrt(vector.x*vector.x + vector.y*vector.y);
        }
};
	
