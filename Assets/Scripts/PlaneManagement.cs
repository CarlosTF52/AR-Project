using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneManagement : MonoBehaviour
{

    [SerializeField]
    private ARPlaneManager _planeManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void DisablePlanes()
    {
        _planeManager.planePrefab = null;
    }
}
