/*
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
 */ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using P1;

namespace MathSample_HW1
{
    // aliases (can also be declared in global namespace)
    // Note: P1.Polygon requires area() in children classes to not be overriden
    // using Polygon = P1.Polygon; // regular class without polymorphism technics
    // using Polygon = P2.Polygon; // Contains virtual area()
    using Polygon = P3.Polygon; // Abstract (Pure virtual in C++) implementation
    class Program
    {
        static void Main(string[] args)
        {
            
            // Note that the base member data, which are "predefined" types, are implicitly initialized to 0
            Square square = new Square(); 
            Triangle triangle = new Triangle();
            square.width = 4;
            square.height = 4;
            triangle.width = 4;
            triangle.height = 4;
            Console.WriteLine("Square area: {0}", square.area());
            Console.WriteLine("Triangle area: {0}", triangle.area());

            // Example of polymorphism
            // Notice how the Polygon objects can't access it's children's (derived class') member data (area() in this case).
            // These declarations of Polygon objects do not call the Polygon constructor. Also, this type of declaration is
            //      mandatory if Polygon is an abstract class, else "Polygon poly = new Polygon();" would result in an error.
            Polygon poly_square = square;
            Polygon poly_triangle = triangle;
            Console.WriteLine("Poly_square area: {0}", poly_square.width * poly_square.height);
            Console.WriteLine("Poly_triangle area: {0}", poly_triangle.width * poly_triangle.height * 1/2);

            // When 'override' modifier is applied to area() in the derived classes. Otherwise, the statements below would return 0.
            Console.WriteLine("Poly_square area (using virtual method): {0}", poly_square.area());
            Console.WriteLine("Poly_square area (using virtual method): {0}", poly_triangle.area()); 

            

        }
    }
    
    public class Square : Polygon
    {
        public Square() { }
        public override double area()
        {
            return width * height;
        }
    }

    public class Triangle : Polygon
    {
        public override double area()
        {
            return 0.5 * width * height;
        }
    }
    
}

// Contains a regular public class Polygon
namespace P1
{
    public class Polygon
    {
        public uint width { get; set; }
        public uint height { get; set; }
        public Polygon()
        {
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

// Contains class Polygon with a virtual function
namespace P2
{
    public class Polygon
    {
        public uint width { get; set; }
        public uint height { get; set; }

        //public virtual double area() { return 0; } // if inheriting classes have their own definition
        public virtual double area() { return 0; }
        public Polygon()
        {
            Console.WriteLine("In Polygon Constructor");
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

// Contains an abstract Polygon class
namespace P3
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
