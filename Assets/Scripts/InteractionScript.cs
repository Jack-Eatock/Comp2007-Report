using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    public List<BasicWeapon> Weapons = new List<BasicWeapon>();
    public iWeapon WeaponEquipped;
    private int currIndex = 0;

    private void Start()
    {
        WeaponEquipped = Weapons[0];
        WeaponEquipped.Found();
    }

    private void Update()
    {
        GetInputs();
    }
    private void CycleWeapons(bool change)
    {
        int newIndex = 0;
        iWeapon weaponn;
        for (int i = 0; i < Weapons.Count; i++)
        {
            newIndex = Cycle(Weapons.Count, currIndex, change);
            weaponn = Weapons[newIndex];
            if (weaponn.isFound())
            {
                // Can be equipped.
                currIndex = newIndex;
                WeaponEquipped.DisabelWeapon();
                WeaponEquipped = weaponn;
                WeaponEquipped.EnableWeapon();
                break;

            }
        }
     
    }

    private void GetInputs()
    {
        if (Input.GetMouseButton(0)) { WeaponEquipped.LeftAttack();  }
        if (Input.GetMouseButton(1)) { WeaponEquipped.RightAttack(); }
        if (Input.mouseScrollDelta.y > 0) { CycleWeapons(true);  }
        else if (Input.mouseScrollDelta.y < 0) { CycleWeapons(false); }
    }

    private int Cycle(int listLength, int currentVal, bool changePos)
    {
        if (changePos)
        {
            if (currentVal + 1 >= listLength)
                return 0;

            return currentVal + 1;
        }
        else
        {
            if (currentVal - 1 < 0)
                return listLength - 1;
            return currentVal - 1;
        }
    }

    public void foundWeapon(int weaponIndex)
    {
        iWeapon weaponn = Weapons[weaponIndex];
        weaponn.Found();
    }

}
