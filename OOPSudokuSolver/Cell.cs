namespace OOPSudokuSolver;

public delegate void SetResultHandler();
public class Cell
{
    
    private delegate void SetPossibilitiesHandler(int pos);

    public event SetResultHandler ResultSetResultHandler;

    private event SetPossibilitiesHandler PossibilitySetPossibilitiesHandler;
    
    private int _value;
    private List<int> _possibilities;
    private int _row;
    private int _column;
    private int _square;

    public int Value
    {
        get => _value;
        
        set
        {
            /*
             * Assign given value to Value,
             * if the given value is different from 0,
             * set that value as solution of the cell
             *
             * If the solution is found,
             * send signal to the ResultSet handler.
             * It will remove all remaining possibilities of the cell
             */
            if(value > 0)
            {
                _value = value;
                ResultSetResultHandler?.Invoke();
            }
        }
    }
    public int Row
    {
        get => _row;
        set => _row = value;
    }
    public int Column
    {
        get => _column;
        set => _column = value;
    }
    public int Square
    {
        get => _square;
        set => _square = value;
    }

    

    /**
     * Initialize a cell with:
     * - 0 as default value
     * - [1, 2, 3, 4, 5, 6, 7, 8, 9] as possibilities
     */
    public Cell()
    {
        _value = 0;
        _possibilities = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        SetUpHandlers();
    }

    public Cell(int num) : this()
    {
        if (num > 0)
        {
            Value = num;
        }
    }

    /**
     * Return if a cell is already answered
     */
    public bool IsResult()
    {
        return _value > 0;
    }

    /**
     * Remove all possibilities when found the answer of a cell
     */
    private void RemoveAllPossibilities()
    {
        _possibilities.Clear();
    }

    private void SetUpHandlers()
    {
        ResultSetResultHandler += RemoveAllPossibilities;
        PossibilitySetPossibilitiesHandler += SetPossibilitiesHandlerValue;
    }


    /**
     * Return A list of all possibilities
     */
    public List<int> GetPossibilities()
    {
        return _possibilities;
    }

    /**
     * Add a possibility to possibilities list
     */
    public void AddPossibility(int pos)
    {
        _possibilities.Add(pos);
    }

    /**
     * Remove a given possibility
     */
    private void RemovePossibility(int pos)
    {
        _possibilities.Remove(pos);
        if (_possibilities.Count == 1)
        {
            PossibilitySetPossibilitiesHandler?.Invoke(_possibilities[0]);
        }
    }
    

    /**
     * remove possibilities in the given list
     */
    public void RemovePossibilities(List<int> poslist)
    {
        foreach (var pos in poslist)
        {
            this.RemovePossibility(pos);
        }
    }

    /**
     * Set the answer of a cell
     */
    private void SetPossibilitiesHandlerValue(int value)
        => Value = value;

    /**
     * Return if the current cell contains the given possibility
     */
    public bool ContainPossibility(int pos)
        => _possibilities.Contains(pos);
}