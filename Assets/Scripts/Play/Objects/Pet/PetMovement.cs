using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PetMovement : PetAction
{
    public void TryMove()
    {
        if (!this.Data.canMove) return;
        Move(this.Data.nextCell);
    }

    private void Move(Cell nextCell)
    {
        CallAction(() =>
        {
            this.pet.transform.DOMove(nextCell.gameObject.transform.position, Data.durationAnim)
                .OnComplete(() =>
                {
                    this.Data.currentCell.PetSpawner.Holder.Items.Remove(this.pet.transform);
                    nextCell.PetSpawner.Holder.AddItem(this.pet.transform);

                    this.pet.Init.AddCurrentCell(nextCell);
                    SetCanMove(false);
                    this.pet.Init.SetIsActive(false);
                });
        });
    }

    public void SetCanMove(bool value)
    {
        this.Data.canMove = value;
    }

    public bool GetCanMove()
    {
        if (this.Data.currentCell.Data.type == pet.Data.finishCellType) return false;

        this.Data.nextCell = this.pet.GetNextCell();
        if (this.Data.nextCell == null) return false;
        if (this.Data.nextCell.PetSpawner.Holder.Items.Any(h => h.gameObject.activeSelf))
        {
            if (pet.IsTeammate(this.Data.nextCell.Pet))
            {
                if (!Data.nextCell.Pet.Data.canMove)
                    return false;
                return true;
            }

            return false;
        }

        this.Data.subsequentCell = this.pet.GetSubsequentCell();
        if (this.Data.subsequentCell == null) return false;
        if (this.Data.subsequentCell.HasHero)
        {
            if (pet.IsOpponent(this.Data.subsequentCell.Pet)) return false;
        }

        return true;
    }
}