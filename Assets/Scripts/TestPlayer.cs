using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer<T> : ScriptableObject where T : ScriptableObject
{
    private static PlayerData _instance;
    public static PlayerData Instance
    {
        get
        {
            if (_instance == null) {
                _instance = Resources.Load("TestPlayer") as PlayerData;
            }
            return _instance;
        }
    }

    public static int CurrentMissionIndex { get => Instance.missionIndex; }
    public static void FinishMission() { Instance.missionIndex++; }

    public static List<string> Inventory { get => Instance.itemInventory; } //Change items to something other than string?
    public static void AddItemToInventory(string itemToAdd)
    {
        Instance.itemInventory.Add(itemToAdd);
    }
    public static void RemoveItemFromInventory(string itemToAdd)
    {
        Instance.itemInventory.Remove(itemToAdd);
    }

    public static void ChangeDeck(string letter) //Change selected deck to enum
    {
        Instance.selectedDeck = letter; //Change selected deck to enum
    }

    public static string GetSelectedDeck() //Change selected deck to enum
    {
        return Instance.selectedDeck;
    }

    public static CharacterPreset CharacterPreset
    {
        get => Instance.armourIndexes;
        set => Instance.armourIndexes = value;
    }

    public static int GetPower()
    {
        return Instance.stats.power;
    }
    public static int GetHealth()
    {
        return Instance.stats.health;
    }
    public static int GetDefense()
    {
        return Instance.stats.defense;
    }

    public static bool PlayerHasItem(string itemToCheck)
    {
        return Instance.itemInventory.Contains(itemToCheck);
    }
}