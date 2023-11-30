namespace OOPSudokuSolver;

public class TableLine
{
    private Cell[] _line;
    private int? _index;
    private List<int> _solvedValues;

    public Cell[] Line
    {
        get => _line;
        set
        {
            for (int i = 0; i < value.Length; i++)
            {
                _line[i] = value[i];
            }
        }
    }
    


    public TableLine()
    {
        _line = new Cell[9];
        _index = null;
        _solvedValues = new List<int>();
    }

    private void UpdateSolvedValues()
    {
        _solvedValues = _line
            .Where(cell => cell.Value > 0)
            .SelectMany(cell => Enumerable.Repeat(cell.Value, 1)).ToList();
    }
    
}