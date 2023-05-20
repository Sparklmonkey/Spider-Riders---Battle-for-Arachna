using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandOfCardsManager
{
    public Vector3 GetSlotPosition(CardSlot slot);
    public bool TryGetSlotPositionFromPosition(Vector3 position, out Vector3 slotPosition);
    public bool TryGetSlot(Vector3 position, out CardSlot slot);
    public bool TryGetCardInSlot(CardSlot slot, out CardInPlay cardInPlay);
    public bool TryGetCardInSlot(Vector3 slotPosition, out CardInPlay cardInPlay);
    public bool TryGetSlotOfCard(CardInPlay cardInPlay, out CardSlot slot);
    public void ClearCardSlot(CardSlot slot);
    public bool TryClearCard(CardInPlay cardInPlay);
    public void SetCard(CardInPlay cardInPlay, CardSlot slot);
    public bool TriggerTrashCanAnimationOrOverlap();
}
