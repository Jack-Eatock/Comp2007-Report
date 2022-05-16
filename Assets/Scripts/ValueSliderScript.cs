using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValueSliderScript : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI  sliderVal;

    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }
    
    public void OnSliderValChange(float val){
        sliderVal.text = val.ToString();
    }
}
