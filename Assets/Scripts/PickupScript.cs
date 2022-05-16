using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public int WeaponToPickup = 0;
    public AudioSource source;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InteractionScript inter = other.GetComponent<InteractionScript>();
            inter.foundWeapon(WeaponToPickup);
            StartCoroutine(use());
            source.Play();
        }
    }

    private IEnumerator use()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
