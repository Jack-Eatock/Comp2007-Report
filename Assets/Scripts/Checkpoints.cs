using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public Player player;

    public void SetSpawn(Vector3 pos)
    {
        player.RespawnPoint = pos;
    }
}
