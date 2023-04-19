
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity: MonoBehaviour
{
    public PlayerData playerData;
    public int health;
    public int defense;
    public int attack;

    private void Start()
    {
        health = playerData.stats.health;
        defense = playerData.stats.defense;
        attack = playerData.stats.attack;
    }
}