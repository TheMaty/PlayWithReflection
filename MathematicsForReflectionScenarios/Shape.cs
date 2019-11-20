using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicsForReflectionScenarios
{
    public class Shape : IShape
    {
        private int colorCode=-1;
        public int ColorCode { get { return colorCode; } set { colorCode = value; } }

        public int Area()
        {
            throw new NotImplementedException();
        }

        public int Perimeter()
        {
            throw new NotImplementedException();
        }
    }
}
