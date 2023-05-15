using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleParticipant
{
    public Vector3 GetSlotPosition(CardSlot slot);
    public bool TryGetSlotPositionFromPosition(Vector3 position, out Vector3 slotPosition);
    public bool TryGetSlot(Vector3 position, out CardSlot slot);
    public bool TryGetCardInSlot(CardSlot slot, out CardInPlay cardInPlay);
    public bool TryGetCardInSlot(Vector3 slotPosition, out CardInPlay cardInPlay);
}
