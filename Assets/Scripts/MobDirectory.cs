using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MobDirectory 
{
    public static MobDirectory Instance {
        get {
            if(_instance == null) { _instance = new(); }
            return _instance;
        }
    }
    private static MobDirectory _instance;

    public MobDataList directory;

    public MobDirectory()
    {
        var jsonString = Resources.Load<TextAsset>("Json/mobData");

        directory = JsonUtility.FromJson<MobDataList>(jsonString.text);
    }

    public MobDataStruct GetMobDataWithId(string mobId)
    {
        return directory.moblist.Find(x => x.id == mobId);
    }
}
