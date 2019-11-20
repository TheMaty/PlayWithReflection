using MathematicsForReflectionScenarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlayWithReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFrom("MathematicsForReflectionScenarios.dll");
            MethodInfo methodInfo = null;
            object result = null;

            //Rectangle
            Console.WriteLine("Rectangle ----------");
            Type type = assembly.GetType("MathematicsForReflectionScenarios.Rectangle");
            if (type != null)
            {
                ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(int), typeof(int) });
                object instance = constructor.Invoke(new object[] { 4, 7 });

                try
                {
                    methodInfo = type.GetMethod("Area");
                    result = methodInfo.Invoke(instance, null);
                    Console.WriteLine("Area Method = " + result.ToString());

                    methodInfo = type.GetMethod("Perimeter");
                    result = methodInfo.Invoke(instance, null);
                    Console.WriteLine("Perimeter Method = " + result.ToString());

                    object[] parametersArray = new object[] { 3, 8 };
                    methodInfo = type.GetMethod("customArea");
                    result = methodInfo.Invoke(instance, parametersArray);
                    Console.WriteLine("customArea Method = " + result.ToString());

                    parametersArray = new object[] { 3, 8 };
                    methodInfo = type.GetMethod("customPerimeter");
                    result = methodInfo.Invoke(instance, parametersArray);
                    Console.WriteLine("customPerimeter Method = " + result.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            //Square
            Console.WriteLine("Square ----------");
            type = assembly.GetType("MathematicsForReflectionScenarios.Square");
            if (type != null)
            {
                ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(int) });
                object instance = constructor.Invoke(new object[] { 4 });

                try
                {
                    methodInfo = type.GetMethod("Area");
                    result = methodInfo.Invoke(instance, null);
                    Console.WriteLine("Area Method = " + result.ToString());

                    methodInfo = type.GetMethod("Perimeter");
                    result = methodInfo.Invoke(instance, null);
                    Console.WriteLine("Perimeter Method = " + result.ToString());

                    object[] parametersArray = new object[] { 3 };
                    methodInfo = type.GetMethod("customArea");
                    result = methodInfo.Invoke(instance, parametersArray);
                    Console.WriteLine("customArea Method = " + result.ToString());

                    parametersArray = new object[] { 3 };
                    methodInfo = type.GetMethod("customPerimeter");
                    result = methodInfo.Invoke(instance, parametersArray);
                    Console.WriteLine("customPerimeter Method = " + result.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Program prg = new Program();
                
                Rectangle rec = new Rectangle(1, 2);
                rec.ColorCode = 0;

                Console.WriteLine("Reflaction : Dynamic Invoke parameter and return objects are same class Rectangle to Rectangle ----------");
                Rectangle r = (Rectangle)prg.moveColorCodeTo1(rec);
                Console.WriteLine("Color Code = " + r.ColorCode.ToString());

                Console.WriteLine("Reflaction : Dynamic Invoke parameter and return objects are different classes Rectangle to Square ----------");
                Square s = (Square)prg.moveColorCodeTo3(rec);
                Console.WriteLine("Color Code = " + s.ColorCode.ToString());

                Console.WriteLine("Reflaction : Circle class through generic type ----------");
                var genericClass = prg.CircleClassOnTheFly();
                type = genericClass.GetType();
                foreach(PropertyInfo prop in type.GetProperties())
                {
                    object instanceTemp = prop.GetValue(genericClass);
                    Type propType = prop.GetValue(genericClass).GetType();
                                        
                    if (propType.GetProperties().Length > 0) // property is a reference to another class or struct
                    {
                        foreach (PropertyInfo pInfo in propType.GetProperties())
                        {
                            if (pInfo.Name == "Method")
                            {
                                methodInfo = (MethodInfo)pInfo.GetValue(instanceTemp);
                                Func<int> converted = (Func<int>) Delegate.CreateDelegate(pInfo.ReflectedType, null, methodInfo);
                                Console.WriteLine(prop.Name + " Method = " + converted().ToString());

                            }
                            else
                                if (pInfo.Name != "Target")
                                    Console.WriteLine(pInfo.Name + " = " + pInfo.GetValue(instanceTemp));
                        }
                    }
                    else
                    {
                        Console.WriteLine(prop.Name + " = " + prop.GetValue(genericClass).ToString());
                    }
                }
            }
        }

        public Shape moveColorCodeTo1 (Shape shape)
        {
            Assembly assembly = Assembly.LoadFrom("MathematicsForReflectionScenarios.dll");
            Type type = assembly.GetType(shape.GetType().ToString());
            ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(int), typeof(int) });
            object instance = constructor.Invoke(new object[] { 4, 7 });
            PropertyInfo propInfo = type.GetProperty("ColorCode");


            propInfo.SetValue(instance, 1);

           return (Shape)instance;
        }

        public Shape moveColorCodeTo3(Shape shape)
        {
            Assembly assembly = Assembly.LoadFrom("MathematicsForReflectionScenarios.dll");
            Type type = assembly.GetType("MathematicsForReflectionScenarios.Square");
            ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(int) });
            object instance = constructor.Invoke(new object[] { 4 });
            PropertyInfo propInfo = type.GetProperty("ColorCode");


            propInfo.SetValue(instance, 3);

            return (Shape)instance;
        }

        public object CircleClassOnTheFly()
        {
            var Circle = new
            {
                shape = new Shape() ,
                radious = -1,
                pi = -1,
                Area = new Func<int>(() => { return (2 * 4 * 4); }),
                Perimeter = new Func<int>(() => { return (2 * 3 * 4); })
            };
            Assembly assembly = Assembly.LoadFrom("MathematicsForReflectionScenarios.dll");
            Type type = Circle.GetType();
            ConstructorInfo constructor = type.GetConstructors()[0];
            object instance = constructor.Invoke(new object[] { new Shape() { ColorCode = 1 } , 4 , 3, Circle.Area, Circle.Perimeter });

            return instance;
        }
    }
}
