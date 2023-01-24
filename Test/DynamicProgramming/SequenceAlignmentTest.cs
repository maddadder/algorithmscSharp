using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Model;
using Extensions;

namespace Test.DynamicProgramming
{
    
    [TestClass]
    public class SequenceAlignmentTest
    {
        int[, ] matrix;

        [TestMethod]
        [DataRow("AGGGCT", "AGGC A", 4, 5)]
        [DataRow("ABC DEF", "ABCDEF", 4, 5)]
        [DataRow("ACACATGCATCATGACTATGCATGCATGACTGACTGCATGCATGCATCCATCATGCATGCATCGATGCATGCATGACCACCTGTGTGACACATGCATGCGTGTGACATGCGAGACTCACTAGCGATGCATGCATGCATGCATGCATGC", "ATGATCATGCATGCATGCATCACACTGTGCATCAGAGAGAGCTCTCAGCAGACCACACACACGTGTGCAGAGAGCATGCATGCATGCATGCATGCATGGTAGCTGCATGCTATGAGCATGCAG", 4, 5)]
        public void SequenceAlignment_Test(string X, string Y, int cost_gap, int cost_mismatch){
            var value = sequenceAlignment(X.ToCharArray(), Y.ToCharArray(), cost_gap, cost_mismatch);
            sequenceAlignmentReconstruction(X.ToCharArray(), Y.ToCharArray(), cost_gap, cost_mismatch);
            Debug.WriteLine($"Result: {value}.");
        }
        public int sequenceAlignment(char[] X, char[] Y, int cost_gap, int cost_mismatch)
        {
            // subproblem solutions
            matrix = new int[X.Length+1, Y.Length+1];

            // base case
            for(var i = 0;i<=X.Length;i++)
            {
                matrix[i, 0] = i*cost_gap;
            }
            for(var j = 0;j<=Y.Length;j++)
            {
                matrix[0, j] = j*cost_mismatch;
            }

            // systematically solve all subproblems
            for(var i = 1;i<=X.Length;i++) {
                for(var j = 1;j<=Y.Length;j++) {
                    var match_cost = int.MaxValue;
                    if (X[i-1] == Y[j-1])
                    {
                        match_cost = 0;
                    }
                    else{
                        match_cost = cost_mismatch;
                    }
                    var case1 = matrix[i-1, j-1] + match_cost;
                    var case2 = matrix[i-1,j] + cost_gap;
                    var case3 = matrix[i,j-1] + cost_gap;
                    matrix[i,j] = Math.Min(Math.Min(case1, case2), case3);
                }
                Debug.WriteLine("");
            }
            return matrix[X.Length, Y.Length]; //solution to largest subproblem
        }
        public void sequenceAlignmentReconstruction(char[] x, char[] y, int cost_gap, int cost_mismatch)
        {
            // Reconstructing the solution
            var i = x.Length; 
            var j = y.Length;

            int l = i + j; // maximum possible length

            int xpos = l;
            int ypos = l;

            // Final answers for the respective strings
            int[] xans = new int[l+1];
            int[] yans = new int[l+1];

            while ( !(i == 0 || j == 0))
            {
                if (x[i - 1] == y[j - 1])
                {
                    xans[xpos--] = (int)x[i - 1];
                    yans[ypos--] = (int)y[j - 1];
                    i--; j--;
                }
                else if (matrix[i - 1,j - 1] + cost_mismatch == matrix[i,j])
                {
                    xans[xpos--] = (int)x[i - 1];
                    yans[ypos--] = (int)y[j - 1];
                    i--; j--;
                }
                else if (matrix[i - 1,j] + cost_gap == matrix[i,j])
                {
                    xans[xpos--] = (int)x[i - 1];
                    yans[ypos--] = (int)'_';
                    i--;
                }
                else if (matrix[i,j - 1] + cost_gap == matrix[i,j])
                {
                    xans[xpos--] = (int)'_';
                    yans[ypos--] = (int)y[j - 1];
                    j--;
                }
            }
            while (xpos > 0)
            {
                if (i > 0) xans[xpos--] = (int)x[--i];
                else xans[xpos--] = (int)'_';
            }
            while (ypos > 0)
            {
                if (j > 0) yans[ypos--] = (int)y[--j];
                else yans[ypos--] = (int)'_';
            }

            // Since we have assumed the answer to be n+m long,
            // we need to remove the extra gaps in the starting 
            // id represents the index from which the arrays
            // xans, yans are useful
            int id = 1;
            for (i = l; i >= 1; i--)
            {
                if ((char)yans[i] == '_' && (char)xans[i] == '_')
                {
                    id = i + 1;
                    break;
                }
            }

            // Printing the final answer
            Debug.Write("Minimum Penalty in aligning the genes = " + matrix[x.Length,y.Length] + "\n");
            Debug.Write("The aligned genes are :\n");
            for (i = id; i <= l; i++)
            {
                Debug.Write((char)xans[i]);
            }
            Debug.Write("\n");
            for (i = id; i <= l; i++)
            {
                Debug.Write((char)yans[i]);
            }
            Debug.Write("\n");
        }
    }
}
