using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Rigidbody rb;
    public float force;
    public GameObject explosionEffect;
    public GameObject projObject;
    public float timeTillExplode = 1f;
    public float explosionForce = 600f;
    public float size = 5;
    public float timeTillDie = 6f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.isKinematic = true;
        StartCoroutine(Explode());    

    }



    private IEnumerator Explode()
    {

        yield return new WaitForSeconds(timeTillExplode);
        transform.parent = null;
        rb.isKinematic = false;

        explosionEffect.SetActive(true);
        projObject.SetActive(false);

        //rb.AddExplosionForce(explosionForce, transform.position, size, 1f, ForceMode.Force);
        rb.AddForce(transform.forward * force);

        yield return new WaitForSeconds(timeTillDie);
        Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            Debug.Log("AA" + other.name);
            IKillable killable =  (IKillable)other.gameObject.GetComponentInParent(typeof(IKillable));
            if (killable != null)
            {
                killable.DealDamage(40f);
            }
        }
    }

}
