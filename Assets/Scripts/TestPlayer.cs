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


    public static void AddCardToInventory(Card card)
    {
        Instance.cardInventory.Add(card);
    }

    public static void AddItemToInventory(string itemToAdd)
    {
        Instance.itemInventory.Add(itemToAdd);
    }
    public static void RemoveItemFromInventory(string itemToAdd)
    {
        Instance.itemInventory.Remove(itemToAdd);
    }

    public static void ChangeDeck(SelectedDeck deck) 
    {
        Instance.selectedDeck = deck;
    }

    public static SelectedDeck GetSelectedDeck() 
    {
        return Instance.selectedDeck;
    }

    public static CharacterGraphicsPreset CharacterPreset
    {
        get => Instance.armourIndexes;
        set => Instance.armourIndexes = value;
    }

    public static int GetAttack()
    {
        return Instance.stats.attack;
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