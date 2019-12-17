using System;
using System.Collections.Generic;
using System.Text;

namespace CosminIv.Games.Common
{
    internal class MatrixUtil<T> {
        public T[][] Init(int rows, int columns) {
            T[][] result = new T[rows][];

            for (int i = 0; i<rows; i++) {
                result[i] = new T[columns];
            }

            return result;
        }
    }
}
