using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Vector2> spawnPoints;
    public List<IngameAttribute> possibleIngameAttributes;
    public List<StartAttribute> possibleStartAttributes;

    public int ingameAttributeCount;
    public int startAttributeCount;

    public Vector2 GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }
}
