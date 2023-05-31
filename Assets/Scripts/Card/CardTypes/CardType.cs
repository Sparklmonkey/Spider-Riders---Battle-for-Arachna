using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "CardType", menuName = "ScriptableObjects/CardType", order = 6)]
public class CardType : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite BackgroundSprite { get; private set; }
    [field: SerializeField] public Sprite StylizedTypeTextSprite { get; private set; }
}
