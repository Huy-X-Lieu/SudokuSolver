namespace OOPSudokuSolver;

public static class Util
{
    /**
     * Calculate the square of a cell with given row and col index
     */
    public static int CalculateSquare(int row, int col) =>
        (row / 3) * 3 + (col / 3);
}