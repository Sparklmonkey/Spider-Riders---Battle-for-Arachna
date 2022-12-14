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
    private bool isMoving, isTransition;
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
            //player = Instantiate(playerPrefab, sceneList.Find(x => x.name == currentScene).transform);
            currentPlayerTile = playerSpawn;
        //player.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        player.transform.position = playerSpawn.tilePosition + new Vector2(0f, 80f);
    }

    private void Update()
    {
        if (isMoving && !isTransition)
        {
            if(currentPath == null) { isMoving = false; return; }
            if(currentPath.Count == 0) { isMoving = false; return; }
            player.transform.position = Vector3.MoveTowards(player.transform.position, currentPath[0].tilePosition + new Vector2(0f, 80f), Time.deltaTime * 160f);

            if(player.transform.position == new Vector3(currentPath[0].tilePosition.x, currentPath[0].tilePosition.y + 80f, player.transform.position.z))
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
                    Destroy(currentPlayerTile.GetComponent(actionItem.ToString()));
                    isMoving = false;
                    currentPath = new List<MapTileDisplay>();
                    return;
                }
                if (currentPlayerTile.isTransition)
                {

                    if (currentPlayerTile.transitionDestination.Contains("-"))
                    {
                        isTransition = true;
                        StartCoroutine(TransitionToBlock(currentPlayerTile.transitionDestination));
                    }
                    else
                    {
                        sceneList.Find(x => x.name == currentScene).SetActive(false);

                        GameObject newScene = sceneList.Find(x => x.name == currentPlayerTile.transitionDestination);
                        pathFinder.map = newScene.GetComponentInChildren<MapGrid>().GenerateGrid();
                        newScene.SetActive(true);

                        MapTileDisplay playerSpawn = newScene.GetComponentInChildren<MapGrid>().GetPlayerSpawn();
                        isMoving = false;
                        player.transform.parent = newScene.transform;
                        player.transform.position = playerSpawn.tilePosition + new Vector2(0f, 80f);
                        currentPlayerTile = playerSpawn;
                        currentPath = new List<MapTileDisplay>();
                        return;
                    }
                    
                }
                currentPath.RemoveAt(0);
            }
        }
    }
    private IEnumerator TransitionToBlock(string desitination)
    {
        Transform destinationTile = GameObject.Find(desitination).transform;
        while(player.transform.position != destinationTile.position + new Vector3(0f, 80f, 0f))
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, destinationTile.position + new Vector3(0f, 80f, 0f), Time.deltaTime * 160f);
            yield return null;
        }
        isTransition = false;
        isMoving = false;
        currentPath = new List<MapTileDisplay>();

    }

}