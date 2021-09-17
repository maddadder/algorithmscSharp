using System;
using XuMath;

namespace algorithmscSharp.Eigenvalues
{
    public class EigenvaluesTester
    {
        
        public static void TestTridiagonalEigenvalues()
        {
            MatrixR A = new MatrixR(new double[,]{{ 5, 1, 2, 2, 4 },
                                                  { 1, 1, 2, 1, 0},
                                                  { 2, 2, 0, 2, 1},
                                                  { 2, 1, 2, 1, 2},
                                                  { 4, 0, 1, 2, 4}});
            int nn = 5;
            MatrixR xx = new MatrixR(A.GetCols(), nn);
            MatrixR V = Eigenvalue.Tridiagonalize(A);
            double[] lambda = Eigenvalue.TridiagonalEigenvalues(nn);
            for (int i = 0; i < nn; i++)
            {
                double s = lambda[i] * 1.001;
                double lam;
                VectorR x = Eigenvalue.TridiagonalEigenvector(s, 1e-8, out lam);
                for (int j = 0; j < A.GetCols(); j++)
                    xx[j, i] = x[j];
            }
            xx = V * xx;

            Console.WriteLine("\n Results from the tridiagonalization method:");
            Console.WriteLine("\n Eigenvalues: \n ({0,10:n6}  {1,10:n6}  {2,10:n6}  {3,10:n6}  {4,10:n6})", lambda[0],lambda[1],lambda[2],lambda[3],lambda[4]);
            Console.WriteLine("\n Eigenvectors:");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(" ({0,10:n6}  {1,10:n6}  {2,10:n6}  {3,10:n6}  {4,10:n6})", xx[i,0],xx[i,1],xx[i,2],xx[i,3],xx[i,4]);
            }



            A = new MatrixR(new double[,]{{ 5, 1, 2, 2, 4 },
                                          { 1, 1, 2, 1, 0},
                                          { 2, 2, 0, 2, 1},
                                          { 2, 1, 2, 1, 2},
                                          { 4, 0, 1, 2, 4}});

            MatrixR xm;
            VectorR lamb;
            Eigenvalue.Jacobi(A, 1e-8, out xm, out lamb);

            Console.WriteLine("\n\n Results from the Jacobi method:");
            Console.WriteLine("\n Eigenvalues: \n ({0,10:n6}  {1,10:n6}  {2,10:n6}  {3,10:n6}  {4,10:n6})", lamb[4], lamb[3], lamb[2], lamb[1], lamb[0]);
            Console.WriteLine("\n Eigenvectors:");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(" ({0,10:n6}  {1,10:n6}  {2,10:n6}  {3,10:n6}  {4,10:n6})", xm[i, 4], xm[i, 3], xm[i, 2], xm[i, 1], xm[i, 0]);
            }
        }

    }
}
