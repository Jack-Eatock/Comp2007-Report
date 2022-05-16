using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ToggleHue : MonoBehaviour
{
    public Volume v;
    private ColorAdjustments c;

    // Start is called before the first frame update
    void Start()
    {
        v.profile.TryGet(out c);
    }

    // Update is called once per frame
    void Update()
    {
        float hue = c.hueShift.value;
        hue += 1;
        if (hue > 180)
        {
            hue = -180;
        }
        c.hueShift.value = hue;
    }
}
