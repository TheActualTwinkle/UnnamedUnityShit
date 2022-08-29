using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Mirror : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private ShardSpawnZone spawnZone;

    [SerializeField] private GameObject lightMirror;
    [SerializeField] private GameObject darkMirror;

    [ReadOnly]
    [SerializeField] private List<MirrorShard> shards;
    public List<MirrorShard> Shards { get => shards; }

    private void OnValidate()
    {
        shards = GetComponentsInChildren<MirrorShard>(true).ToList();
    }

    private void Start()
    {
        MirrorData mirrorData = SaveLoadSystem.LoadMirrorData();

        if (mirrorData != null)
        {
            foreach (var activeShardName in mirrorData.activeShards)
            {
                MirrorShard activeShard = GetComponentsInChildren<MirrorShard>(true).Where(x => x.name == activeShardName).First();

                SpawnShard(activeShard, Vector3.zero);
                activeShard.Activate();
            }
        }
    }

    public void SwapMirros()
    {   
        // Some animation.
        if (lightMirror.activeSelf)
        {
            lightMirror.SetActive(false);
            darkMirror.SetActive(true);
        }
        else
        {
            darkMirror.SetActive(false);
            lightMirror.SetActive(true);
        }
    }

    public void SpawnShard(MirrorShard shard, Vector3 position)
    {
        if (shard.IsActivated == false) 
        {
            shard.gameObject.SetActive(true);
            shard.transform.position = position;
        }
        else
        {
            Debug.LogError("Shard '" + shard.name + "' already acrivated");
        }
    }
}