namespace OOPSudokuSolver;

public delegate void SetResultHandler();
public class Cell
{
    
    private delegate void SetPossibilitiesHandler(int pos);

    public event SetResultHandler resultSet;

    private event SetPossibilitiesHandler possibilitySet;
    
    private int _value;
    private List<int> posibilities;

    public int Value
    {
        get => _value;
        set
        {
            if(value > 0)
            {
                _value = value;
                resultSet?.Invoke();
            }
        }
    }

    

    public Cell()
    {
        _value = 0;
        posibilities = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
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
    public bool isResult()
    {
        return _value > 0;
    }

    /**
     * Remove all possibilities when found the answer of a cell
     */
    private void RemoveAllPossibilities()
    {
        posibilities.Clear();
    }

    private void SetUpHandlers()
    {
        resultSet += RemoveAllPossibilities;
        possibilitySet += SetValue;
    }


    /**
     * Return A list of all possibilities
     */
    public List<int> GetPossibilities()
    {
        return posibilities;
    }

    /**
     * Add a possibility to possibilities list
     */
    public void AddPossibility(int pos)
    {
        posibilities.Add(pos);
    }

    /**
     * Remove a given possibility
     */
    private void RemovePossibility(int pos)
    {
        posibilities.Remove(pos);
        if (posibilities.Count == 1)
        {
            possibilitySet?.Invoke(posibilities[0]);
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
    private void SetValue(int value)
    {
        Value = value;
    }
}