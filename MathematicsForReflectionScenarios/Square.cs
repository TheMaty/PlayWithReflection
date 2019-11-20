using System;

namespace MathematicsForReflectionScenarios
{
    public sealed class Square : Shape
    {
        int side;
        public Square()
        {

        }

        public Square(int Side)
        {
            side = Side;
        }

        public new int Area()
        {
            return side * side;
        }
        public new int Perimeter()
        {
            return 4 * side;
        }

        public int customArea(int Side)
        {
            return Side * Side;
        }

        public int customPerimeter(int Side)
        {
            return 4 * Side;
        }
    }
}
