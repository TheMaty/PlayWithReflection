using System;

namespace MathematicsForReflectionScenarios
{
    public sealed class Rectangle : Shape
    {
        int height;
        int weight;
        public Rectangle()
        {

        }

        public Rectangle(int Height, int Weight)
        {
            height = Height;
            weight = Weight;
        }

        public new int Area()
        {
            return height * weight;
        }

        public new int Perimeter()
        {
            return 2 * (weight + height);
        }

        public int customArea(int Height, int Weight)
        {
            return Height * Weight;
        }

        public int customPerimeter(int Height, int Weight)
        {
            return 2 * (Height + Weight);
        }
    }
}
