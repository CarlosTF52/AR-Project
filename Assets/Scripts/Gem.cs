using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public string gemColorName = "";

    public Material targetMaterial;

    private Color _originalEmissionColor;

    // Start is called before the first frame update
    void Start()
    {
        //cache the gems initial emission color
        _originalEmissionColor = targetMaterial.color;
        // set the gem emission color to black
        targetMaterial.SetColor("_EmissionColor", Color.black);
    }

    public void ChangeEmission(bool isEmitting)
    {
        if(isEmitting == true)
        {
            //make this gem emissive
            targetMaterial.SetColor("_EmissionColor", _originalEmissionColor);
        }
        else
        {
            //make this gem unemissive
            targetMaterial.SetColor("_EmissionColor", Color.black);
        }
    }

}
