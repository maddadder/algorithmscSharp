using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XuMath;
using Lib.MechanismDesign;

namespace Test
{
    [TestClass]
    public class TestNashEq
    {
        
        [TestMethod]
        public void Test_Prisoner_Dilemma()
        {
            
            Debug.WriteLine("Prisoner's Dilemma");
            MatrixR P1columns = new MatrixR(new double[,]{{ 3, 0, },
                                                         {  5, 1, }});
            MatrixR P2rows = new MatrixR(new double[,]{{ 3, 5, },
                                                      {  0, 1, }});
            var test = NashEq.FindNashEq(P1columns, P2rows);
            foreach (var tuple in test)
            {
                Debug.WriteLine("P1 {0} - P2 {1} - Score {2}", tuple.Item1, tuple.Item2, tuple.Item3);
            }
            
        }
        [TestMethod]
        public void Test_Hawk_Dove()
        {
            Debug.WriteLine("Hawk - Dove");
            var P1columns = new MatrixR(new double[,]{{ 0, 1, },
                                                      { 3, 2, }});
            var P2rows = new MatrixR(new double[,]{{ 0, 3, },
                                                   { 1, 2, }});
            var test = NashEq.FindNashEq(P1columns, P2rows);
            foreach (var tuple in test)
            {
                Debug.WriteLine("P1 {0} - P2 {1} - Score {2}", tuple.Item1, tuple.Item2, tuple.Item3);
            }

        }
        [TestMethod]
        public void Test_Pigs_Game()
        {
            Debug.WriteLine("Pigs Game");
            var P1columns = new MatrixR(new double[,]{{ 4, 2, },
                                                      { 2, 3, }});
            var P2rows = new MatrixR(new double[,]{{ 6, 0, },
                                                  { -1, 0, }});
            var test = NashEq.FindNashEq(P1columns, P2rows);
            foreach (var tuple in test)
            {
                Debug.WriteLine("P1 {0} - P2 {1} - Score {2}", tuple.Item1, tuple.Item2, tuple.Item3);
            }
        }
        [TestMethod]
        public void Test_Matching_Pennies()
        {
            Debug.WriteLine("Matching Pennies");
            var P1columns = new MatrixR(new double[,]{{ 1, -1, },
                                               { -1, 1, }});
            var P2rows = new MatrixR(new double[,]{{ -1, 1, },
                                               { 1, -1, }});
            var test = NashEq.FindNashEq(P1columns, P2rows);
            foreach (var tuple in test)
            {
                Debug.WriteLine("P1 {0} - P2 {1} - Score {2}", tuple.Item1, tuple.Item2, tuple.Item3);
            }
        }
    }
}
