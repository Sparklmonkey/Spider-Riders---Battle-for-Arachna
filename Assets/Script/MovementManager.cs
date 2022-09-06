using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> sceneList;
    [SerializeField]
    private GameObject playerPrefab;
    public GameObject player;
    public List<MapTileDisplay> currentPath;
    private string currentScene = "Scene1";
    public MapTileDisplay currentPlayerTile;
    private PathFinder pathFinder;
    private bool isMoving;
    public void StartPathFinding(MapTileDisplay destination)
    {
        var path = pathFinder.FindPath(currentPlayerTile, destination);
        Debug.Log(path);
        currentPath = path;
        isMoving = true;
    }

    // Update is called once per frame
    void Start()
    {
        pathFinder = new PathFinder();
        pathFinder.map = sceneList.Find(x => x.name == currentScene).GetComponentInChildren<MapGrid>().GenerateGrid();
        foreach(var scene in sceneList)
        {
            if(scene.name == currentScene) { continue; }
            scene.SetActive(false);
        }
        MapTileDisplay playerSpawn = sceneList.Find(x => x.name == currentScene).GetComponentInChildren<MapGrid>().GetPlayerSpawn();
            player = Instantiate(playerPrefab, sceneList.Find(x => x.name == currentScene).transform);
            currentPlayerTile = playerSpawn;
            player.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            player.transform.position = playerSpawn.tilePosition;
    }

    private void Update()
    {
        if (isMoving)
        {
            if(currentPath == null) { isMoving = false; return; }
            if(currentPath.Count == 0) { isMoving = false; return; }
            player.transform.position = Vector3.MoveTowards(player.transform.position, currentPath[0].tilePosition, Time.deltaTime * 160f);

            if(player.transform.position == new Vector3(currentPath[0].tilePosition.x, currentPath[0].tilePosition.y, player.transform.position.z))
            {
                currentPlayerTile = currentPath[0];
                if(currentPath.Count == 1)
                {
                    isMoving = false;
                }
                if(currentPlayerTile.itemName != "")
                {
                    currentPlayerTile.PickUpItem();
                    isMoving = false;
                    currentPath = new List<MapTileDisplay>();
                    return;
                }
                IActionItem actionItem = currentPlayerTile.GetComponent<IActionItem>();
                if (actionItem != null)
                {
                    actionItem.ActionActivate();
                    isMoving = false;
                    currentPath = new List<MapTileDisplay>();
                    return;
                }
                if (currentPlayerTile.isTransition)
                {
                    sceneList.Find(x => x.name == currentScene).SetActive(false);

                    GameObject newScene = sceneList.Find(x => x.name == currentPlayerTile.transitionDestination);
                    pathFinder.map = newScene.GetComponentInChildren<MapGrid>().GenerateGrid();
                    newScene.SetActive(true);

                    MapTileDisplay playerSpawn = newScene.GetComponentInChildren<MapGrid>().GetPlayerSpawn();
                    isMoving = false;
                    player.transform.parent = newScene.transform;
                    player.transform.position = playerSpawn.tilePosition;
                    currentPlayerTile = playerSpawn;
                    currentPath = new List<MapTileDisplay>();
                    return;
                }
                currentPath.RemoveAt(0);
            }
        }
    }


}