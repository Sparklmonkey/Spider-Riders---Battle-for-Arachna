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

    public void AddClosedDoorImage()
    {
        Debug.Log($"Prefabs/ActionDetails/Mission_{_missionIndex}/Door_Closed");
        GameObject backgroundPrefab = Resources.Load<GameObject>($"Prefabs/ActionDetails/Mission_{_missionIndex}/Door_Closed");
        GameObject backgroundObject = Instantiate(backgroundPrefab, transform);
        Debug.Log(backgroundObject);
        backgroundObject.transform.localPosition = new Vector3(0, 0, -1);
    }
    public void AddRopeUsedImage()
    {
        GameObject backgroundPrefab = Resources.Load<GameObject>($"Prefabs/ActionDetails/Mission_{_missionIndex}/Rope_Used");
        GameObject backgroundObject = Instantiate(backgroundPrefab, transform);
        backgroundObject.transform.localPosition = new Vector3(0, 0, -1);
    }
    public void AddBoardUsedImage()
    {
        GameObject backgroundPrefab = Resources.Load<GameObject>($"Prefabs/ActionDetails/Mission_{_missionIndex}/Board_Used");
        GameObject backgroundObject = Instantiate(backgroundPrefab, transform);
        backgroundObject.transform.localPosition = new Vector3(0, 0, -1);
    }
    public void OpenManaclePortal()
    {
        GameObject backgroundPrefab = Resources.Load<GameObject>($"Prefabs/ActionDetails/Mission_{_missionIndex}/Portal_Open");
        GameObject backgroundObject = Instantiate(backgroundPrefab, transform);
        backgroundObject.transform.localPosition = new Vector3(0, 0, -1);
    }
    public void ChangeDoorToOpen()
    {
        Transform doorClosed = transform.Find("Door_Closed");
        if(doorClosed != null)
        {
            Destroy(doorClosed.gameObject);
        }
        GameObject backgroundPrefab = Resources.Load<GameObject>($"Prefabs/ActionDetails/Mission_{_missionIndex}/Door_Open");
        GameObject backgroundObject = Instantiate(backgroundPrefab, transform);
        backgroundObject.transform.localPosition = new Vector3(0, 0, -1);
    }
    public void SetupMapScene(int scene)
    {
        _missionIndex = TestPlayer<PlayerData>.CurrentMissionIndex;
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
            mapDetails.transform.localPosition = new Vector3(0, 0, -1);
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
