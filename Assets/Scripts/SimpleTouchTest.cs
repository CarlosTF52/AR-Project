using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class SimpleTouchTest : MonoBehaviour
{

    [SerializeField] private ARTranslationInteractable translationInteractable;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch began at: " + touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Touch moved by: " + touch.deltaPosition);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Touch ended at: " + touch.position);
            }
        }
    }

    public void TurnTranslationOff()
    {
        if (translationInteractable != null) translationInteractable.enabled = false;
    }

    public void TurnTranslationOn()
    {
        if (translationInteractable != null) translationInteractable.enabled = true;
    }

}
