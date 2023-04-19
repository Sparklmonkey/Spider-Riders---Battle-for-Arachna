using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mob", menuName = "ScriptableObjects/Mob", order = 2)]
public class MobData : ScriptableObject
{
    public string mobName;
    public int vPoints;
    public List<Card> deck;
    public BattleParticipantStats stats;
    public GameObject enemyPrefab;
}