﻿/*
 * There is no implicit declarations in C# compared to C++. For example, Square square; is invalid
 * because if square were to be used (ex. square.width), the compiler would return "use of unassigned 
 * local variable." To resolve this do Square square = new Square();
 * 
 * Any class in c# is a reference class.
 * If you pass a variable into a function where the function parameter is of type 'out', then that variable
 * will be reinitialized to 0. Example: OutIntChange(out myInt){ return ++myInt; } // myInt = 1 regardless of previous value.
 * 
 * A class cannot inherit from another class whose access denomination is higher.
 * Example: Given "public class Polygon", "class Square : Polygon" would be illegal
 * because Square has an access of 'internal' (note: it's not explicitly specified). To
 * resolve this issue, must define Square class as : "public class Square." 
 * 
 * Static classes must derive from objects.
 * 
 * Abstract classes can't be instantiated and they must be inherited. Abstract methods are implicitly
 * virtual and are only allowed in abstract classes.
 * 
 * "sealed" modifier prevents a class from being inherited
 * 
 * C# does not allow deterministic disposition of objects, etc. hench managed data.
 * 
 * Using delegates, the library can be altered at runtime.
 * 
 * Delegates are used to pass methods as arguments to other methods. Compare this to C function pointers.
 * 
 * The ref and out keywords cause different run-time behavior and they are not considered part of the
 * method signature at compile time. Thus methods cannot be overloaded if the difference only has to do
 * with the inclusion/ exclusion of these keywords.
 * 
 * [1] http://www.codeproject.com/Articles/992340/Generic-Math-in-Csharp-Using-Runtime-Compilation
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.CodeDom.Compiler; // CompilerParameters, GenerateInMemory
using Microsoft.CSharp; // CSharpCodeProvider
using System.Reflection; // MethodInfo

//using PClass1;
 using PClass2;
// using PClass3;

namespace MathSample_HW1
{
    // using PClass2;
    // aliases (can also be declared in global namespace). Closest thing to C++ typedef
    // Note: PClass1.Polygon requires area() in children classes to not be overriden
    // using Polygon = PClass2.Polygon<double>; // Contains virtual area()
    // using Polygon = PClass3.Polygon;      // Abstract (Pure virtual in C++) implementation
    class Program
    {

        static void Main(string[] args)
        {
            // Note that the base member data, which are "predefined" types, are implicitly initialized to 0
            Square<double> square = new Square<double>();
            // Triangle triangle = new Triangle();
            square.width = 4;
            square.height = 4;
            // triangle.width = 4;
            // triangle.height = 4;
            Console.WriteLine("Square area: {0}", square.area());
            // Console.WriteLine("Triangle area: {0}", triangle.area());

            // Example of polymorphism
            // Notice how the Polygon objects can't access it's children's (derived class') member data (area() in this case).
            // These declarations of Polygon objects do not call the Polygon constructor. Also, this type of declaration is
            //      mandatory if Polygon is an abstract class, else "Polygon poly = new Polygon();" would result in an error.
            // Polygon poly_square = square;
            // Polygon poly_triangle = triangle;
            // Console.WriteLine("Poly_square area: {0}", poly_square.width * poly_square.height);
            // Console.WriteLine("Poly_triangle area: {0}", poly_triangle.width * poly_triangle.height * 1 / 2);

            // When 'override' modifier is applied to area() in the derived classes. Otherwise, the statements below would return 0.
            // Console.WriteLine("Poly_square area (using virtual method): {0}", poly_square.area());
            // Console.WriteLine("Poly_square area (using virtual method): {0}", poly_triangle.area());



        }
    }

    public class Square<T> : Polygon<T>
    {
        public Square() { }
        public override T area()
        {
            return Generic_Math<T>.Product(new T[] { width, height });
        }
    }
    /*
    public class Triangle : Polygon
    {
        public override T area()
        {
            return (T)Convert.ChangeType(0.5 * width * height, typeof(T));
        }
    }
    */

    public static class Generic_Math<T>   // see top comment [1]
    {
        public static Func<T[], T> Sum = (T[] array) =>
        {
            string code = "(System.Func<NUMBER[], NUMBER>)((NUMBER[] array) => { NUMBER sum = 0; for (int i = 0; i < array.Length; i++) sum += array[i]; return sum; })";
            code = code.Replace("NUMBER", typeof(T).ToString());
            Generic_Math<T>.Sum = Generate.Object< Func<T[], T> >(new string[] { }, new string[] { }, code);
            return Generic_Math<T>.Sum(array);
        };

        public static Func<T[], T> Product = (T[] array) =>
        {
            string code = "(System.Func<NUMBER[], NUMBER>)((NUMBER[] array) => { NUMBER product = 1; for (int i = 0; i < array.Length; i++) product *= array[i]; return product; })";
            code = code.Replace("NUMBER", typeof(T).ToString());
            Generic_Math<T>.Product = Generate.Object< Func<T[], T> >(new string[] { }, new string[] { }, code);
            return Generic_Math<T>.Product(array);
        };
    }

    internal static class Generate  // see top comment [1]
    {
        internal static T Object<T>(string[] references, string[] name_spaces, string code)
        {
            string full_code = string.Empty;
            if (name_spaces != null)
                for (int i = 0; i < name_spaces.Length; i++)
                    full_code += "using " + name_spaces[i] + ";";

            full_code += "namespace Seven.Generated {";
            full_code += "public class Generator {";
            full_code += "public static object Generate() { return " + code + "; } } }";

            CompilerParameters parameters = new CompilerParameters();
            foreach (string reference in references)
                parameters.ReferencedAssemblies.Add(reference);

            parameters.GenerateInMemory = true;
            Console.WriteLine(full_code);
            CompilerResults results = new CSharpCodeProvider().CompileAssemblyFromSource(parameters, full_code);

            if(results.Errors.HasErrors)
            {
                Console.WriteLine("In if(results.Errors.HasErrors");
                string error = string.Empty;
                foreach (CompilerError compiler_error in results.Errors)
                    error += compiler_error.ErrorText.ToString() + "\n";

                throw new Exception(error);
            }
            
            MethodInfo generate = results.CompiledAssembly.GetType("Seven.Generated.Generator").GetMethod("Generate");

            return (T)generate.Invoke(null, null);
        }
    }
}

