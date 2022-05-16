using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMeAndDie : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            Debug.Log("AA" + other.name);
            IKillable killable = (IKillable)other.gameObject.GetComponentInParent(typeof(IKillable));
            if (killable != null)
            {
                killable.DealDamage(100f);
            }
        }
    }
}
