using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKillable
{
    public float Health = 100;
    public Vector3 RespawnPoint;

    public Healthbar healthBar;
    public GameObject playerTrans;
    public AudioSource soruce;
    public AudioSource ouch;

    bool dying = false;

    public void DealDamage(float damage)
    {
      
        healthBar.AdjustVal(damage/100, false);
        if (Health -  damage <= 0)
        {
            // Dead
            StartCoroutine(Death());
            return;
        }
        ouch.Play();
        Health -= damage;
    }

    public void Kill()
    {
        throw new System.NotImplementedException();
    }


    private IEnumerator Death()
    {
        if (dying) { yield return null; }
        soruce.Play();
        dying = true;
        playerTrans.GetComponent<CharacterController>().enabled = false;
        playerTrans.transform.position = RespawnPoint;
        playerTrans.GetComponent<CharacterController>().enabled = true;
        Health = 100;
        healthBar.AdjustVal(1 , true);
        yield return new WaitForSeconds(1);
        dying = false;
        yield return null;
    }
}
