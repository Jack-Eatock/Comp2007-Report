using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public enum specialCheckpoint { a, b, c, e};
    public Transform spawnPoint;
    public Checkpoints checkpoints;
    public specialCheckpoint checkpointType;
    public GameObject specialobjectInteraction;
    public GameObject specialobjectInteraction2;
    public Animator specialAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (checkpointType)
            {
                case specialCheckpoint.a: a();break;
                case specialCheckpoint.c: c(); break;
            }

             checkpoints.SetSpawn(spawnPoint.position);
        }
    }

    private void a()
    {
        specialAnim.SetTrigger("Play");
    }

    private void c()
    {
        specialobjectInteraction.SetActive(false);
        specialobjectInteraction2.SetActive(true);
    }
}
