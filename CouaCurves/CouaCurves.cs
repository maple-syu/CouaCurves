namespace CouaCurves
{
    using CouaFloat = System.Single;
    public struct CouaVector2
    {
        public CouaFloat x;
        public CouaFloat y;

        public CouaVector2(CouaFloat x, CouaFloat y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(CouaVector2 a, CouaVector2 b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(CouaVector2 a, CouaVector2 b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public static CouaVector2 operator +(CouaVector2 a, CouaVector2 b)
        {
            return new CouaVector2(a.x + b.x, a.y + b.y);
        }
        public static CouaVector2 operator -(CouaVector2 a, CouaVector2 b)
        {
            return new CouaVector2(a.x - b.x, a.y - b.y);
        }

        public static CouaVector2 Zero { get { return new CouaVector2(0f, 0f); } }
        public static CouaVector2 One { get { return new CouaVector2(1f, 1f); } }
    }

    public class CouaBezier
    {
        CouaVector2 pointA, pointB, pointC, pointD;

        public CouaBezier()
        {
            pointA = new CouaVector2(0f, 0f);
            pointB = new CouaVector2(0.33f, 0f);
            pointC = new CouaVector2(0.66f, 0f);
            pointD = new CouaVector2(1f, 0f);
        }
        public CouaBezier(CouaVector2 pA, CouaVector2 pB, CouaVector2 pC, CouaVector2 pD)
        {
            pointA = pA;
            pointB = pB;
            pointC = pC;
            pointD = pD;
        }
        public CouaBezier(CouaFloat pA, CouaFloat vA, CouaFloat pB, CouaFloat vB, CouaFloat pC, CouaFloat vC, CouaFloat pD, CouaFloat vD)
        {
            pointA = new CouaVector2(pA, vA);
            pointB = new CouaVector2(pB, vB);
            pointC = new CouaVector2(pC, vC);
            pointD = new CouaVector2(pD, vD);
        }

        public void SetPointA(CouaFloat position, CouaFloat value)
        {
            pointA.x = position;
            pointA.y = value;
        }
        public void SetPointB(CouaFloat position, CouaFloat value)
        {
            pointB.x = position;
            pointB.y = value;
        }
        public void SetPointC(CouaFloat position, CouaFloat value)
        {
            pointC.x = position;
            pointC.y = value;
        }
        public void SetPointD(CouaFloat position, CouaFloat value)
        {
            pointD.x = position;
            pointD.y = value;
        }
        public float EvaluateY(float t)
        {
            float omt = 1 - t;
            float eval = omt * (omt * (omt * pointA.y + t * pointB.y) + t * (omt * pointB.y + t * pointC.y)) + t * (omt * (omt * pointB.y + t * pointC.y) + t * (omt * pointC.y + t * pointD.y));
            return eval;
        }
        public float EvaluateX(float t)
        {
            float omt = 1 - t;
            float eval = omt * (omt * (omt * pointA.x + t * pointB.x) + t * (omt * pointB.x + t * pointC.x)) + t * (omt * (omt * pointB.x + t * pointC.x) + t * (omt * pointC.x + t * pointD.x));
            return eval;
        }

        public CouaVector2 EvaluateCoords(float t)
        {
            float omt = 1 - t;
            float evalX = omt * (omt * (omt * pointA.x + t * pointB.x) + t * (omt * pointB.x + t * pointC.x)) + t * (omt * (omt * pointB.x + t * pointC.x) + t * (omt * pointC.x + t * pointD.x));
            float evalY = omt * (omt * (omt * pointA.y + t * pointB.y) + t * (omt * pointB.y + t * pointC.y)) + t * (omt * (omt * pointB.y + t * pointC.y) + t * (omt * pointC.y + t * pointD.y));
            return new CouaVector2(evalX, evalY);
        }

        public static CouaBezier SineCurve { get { return new CouaBezier(0f, 0f, 0f, 0.6f, 0.4f, 1f, 1f, 1f); } }
        public static CouaBezier InverseSineCurve { get { return new CouaBezier(0f, 0f, 0.6f, 0f, 1f, 0.4f, 1f, 1f); } }
        public static CouaBezier SCurve { get { return new CouaBezier(0f, 0f, 0.8f, 0.2f, 0.2f, 0.8f, 1f, 1f); } }
        public static CouaBezier HillCurve { get { return new CouaBezier(0f, 0f, 0.2f, 1.3f, 0.8f, 1.3f, 0f, 0f); } }
    }
}
