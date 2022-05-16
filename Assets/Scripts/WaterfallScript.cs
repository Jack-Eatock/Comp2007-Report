using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterfallScript : MonoBehaviour
{
    Renderer rend;
    Material mat;
    Vector2 newOffset;

    private void Awake() {
        rend = GetComponent<Renderer>();
        mat  = rend.material;
    }
    // Update is called once per frame
    void Update()
    {
        newOffset = new Vector2(mat.mainTextureOffset.x, mat.mainTextureOffset.y + .25f * Time.deltaTime);
        mat.mainTextureOffset = newOffset;
    }
}