// Contains class Polygon with a virtual function
namespace PClass2
{
    public class Polygon<T>
    {
        private T _width;
        private T _height;

        public T width {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }
        public T height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        //public virtual double area() { return 0; } // if inheriting classes have their own definition
        public virtual T area() { return (T)Convert.ChangeType(0, typeof(T)); }
        public Polygon()
        {
            // width = 0;
            // height = 0;
        }

        public Polygon(T w, T h)
        {
            width = w;
            height = h;
        }
    }
}

// Contains an abstract Polygon class
namespace PClass3
{
    public abstract class Polygon
    {
        public uint width { get; set; }
        public uint height { get; set; }

        //public virtual double area() { return 0; } // if inheriting classes have their own definition
        public abstract double area();
        public Polygon()
        {
            //Console.WriteLine("In Polygon Constructor");
            width = 0;
            height = 0;
        }

        public Polygon(uint w, uint h)
        {
            width = w;
            height = h;
        }
    }
}




// A compiler option to allow unsafe code must be provided.
// mcs *.cs -out:main.exe -unsafe, or set "allow unsafe" in VS Project Properties.
/*
unsafe {
    Square s;
    Polygon* poly_square;
    poly_square = &s;//new Square();
    Polygon* poly_triangle = &triangle;
    poly_square.width = 4;
    poly_square.height = 4;
    poly_triangle.width = 4;
    poly_triangle.height = 4;
    Console.WriteLine("Square area: {0}", )
}
*/
