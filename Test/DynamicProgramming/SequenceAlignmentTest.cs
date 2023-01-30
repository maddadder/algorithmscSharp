using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Model;
using Extensions;
using Lib.BinarySearchTree;
using System.Text;
using Lib.DynamicProgramming;
namespace Test.DynamicProgramming
{
    
    [TestClass]
    public class SequenceAlignmentTest
    {

        [TestMethod]
        [DataRow("AGGGCT", "AGGC A", 4, 5, 13)]
        [DataRow("ABC DEF", "ABCDEF", 4, 5, 4)]
        [DataRow("ACACATGCATCATGACTATGCATGCATGACTGACTGCATGCATGCATCCATCATGCATGCATCGATGCATGCATGACCACCTGTGTGACACATGCATGCGTGTGACATGCGAGACTCACTAGCGATGCATGCATGCATGCATGCATGC", "ATGATCATGCATGCATGCATCACACTGTGCATCAGAGAGAGCTCTCAGCAGACCACACACACGTGTGCAGAGAGCATGCATGCATGCATGCATGCATGGTAGCTGCATGCTATGAGCATGCAG", 4, 5, 224)]
        public void SequenceAlignment_Test(string X, string Y, int cost_gap, int cost_mismatch, int expectedResult){
            SequenceAligner sq = new SequenceAligner();
            var value = sq.sequenceAlignment(X.ToCharArray(), Y.ToCharArray(), cost_gap, cost_mismatch);
            sq.sequenceAlignmentReconstruction(X.ToCharArray(), Y.ToCharArray(), cost_gap, cost_mismatch);
            Debug.WriteLine($"Result: {value}.");
            Assert.AreEqual(expectedResult, value);
        }
        
    }
}
