[System.Serializable]
public class CellData
{
    public const int DefaultNextCellId = -1;

    public int id;
    public string type;
    public int row;
    public int column;
    public int botNextCellId;
    public int topNextCellId;
    public int botPathId;
    public int topPathId;
}