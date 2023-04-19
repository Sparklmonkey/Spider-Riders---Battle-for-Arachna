using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RankStats", menuName = "ScriptableObjects/RankStats", order = 5)]
public class SpiderRiderRankStats : ScriptableObject
{
    [field: SerializeField] public int RankNumber { get; private set; }
    [field: SerializeField] public string RankTitle { get; private set; }
    [field: SerializeField] public int MinimumVP { get; private set; }
    [field: SerializeField] public BattleParticipantStats Stats { get; private set; }
}

public class ByMinimumVP : IComparer<SpiderRiderRankStats>
{
    public int Compare(SpiderRiderRankStats x, SpiderRiderRankStats y)
    {
        if (x.MinimumVP == y.MinimumVP) return 0;
        if (x.MinimumVP < y.MinimumVP) return -1;
        return 1;
    }
}