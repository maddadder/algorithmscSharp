using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lib.Encoding
{
    // Huffman class which implements a Huffman tree

    public class Huffman
    {
        private Node HuffmanTree;   //Huffman tree to create codes and decode text    // Reference to the root of the tree
        private Dictionary<char, string> D = new Dictionary<char, string>(); // Dictionary to encode text
        Dictionary<char, int> charDictionary = new Dictionary<char, int>();  // Dictionary to store characters and frequencies

        public Huffman(string S)
        {
            Dictionary<char, int> charWithFreq = new Dictionary<char, int>();
            charWithFreq = this.AnalyzeText(S); // Calling analyzetext which retruns a dictionary of characters and frequencies
            this.Build(); // Building huffman tree with the given dictionary
            this.CreateCodes();
        }


        // Method : AnalyzeText
        // Parameters: Takes the string from the user
        // Return Type : Dictionary
        // Description: Given a certain string, a dictionary is created with characters and their frequencies, and returned.

        public Dictionary<char, int> AnalyzeText(string S) // Key is the character and the value is the frequency
        {
            foreach (char currentChar in S)
            {
                if (charDictionary.ContainsKey(currentChar)) /* If a certain char(key) exists in the dictionary, 
                                                                increase its frequency*/
                {
                    charDictionary[currentChar]++; // [] signifies to alter a value(frequency) of the 
                    // Specified key,  key == character
                }
                else
                {
                    charDictionary.Add(currentChar, 1);  //If it's a new character
                }
            }
            return charDictionary;
        }


        // Method : Build
        // Parameters: Dictionary of characters and their frequency
        // Return Type : Void
        /* Description: Given a dictionary, we build a tree using the Priority Queue 
            where 2 nodes with the highest priority are removed. */

        public void Build()
        {
            // Implementing priority queue class and using it's methods to mainpulate the huffman tree
            PriorityQueue<Node, int> PriorityQueue;

            PriorityQueue = new PriorityQueue<Node, int>(this.charDictionary.Count); // Creating a priority queue of Nodes 
                                                        // Based on the size of the dictionary(char,frequency)

            foreach (KeyValuePair<char, int> entry in this.charDictionary) // Adding each character as a node
            {
                Node q;
                q = new Node(entry.Key, entry.Value, null, null);
                PriorityQueue.Enqueue(q, q.Frequency);              // Creating leaf nodes 
            }

            /* To create root nodes, HuffmanTree gets put back into the queue each time until size !=1*/
            while (PriorityQueue.Count != 1)
            {
                // Removing the two highest priority nodes
                // Less frequent == higher priority
                Node left = PriorityQueue.Dequeue();
                Node right = PriorityQueue.Dequeue();

                // Error checking if user enters nothing
                if (left == null || right == null)
                    throw new ArgumentException("Input cannot be empty, exiting program.");

                // Forming a root node by adding the removed nodes frequency
                HuffmanTree = new Node(default(char), left.Frequency + right.Frequency, left, right);
                PriorityQueue.Enqueue(HuffmanTree,HuffmanTree.Frequency);
            }
            HuffmanTree = PriorityQueue.Dequeue(); // First node in the queue will be the tree created with all the left and right references
        }




        // Method : CreateCodes
        // Parameters: Root Node(Tree) and an empty string of bits
        // Return Type : Void
        /* Description: Given a root(tree), traversal is done of the tree recursively whilst appending 
            bits and adding them to dictionary D when we reach a leaf node. Key => char, Value=> bits */
        public void CreateCodes()
        {
            this.CreateCodesRecursive(this.HuffmanTree, "");
        }
        private void CreateCodesRecursive(Node HuffmanTree, string bits)
        {
            if (charDictionary.Count == 1) // If we only have one character(no distinct characters)
                D.Add(HuffmanTree.Character, "0");  // Append a 0(value) to a given character(key) into Dictionary D

            else if (HuffmanTree.Left == null && HuffmanTree.Right == null) //If we reach base node
                D.Add(HuffmanTree.Character, bits); // Add all the bits appended to the Dictionary along with the character

            else
            {
                if (HuffmanTree.Left != null)
                {
                    CreateCodesRecursive(HuffmanTree.Left, bits + "0"); // Traverse on the left of the tree recursively
                }

                if (HuffmanTree.Right != null)
                {
                    CreateCodesRecursive(HuffmanTree.Right, bits + "1"); // Traverse on the right of the tree recursively
                }
            }
        }



        // Method : Encode
        // Parameters: String the user entered
        // Return Type : String of Bits
        /* Description: Dictionary D has all the characters and the required bits to enocode the string passed.
            This method simply appends all the bits(value) given a certain char(key)*/

        public string Encode(string s)
        {
            string bits = "";

            foreach (char c in s) // Looping through each char in given string
                bits += D[c];  // Appending bits(value) from the dictionary based on the current char in s

            return bits; // z => 10101
        }


        // Method : Decode
        // Parameters: String of bits(generated through Encode), Root of the tree
        // Return Type : Decoded Text(String)
        /* Description: Traversal of the tree is done iteratively based on the path 
            specified by bits generated "0"=> left, "1"=>right. */
        public string Decode(string S)
        {
            Node temp = this.HuffmanTree;    // Reference to the root
            string decoded = "";  // Variable to store the decoded text

            if (temp.Left == null && temp.Right == null) // If we only have one character in the tree
            {
                foreach (char v in S) // Loop through all the bits and decode append the character
                {
                    decoded += HuffmanTree.Character;
                }
            }
            else
            {
                // Traversing the tree based on the path given by the string
                foreach (char c in S) // Looping through the bits
                {
                    if (c == '0')           // If it's 0, go left
                        temp = temp.Left;
                    else                     // Else go right
                        temp = temp.Right;

                    if (temp.Left == null && temp.Right == null) // If we reach the leaf node
                    {
                        decoded += temp.Character;        // Append the character to decoded
                        temp = HuffmanTree; // Start at the root once again once a char has been found
                    }
                }
            }
            return decoded; // 10101 => z
        }


        // Method : Print
        // Parameters: String of bits(generated through Encode), Root of the tree
        // Return Type : Decoded Text(String)
        /* Description: Print method to display the binary tree at a 90 degree angle. Done 
            recursively. ---The code for this method was taken from the lecture slides on Binary Trees--- */

        public void PrintHuffmanTree()
        {
            PrintRecursive(this.HuffmanTree,1);
        }
        public void PrintRecursive(Node p, int padding)
        {
            if (p != null)
            {
                if (p.Right != null)
                {
                    PrintRecursive(p.Right, padding + 4);
                }
                if (padding > 0)
                {
                    Console.Write(" ".PadLeft(padding));
                }
                if (p.Right != null)
                {
                    Console.Write("/\n");
                    Console.Write(" ".PadLeft(padding));
                }
                Console.Write($"{p.Frequency}:{p.Character}\n");
                if (p.Left != null)
                {
                    Console.Write(" ".PadLeft(padding) + "\\\n");
                    PrintRecursive(p.Left, padding + 4);
                }
            }
        }
    }
}