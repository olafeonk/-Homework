﻿﻿﻿using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X, Y;

        public double GetLength()
        {
            return Geometry.GetLength(new Vector {X = X, Y = Y});
        }

        public Vector Add(Vector vector)
        {
            return Geometry.Add(new Vector{X = X, Y = Y}, vector);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(new Vector{X = X, Y = Y}, segment);
        }
    }

    public class Segment
    {
        public Vector Begin, End;

        public double GetLength()
        {
            return Geometry.GetLength(new Segment {Begin = Begin, End = End});
        }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, new Segment {Begin = Begin, End = End});
        }
    }
    
    public class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector Add(Vector vectorA, Vector vectorB)
        {
            return new Vector { X = vectorA.X + vectorB.X, Y = vectorA.Y + vectorB.Y};
        }

        public static Vector Deduct(Vector vectorA, Vector vectorB)
        {
            return new Vector { X = vectorA.X - vectorB.X, Y = vectorA.Y - vectorB.Y};
        }
        
        public static double Cross(Vector vectorA, Vector vectorB)
        {
            return vectorA.X * vectorB.Y - vectorA.Y * vectorB.X;
        }

        public static double GetLength(Segment segment)
        {
            return GetLength(Deduct(segment.Begin, segment.End));
        }

        public static bool IsPointInVector(Vector vector, double point)
        {
            return vector.X <= point && point <= vector.Y || vector.Y <= point && point <= vector.X;
        }
        
        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            return Math.Abs(Cross(Deduct(vector, segment.Begin), Deduct(segment.End, segment.Begin))) < double.Epsilon
                   && IsPointInVector(new Vector {X = segment.Begin.X, Y = segment.End.X}, vector.X)
                   && IsPointInVector(new Vector {X = segment.Begin.Y, Y = segment.End.Y}, vector.Y);
        }
    }
}