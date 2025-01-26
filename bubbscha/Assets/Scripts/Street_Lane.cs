//using System.Collections;
//using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using Collectibles_Obstacles;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Street_Lane : MonoBehaviour
{
    public Camera mainCamera;
    public Transform startPoint; //Point from where ground tiles will start
    public Street_Tiles[] tilePrefabs;
    public float movingSpeed = 12;
    public int tilesToPreSpawn = 15; //How many tiles should be pre-spawned

    private List<Street_Tiles> _spawnedTiles;
    

    // Start is called before the first frame update
    void Start()
    {
        _spawnedTiles = new List<Street_Tiles>();
        var spawnPosition = startPoint.position;
        for (var i = 0; i < tilesToPreSpawn; i++)
        {
            var tilePrefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];
            spawnPosition -= tilePrefab.startPoint.localPosition;
            var spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity);

            spawnPosition = spawnedTile.endPoint.position;
            spawnedTile.transform.SetParent(transform);
            _spawnedTiles.Add(spawnedTile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object upward in world space x unit/second.
        //Increase speed the higher score we get
        var frontTile = _spawnedTiles.First();
        if (GameManager.instance.isRunning)
        {
            transform.Translate(-_spawnedTiles.First().transform.forward * (Time.deltaTime * movingSpeed), Space.World);
        }

        if (mainCamera.WorldToViewportPoint(_spawnedTiles[0].endPoint.position).z < 0)
        {
            //Move the tile to the front if it's behind the Camera
            
            _spawnedTiles.RemoveAt(0);
            var collectibles = frontTile.GetComponentsInChildren <ACollectible>();
            foreach (var aCollectible in collectibles)
            {
                aCollectible.Respawn();
            }
            frontTile.transform.position = _spawnedTiles[^1].endPoint.position - frontTile.startPoint.localPosition;
            _spawnedTiles.Add(frontTile);
        }
    }
}
