using System;
using XuMath;

namespace XuMath
{
    public class NonlinearSystem
    {
        public delegate double Function(double x);

        public NonlinearSystem()
        {
        }

        public static double IncrementSearch(Function f, double x0, double h, int nMaxIncrements)
        {
            double f0 = f(x0);
            double fx = f0, p = 0;
            double x = x0;
            for (int i = 0; i < nMaxIncrements; i++)
            {
                x = x0 + i * h;
                fx = f(x);
                p = f0 * fx;
                if (p < 0)
                    break;
            }
            if (p > 0)
                throw new ArgumentException("Solution not found!");
            else
            {
                return x = x - h * fx / (fx - (f(x - h)));
            }
        }

        public static double FixedPoint(Function f, double x0, double tolerance, int nMaxIterations)
        {
            double x1 = x0, x2 = x0;
            double tol = 0.0;
            for (int i = 0; i < nMaxIterations; i++)
            {
                x2 = f(x1);
                tol = Math.Abs(x1 - x2);
                x1 = x2;
                if (tol < tolerance)
                    break;
            }
            if (tol > tolerance)
            {
                throw new ArgumentException("Solution not found!");
            }
            return x1;
        }

        public static double Bisection(Function f, double xa, double xb, double tolerance)
        {
            double x1 = xa;
            double x2 = xb;
            double fb = f(xb);
            while (Math.Abs(x2 - x1) > tolerance)
            {
                double xm = 0.5 * (x1 + x2);
                if (fb * f(xm) > 0)
                    x2 = xm;
                else
                    x1 = xm;
            }
            return x2 - (x2 - x1) * f(x2) / (f(x2) - f(x1));
        }

        public static double FalsePosition(Function f, double xa, double xb, double tolerance)
        {
            double x1 = xa;
            double x2 = xb;
            double fb = f(xb);
            while (Math.Abs(x2 - x1) > tolerance)
            {
                double xm = x2 - (x2 - x1) * f(x2) / (f(x2) - f(x1));
                if (fb * f(xm) > 0)
                    x2 = xm;
                else
                    x1 = xm;
                if (Math.Abs(f(xm)) < tolerance)
                    break;
            }
            return x2 - (x2 - x1) * f(x2) / (f(x2) - f(x1));
        }

        public static double NewtonRaphson(Function f, Function f1, double x0, double tolerance)
        {
            double f0 = f(x0);
            double x = x0;
            while (Math.Abs(f(x)) > tolerance)
            {
                x -= f0 / f1(x);
                f0 = f(x);
            }
            return x;
        }

        public static double Secant(Function f, double xa, double xb, double tolerance)
        {
            double x1 = xa;
            double x2 = xb;
            double fb = f(xb);
            while(Math.Abs(f(x2)) > tolerance)
            {
                double xm = x2 - (x2 - x1) * fb / (fb - f(x1));
                x1 = x2;
                x2 = xm;
                fb = f(x2);
            }
            return x2;
        }

        public static double[] NewtonMultiRoots(Function f, double x0, int nRoots, 
                               int nIterations, double tolerance)
        {
            double h, delta = 10*tolerance, f1, f2, f3, x = x0;
            double[] roots = new double[nRoots];
            int nroot = 0, i =0;
            while (i < nIterations && nroot < nRoots)
            {
                i = 0;
                while (i < nIterations && Math.Abs(f(x)) > tolerance)
                {
                    if (Math.Abs(x) > 1)
                        h = 0.01 * x;
                    else
                        h = 0.01;
                    f1 = f(x - h);
                    f2 = f(x);
                    f3 = f(x + h);
                    if (nroot > 0)
                    {
                        for (int j = 0; j < nroot; j++)
                        {
                            f1 /= (x - h - roots[j]);
                            f2 /= (x - roots[j]);
                            f3 /= (x + h - roots[j]);
                        }
                    }
                    delta = 2 * h * f2 / (f3 - f1);
                    x -= delta;
                    i++;
                }
                if (Math.Abs(f(x)) <= tolerance)
                {
                    roots[nroot] = x;
                    nroot++;
                    if (x < 0)
                        x *= 0.95;
                    else if (x > 0)
                        x *= 1.05;
                    else
                        x = 0.05;
                }
            }
            return roots;
        }

        public static double[] BirgeVieta(double[] a, double x0, int nOrder, int nRoots,
                                          int nIterations, double tolerance)
        {
            double x = x0;
            double[] roots = new double[nRoots];
            double[] a1 = new double[nOrder + 1];
            double[] a2 = new double[nOrder + 1];
            double[] a3 = new double[nOrder + 1];
            for (int j = 0; j <= nOrder; j++)
                a1[j] = a[j];
            double delta = 10 * tolerance;
            int i = 1, n = nOrder + 1, nroot = 0;
            while (i++ < nIterations && n > 1)
            {
                double x1 = x;
                a2[n-1] = a1[n-1];
                a3[n-1] = a1[n-1];
                for (int j = n - 2; j > 0; j--)
                {
                    a2[j] = a1[j] + x1 * a2[j + 1];
                    a3[j] = a2[j] + x1 * a3[j + 1];
                }
                a2[0] = a1[0] + x1 * a2[1];
                delta = a2[0] / a3[1];
                x -= delta;
                if (Math.Abs(delta) < tolerance)
                {
                    i = 1;
                    n--;
                    roots[nroot] = x;
                    nroot++;
                    for (int j = 0; j < n; j++)
                    {
                        a1[j] = a2[j + 1];
                        if (n == 2)
                        {
                            n--;
                            roots[nroot] = -a1[0];
                            nroot++;
                        }
                    }
                }
            }
            return roots;
        }

        public delegate VectorR MFunction(VectorR x);

        public static VectorR NewtonMultiEquations(MFunction f, VectorR x0, double tolerance)
        {
            LinearSystem ls = new LinearSystem();
            VectorR dx = new VectorR(x0.GetSize());
            do
            {
                MatrixR A = Jacobian(f, x0);
                if (Math.Sqrt(VectorR.DotProduct(f(x0), f(x0)) / x0.GetSize()) < tolerance)
                    return x0;
                dx = ls.GaussJordan(A, -f(x0));
                x0 = x0 + dx;
            }
            while (Math.Sqrt(VectorR.DotProduct(dx, dx)) > tolerance);
            return x0;
        }

        private static MatrixR Jacobian(MFunction f, VectorR x)
        {
            double h = 0.0001;
            int n = x.GetSize();
            MatrixR jacobian = new MatrixR(n, n);
            VectorR x1 = x.Clone();
            for (int j = 0; j < n; j++)
            {
                x1[j] = x[j] + h;
                for (int i = 0; i < n; i++)
                {
                    jacobian[i, j] = (f(x1)[i] - f(x)[i]) / h;
                }
            }
            return jacobian;
        }

    }
}
