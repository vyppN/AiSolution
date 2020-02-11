using System;
using System.Text;

namespace AiLibraries.Legacy.Old
{
    /// <summary>
    /// Classes Contained:
    /// 	Matrix (version 1.1)
    /// 	MatrixException
    /// 	double (Version 2.0)
    /// 	FractionException
    /// </summary>
    /// <summary>
    /// Class name: Matrix
    /// Version: 1.1
    /// Class used: double
    /// Developed by: Syed Mehroz Alam
    /// Email: smehrozalam@yahoo.com
    /// URL: Programming Home "http://www.geocities.com/smehrozalam/"
    /// 
    /// What's New in version 1.1
    /// 	*	Added DeterminentFast() method
    /// 	*	Added InverseFast() method
    /// 	*	renamed ConvertToString to (override) ToString()
    /// 	*	some minor bugs fixed
    /// 
    /// Constructors:
    /// 	( double[,] ):	takes 2D Fractions array	
    /// 	( int[,] ):	takes 2D integer array, convert them to fractions	
    /// 	( double[,] ):	takes 2D double array, convert them to fractions
    /// 	( int Rows, int Cols )	initializes the dimensions, indexers may be used 
    /// 							to set individual elements' values
    /// 
    /// Properties:
    /// 	Rows: read only property to get the no. of rows in the current matrix
    /// 	Cols: read only property to get the no. of columns in the current matrix
    /// 
    /// Indexers:
    /// 	[i,j] = used to set/get elements in the form of a double object
    /// 
    /// Public Methods (Description is given with respective methods' definitions)
    /// 	string ToString()
    /// 	Matrix Minor(Matrix, Row,Col)
    /// 	MultiplyRow( Row, double )
    /// 	MultiplyRow( Row, integer )
    /// 	MultiplyRow( Row, double )
    /// 	AddRow( TargetRow, SecondRow, Multiple)
    /// 	InterchangeRow( Row1, Row2)
    /// 	Matrix Concatenate(Matrix1, Matrix2)
    /// 	double Determinent()
    /// 	double DeterminentFast()
    /// 	Matrix EchelonForm()
    /// 	Matrix ReducedEchelonForm()
    /// 	Matrix Inverse()
    /// 	Matrix InverseFast()
    /// 	Matrix Adjoint()
    /// 	Matrix Transpose()
    /// 	Matrix Duplicate()
    /// 	Matrix ScalarMatrix( Rows, Cols, K )
    /// 	Matrix IdentityMatrix( Rows, Cols )
    /// 	Matrix UnitMatrix(Rows, Cols)
    /// 	Matrix NullMatrix(Rows, Cols)
    /// 
    /// Private Methods
    /// 	double GetElement(int iRow, int iCol)
    /// 	SetElement(int iRow, int iCol, double value)
    /// 	Negate(Matrix)
    /// 	Add(Matrix1, Matrix2)
    /// 	Multiply(Matrix1, Matrix2)
    /// 	Multiply(Matrix1, double)
    /// 	Multiply(Matrix1, integer)
    /// 
    /// Operators Overloaded Overloaded
    /// 	Unary: - (negate matrix)
    /// 	Binary: 
    /// 		+,- for two matrices
    /// 		* for two matrices or one matrix with integer or fraction or double
    /// 		/ for matrix with integer or fraction or double
    /// </summary>
    public class Matrix
    {
        private readonly int m_iCols;
        private readonly double[,] m_iElement;

        /// <summary>
        /// Class attributes/members
        /// </summary>
        private readonly int m_iRows;


