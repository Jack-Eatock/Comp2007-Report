using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sabre : BasicWeapon
{


    public override void LeftAttack()
    {
        
        base.LeftAttack();

      

    }

    public override void RightAttack()
    {
        base.RightAttack();
    }

    public override void Reload()
    {
        base.Reload();
    }

    protected override void OnBladeTrigger(Transform enemy)
    {

        base.OnBladeTrigger(enemy);
        
    }
}
