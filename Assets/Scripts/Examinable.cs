
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;


public class Examinable : MonoBehaviour
{
    [SerializeField]
    public float _examineScaleOffset = 1f;

    [SerializeField]
    public GameObject _visualization;

    [SerializeField]
    private ExamineObjectAssign _objectAssign;

    [SerializeField]
    private ExamineRotate _examineRotate;



    // Start is called before the first frame update
    void Start()
    {
        _objectAssign = GameObject.Find("ExamineTarget").GetComponent<ExamineObjectAssign>();
        
    }

    public void RequestExamine()
    {
        
        if (this.transform.tag == "Barrel" && _objectAssign != null )
        {
            _objectAssign.ShowBarrel();
            _examineRotate = GameObject.Find("BarrelExamineObj(Clone)").GetComponent<ExamineRotate>();
        }
        if (this.transform.tag == "CampFire" && _objectAssign != null)
        {
            _objectAssign.ShowCampFire();
            _examineRotate = GameObject.Find("CampFireExamineObj(Clone)").GetComponent<ExamineRotate>();
        }
        if (this.transform.tag == "Crossbow" && _objectAssign != null)
        {
            _objectAssign.ShowCrossbow();
            _examineRotate = GameObject.Find("CrossbowExamineObj(Clone)").GetComponent<ExamineRotate>();
        }
        if (this.transform.tag == "Box" && _objectAssign != null)
        {
            _objectAssign.ShowBox();
            _examineRotate = GameObject.Find("BoxExamineObj(Clone)").GetComponent<ExamineRotate>();
        }

        _visualization.SetActive(false);
        _examineRotate.RequestRotate();
    }

    public void RequestUnexamine()
    {

        _objectAssign.Destroy();
        _visualization.SetActive(true);
    }

}