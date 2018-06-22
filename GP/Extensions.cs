using MathNet.Numerics.LinearAlgebra;
using System;

namespace GP
{
    internal static class Extensions
    {
        public static void ForEach(this Matrix<double> matrix, Action<int,int,double> action)
        {
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    action(i,j, matrix[i,j]);
                }
            }
        }

        public static void ForEach<T>(this T[] input, Action<T> action)
        {
            for (int i = 0; i < input.Length; i++)
            {
                action(input[i]);
            }
        }

        public static void ForEach<T>(this T[] input, Action<int,T> action)
        {
            for (int i = 0; i < input.Length; i++)
            {
                action(i,input[i]);
            }
        }

        public static void ForEach<T>(this T[,] input, Action<T> action)
        {
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    action(input[i, j]);
                }
            }
        }

        public static void ForEach<T>(this T[,] input, Action<int,int,T> action)
        {
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    action(i,j,input[i, j]);
                }
            }
        }

        public static void ForEach<T>(this T[,] input, Action<int, int> action)
        {
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    action(i, j);
                }
            }
        }
    }
}
