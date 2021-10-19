using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace algorithmscSharp.Selection
{

    public class TestReverse
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static void PrintOutput()
        {
            string input = RandomString((int)Math.Pow(10, 8));
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("test begin");
            sw.Start();
            string test = Reverse(input);
            sw.Stop();
            var testDuration = sw.Elapsed;
            Console.WriteLine("test end");
            Console.WriteLine(sw.Elapsed.Ticks);
            
            Console.WriteLine("control begin");
            sw.Reset();
            sw.Start();
            string control = Control(input);
            sw.Stop();
            var controlDuration = sw.Elapsed;
            Console.WriteLine("control end");
            Console.WriteLine(sw.Elapsed.Ticks);
            if(test != control){
                throw new Exception("values do not match");
            }
            if(testDuration > controlDuration){
                Console.WriteLine("Control Wins");
            }
            else{
                Console.WriteLine("Test Wins");
            }
        }
        public static string Reverse(string input)
        {
            char[] output = input.ToCharArray();
            int length = input.Length;
            int mid = length/2;
            int len = length - 1;
            for(var i=0;i<mid;i++)
            {
                char left = input[i];
                char right = input[len-i];
                output[i] = right;
                output[len-i] = left;
            }
            return new string(output);
        }
        public static string Control( string s )
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse( charArray );
            return new string( charArray );
        }
    }
}