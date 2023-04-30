using UnityEngine;

[CreateAssetMenu(fileName = "CardSlot", menuName = "ScriptableObjects/BattleObjects/CardSlot", order = 1)]
public class CardSlot : ScriptableObject
{
    [field: SerializeField] public Vector3 PositionPlayer { get; private set; }
    [field: SerializeField] public Vector3 PositionOpponent { get; private set; }
}