using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface iWeapon
{
    void LeftAttack();

    void RightAttack();

    void Reload();

    bool isFound();

    void Found();

    void DisabelWeapon();

    void EnableWeapon();

}
