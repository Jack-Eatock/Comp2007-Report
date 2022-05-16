using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostShooter : Enemy
{
    public GameObject Proejctile;
    public Transform gunRoot;
    public GameObject deathsys;


    protected override void OnAttack()
    {
        Debug.Log("ATTACK");
        GameObject proj =  Instantiate(Proejctile);
        proj.transform.position = gunRoot.position;
        proj.transform.rotation = gunRoot.rotation;
        proj.transform.parent = transform;

    }

    private IEnumerator Death()
    {
        GameObject death = Instantiate(deathsys);
        death.transform.parent = null;
        death.transform.position = transform.position;
        Destroy(gameObject);
        Debug.Log("Killed");
        yield return null;
    }

    public override void Kill()
    {
        StartCoroutine(Death());
    }


}
