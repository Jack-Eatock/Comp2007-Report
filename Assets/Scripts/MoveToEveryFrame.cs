using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToEveryFrame : MonoBehaviour
{
    [SerializeField]
    private Transform objToFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = objToFollow.position;
    }
}
