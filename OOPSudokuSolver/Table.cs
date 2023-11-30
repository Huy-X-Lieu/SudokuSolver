using System.Data;
using System.Text;

namespace OOPSudokuSolver;

public class Table
{
    private delegate void ResultUpdatedHandler();

    private event ResultUpdatedHandler puzzleUpdated;
    
    
    private Cell[,]? table;
    private int _numSolves = 0;

    private int NumSolves
    {
        get => _numSolves;
        set
        {
            _numSolves = value;
            if (_numSolves == 81)
            {
                puzzleUpdated.Invoke();
            }
        }
    }

    private bool isSolved = false;

    public Table()
    {
        table = new Cell[9,9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                table[i,j] = new Cell();
                table[i,j].ResultSetResultHandler += OnResultSetResultHandler;
            }
        }

        puzzleUpdated += NotifyWhenPuzzleIsSolved;
        NumSolves = 0;
    }
    
    /**
     * Add a 2D array of type int to the table
     */
    public void AddInputToTable(int[,] inputs)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (inputs[row,col] > 0)
                {
                    table[row, col].Value = inputs[row,col];
                }

            }
        }
    }
    

    /**
     * Solve the puzzle
     */
    public void Solve()
    {
        while (isSolved == false)
        {
            EliminateImpossiblePossibilities();
        }
    }

    /**
     * Return all values of filled cells on a given row
     */
    private List<int> GetAllValuesOfARow(int rowIndex)
    {
        HashSet<int> values = new HashSet<int>();
        for (int i = 0; i < 9; i++)
        {
            values.Add(table[rowIndex, i].Value);
        }

        values.Remove(0);
        return values.ToList();
    }

    /**
     * Return all values of filled cells on a given column
     */
    private List<int> GetAllValuesOfAColumn(int colIndex)
    {
        HashSet<int> values = new HashSet<int>();
        for (int i = 0; i < 9; i++)
        {
            {
                values.Add(table[i, colIndex].Value);
            }
        }

        values.Remove(0);

        return values.ToList();
    }
    
    /**
     * Return all values of filled cells in a given square
     */
    private List<int> GetAllValuesOfASquare(int squareIndex)
    {
        HashSet<int> values = new HashSet<int>();
        for (int row = 0; row <= 2; row++)
        {
            for (int col = 0; col <= 2; col++)
            {
                values.Add(table[(3 * (squareIndex / 3) + row), (3 * (squareIndex % 3) + col)].Value);
            }
        }

        values.Remove(0);
        return values.ToList();
    }

    /**
     * Remove all IMPOSSIBLE possibility in each cell
     */
    private void EliminateImpossiblePossibilities()
    {
        for (int i = 0; i < 9; i++)
        {
            EliminateImpossiblePossibilitiesInASquare(i);
            EliminateImpossiblePossibilitiesInARow(i);
            EliminateImpossiblePossibilitiesInAColumn(i);
        }
    }
    

    /**
     * Remove all IMPOSSIBLE possibility in each cell on the given row
     */
    private void EliminateImpossiblePossibilitiesInARow(int rowIndex)
    {
        List<int> values = GetAllValuesOfARow(rowIndex);
        for (int i = 0; i < 9; i++)
        {
            if (table[rowIndex, i].IsResult() == false)
            {
                table[rowIndex,i].RemovePossibilities(values);
            }
        }
    }
    
    /**
     * Remove all IMPOSSIBLE possibility in each cell on the given column
     */
    private void EliminateImpossiblePossibilitiesInAColumn(int colIndex)
    {
        List<int> values = GetAllValuesOfAColumn(colIndex);
        for (int i = 0; i < 9; i++)
        {
            if (table[i,colIndex].IsResult() == false)
            {
                table[i,colIndex].RemovePossibilities(values);
            }
        }
    }
    
    /**
     * Remove all IMPOSSIBLE possibility in each cell in the given square
     */
    private void EliminateImpossiblePossibilitiesInASquare(int squareIndex)
    {
        List<int> values = GetAllValuesOfASquare(squareIndex);
        for (int row = 0; row <= 2; row++)
        {
            for (int col = 0; col <= 2; col++)
            {
                if (table[(3 * (squareIndex / 3) + row), (3 * (squareIndex % 3) + col)].IsResult() == false)
                {
                    table[(3 * (squareIndex / 3) + row), (3 * (squareIndex % 3) + col)].RemovePossibilities(values);
                }
            }
        }
    }


    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                result.Append(table[row, col].Value + "  ");
            }

            result.Append("\n");
        }

        return result.ToString();
    }

    /**
     * Increases number of solved cells by 1 each time a cell is solved
     */
    private void OnResultSetResultHandler()
    {
        NumSolves++;
    }

    /**
     * Signal when the puzzle is solved
     */
    private void NotifyWhenPuzzleIsSolved()
    {
        isSolved = true;
    }

    /**
     * displays number of solved cells
     */
    public void PrintNumSolves()
    {
        Console.WriteLine(NumSolves);
    }
}