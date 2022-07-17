using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public GameObject block;

    public Vector2 GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)].position;
    }
}
