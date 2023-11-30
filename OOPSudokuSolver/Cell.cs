namespace OOPSudokuSolver;

public class Cell
{
    private delegate void SetResultHandler();

    private delegate void SetPossibilitiesHandler(int pos);

    private event SetResultHandler resultSet;

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

    public bool isResult()
    {
        return _value > 0;
    }

    private void RemoveAllPossibilities()
    {
        posibilities.Clear();
    }

    private void SetUpHandlers()
    {
        resultSet += RemoveAllPossibilities;
        possibilitySet += SetValue;
    }


    public List<int> GetPossibilities()
    {
        return posibilities;
    }

    public void AddPossibility(int pos)
    {
        posibilities.Add(pos);
    }

    public void RemovePossibility(int pos)
    {
        posibilities.Remove(pos);
        if (posibilities.Count == 1)
        {
            possibilitySet?.Invoke(posibilities[0]);
        }
    }

    public void RemovePossibilities(List<int> poslist)
    {
        foreach (var pos in poslist)
        {
            this.RemovePossibility(pos);
        }
    }

    private void SetValue(int value)
    {
        Value = value;
    }
}