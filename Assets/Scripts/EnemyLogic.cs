using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyLogic : Enemy
{
    public float Damage = 10;
    public Animator attackAnim;
    public float AttackDuration = 1f;
    protected bool attacking = false;

    public override void Kill()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        gameObject.SetActive(false);
        Debug.Log("Killed");
        yield return null;
    }

    public override void DealDamage(float damage)
    {
        throw new System.NotImplementedException();
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player" && attacking)
        {
            playerDmg.DealDamage(Damage);
        }
    }

    protected override void OnAttack()
    {
        attackAnim.SetTrigger("Attack");
        attacking = true;
        Debug.Log("ATTACK");
        Invoke("StopAttacking", AttackDuration);
    }


    private void StopAttacking()
    {
        attacking = false;
    }

}
