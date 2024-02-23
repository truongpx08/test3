[System.Serializable]
public class CellData
{
    public const int DefaultNextCellId = -1;
    
    public int id;
    public int row;
    public int column;
    public int allyNextCell;
    public int enemyNextCell;
    public int allyPathId;
    public int enemyPathId;
    public string type;
}