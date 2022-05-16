using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Slider healthBarSlider;
    private Image fillImage;
    private Color defaultFillColour;
    private bool customColourSet = false;
    private float desiredValue;
    private bool travelToDesiredVal = false;
    private float originalVal = 0;

    private float t = 0;
    private bool IsHealing = false;

    [SerializeField] private Color healColour;
    [SerializeField] private Color damageColour;
    [SerializeField] private float adjustmentRate = .5f;
    [SerializeField] private AnimationCurve healingAdjustmentCurve;
    [SerializeField] private AnimationCurve damageAdjustmentCurve;

    void Awake() {
        healthBarSlider = GetComponent<Slider>();  // Grab a reference to the slider.
        fillImage =     healthBarSlider.fillRect.GetComponent<Image>(); // Grab a reference to the sliders image, so that we can switch the colour out.
        defaultFillColour = fillImage.color;    // Grab the original colour of the slider, used later when setting the colour back to default.
    }

    public void AdjustVal(float change, bool increase){
        float newVal = 0;

        // are we already in a transition? If we are we should consider the value we are moving towards instead of the current value.
        if (travelToDesiredVal){ newVal = desiredValue;   }
        else{ newVal = healthBarSlider.value;  }
         
        // Apply Change
        if (increase){newVal += change; }
        else{newVal -= change;}

        // Prevent overflow
        if (newVal > 1){newVal = 1f;}
        else if (newVal < 0){newVal = 0f;}

        // Apply change
        originalVal = healthBarSlider.value;
        desiredValue = newVal; // Sets the final value we want to interpolate towards.
        t = 0; // Restart the interpolation
        travelToDesiredVal = true; // Start interpolating 

        // Are we healing or dealing damage overall? for example if they did a heal twice then a damage. It should still be a heal.
        if (desiredValue - healthBarSlider.value > 0){ IsHealing = true;  }
        else{ IsHealing = false;  }

        // Apply colour values 
        if (IsHealing){
            fillImage.color = healColour;
            customColourSet = true;
        }
        else{
            fillImage.color = damageColour;
            customColourSet = true;
        }
        
    }

    private void Update() {
        
        // If not moving to desired value, Stop!
        if (!travelToDesiredVal){

            // is the slider still coloured ?? if so turn it back to default
            if (customColourSet){
                fillImage.color = defaultFillColour;
                customColourSet = false;
            }
            return;
        }

        // If we are traveling to desired val, start interpolating.
        t += adjustmentRate * Time.deltaTime;

        // Apply animation curve. Are we healing or damaging?
        float curveValAtT = 0;
        if (IsHealing){curveValAtT = healingAdjustmentCurve.Evaluate(t);} // Get the value of the heal curve at the current interpolation
        else{curveValAtT = damageAdjustmentCurve.Evaluate(t);} // Get the value of the damage curve at the current interpolation
           
        // Apply the visual change to the slider. 
        healthBarSlider.value = Mathf.Lerp(originalVal, desiredValue, curveValAtT);

        // Makes sure that t is reset once the interpolation is over.
        if (t > 1.0f){
            travelToDesiredVal = false;
            healthBarSlider.value = desiredValue;
            t = 0f;
        }

    }
}
