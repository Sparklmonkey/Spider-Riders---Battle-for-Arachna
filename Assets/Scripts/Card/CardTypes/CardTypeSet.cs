using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "CardTypeSet", menuName = "ScriptableObjects/CardTypeSet", order = 7)]
public class CardTypeSet : ScriptableObject
{
    [field: SerializeField] public List<CardType> CardTypes { get; private set; }
}
