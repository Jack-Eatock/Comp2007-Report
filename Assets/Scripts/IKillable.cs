using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable 
{
    void Kill();

    void DealDamage(float damage);
}
