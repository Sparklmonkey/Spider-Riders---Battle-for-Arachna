using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player", order = 1)]
public class PlayerData : ScriptableObject
{
    public string username;
    public List<Card> deckA;
    public List<Card> deckB;
    public List<Card> deckC;
    public List<Card> cardInventory;
    public List<string> itemInventory;
    public Stats stats;
    public CharacterPreset armourIndexes;

}

[Serializable]
public class CharacterPreset
{
    public int skinIndex, upperSetIndex, lowerSetIndex, armourIndex, hairIndex, eyeIndex;
    public bool isMale = true;
}

[Serializable]
public class Stats
{
    public int power, defense, health;
}