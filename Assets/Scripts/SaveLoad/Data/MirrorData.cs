using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class MirrorData
{
    public readonly List<string> activeShards = new List<string>();

    public MirrorData(Mirror mirror)
    {
        activeShards = mirror.Shards.Where(x => x.IsActivated).Select(x => x.name).ToList();
    }
}