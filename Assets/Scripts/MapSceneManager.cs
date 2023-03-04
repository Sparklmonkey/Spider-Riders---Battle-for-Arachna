using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneManager : MonoBehaviour
{

    [SerializeField]
    private int _missionIndex;
    private static MapSceneManager _instance;
    public static MapSceneManager Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void AddRopeUsedImage()
    {
        GameObject backgroundPrefab = Resources.Load<GameObject>($"Prefabs/ActionDetails/Mission_{_missionIndex}/RopeUsed");
        GameObject backgroundObject = Instantiate(backgroundPrefab, transform);
        backgroundObject.transform.localPosition = new Vector3(0, 0, -1);
    }
    public void SetupMapScene(int scene)
    {
        _missionIndex = TestPlayer<PlayerData>.GetPlayerMission();
        ClearMapScene();

        GameObject backgroundPrefab = Resources.Load<GameObject>($"Prefabs/MissionBackground/Mission_{_missionIndex}/Background_{scene}");
        if (backgroundPrefab != null)
        {
            GameObject backgroundObject = Instantiate(backgroundPrefab, transform);
            backgroundObject.transform.localPosition = new Vector3(0, 0, 0);
        }

        GameObject detailPrefab = Resources.Load<GameObject>($"Prefabs/MissionDetails/Mission_{_missionIndex}/Details_{scene}");
        if(detailPrefab != null)
        {
            GameObject mapDetails = Instantiate(detailPrefab, transform);
            mapDetails.transform.localPosition = new Vector3(0, 0, 0);
        }

    }

    private void ClearMapScene()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
