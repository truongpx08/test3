using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Button]
    void UseAlly()
    {
        var cell = PlayObjects.Instance.CellSpawner.Cells.Find(c => c.Data.type == CellType.ReserveAlly && !c.HasHero);
        if (!cell) return;
        cell.HeroSpawner.SpawnAlly();
    }

    [Button]
    void UseEnemy()
    {
        var cell = PlayObjects.Instance.CellSpawner.Cells.Find(c => c.Data.type == CellType.ReserveEnemy && !c.HasHero);
        if (!cell) return;
        cell.HeroSpawner.SpawnEnemy();
    }
}