using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<SpawnArea> spawnPoints;

    public Vector2 GetRandomSpawnPoint()
    {
        SpawnArea spawnArea = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Vector2 spawnPoint = (Vector2)spawnArea.transform.position + 
            new Vector2(Random.Range(-(spawnArea.widthHeight.x / 2), spawnArea.widthHeight.x / 2),
                        Random.Range(-(spawnArea.widthHeight.y / 2), spawnArea.widthHeight.y / 2));
        return spawnPoint;
    }
}
