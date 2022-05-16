using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour, iWeapon
{
    // If blade
    public bool isBlade = false;
    private bool isSwinging = false;
    public Animator BladeAnim = null;
    public float coolDown = 1f;
    private float lastAttack = 0;
    public float swingDuration = 1f;

    public AudioSource source;

    bool isFoundBool = false; // Can this item be used?

    public virtual void LeftAttack()
    {
        if (Time.time - lastAttack > coolDown)
        {
            isSwinging = true;
            lastAttack = Time.time;
            BladeAnim.SetTrigger("Swing");
            if (source != null) { source.Play(); }
            Invoke("cancelSwing", swingDuration);
        }

    }


    public virtual void Reload()
    {
        throw new System.NotImplementedException();
    }

    public virtual void RightAttack()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (isBlade && isSwinging)
            {
                OnBladeTrigger(other.transform);
            }
        }
    }

    protected virtual void OnBladeTrigger(Transform enemy)
    {
        IKillable killable = (IKillable)enemy.GetComponentInParent(typeof(IKillable));
        Debug.Log(killable);
        if (killable != null)
        {
            killable.Kill();
        }
    }

    private void cancelSwing()
    {
        isSwinging = false;
    }

    public bool isFound()
    {
        return isFoundBool;
    }

    public void Found()
    {
        isFoundBool = true;
    }

    public void DisabelWeapon()
    {
        gameObject.SetActive(false);
    }

    public void EnableWeapon()
    {
        gameObject.SetActive(true);
    }
}
