using System;

namespace XuMath
{
    public class Eigenvalue
    {
        public static void Jacobi(MatrixR A, double tolerance, out MatrixR x, out VectorR lambda)
        {
            MatrixR AA = A.Clone();
            int n = A.GetCols();
            int maxTransform = 5 * n * n;
            MatrixR matrix = new MatrixR(n, n);
            MatrixR R = matrix.Identity();
            MatrixR R1 = R;
            MatrixR A1 = A;
            lambda = new VectorR(n);
            x = R;
           
            double maxTerm = 0.0;
            int I, J;

            do
            {
                maxTerm = MaxTerm(A, out I, out J);
                Transformation(A, R, I, J, out A1, out R1);
                A = A1;
                R = R1;
            }
            while (maxTerm > tolerance);

            x = R;
            for (int i = 0; i < n; i++)
                lambda[i] = A[i, i];

            for (int i = 0; i < n - 1; i++)
            {
                int index = i;
                double d = lambda[i];
                for (int j = i + 1; j < n; j++)
                {
                    if (lambda[j] > d)
                    {
                        index = j;
                        d = lambda[j];
                    }
                }
                if (index != i)
                {
                    lambda = lambda.GetSwap(i, index);
                    x = x.GetColSwap(i, index);
                }
            }
        }

        private static double MaxTerm(MatrixR A, out int I, out int J)
        {
            int n = A.GetCols();
            double result = 0.0;
            I = 0;
            J = 1;

            for (int i = 0; i < n-1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (Math.Abs(A[i, j]) > result)
                    {
                        result = Math.Abs(A[i, j]);
                        I = i;
                        J = j;
                    }
                }
            }
            return result;
        }

        private static void Transformation(MatrixR A, MatrixR R, int I, int J, out MatrixR A1, out MatrixR R1)
        {
            int n = A.GetCols();
            double t = 0.0;
            double da = A[J, J] - A[I, I];
            if (Math.Abs(A[I, J]) < Math.Abs(da) * 1e-30)
            {
                t = A[I, J] / da;
            }
            else
            {
                double phi = da / (2.0 * A[I, J]);
                t = 1.0 / (Math.Abs(phi) + Math.Sqrt(1.0 + phi * phi));
                if (phi < 0.0)
                    t = -t;
            }

            double c = 1.0 / Math.Sqrt(Math.Abs(t * t + 1.0));
            double s = t * c;
            double tau = s / (1.0 + c);
            double temp = A[I, J];
            A[I, J] = 0.0;
            A[I, I] -= t * temp;
            A[J, J] += t * temp;

            for (int i = 0; i < I; i++)
            {
                temp = A[i, I];
                A[i, I] = temp - s * (A[i, J] + tau * temp);
                A[i, J] += s * (temp - tau * A[i, J]);
            }
            for (int i = I + 1; i < J; i++)
            {
                temp = A[I, i];
                A[I, i] = temp - s * (A[i, J] + tau * A[I, i]);
                A[i, J] += s * (temp - tau * A[i, J]);
            }
            for (int i = J + 1; i < n; i++)
            {
                temp = A[I, i];
                A[I, i] = temp - s * (A[J, i] + tau * temp);
                A[J, i] += s * (temp - tau * A[J, i]);
            }

            for (int i = 0; i < n; i++)
            {
                temp = R[i, I];
                R[i, I] = temp - s * (R[i, J] + tau * R[i, I]);
                R[i, J] += s * (temp - tau * R[i, J]);
            }

            A1 = A;
            R1 = R;
        }

        public static void Power(MatrixR A, double tolerance, out VectorR x, out double lambda)
        {
            int n = A.GetCols();
            x = new VectorR(n);
            lambda = 0.0;
            double delta = 0.0;

            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                x[i] = random.NextDouble();
            }

