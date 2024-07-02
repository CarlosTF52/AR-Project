using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ChangeBehaviour : MonoBehaviour
{
    [SerializeField]
    private ARSelectionInteractable _selection;

    [SerializeField]
    private ARScaleInteractable _scaleInteractable;

    [SerializeField]
    private ARTranslationInteractable _translationInteractable;

    [SerializeField]
    private Examinable _examinable;

    void Start()
    {
       
    }
    public void Examine()
    {
        _scaleInteractable.enabled = false;
        _translationInteractable.enabled= false;
    }

    // Update is called once per frame
    public void Place()
    {
        _scaleInteractable.enabled = true;
        _translationInteractable.enabled = true;
    }
}

