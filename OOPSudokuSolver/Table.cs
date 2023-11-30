using System.Text;

namespace OOPSudokuSolver;

public class Table
{
    
    
    private Cell[,]? table;
    private int NumSolves = 0;

    public Table()
    {
        table = new Cell[9, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                table[i, j] = new Cell();
            }
        }
    }
    
    public Table(int[,] inputs)
    {
        table = new Cell[9, 9];
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                table[row, col] = new Cell(inputs[row,col]);
                if (inputs[row,col] > 0)
                {
                    NumSolves++;
                }
            }
        }
    }

    private void NarrowDownPossibilities()
    {
        
    }

    private void Solve(int rowIndex)
    {
        for (int i = 0; i < 9; i++)
        {
            
        }
    }

    private List<int> GetAllValuesOfARow(int rowIndex)
    {
        HashSet<int> values = new HashSet<int>();
        for (int i = 0; i < 9; i++)
        {
            values.Add(table[rowIndex, i].Value);
        }

        return values.ToList();
    }

    private List<int> GetAllValuesOfAColumn(int colIndex)
    {
        HashSet<int> result = new HashSet<int>();
        for (int i = 0; i < 9; i++)
        {
            result.Add(table[colIndex, i].Value);
        }

        return result.ToList();
    }

    private void EliminateImpossiblePossibilities()
    {
        for (int i = 0; i < 9; i++)
        {
            EliminateImpossiblePossibilitiesInARow(i);
            EliminateImpossiblePossibilitiesInAColumn(i);
        }
    }

    private void EliminateImpossiblePossibilitiesInARow(int rowIndex)
    {
        List<int> values = GetAllValuesOfAColumn(rowIndex);
        for (int i = 0; i < 9; i++)
        {
            if (table[rowIndex, i].isResult() == false)
            {
                table[rowIndex,i].RemovePossibilities(values);
            }
        }
    }
    
    private void EliminateImpossiblePossibilitiesInAColumn(int colIndex)
    {
        List<int> values = GetAllValuesOfAColumn(colIndex);
        for (int i = 0; i < 9; i++)
        {
            if (table[i,colIndex].isResult() == false)
            {
                table[i,colIndex].RemovePossibilities(values);
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

            result.Append('\n');
        }

        return result.ToString();
    }
}