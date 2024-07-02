using UnityEngine;


namespace PotteryLowpolyPack
{
    public class CameraRay : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.CompareTag("Destroyable"))
                    {
                        if (hit.collider.TryGetComponent<Destroyable_WholeItem>(out Destroyable_WholeItem _destroyable_WholeItem))
                            _destroyable_WholeItem.TryDestroy_AcordingToAction();
                    }
                }
            }
        }
    }
}
