using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PetMovement : PetAction
{
    
    public void TryMove()
    {
        if (!this.PetData.canMove) return;
        Move(this.PetData.nextCell);
    }

    private void Move(Cell nextCell)
    {
        CallAction(() =>
        {
            this.pet.transform.DOMove(nextCell.gameObject.transform.position, PetData.durationAnim)
                .OnComplete(() =>
                {
                    this.PetData.currentCell.PetSpawner.Holder.Items.Remove(this.pet.transform);
                    nextCell.PetSpawner.Holder.AddItem(this.pet.transform);

                    this.pet.Init.SetCurrentCell(nextCell);
                    this.pet.Init.SetIsActive(false);
                });
        });
    }

    public void SetCanMove(bool value)
    {
        this.PetData.canMove = value;
    }

    public bool GetCanMove()
    {
        if (this.PetData.currentCell.Data.type == pet.Data.finishCellType) return false;

        this.PetData.nextCell = this.pet.GetNextCell();
        if (this.PetData.nextCell == null) return false;
        if (this.PetData.nextCell.PetSpawner.Holder.Items.Any(h => h.gameObject.activeSelf))
        {
            if (pet.IsTeammate(this.PetData.nextCell.Pet))
            {
                if (!PetData.nextCell.Pet.Data.canMove)
                    return false;
                return true;
            }

            return false;
        }

        this.PetData.subsequentCell = this.pet.GetSubsequentCell();
        if (this.PetData.subsequentCell == null) return false;
        if (this.PetData.subsequentCell.HasPet)
        {
            if (pet.IsOpponent(this.PetData.subsequentCell.Pet)) return false;
        }

        return true;
    }
}