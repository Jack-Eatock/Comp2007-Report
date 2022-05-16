using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSprayGold : MonoBehaviour
{
    public GameObject GoldSray;

    public void SprayGold(){
        GoldSray.SetActive(true);
    }
}
