using System;
using System.Collections.Generic;

[Serializable]
public class MobDataStruct
{
    public string mobName;
	public string id;
	public int vp;
    public List<string> deck;
    public BattleParticipantStats stats;
}

[Serializable]
public class MobDataList
{
    public List<MobDataStruct> moblist;
}