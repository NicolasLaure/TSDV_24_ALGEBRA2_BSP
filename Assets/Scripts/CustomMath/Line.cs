using System.Collections.Generic;

namespace CustomMath
{
    public class Line
    {
        public Vec3 startPos;
        public Vec3 endPos;
        public List<Vec3> points = new List<Vec3>();
        public Line(Vec3 startPos, Vec3 endPos)
        {
            this.startPos = startPos;
            this.endPos = endPos;
        }
        public void SetLine(Vec3 startPos, Vec3 endPos)
        {
            this.startPos = startPos;
            this.endPos = endPos;
        }
    }
}