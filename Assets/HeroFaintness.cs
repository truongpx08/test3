using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFaintness : HeroAction
{
    public void TryFaint()
    {
        if (this.Data.hp <= 0) Faint();
    }

    private void Faint()
    {
        CallActionWithDelay(() =>
        {
            var cell = PlayObjects.Instance.CellSpawner.GetCellWithType(this.hero.GetReserveCellType());
            cell.HeroDespawner.DespawnObject(this.hero.transform);
            HeroReference.Instance.heroes.Remove(this.hero);
        });
    }
}