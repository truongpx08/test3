using System.Collections;
using System.Collections.Generic;
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

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        this.spacing = 1.05f;
    }

    protected override void LoadPrefabInResource()
    {
        LoadPrefabInResourceWithPrefabName("Cell");
    }


    protected override void OnTimeChange(int value)
    {
    }

    protected override void OnStateChange(string value)
    {
        if (value != GameState.OnStart) return;
        SpawnCells(Row, Column);
        SetAllyCellIdToJumpCells();
        SetEnemyCellIdToJumpCells();
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
                    cellToJumpOfAlly = -1,
                    cellToJumpOfEnemy = -1
                });
                cell.AddName();
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


    private void SetEnemyCellIdToJumpCells()
    {
        var allySpawnPoint = this.cells.Find(c => c.Data.type == CellType.EnemySpawnPoint);
        SetEnemyCellIdToJumpCell(allySpawnPoint);

        void SetEnemyCellIdToJumpCell(Cell cell)
        {
            var cellsSameRow = new List<Cell>();
            // Set Cells Same Row
            this.cells.ForEach(c =>
            {
                if (c.Data.row != cell.Data.row) return;
                if (c == cell) return;
                if (c.Data.cellToJumpOfEnemy != -1) return;
                cellsSameRow.Add(c);
            });
            // Set CellId To Jump
            if (cellsSameRow.Count == 0)
            {
                var cellNextRow =
                    this.cells.Find(c => c.Data.column == cell.Data.column && c.Data.row == cell.Data.row + 1);
                if (cellNextRow == null) return;
                cell.SetEnemyIdCellToJump(cellNextRow.Data.id);
                SetEnemyCellIdToJumpCell(cellNextRow);
                return;
            }

            Cell nextCellInRow = null;
            cellsSameRow.ForEach(c =>
            {
                if (c.Data.column == cell.Data.column + 1)
                {
                    cell.SetEnemyIdCellToJump(c.Data.id);
                    nextCellInRow = c;
                }

                if (c.Data.column == cell.Data.column - 1)
                {
                    cell.SetEnemyIdCellToJump(c.Data.id);
                    nextCellInRow = c;
                }
            });
            // Again Next Cell
            if (nextCellInRow == null) return;
            SetEnemyCellIdToJumpCell(nextCellInRow);
        }
    }

    private void SetAllyCellIdToJumpCells()
    {
        var allySpawnPoint = this.cells.Find(c => c.Data.type == CellType.AllySpawnPoint);
        SetAllyCellIdToJumpCell(allySpawnPoint);

        void SetAllyCellIdToJumpCell(Cell cell)
        {
            var cellsSameRow = new List<Cell>();
            // Set Cells Same Row
            this.cells.ForEach(c =>
            {
                if (c.Data.row != cell.Data.row) return;
                if (c == cell) return;
                if (c.Data.cellToJumpOfAlly != -1) return;
                cellsSameRow.Add(c);
            });
            // Set CellId To Jump
            if (cellsSameRow.Count == 0)
            {
                var cellNextRow =
                    this.cells.Find(c => c.Data.column == cell.Data.column && c.Data.row == cell.Data.row - 1);
                if (cellNextRow == null) return;
                cell.SetAllyIdCellToJump(cellNextRow.Data.id);
                SetAllyCellIdToJumpCell(cellNextRow);
                return;
            }

            Cell nextCellInRow = null;
            cellsSameRow.ForEach(c =>
            {
                if (c.Data.column == cell.Data.column + 1)
                {
                    cell.SetAllyIdCellToJump(c.Data.id);
                    nextCellInRow = c;
                }

                if (c.Data.column == cell.Data.column - 1)
                {
                    cell.SetAllyIdCellToJump(c.Data.id);
                    nextCellInRow = c;
                }
            });
            // Again Next Cell
            if (nextCellInRow == null) return;
            SetAllyCellIdToJumpCell(nextCellInRow);
        }
    }

    public Cell GetCellWithId(int id)
    {
        return cells.Find(item => item.Data.id == id);
    }
}