        /// <summary>
        /// Constructors
        /// </summary>
        public Matrix(int[,] elements)
        {
            m_iRows = elements.GetLength(0);
            m_iCols = elements.GetLength(1);
            m_iElement = new double[m_iRows,m_iCols];
            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    this[i, j] = elements[i, j];
                }
            }
        }

        public Matrix(double[,] elements)
        {
            m_iRows = elements.GetLength(0);
            m_iCols = elements.GetLength(1);
            m_iElement = new double[m_iRows,m_iCols];
            for (int i = 0; i < elements.GetLength(0); i++)
            {
                for (int j = 0; j < elements.GetLength(1); j++)
                {
                    this[i, j] = elements[i, j];
                }
            }
        }

        public Matrix(int iRows, int iCols)
        {
            m_iRows = iRows;
            m_iCols = iCols;
            m_iElement = new double[iRows,iCols];
        }

        /// <summary>
        /// Properites
        /// </summary>
        public int Rows
        {
            get { return m_iRows; }
        }

        public int Cols
        {
            get { return m_iCols; }
        }

        /// <summary>
        /// Indexer
        /// </summary>
        public double this[int iRow, int iCol] // matrix's index starts at 0,0
        {
            get { return GetElement(iRow, iCol); }
            set { SetElement(iRow, iCol, value); }
        }

        /// <summary>
        /// Internal functions for getting/setting values
        /// </summary>
        private double GetElement(int iRow, int iCol)
        {
            if (iRow < 0 || iRow > Rows - 1 || iCol < 0 || iCol > Cols - 1)
                throw new MatrixException("Invalid index specified");
            return m_iElement[iRow, iCol];
        }

        private void SetElement(int iRow, int iCol, double value)
        {
            if (iRow < 0 || iRow > Rows - 1 || iCol < 0 || iCol > Cols - 1)
                throw new MatrixException("Invalid index specified");
            m_iElement[iRow, iCol] = value;
        }


        /// <summary>
        /// The function returns the current Matrix object as a string
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    sb.Append(this[i, j]).Append("\t");
                sb.AppendLine();
            }
            return sb.ToString();
        }


        /// <summary>
        /// The function return the Minor of element[Row,Col] of a Matrix object 
        /// </summary>
        public static Matrix Minor(Matrix matrix, int iRow, int iCol)
        {
            var minor = new Matrix(matrix.Rows - 1, matrix.Cols - 1);
            int m = 0;
            for (int i = 0; i < matrix.Rows; i++)
            {
                if (i == iRow)
                    continue;
                int n = 0;
                for (int j = 0; j < matrix.Cols; j++)
                {
                    if (j == iCol)
                        continue;
                    minor[m, n] = matrix[i, j];
                    n++;
                }
                m++;
            }
            return minor;
        }


        /// <summary>
        /// The function multiplies the given row of the current matrix object by a double 
        /// </summary>
        public void MultiplyRow(int iRow, double frac)
        {
            for (int j = 0; j < Cols; j++)
            {
                this[iRow, j] *= frac;
            }
        }

        /// <summary>
        /// The function adds two rows for current matrix object
        /// It performs the following calculation:
        /// iTargetRow = iTargetRow + iMultiple*iSecondRow
        /// </summary>
        public void AddRow(int iTargetRow, int iSecondRow, double iMultiple)
        {
            for (int j = 0; j < Cols; j++)
                this[iTargetRow, j] += (this[iSecondRow, j]*iMultiple);
        }

        /// <summary>
        /// The function interchanges two rows of the current matrix object
        /// </summary>
        public void InterchangeRow(int iRow1, int iRow2)
        {
            for (int j = 0; j < Cols; j++)
            {
                double temp = this[iRow1, j];
                this[iRow1, j] = this[iRow2, j];
                this[iRow2, j] = temp;
            }
        }

        /// <summary>
        /// The function concatenates the two given matrices column-wise
        /// it can be helpful in a equation solver class where the augmented matrix is obtained by concatenation
        /// </summary>
        public static Matrix Concatenate(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Rows != matrix2.Rows)
                throw new MatrixException("Concatenation not possible");
            var matrix = new Matrix(matrix1.Rows, matrix1.Cols + matrix2.Cols);
            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Cols; j++)
                {
                    if (j < matrix1.Cols)
                        matrix[i, j] = matrix1[i, j];
                    else
                        matrix[i, j] = matrix2[i, j - matrix1.Cols];
                }
            return matrix;
        }

        /// <summary>
        /// The function returns the determinent of the current Matrix object as double
        /// It computes the determinent by reducing the matrix to reduced echelon form using row operations
        /// The function is very fast and efficient but may raise overflow exceptions in some cases.
        /// In such cases use the Determinent() function which computes determinent in the traditional 
        /// manner(by using minors)
        /// </summary>
        public double DeterminentFast()
        {
            if (Rows != Cols)
                throw new MatrixException("Determinent of a non-square matrix doesn't exist");
            double det = 1;
            try
            {
                Matrix ReducedEchelonMatrix = Duplicate();
                for (int i = 0; i < Rows; i++)
                {
                    if (ReducedEchelonMatrix[i, i] == 0) // if diagonal entry is zero, 
                        for (int j = i + 1; j < ReducedEchelonMatrix.Rows; j++)
                            if (ReducedEchelonMatrix[j, i] != 0) //check if some below entry is non-zero
                            {
                                ReducedEchelonMatrix.InterchangeRow(i, j); // then interchange the two rows
                                det *= -1; //interchanging two rows negates the determinent
                            }

                    det *= ReducedEchelonMatrix[i, i];
                    ReducedEchelonMatrix.MultiplyRow(i, 1.0/ReducedEchelonMatrix[i, i]);

                    for (int j = i + 1; j < ReducedEchelonMatrix.Rows; j++)
                    {
                        ReducedEchelonMatrix.AddRow(j, i, -ReducedEchelonMatrix[j, i]);
                    }
                    for (int j = i - 1; j >= 0; j--)
                    {
                        ReducedEchelonMatrix.AddRow(j, i, -ReducedEchelonMatrix[j, i]);
                    }
                }
                return det;
            }
            catch (Exception)
            {
                throw new MatrixException("Determinent of the given matrix could not be calculated");
            }
        }

        /// <summary>
        /// The function returns the determinent of the current Matrix object as double
        /// It computes the determinent in the traditional way (i.e. using minors)
        /// It can be much slower(due to recursion) if the given matrix has order greater than 6
        /// Try using DeterminentFast() function if the order of matrix is greater than 6
        /// </summary>
        public double Determinent()
        {
            return Determinent(this);
        }

        /// <summary>
        /// The helper function for the above Determinent() method
        /// it calls itself recursively and computes determinent using minors
        /// </summary>
        private double Determinent(Matrix matrix)
        {
            double det = 0;
            if (matrix.Rows != matrix.Cols)
                throw new MatrixException("Determinent of a non-square matrix doesn't exist");
            if (matrix.Rows == 1)
                return matrix[0, 0];
            for (int j = 0; j < matrix.Cols; j++)
                det += (matrix[0, j]*Determinent(Minor(matrix, 0, j))*(int) Math.Pow(-1, 0 + j));
            return det;
        }


        /// <summary>
        /// The function returns the Echelon form of the current matrix
        /// </summary>
        public Matrix EchelonForm()
        {
            try
            {
                Matrix EchelonMatrix = Duplicate();
                for (int i = 0; i < Rows; i++)
                {
                    if (EchelonMatrix[i, i] == 0) // if diagonal entry is zero, 
                        for (int j = i + 1; j < EchelonMatrix.Rows; j++)
                            if (EchelonMatrix[j, i] != 0) //check if some below entry is non-zero
                                EchelonMatrix.InterchangeRow(i, j); // then interchange the two rows
                    if (EchelonMatrix[i, i] == 0) // if not found any non-zero diagonal entry
                        continue; // increment i;
                    if (EchelonMatrix[i, i] != 1) // if diagonal entry is not 1 , 	
                        for (int j = i + 1; j < EchelonMatrix.Rows; j++)
                            if (EchelonMatrix[j, i] == 1) //check if some below entry is 1
                                EchelonMatrix.InterchangeRow(i, j); // then interchange the two rows
                    EchelonMatrix.MultiplyRow(i, 1.0/EchelonMatrix[i, i]);
                    for (int j = i + 1; j < EchelonMatrix.Rows; j++)
                        EchelonMatrix.AddRow(j, i, -EchelonMatrix[j, i]);
                }
                return EchelonMatrix;
            }
            catch (Exception)
            {
                throw new MatrixException("Matrix can not be reduced to Echelon form");
            }
        }

        /// <summary>
        /// The function returns the reduced echelon form of the current matrix
        /// </summary>
        public Matrix ReducedEchelonForm()
        {
            try
            {
                Matrix ReducedEchelonMatrix = Duplicate();
                for (int i = 0; i < Rows; i++)
                {
                    if (ReducedEchelonMatrix[i, i] == 0) // if diagonal entry is zero, 
                        for (int j = i + 1; j < ReducedEchelonMatrix.Rows; j++)
                            if (ReducedEchelonMatrix[j, i] != 0) //check if some below entry is non-zero
                                ReducedEchelonMatrix.InterchangeRow(i, j); // then interchange the two rows
                    if (ReducedEchelonMatrix[i, i] == 0) // if not found any non-zero diagonal entry
                        continue; // increment i;
                    if (ReducedEchelonMatrix[i, i] != 1) // if diagonal entry is not 1 , 	
                        for (int j = i + 1; j < ReducedEchelonMatrix.Rows; j++)
                            if (ReducedEchelonMatrix[j, i] == 1) //check if some below entry is 1
                                ReducedEchelonMatrix.InterchangeRow(i, j); // then interchange the two rows
                    ReducedEchelonMatrix.MultiplyRow(i, 1.0/ReducedEchelonMatrix[i, i]);
                    for (int j = i + 1; j < ReducedEchelonMatrix.Rows; j++)
                        ReducedEchelonMatrix.AddRow(j, i, -ReducedEchelonMatrix[j, i]);
                    for (int j = i - 1; j >= 0; j--)
                        ReducedEchelonMatrix.AddRow(j, i, -ReducedEchelonMatrix[j, i]);
                }
                return ReducedEchelonMatrix;
            }
            catch (Exception)
            {
                throw new MatrixException("Matrix can not be reduced to Echelon form");
            }
        }

        /// <summary>
        /// The function returns the inverse of the current matrix using Reduced Echelon Form method
        /// The function is very fast and efficient but may raise overflow exceptions in some cases.
        /// In such cases use the Inverse() method which computes inverse in the traditional way(using adjoint).
        /// </summary>
        public Matrix InverseFast()
        {
            if (DeterminentFast() == 0)
                throw new MatrixException("Inverse of a singular matrix is not possible");
            try
            {
                Matrix IdentityMatrix = Matrix.IdentityMatrix(Rows, Cols);
                Matrix ReducedEchelonMatrix = Duplicate();
                for (int i = 0; i < Rows; i++)
                {
                    if (ReducedEchelonMatrix[i, i] == 0) // if diagonal entry is zero, 
                        for (int j = i + 1; j < ReducedEchelonMatrix.Rows; j++)
                            if (ReducedEchelonMatrix[j, i] != 0) //check if some below entry is non-zero
                            {
                                ReducedEchelonMatrix.InterchangeRow(i, j); // then interchange the two rows
                                IdentityMatrix.InterchangeRow(i, j); // then interchange the two rows
                            }
                    IdentityMatrix.MultiplyRow(i, 1.0/ReducedEchelonMatrix[i, i]);
                    ReducedEchelonMatrix.MultiplyRow(i, 1.0/ReducedEchelonMatrix[i, i]);

                    for (int j = i + 1; j < ReducedEchelonMatrix.Rows; j++)
                    {
                        IdentityMatrix.AddRow(j, i, -ReducedEchelonMatrix[j, i]);
                        ReducedEchelonMatrix.AddRow(j, i, -ReducedEchelonMatrix[j, i]);
                    }
                    for (int j = i - 1; j >= 0; j--)
                    {
                        IdentityMatrix.AddRow(j, i, -ReducedEchelonMatrix[j, i]);
                        ReducedEchelonMatrix.AddRow(j, i, -ReducedEchelonMatrix[j, i]);
                    }
                }
                return IdentityMatrix;
            }
            catch (Exception)
            {
                throw new MatrixException("Inverse of the given matrix could not be calculated");
            }
        }

        /// <summary>
        /// The function returns the inverse of the current matrix in the traditional way(by adjoint method)
        /// It can be much slower if the given matrix has order greater than 6
        /// Try using InverseFast() function if the order of matrix is greater than 6
        /// </summary>
        public Matrix Inverse()
        {
            if (Determinent() == 0)
                throw new MatrixException("Inverse of a singular matrix is not possible");
            return (Adjoint()/Determinent());
        }

        /// <summary>
        /// The function returns the adjoint of the current matrix
        /// </summary>
        public Matrix Adjoint()
        {
            if (Rows != Cols)
                throw new MatrixException("Adjoint of a non-square matrix does not exists");
            var AdjointMatrix = new Matrix(Rows, Cols);
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    AdjointMatrix[i, j] = Math.Pow(-1, i + j)*(Minor(this, i, j).Determinent());
            AdjointMatrix = AdjointMatrix.Transpose();
            return AdjointMatrix;
        }

        /// <summary>
        /// The function returns the transpose of the current matrix
        /// </summary>
        public Matrix Transpose()
        {
            var TransposeMatrix = new Matrix(Cols, Rows);
            for (int i = 0; i < TransposeMatrix.Rows; i++)
                for (int j = 0; j < TransposeMatrix.Cols; j++)
                    TransposeMatrix[i, j] = this[j, i];
            return TransposeMatrix;
        }

        /// <summary>
        /// The function duplicates the current Matrix object
        /// </summary>
        public Matrix Duplicate()
        {
            var matrix = new Matrix(Rows, Cols);
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    matrix[i, j] = this[i, j];
            return matrix;
        }

        /// <summary>
        /// The function returns a Scalar Matrix of dimension ( Row x Col ) and scalar K
        /// </summary>
        public static Matrix ScalarMatrix(int iRows, int iCols, int K)
        {
            const double zero = 0;
            double scalar = K;
            var matrix = new Matrix(iRows, iCols);
            for (int i = 0; i < iRows; i++)
                for (int j = 0; j < iCols; j++)
                {
                    if (i == j)
                        matrix[i, j] = scalar;
                    else
                        matrix[i, j] = zero;
                }
            return matrix;
        }

        /// <summary>
        /// The function returns an identity matrix of dimensions ( Row x Col )
        /// </summary>
        public static Matrix IdentityMatrix(int iRows, int iCols)
        {
            return ScalarMatrix(iRows, iCols, 1);
        }

        /// <summary>
        /// The function returns a Unit Matrix of dimension ( Row x Col )
        /// </summary>
        public static Matrix UnitMatrix(int iRows, int iCols)
        {
            const double temp = 1;
            var matrix = new Matrix(iRows, iCols);
            for (int i = 0; i < iRows; i++)
                for (int j = 0; j < iCols; j++)
                    matrix[i, j] = temp;
            return matrix;
        }

        /// <summary>
        /// The function returns a Null Matrix of dimension ( Row x Col )
        /// </summary>
        public static Matrix NullMatrix(int iRows, int iCols)
        {
            const double temp = 0;
            var matrix = new Matrix(iRows, iCols);
            for (int i = 0; i < iRows; i++)
                for (int j = 0; j < iCols; j++)
                    matrix[i, j] = temp;
            return matrix;
        }

        /// <summary>
        /// Operators for the Matrix object
        /// includes -(unary), and binary opertors such as +,-,*,/
        /// </summary>
        public static Matrix operator -(Matrix matrix)
        {
            return Negate(matrix);
        }

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            return Add(matrix1, matrix2);
        }

        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            return Add(matrix1, -matrix2);
        }

        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            return Multiply(matrix1, matrix2);
        }

        public static Matrix operator *(Matrix matrix1, double dbl)
        {
            return Multiply(matrix1, dbl);
        }

        public static Matrix operator *(double dbl, Matrix matrix1)
        {
            return Multiply(matrix1, dbl);
        }

        public static Matrix operator /(Matrix matrix1, double dbl)
        {
            return Multiply(matrix1, 1.0/dbl);
        }


        /// <summary>
        /// Internal Fucntions for the above operators
        /// </summary>
        private static Matrix Negate(Matrix matrix)
        {
            return Multiply(matrix, -1);
        }

        private static Matrix Add(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Rows != matrix2.Rows || matrix1.Cols != matrix2.Cols)
                throw new MatrixException("Operation not possible");
            var result = new Matrix(matrix1.Rows, matrix1.Cols);
            for (int i = 0; i < result.Rows; i++)
                for (int j = 0; j < result.Cols; j++)
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
            return result;
        }

        private static Matrix Multiply(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.Cols != matrix2.Rows)
                throw new MatrixException("Operation not possible");
            Matrix result = NullMatrix(matrix1.Rows, matrix2.Cols);
            for (int i = 0; i < result.Rows; i++)
                for (int j = 0; j < result.Cols; j++)
                    for (int k = 0; k < matrix1.Cols; k++)
                        result[i, j] += matrix1[i, k]*matrix2[k, j];
            return result;
        }

        private static Matrix Multiply(Matrix matrix, int iNo)
        {
            var result = new Matrix(matrix.Rows, matrix.Cols);
            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Cols; j++)
                    result[i, j] = matrix[i, j]*iNo;
            return result;
        }

        private static Matrix Multiply(Matrix matrix, double frac)
        {
            var result = new Matrix(matrix.Rows, matrix.Cols);
            for (int i = 0; i < matrix.Rows; i++)
                for (int j = 0; j < matrix.Cols; j++)
                    result[i, j] = matrix[i, j]*frac;
            return result;
        }
    }

    //end class Matrix
    /// <summary>
    /// Exception class for Matrix class, derived from System.Exception
    /// </summary>
    public class MatrixException : Exception
    {
        public MatrixException()
        {
        }

        public MatrixException(string Message)
            : base(Message)
        {
        }

        public MatrixException(string Message, Exception InnerException)
            : base(Message, InnerException)
        {
        }
    }

    // end class MatrixException
}