using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Lib.Encoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Lib.Encoding.Node;

namespace Test.Encoding
{
    [TestClass]
    public class HuffmanAlgorithmTest
    {
        [TestMethod]
        public void HuffmanAlgorithm_Test()
        {
            var input = "abcdefghhiijjkk";
            Huffman h = new Huffman(input);
            
            // Printing the huffman tree
            h.PrintHuffmanTree();

            // Encoding
            string bits = h.Encode(input);

            Debug.WriteLine("Your currently encoded message is: ");
            Debug.WriteLine(bits);

            Debug.WriteLine("Number of bits in encoded message: ");
            Debug.WriteLine(bits.Length);

            // Decoding
            Debug.Write("Decoding ");
            Debug.Write(bits);
            Debug.Write(" yields the message: ");
            var decoded = h.Decode(bits);
            Debug.WriteLine(decoded);
            Assert.AreEqual(input,decoded);
        }
        public void Render_HuffmanAlgorithm()
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
