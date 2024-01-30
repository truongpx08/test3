[System.Serializable]
public class CellData
{
    public int id;
    public int row;
    public int column;
    public int nextCellToJumpOfAlly;
    public int nextCellToJumpOfEnemy;
    public string type;
    public const string EnemySpawnPoint = "EnemySpawnPoint";
    public const string AllySpawnPoint = "AllySpawnPoint";
    public const string Normal = "Normal";
}