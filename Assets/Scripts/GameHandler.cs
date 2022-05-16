using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameHandler : MonoBehaviour
{
    [SerializeField] private Healthbar healthBar;
    [SerializeField] private Slider valueSlider; // How much damage / health to add
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)){
            AddHealth();
        }
        if (Input.GetKeyDown(KeyCode.Y)){
            TakeDamage();
        }
    }

    public void TakeDamage(){
        healthBar.AdjustVal(valueSlider.value, false);
    }

    public void AddHealth(){
        healthBar.AdjustVal(valueSlider.value, true);
    }

    
}
