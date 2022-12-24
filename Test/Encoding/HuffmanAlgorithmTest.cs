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
            var input = "asdf1234";
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
            Debug.WriteLine(h.Decode(bits));

        }

    }
}
