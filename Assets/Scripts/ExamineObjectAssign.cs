using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;




public class ExamineObjectAssign : MonoBehaviour
{
    [SerializeField]
    public GameObject[] _targetExamine;

    [SerializeField]
    private GameObject _instantiated;

    [SerializeField]
    private Transform _parent;

    // Start is called before the first frame update
    private void Update()
    {
        //_targetExamine[0] = GameObject.FindWithTag("Barrel");
    }

    // Update is called once per frame
    public void ShowBarrel()
    {
        
        if (_targetExamine[0] != null)
        {
            _instantiated = Instantiate(_targetExamine[0], transform.position, Quaternion.identity);
           _instantiated.transform.SetParent(_parent, true);
        }
      
    }

    public void ShowCampFire()
    {

        if (_targetExamine[1] != null)
        {
            _instantiated = Instantiate(_targetExamine[1], transform.position, Quaternion.identity);
            _instantiated.transform.SetParent(_parent, true);
        }

    }
    public void ShowBox()
    {

        if (_targetExamine[2] != null)
        {
            _instantiated = Instantiate(_targetExamine[2], transform.position, Quaternion.identity);
            _instantiated.transform.SetParent(_parent, true);
        }

    }

    public void ShowCrossbow()
    {

        if (_targetExamine[3] != null)
        {
            _instantiated = Instantiate(_targetExamine[3], transform.position, Quaternion.identity);
            _instantiated.transform.SetParent(_parent, true);
        }

    }

    public void Destroy()
    {
        Destroy(_instantiated);
    }
}
