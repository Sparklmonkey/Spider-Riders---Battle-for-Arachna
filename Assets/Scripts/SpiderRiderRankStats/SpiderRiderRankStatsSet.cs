using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "RankStats", menuName = "ScriptableObjects/RankStatsSet", order = 6)]
public class SpiderRiderRankStatsSet : ScriptableObject
{
    [SerializeField] private List<SpiderRiderRankStats> _rankStats;
    public SpiderRiderRankStats GetRankStatsAtVP(int victoryPoints)
    {
        for (int i = _rankStats.Count; i > 0; i--)
        {
            if (_rankStats[i].MinimumVP <= victoryPoints)
            {
                return _rankStats[i];
            }
        }
        return _rankStats[0];
    }
    public bool TryRankUp(int startingVP, int VPToGain, out SpiderRiderRankStats newRankStats)
    {
        SpiderRiderRankStats currentRankStats = GetRankStatsAtVP(startingVP);
        newRankStats = GetRankStatsAtVP(startingVP + VPToGain);
        return currentRankStats != newRankStats;
    }

    private void OnValidate()
    {
        if (_rankStats == null) return;
        _rankStats.Sort(new ByMinimumVP());
    }
}
