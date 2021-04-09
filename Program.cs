using System;
using System.Linq;
using System.Text;

namespace algorithmscSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "THE OFFICE party will be in Dec. this year.  the ExacT date will be sET by mid Nov.  hope to see you ALL there!  tickets are $15.00 to help cover the cost of Catering.";
            string output = TextInputHandler.formatInput(input);
            Console.WriteLine(output);
        }
    }
}
