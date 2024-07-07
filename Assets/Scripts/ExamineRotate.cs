using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineRotate : MonoBehaviour
{
    [SerializeField]
    public GameObject _examinedObject;

    


    // Start is called before the first frame update
    void Start()
    {
        //_examinedObject[0] = GameObject.FindWithTag("Target").GetComponent<ExamineObjectAssign>();

    }

    // Update is called once per frame
    void Update()
    {
        RequestRotate();
    }

    public void RequestRotate()
    {
        Debug.Log("Rotate!");
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                _examinedObject.transform.Rotate(touch.deltaPosition.x, touch.deltaPosition.y, 0);
            }
        }
    }
}
