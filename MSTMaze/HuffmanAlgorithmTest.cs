using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Lib.Encoding;

namespace Test.Encoding
{
    public class HuffmanAlgorithmTest
    {
        public void HuffmanAlgorithm_Test()
        {
            var input = "abcdefghhiijjkk";
            Huffman h = new Huffman(input);
            
            // Printing the huffman tree
            h.PrintHuffmanTree();

            // Encoding
            string bits = h.Encode(input);

            Console.WriteLine("Your currently encoded message is: ");
            Console.WriteLine(bits);

            Console.WriteLine("Number of bits in encoded message: ");
            Console.WriteLine(bits.Length);

            // Decoding
            Console.Write("Decoding ");
            Console.Write(bits);
            Console.Write(" yields the message: ");
            var decoded = h.Decode(bits);
            Console.WriteLine(decoded);
        }

    }
}
