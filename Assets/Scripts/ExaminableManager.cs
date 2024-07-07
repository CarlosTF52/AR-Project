using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ExaminableManager : MonoBehaviour
{
    [SerializeField]
    private Transform _examineTarget;

    private Vector3 _cachedPosition;
    private Quaternion _cachedRotation;
    private Vector3 _cachedScale;

    private Examinable _currentExaminedObject;
  

    private bool _isExamining = false;

    [SerializeField]
    private float _rotateSpeed = 1f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //RotateExaminable();
        
    }

    public void PerformExamine(Examinable _examinable)
    {
        _currentExaminedObject = _examinable;

        _cachedPosition = _currentExaminedObject.transform.position;
        _cachedRotation = _currentExaminedObject.transform.rotation;
        _cachedScale = _currentExaminedObject.transform.localScale;

        _currentExaminedObject.transform.position = _examineTarget.position;
        _currentExaminedObject.transform.parent = _examineTarget;

        Vector3 _offsetScale = _cachedScale * _examinable._examineScaleOffset;
        _currentExaminedObject.transform.localScale = _offsetScale;

        _isExamining = true;
    }

    public void PerformUnexamine()
    {
        _currentExaminedObject.transform.position = _cachedPosition;
        _currentExaminedObject.transform.rotation = _cachedRotation;
        _currentExaminedObject.transform.localScale = _cachedScale;
        _currentExaminedObject.transform.parent = null;
        _currentExaminedObject = null;
        
        _isExamining = false;
    }

    private void RotateExaminable()
    {
        if (_isExamining == true)
        {
            
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    _currentExaminedObject.transform.Rotate(touch.deltaPosition.x * _rotateSpeed, touch.deltaPosition.y * _rotateSpeed, 0);
                }
            }
        }
    }
}
