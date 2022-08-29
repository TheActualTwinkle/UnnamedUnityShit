using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShardSpawnZone : MonoBehaviour
{
    private List<Transform> spawnPositions;

    private void Start()
    {
        spawnPositions = GetComponentsInChildren<Transform>().ToList();
        spawnPositions.RemoveAt(0);
    }

    public Transform GetSpawnZone()
    {
        return spawnPositions[Random.Range(0, spawnPositions.Count)];
    } 
}
