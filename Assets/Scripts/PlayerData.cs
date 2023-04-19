using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player", order = 1)]
public class PlayerData : ScriptableObject
{
    public string username;
    public string selectedDeck;
    public List<Card> deckA;
    public List<Card> deckB;
    public List<Card> deckC;
    public List<Card> cardInventory;
    public List<string> itemInventory;
    public BattleParticipantStats stats;
    public CharacterGraphicsPreset armourIndexes;
    public int missionIndex;
}

[Serializable]
public class CharacterGraphicsPreset
{
    public int skinIndex, upperSetIndex, lowerSetIndex, armourIndex, hairIndex, eyeIndex;
    public bool isMale = true;
}

[Serializable]
public class BattleParticipantStats
{
    [UnityEngine.Serialization.FormerlySerializedAs("power")] public int attack;
    public int defense, health;
}