            do
            {
                VectorR temp = x;
                x = MatrixR.Transform(A, x);
                x.Normalize();
                if (VectorR.DotProduct(temp, x) < 0)
                    x = -x;
                VectorR dx = temp - x;
                delta = dx.GetNorm();
            }
            while (delta > tolerance);
            lambda = VectorR.DotProduct(x, MatrixR.Transform(A,x));
        }

        public static void Inverse(MatrixR A, double s, double tolerance, out VectorR x, out double lambda)
        {
            int n = A.GetCols();
            x = new VectorR(n);
            lambda = 0.0;
            double delta = 0.0;
            MatrixR identity = new MatrixR(n, n);
            A = A - s * (identity.Identity());
            LinearSystem ls = new LinearSystem();
            A = ls.LUInverse(A);

            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                x[i] = random.NextDouble();
            }
            do
            {
                VectorR temp = x;
                x = MatrixR.Transform(A, x);
                x.Normalize();
                if (VectorR.DotProduct(temp, x) < 0)
                    x = -x;
                VectorR dx = temp - x;
                delta = dx.GetNorm();
            }
            while (delta > tolerance);
            lambda = s + 1.0 / (VectorR.DotProduct(x, MatrixR.Transform(A, x)));
        }

        public static void Rayleigh(MatrixR A, double tolerance, out VectorR x, out double lambda)
        {
            int n = A.GetCols();
            double delta = 0.0;
            Random random = new Random();
            x = new VectorR(n);
            for (int i = 0; i < n; i++)
            {
                x[i] = random.NextDouble();
            }
            x.Normalize();
            VectorR x0 = MatrixR.Transform(A, x);
            x0.Normalize();
            lambda = VectorR.DotProduct(x, x0);
            double temp = lambda;
            do
            {
                temp = lambda;
                x0 = x;
                x0.Normalize();
                x = MatrixR.Transform(A, x0);
                lambda = VectorR.DotProduct(x, x0);
                delta = Math.Abs((temp - lambda) / lambda);
            }
            while (delta > tolerance);
            x.Normalize();
        }

        public static void RayleighQuotient(MatrixR A, double tolerance, int flag, out VectorR x, out double lambda)
        {
            int n = A.GetCols();
            double delta = 0.0;
            Random random = new Random();
            x = new VectorR(n);
            if (flag != 2)
            {
                for (int i = 0; i < n; i++)
                {
                    x[i] = random.NextDouble();
                }
                x.Normalize();
                lambda = VectorR.DotProduct(x, MatrixR.Transform(A, x));
            }
            else
            {
                lambda = 0.0;
                Rayleigh(A, 1e-2, out x, out lambda);
            }

            double temp = lambda;
            MatrixR identity = new MatrixR(n, n);
            LinearSystem ls = new LinearSystem();

            do
            {
                temp = lambda;
                double d = ls.LUCrout(A - lambda * identity.Identity(), x);
                x.Normalize();
                lambda = VectorR.DotProduct(x, MatrixR.Transform(A, x));
                delta = Math.Abs((temp - lambda) / lambda);
            }
            while (delta > tolerance);
        }






        // matrix tridiagonalization:

        public static double[] Alpha { get; set; }
        public static double[] Beta { get; set; }

        public static MatrixR Tridiagonalize(MatrixR A)
        {
            int n = A.GetCols();
            MatrixR A1 = new MatrixR(n, n);
            A1 = A.Clone();
            double h, g, unorm;
            for (int i = 0; i < n - 2; i++)
            {
                VectorR u = new VectorR(n - i - 1);
                for (int j = i + 1; j < n; j++)
                {
                    u[j - i - 1] = A[i, j];
                }
                unorm = u.GetNorm();
                if (u[0] < 0.0)
                    unorm = -unorm;
                u[0] += unorm;

                for (int j = i + 1; j < n; j++)
                {
                    A[j, i] = u[j - i - 1];
                }

                h = VectorR.DotProduct(u, u) * 0.5;

                VectorR v = new VectorR(n - i - 1);
                MatrixR a1 = new MatrixR(n - i - 1, n - i - 1);
                for (int j = i + 1; j < n; j++)
                {
                    for (int k = i + 1; k < n; k++)
                        a1[j - i - 1, k - i - 1] = A[j, k];
                }
                v = MatrixR.Transform(a1, u) / h;
                g = VectorR.DotProduct(u, v) / (2.0 * h);
                v -= g * u;

                for (int j = i + 1; j < n; j++)
                {
                    for (int k = i + 1; k < n; k++)
                        A[j, k] = A[j, k] - v[j - i - 1] * u[k - i - 1] - u[j - i - 1] * v[k - i - 1];
                }
                A[i, i + 1] = -unorm;
            }
            Alpha = new double[n];
            Beta = new double[n - 1];
            Alpha[0] = A[0, 0];
            for (int i = 1; i < n; i++)
            {
                Alpha[i] = A[i, i];
                Beta[i - 1] = A[i - 1, i];
            }
            
            MatrixR V = new MatrixR(n, n);
            V = V.Identity();

            for (int i = 0; i < n - 2; i++)
            {
                VectorR u = new VectorR(n - i - 1);
                for (int j = i + 1; j < n; j++)
                    u[j - i - 1] = A.GetColVector(i)[j];
                h = VectorR.DotProduct(u, u) * 0.5;
                VectorR v = new VectorR(n - 1);
                MatrixR v1 = new MatrixR(n - 1, n - i - 1);
                for (int j = 1; j < n; j++)
                {
                    for (int k = i + 1; k < n; k++)
                        v1[j - 1, k - i - 1] = V[j, k];
                }

                v = MatrixR.Transform(v1, u) / h;

                for (int j = 1; j < n; j++)
                {
                    for (int k = i + 1; k < n; k++)
                    {
                        V[j, k] -= v[j - 1] * u[k - i - 1];
                    }
                }               
            }
            return V;
        }


        public static MatrixR SetTridiagonalMatrix()
        {
            int n = Alpha.GetLength(0);
            MatrixR t = new MatrixR(n, n);
            t[0, 0] = Alpha[0];
            for (int i = 1; i < n; i++)
            {
                t[i, i] = Alpha[i];
                t[i - 1, i] = Beta[i - 1];
                t[i, i - 1] = Beta[i - 1];
            }
            return t;
        }






        // Eigenvalues and eigenvectors of a tridiagonal matrix:

        public static void SetAlphaBeta(MatrixR T)
        {
            int n = T.GetCols();
            Alpha = new double[n];
            Beta = new double[n - 1];
            Alpha[0] = T[0, 0];
            for (int i = 1; i < n; i++)
            {
                Alpha[i] = T[i, i];
                Beta[i - 1] = T[i - 1, i];
            }
        }

        public static double[] SturmSequence(double lambda)
        {
            int n = Alpha.GetLength(0);
            double[] p = new double[n + 1];
            p[0] = 1;
            p[1] = Alpha[0] - lambda;
            for (int i = 2; i < n + 1; i++)
            {
                p[i] = (Alpha[i - 1] - lambda) * p[i - 1] - Beta[i - 2] * Beta[i - 2] * p[i - 2];
            }
            return p;
        }

        public static int NumberOfEigenvalues(double[] p)
        {
            int n = p.GetLength(0);
            int sign1 = 1;
            int sign = 0;
            int nEigenvalues = 0;
            for (int i = 1; i < n; i++)
            {
                if (p[i] > 0.0)
                    sign = 1;
                else if (p[i] < 0.0)
                    sign = -1;
                else
                    sign = -sign1;
                if (sign1 * sign < 0)
                    nEigenvalues++;
                sign1 = sign;
            }
            return nEigenvalues;
        }


        public static double[] EigenvalueBound()
        {
            int n = Alpha.GetLength(0);
            double min = Alpha[0] - Math.Abs(Beta[0]);
            double max = Alpha[0] + Math.Abs(Beta[0]);
            double lambda;
            for (int i = 1; i < n-1; i++)
            {
                lambda = Alpha[i] - Math.Abs(Beta[i]) - Math.Abs(Beta[i - 1]);
                if (lambda < min)
                    min = lambda;
                lambda = Alpha[i] + Math.Abs(Beta[i]) + Math.Abs(Beta[i - 1]);
                if (lambda > max)
                    max = lambda;
            }
            lambda = Alpha[n - 1] - Math.Abs(Beta[n - 2]);
            if (lambda < min)
                min = lambda;
            lambda = Alpha[n - 1] + Math.Abs(Beta[n - 2]);
            if (lambda > max)
                max = lambda;
            return new double[] { min, max };
        }

        public static double[] EigenvalueRange(int nEigenvalues)
        {
            int n = nEigenvalues;
            double[] r = new double[n + 1];
            double lambda, h;
            int nlambda;
            double[] bound = EigenvalueBound();
            r[0] = bound[0];

            for (int i = n; i > 0; i--)
            {
                lambda = 0.5 * (bound[0] + bound[1]);
                h = 0.5 * (bound[1] - bound[0]);
                for (int j = 0; j < 1000; j++)
                {
                    double[] p = SturmSequence(lambda);
                    nlambda = NumberOfEigenvalues(p);
                    h *= 0.5;
                    if (nlambda < i)
                        lambda += h;
                    else if (nlambda > i)
                        lambda -= h;
                    else
                        break;
                }
                bound[1] = lambda;
                r[i] = lambda;
            }
            return r;
        }


        public static double[] TridiagonalEigenvalues(int nEigenvalues)
        {
            double[] lambda = new double[nEigenvalues];
            double[] range = EigenvalueRange(nEigenvalues);
            for (int i = 0; i < nEigenvalues; i++)
            {
                lambda[i] = NonlinearSystem.FalsePosition(f, range[i], range[i + 1], 1e-8);
            }
            return lambda;
        }

        private static double f(double lambda)
        {
            double[] p = SturmSequence(lambda);
            return p[p.GetLength(0) - 1];
        }

        public static VectorR TridiagonalEigenvector(double s, double tolerance, out double lambda)
        {
            int n = Alpha.GetLength(0);
            double[] gamma = (double[])Beta.Clone();
            double[] beta = (double[])Beta.Clone();
            double[] alpha = new double[n];
            for (int i = 0; i < n; i++)
            {
                alpha[i] = Alpha[i] - s;
            }
            double[] gamma1, alpha1, beta1;
            LUDecomposition(gamma, alpha, beta, out gamma1, out alpha1, out beta1);
            VectorR x = new VectorR(n);
            Random random = new Random();
            for (int i = 0; i < n; i++)
                x[i] = random.NextDouble();
            x.Normalize();
            VectorR x1 = new VectorR(n); ;
            double sign;
            do
            {
                x1 = x.Clone();
                LUSolver(gamma1, alpha1, beta1, x);
                x.Normalize();
                if (VectorR.DotProduct(x1, x) < 0.0)
                {
                    sign = -1.0;
                    x = -x;
                }
                else
                {
                    sign = 1.0;
                }
            }
            while ((x - x1).GetNorm() > tolerance);
            lambda = s + sign / x.GetNorm();
            return x;
        }


        public static void LUDecomposition(double[] gamma, double[] alpha, double[] beta, 
                                            out double[] gamma1, out double[] alpha1, out double[] beta1)
        {
            int n = alpha.GetLength(0);
            for (int i = 1; i < n; i++)
            {
                double c = gamma[i - 1] / alpha[i - 1];
                alpha[i] -= c * beta[i - 1];
                gamma[i - 1] = c;
            }
            gamma1 = gamma;
            alpha1 = alpha;
            beta1 = beta;
        }

        public static VectorR LUSolver(double[] gamma, double[] alpha, double[] beta, VectorR b)
        {
            int n = alpha.GetLength(0);
            for (int i = 1; i < n; i++)
            {
                b[i] -= gamma[i - 1] * b[i - 1];
            }
            b[n - 1] /= alpha[n - 1];
            for (int i = n - 2; i > -1; i--)
            {
                b[i] = (b[i] - beta[i] * b[i + 1]) / alpha[i];
            }

            return b;
        }
    }
}
