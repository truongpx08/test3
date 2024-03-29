using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFaintness : PetAction
{
    public void TryFaint()
    {
        if (this.PetData.hp > 0) return;
        if (this.PetData.isBoss)
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
            this.PetData.currentCell.PetSpawner.Holder.Items.Clear();
            var cell = PlayObjects.Instance.CellSpawner.GetCellWithType(this.pet.GetReserveType());
            cell.PetDespawner.DespawnObject(this.pet.transform);
            PetReference.Instance.Pets.Remove(this.pet);
        });
    }
}