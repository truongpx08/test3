using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class CellSpawner : SpawnerObj
{
    public const int Row = 6;
    public const int Column = 4;
    [SerializeField] private float spacing;
    [SerializeField] private float top;
    [SerializeField] private float left;
    [SerializeField] private int count;
    [SerializeField] private List<Cell> cells;
    public List<Cell> Cells => cells;

    [SerializeField] private List<Cell> botPath;
    public List<Cell> BotPath => botPath;
    [SerializeField] private List<Cell> topPath;
    public List<Cell> TopPath => topPath;
    [SerializeField] private List<Cell> reserveBotCells;
    public List<Cell> ReserveBotCells => reserveBotCells;
    [SerializeField] private List<Cell> reserveTopCells;
    public List<Cell> ReserveTopCells => reserveTopCells;

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        this.spacing = 1.05f;
    }

    protected override void LoadPrefabInResource()
    {
        LoadPrefabInResourceWithPrefabName("Cell");
    }

    protected override void OnStateChange(string value)
    {
        if (value != GameState.OnStart) return;
        SpawnCells(Row, Column);
        AddBotNextCell();
        AddTopNextCell();
        AddNameCells();
        AddReserveBotCells();
        AddReserveTopCells();
        AddPath();
    }

    private void AddPath()
    {
        this.botPath.ForEach(curCell =>
        {
            if (curCell == this.botPath.Last()) return;
            var nextCell = this.botPath[this.botPath.IndexOf(curCell) + 1];
            if (curCell.Data.row == nextCell.Data.row)
            {
                var horPath = PlayObjects.Instance.PathSpawner.SpawnHorPath();
                AddPosPath(horPath);
                return;
            }

            var verticalPath = PlayObjects.Instance.PathSpawner.SpawnVerticalPath();
            AddPosPath(verticalPath);

            void AddPosPath(GameObject horPath)
            {
                horPath.transform.position = GetMidPoint();
            }

            Vector2 GetMidPoint()
            {
                Vector2 midpoint = (curCell.transform.position + nextCell.transform.position) / 2f;
                return midpoint;
            }
        });
    }


    private void AddBotNextCell()
    {
        SetNextCell(PetType.Bot, botPath, -1);
    }

    private void AddTopNextCell()
    {
        SetNextCell(PetType.Top, topPath, 1);
    }

    private void AddNameCells()
    {
        this.Cells.ForEach(c => c.AddName());
    }


    [Button]
    public void SpawnCells(int rowP, int columnP)
    {
        cells.Clear();
        InitVarToSetPosition();
        this.count = 0;

        for (int r = 0; r < rowP; r++)
        {
            for (int c = 0; c < columnP; c++)
            {
                var obj = SpawnCellWithRow(r);
                SetPositionCell(obj, r, c);
                var cell = obj.GetComponent<Cell>();

                cell.AddData(new CellData()
                {
                    id = count,
                    row = r,
                    column = c,
                    botNextCellId = CellData.DefaultNextCellId,
                    topNextCellId = CellData.DefaultNextCellId
                });
                cell.AddType();

                this.cells.Add(cell);
                count++;
            }
        }
    }

    private Transform SpawnCellWithRow(int r)
    {
        if (r == 0)
            return SpawnObjectWithName("ReserveCell");
        if (r == Row - 1)
            return SpawnObjectWithName("ReserveCell");
        return SpawnObjectWithName("CombatCell");
    }

    private void InitVarToSetPosition()
    {
        float maxHeight = (Row - 1) * this.spacing;
        float maxWidth = (Column - 1) * this.spacing;
        this.top = maxHeight / 2;
        this.left = maxWidth / 2;
    }

    private void SetPositionCell(Transform obj, int r, int c)
    {
        obj.transform.position = new Vector3(c * spacing - left, r * -spacing + top, 0);
    }

    private void SetNextCell(string type, List<Cell> cellPathList, int nextRow)
    {
        cellPathList.Clear();
        var firstCell = GetFirstCellOfPet(type);
        var lastCell = GetLastCellOfPet(type);

        int countLoop = 0;
        Loop(firstCell);

        void Loop(Cell cell)
        {
            if (cell == lastCell)
            {
                SetUpPath();
                return;
            }

            // Get Cell Same Raw
            var cellsSameRow = new List<Cell>();
            this.cells.ForEach(c =>
            {
                if (c.Data.row != cell.Data.row) return;
                if (c == cell) return;

                switch (type)
                {
                    case PetType.Bot:
                        if (c.Data.botNextCellId != CellData.DefaultNextCellId) return;
                        break;

                    case PetType.Top:
                        if (c.Data.topNextCellId != CellData.DefaultNextCellId) return;
                        break;
                    default:
                        Debug.LogError("Error");
                        break;
                }

                cellsSameRow.Add(c);
            });
            // Cell at NextRow
            if (cellsSameRow.Count == 0)
            {
                var cellNextRow =
                    this.cells.Find(c => c.Data.column == cell.Data.column && c.Data.row == cell.Data.row + nextRow);
                if (cellNextRow == null) return;
                SetUpPath(cellNextRow);
                Loop(cellNextRow);
                return;
            }

            // Cell at NextColumn   
            Cell nextCellInRow = null;
            cellsSameRow.ForEach(c =>
            {
                if (c.Data.column == cell.Data.column + 1)
                {
                    SetUpPath(c);
                    nextCellInRow = c;
                }

                if (c.Data.column == cell.Data.column - 1)
                {
                    SetUpPath(c);
                    nextCellInRow = c;
                }
            });
            if (nextCellInRow == null) return;
            Loop(nextCellInRow);

            void SetUpPath(Cell nextCell = null)
            {
                switch (type)
                {
                    case PetType.Bot:
                        if (nextCell != null)
                            cell.SetBotNextCell(nextCell.Data.id);
                        cell.SetBotPathId(countLoop);
                        break;

                    case PetType.Top:
                        if (nextCell != null)
                            cell.SetTopNextCell(nextCell.Data.id);
                        cell.SetTopPathId(countLoop);
                        break;
                    default:
                        Debug.LogError("Error");
                        break;
                }

                cellPathList.Add(cell);
                countLoop++;
            }
        }
    }

    private Cell GetFirstCellOfPet(string type)
    {
        switch (type)
        {
            case PetType.Top:
                return PlayObjects.Instance.CellSpawner.cells.Find(cell =>
                    cell.Data.row == 0 && cell.Data.column == Column - 1);

            case PetType.Bot:
                return PlayObjects.Instance.CellSpawner.cells.Find(cell =>
                    cell.Data.row == Row - 1 && cell.Data.column == Column - 1);
        }

        Debug.LogError("Error");
        return null;
    }

    public Cell GetLastCellOfPet(string type)
    {
        switch (type)
        {
            case PetType.Top:
                return PlayObjects.Instance.CellSpawner.cells.Find(cell =>
                    cell.Data.row == Row - 1 && cell.Data.column == Column - 1);

            case PetType.Bot:
                return PlayObjects.Instance.CellSpawner.cells.Find(cell =>
                    cell.Data.row == 0 && cell.Data.column == Column - 1);
        }

        Debug.LogError("Error");
        return null;
    }


    public Cell GetCellWithId(int id)
    {
        return cells.Find(item => item.Data.id == id);
    }

    public Cell GetCellWithType(string type)
    {
        return cells.Find(item => item.Data.type == type);
    }

    private void AddReserveTopCells()
    {
        this.topPath.ForEach(c =>
        {
            if (c.Data.type == CellType.TopReserve) this.reserveTopCells.Add(c);
        });
    }

    private void AddReserveBotCells()
    {
        this.botPath.ForEach(c =>
        {
            if (c.Data.type == CellType.BotReserve) this.reserveBotCells.Add(c);
        });
    }
}