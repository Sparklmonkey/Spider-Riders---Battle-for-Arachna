
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer<T> : ScriptableObject where T : ScriptableObject
{
    private static PlayerData _instance;

    public static PlayerData GetInstance()
    {
        if (_instance == null)
        {
            _instance = Resources.Load("TestPlayer") as PlayerData;
        }
        return _instance;
    }

    public static int GetPlayerMission() { return GetInstance().missionIndex; }
    public static void FinishMission() { GetInstance().missionIndex++; }
    public static void AddItemToInventory(string itemToAdd)
    {
        GetInstance().itemInventory.Add(itemToAdd);
    }
    public static void RemoveItemFromInventory(string itemToAdd)
    {
        GetInstance().itemInventory.Remove(itemToAdd);
    }
    public static List<string> GetInventory()
    {
        return GetInstance().itemInventory;
    }

    public static void ChangeDeck(string letter)
    {
        GetInstance().selectedDeck = letter;
    }

    public static string GetSelectedDeck()
    {
        return GetInstance().selectedDeck;
    }
    public static CharacterPreset GetCharacterPreset()
    {
        return GetInstance().armourIndexes;
    }

    public static void SaveCharacterPreset(CharacterPreset characterPreset)
    {
        GetInstance().armourIndexes = characterPreset;
    }

    public static int GetPower()
    {
        return GetInstance().stats.power;
    }
    public static int GetHealth()
    {
        return GetInstance().stats.health;
    }
    public static int GetDefense()
    {
        return GetInstance().stats.defense;
    }


    public static bool PlayerHasItem(string itemToCheck)
    {
        return GetInstance().itemInventory.Contains(itemToCheck);
    }
}