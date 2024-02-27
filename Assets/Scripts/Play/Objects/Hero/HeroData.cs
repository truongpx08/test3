[System.Serializable]
public class HeroData
{
    public int id;
    public string type;
    public int hp;
    public int atk;
    public Cell currentCell;
    public Cell nextCell;
    public Cell subsequentCell;
    public bool isActive;
    public float durationAnim;
    public bool canMove;
    public string finishCellType;
    public bool isBoss;
}