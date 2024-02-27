using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFaintness : HeroAction
{
    public void TryFaint()
    {
        if (this.Data.hp > 0) return;
        if (this.Data.isBoss)
        {
            // Todo GameOver
            return;
        }

        Faint();
    }

    private void Faint()
    {
        CallActionWithDelay(() =>
        {
            this.Data.currentCell.HeroSpawner.Holder.Items.Clear();
            var cell = PlayObjects.Instance.CellSpawner.GetCellWithType(this.hero.GetReserveType());
            cell.HeroDespawner.DespawnObject(this.hero.transform);
            HeroReference.Instance.Heroes.Remove(this.hero);
        });
    }
}