using System;

namespace XuMath
{
    public class LinearSystem
    {
        double epsilon = 1.0e-500;

        public LinearSystem()
        {
        }

        #region Gauss-Jordan elimination:
        public VectorR GaussJordan(MatrixR A, VectorR b)
        {
            Triangulate(A, b);
            int n = b.GetSize();
            VectorR x = new VectorR(n);
            for (int i = n - 1; i >= 0; i--)
            {
                double d = A[i, i];
                if (Math.Abs(d) < epsilon)
                    throw new ArgumentException("Diagonal element is too small!");
                x[i] = (b[i] - VectorR.DotProduct(A.GetRowVector(i), x)) / d;
            }
            return x;
        }

        private void Triangulate(MatrixR A, VectorR b)
        {
            int n = A.GetRows();
            VectorR v = new VectorR(n);
            for (int i = 0; i < n - 1; i++)
            {
                //double d = A[i, i];
                double d = pivot(A, b, i);
                if (Math.Abs(d) < epsilon)
                    throw new ArgumentException("Diagonal element is too small!");
                for (int j = i + 1; j < n; j++)
                {
                    double dd = A[j, i] / d;
                    for (int k = i + 1; k < n; k++)
                    {
                        A[j, k] -= dd * A[i, k];
                    }
                    b[j] -= dd * b[i];
                }
            }
        }

        private double pivot(MatrixR A, VectorR b, int q)
        {
            int n = b.GetSize();
            int i = q;
            double d = 0.0;
            for (int j = q; j < n; j++)
            {
                double dd = Math.Abs(A[j, q]);
                if (dd > d)
                {
                    d = dd;
                    i = j;
                }
            }
            if (i > q)
            {
                A.GetRowSwap(q, i);
                b.GetSwap(q, i);
            }
            return A[q, q];
        }
        #endregion

        #region LU decomposition: the Crout algorithm with pivoting
        public double LUCrout(MatrixR A, VectorR b)
        {
            LUDecompose(A);
            return LUSubstitute(A, b);
        }

        public MatrixR LUInverse(MatrixR m)
        {
            int n = m.GetRows();
            MatrixR u = m.Identity();
            LUDecompose(m);
            VectorR uv = new VectorR(n);
            for (int i = 0; i < n; i++)
            {
                uv = u.GetRowVector(i);
                LUSubstitute(m, uv);
                u.ReplaceRow(uv, i);
            }
            MatrixR inv = u.GetTranspose();
            return inv;
        }

        private void LUDecompose(MatrixR m)
        {
            int n = m.GetRows();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double d = m[i, j];
                    for (int k = 0; k < Math.Min(i, j); k++)
                    {
                        d -= m[i, k] * m[k, j];
                    }
                    if (j > i)
                    {
                        double dd = m[i, i];
                        if (Math.Abs(d) < epsilon)
                            throw new ArgumentException("Diagonal element is too small!");
                        d /= dd;
                    }
                    m[i, j] = d;
                }
            }
        }

        private double LUSubstitute(MatrixR m, VectorR v) // m = A, v = b
        {
            int n = v.GetSize();
            double det = 1.0;
            for (int i = 0; i < n; i++)               // Ly = b
            {
                double d = v[i];
                for (int j = 0; j < i; j++)
                {
                    d -= m[i, j] * v[j];
                }
                double dd = m[i, i];
                if (Math.Abs(d) < epsilon)
                    throw new ArgumentException("Diagonal element is too small!");
                d /= dd;
                v[i] = d;       //v = y
                det *= m[i, i];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                double d = v[i];
                for (int j = i + 1; j < n; j++)
                {
                    d -= m[i, j] * v[j];
                }
                v[i] = d;       // v=x
            }
            return det;
        }
        #endregion

        #region Gauss-Jacobi method
        public VectorR GaussJacobi(MatrixR A, VectorR b, int MaxIterations, double tolerance)
        {
            int n = b.GetSize();
            VectorR x = new VectorR(n);
            for (int nIteration = 0; nIteration < MaxIterations; nIteration++)
            {
                VectorR xOld = x.Clone();
                for (int i = 0; i < n; i++)
                {
                    double db = b[i];
                    double da = A[i, i];
                    if (Math.Abs(da)<epsilon)
                        throw new ArgumentException("Diagonal element is too small!");
                    for (int j = 0; j < n; j++)
                    {
                        if (j != i)
                        {
                            db -= A[i, j] * xOld[j];
                        }
                    }
                    x[i] = db / da;
                }
                VectorR dx = x - xOld;
                if (dx.GetNorm() < tolerance)
                {
                    //MessageBox.Show(nIteration.ToString());
                    return x;
                }
            }
            return x;
        }
        #endregion

        #region Gauss-Seidel method
        public VectorR GaussSeidel(MatrixR A, VectorR b, int MaxIterations, double tolerance)
        {
            int n = b.GetSize();
            VectorR x = new VectorR(n);
            for (int nIteration = 0; nIteration < MaxIterations; nIteration++)
            {
                VectorR xOld = x.Clone();
                for (int i = 0; i < n; i++)
                {
                    double db = b[i];
                    double da = A[i, i];
                    if (Math.Abs(da) < epsilon)
                        throw new ArgumentException("Diagonal element is too small!");
                    for (int j = 0; j < i; j++)
                    {
                        db -= A[i, j] * x[j];
                    }
                    for (int j = i + 1; j < n; j++)
                    {
                        db -= A[i, j] * xOld[j];
                    }
                    x[i] = db / da;
                }
                VectorR dx = x - xOld;
                if (dx.GetNorm() < tolerance)
                {
                    //MessageBox.Show(nIteration.ToString());
                    return x;
                }
            }
            return x;
        }
        #endregion
    }
}
