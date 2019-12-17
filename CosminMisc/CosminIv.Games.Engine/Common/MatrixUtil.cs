namespace CosminIv.Games.Engine.Common
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
