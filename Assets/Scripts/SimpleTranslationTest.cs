using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class SimpleTranslationTest : MonoBehaviour
{
    private ARTranslationInteractable translationInteractable;

    private void Start()
    {
        translationInteractable = GetComponent<ARTranslationInteractable>();
        if (translationInteractable == null)
        {
            Debug.LogError("ARTranslationInteractable not found.");
        }
    }

    private void Update()
    {
        if (translationInteractable != null && translationInteractable.enabled)
        {
            Debug.Log("ARTranslationInteractable is enabled and should respond to touch input.");
        }
    }
}
