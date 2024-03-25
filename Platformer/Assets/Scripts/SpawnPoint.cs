using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public void OnDrawGizmos()
    {
        Gizmos.DrawIcon(gameObject.transform.position,
        "spawnpoint");

    }
    public static void DrawIcon(Vector3 center, string name, bool allowScaling = true)
    {
        Gizmos.DrawIcon(center, name, allowScaling);
    }
}
