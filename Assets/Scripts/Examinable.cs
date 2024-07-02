using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;


public class Examinable : MonoBehaviour
{
    [SerializeField]
    private ExaminableManager _examinableManager;

    [SerializeField]
    public float _examineScaleOffset = 1f;

    [SerializeField]
    private ARScaleInteractable _scaleInteractable;

    [SerializeField]
    private ARTranslationInteractable _translationInteractable;

    [SerializeField]
    private Examinable _examinable;


    // Start is called before the first frame update
    void Start()
    {
        _examinableManager = GameObject.Find("ExaminableManager").GetComponent<ExaminableManager>();
    }

    public void RequestExamine()
    {
        _examinableManager.PerformExamine(this);
        Examine();
    }

    public void RequestUnexamine()
    {
        _examinableManager.PerformUnexamine();
        Place();
    }

    private void Examine()
    {
        _scaleInteractable.enabled = false;
        _translationInteractable.enabled = false;
    }

    // Update is called once per frame
    private void Place()
    {
        _scaleInteractable.enabled = true;
        _translationInteractable.enabled = true;
    }

}
