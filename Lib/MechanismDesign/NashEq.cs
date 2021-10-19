using System;
using System.Collections.Generic;
using XuMath;

namespace Lib.MechanismDesign
{
    public static class NashEq
    {
        
        public static List<Tuple<int, int, double>> FindNashEq(MatrixR P1, MatrixR P2)
        {
            int columnCount = P1.GetCols();
            int rowCount = P1.GetCols();
            var best_payouts = new List<Tuple<int, int, double>>
            {
                //new Tuple<int,int>(1,1),
            };
            var best_payout_labels = new List<Tuple<int, int, double>>
            {
                //new Tuple<int,int>(1,1),
            };
            // column then row
            for (int c = 0; c < rowCount; c++)
            {
                // get max_payout per column
                double max_payout = double.NegativeInfinity;
                for (int r = 0; r < columnCount; r++)
                {
                    //Console.WriteLine(P1[r,c]);
                    if(P1[r,c] > max_payout)
                        max_payout = P1[r,c];
                }
                for (int r = 0; r < columnCount; r++)
                {
                    if(P1[r,c] == max_payout){
                        best_payouts.Add(new Tuple<int,int, double>(r,c,max_payout));
                    }
                }
            }
            // row then column
            for (int r = 0; r < rowCount; r++)
            {
                double max_payout = double.NegativeInfinity;
                for (int c = 0; c < columnCount; c++)
                {
                    //Console.WriteLine(P2[r,c]);
                    if(P2[r,c] > max_payout)
                        max_payout = P2[r,c];
                }
                for (int c = 0; c < columnCount; c++)
                {
                    if(P2[r,c] == max_payout)
                    {
                        var item = best_payouts.Find(x => x.Item1 == r && x.Item2 == c);
                        if(item != null)
                        {
                            best_payout_labels.Add(item);
                        }
                    }
                }
            }
            return best_payout_labels;
        }

    }
}
