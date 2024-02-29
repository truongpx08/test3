[System.Serializable]
public class PetData : PetsData.PetData
{
    public const int BossId = 1;
    
    public string type;
    public Cell currentCell;
    public Cell nextCell;
    public Cell subsequentCell;
    public bool isActive;
    public float durationAnim;
    public bool canMove;
    public string finishCellType;
    public bool isBoss;
}