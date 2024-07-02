using UnityEditor;
using UnityEngine;

namespace PotteryLowpolyPack
{

    public class Generate_Prefabs : MonoBehaviour
    {
        [SerializeField] static string m_tag_Destroyables = "Destroyable";
        [SerializeField] static int m_layer_Destroyables = 3;

        [MenuItem("GameObject/CatBorg Studio/Breakables/DestroyedItem - MeshColider", false, -12)] // now it show on right click on the object
        public static void PreparePrefab_DestroyedItem_MeshColider()
        {
            if (PrefabUtility.IsAnyPrefabInstanceRoot(Selection.activeTransform.gameObject))
            {
                PrefabUtility.UnpackPrefabInstance(Selection.activeTransform.gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            }
            Selection.activeTransform.transform.position = Vector3.zero;

            string _s = Selection.activeTransform.gameObject.name;
            string _endPrefix = "b";
            char _c = _s[_s.Length - 1];
            char _b = _endPrefix[0];
            if (_c != _b)
            {
                Selection.activeTransform.gameObject.name = $"{Selection.activeTransform.gameObject.name} b";
            }


            Selection.activeTransform.gameObject.tag = m_tag_Destroyables;
            Selection.activeTransform.gameObject.layer = m_layer_Destroyables;
            Destroyable_InParts _destroyable_ItemInParts = Selection.activeTransform.gameObject.AddComponent<Destroyable_InParts>();
            _destroyable_ItemInParts.SetDestroyable_InParts_Name(Destroyable_InParts_Name.Custom_Fast);

            MeshCollider _meshColider;
            Transform[] _childTransforms = Selection.activeTransform.GetComponentsInChildren<Transform>();
            foreach (Transform _childTransform in _childTransforms)
            {
                _childTransform.gameObject.tag = m_tag_Destroyables;
                _childTransform.gameObject.layer = m_layer_Destroyables;
                _meshColider = _childTransform.gameObject.AddComponent<MeshCollider>();
                _meshColider.convex = true;
                _childTransform.gameObject.AddComponent<Rigidbody>();
            }

            Debug.Log($"<color=green>Succes! </color> Please Remeber to set proper - new addedd - DestroyableType value in Destroyable_InParts", _destroyable_ItemInParts);
        }

        [MenuItem("GameObject/CatBorg Studio/Breakables/WholeItem - BoxColider", false, -13)] // now it show on right click on the object
        public static void PreparePrefab_WholeItem_MeshColider()
        {
            if (PrefabUtility.IsAnyPrefabInstanceRoot(Selection.activeTransform.gameObject))
            {
                PrefabUtility.UnpackPrefabInstance(Selection.activeTransform.gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            }
            Selection.activeTransform.transform.position = Vector3.zero;

            string _s = Selection.activeTransform.gameObject.name;
            string _endPrefix = "c";
            char _c = _s[_s.Length - 1];
            char _b = _endPrefix[0];
            if (_c != _b)
            {
                Selection.activeTransform.gameObject.name = $"{Selection.activeTransform.gameObject.name} c";
            }


            Selection.activeTransform.gameObject.tag = m_tag_Destroyables;
            Selection.activeTransform.gameObject.layer = m_layer_Destroyables;
            BoxCollider _boxColider = Selection.activeTransform.gameObject.AddComponent<BoxCollider>();
            Destroyable_WholeItem _destroyable_WholeItem = Selection.activeTransform.gameObject.AddComponent<Destroyable_WholeItem>();
            _destroyable_WholeItem.m_DestroyableType = Destroyable_InParts_Name.Custom_Fast;

            Transform[] _childTransforms = Selection.activeTransform.GetComponentsInChildren<Transform>();
            foreach (Transform _childTransform in _childTransforms)
            {
                _childTransform.gameObject.tag = m_tag_Destroyables;
                _childTransform.gameObject.layer = m_layer_Destroyables;
            }

            foreach (GameObject rootGameObject in Selection.gameObjects)
            {
                if (!(rootGameObject.GetComponent<Collider>() is BoxCollider))
                {
                    continue;
                }

                bool hasBounds = false;
                Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

                for (int i = 0; i < rootGameObject.transform.childCount; ++i)
                {
                    Renderer childRenderer = rootGameObject.transform.GetChild(i).GetComponent<Renderer>();
                    if (childRenderer != null)
                    {
                        if (hasBounds)
                        {
                            bounds.Encapsulate(childRenderer.bounds);
                        }
                        else
                        {
                            bounds = childRenderer.bounds;
                            hasBounds = true;
                        }
                    }
                }

                BoxCollider collider = (BoxCollider)rootGameObject.GetComponent<Collider>();
                collider.center = bounds.center - rootGameObject.transform.position;
                collider.size = bounds.size;
            }

            Debug.Log($"<color=green>Succes! </color> Please Remeber to set proper - new addedd - DestroyableType value in Destroyable_WholeItem", _destroyable_WholeItem);
        }

        [MenuItem("GameObject/CatBorg Studio/Breakables/CustomFast - BoxColider", false, -14)] // now it show on right click on the object
        public static void PreparePrefab_WholeItem_BoxColider()
        {
            if (PrefabUtility.IsAnyPrefabInstanceRoot(Selection.activeTransform.gameObject))
            {
                PrefabUtility.UnpackPrefabInstance(Selection.activeTransform.gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            }
            Selection.activeTransform.transform.position = Vector3.zero;
            Selection.activeTransform.gameObject.name = $"{Selection.activeTransform.gameObject.name} c";
            Selection.activeTransform.gameObject.tag = m_tag_Destroyables;
            Selection.activeTransform.gameObject.layer = m_layer_Destroyables;
            BoxCollider _meshColider = Selection.activeTransform.gameObject.AddComponent<BoxCollider>();
            Destroyable_WholeItem _destroyable_WholeItem = Selection.activeTransform.gameObject.AddComponent<Destroyable_WholeItem>();
            _destroyable_WholeItem.m_DestroyableType = Destroyable_InParts_Name.Custom_Fast;

            Transform[] _childTransforms = Selection.activeTransform.GetComponentsInChildren<Transform>();
            foreach (Transform _childTransform in _childTransforms)
            {
                _childTransform.gameObject.tag = m_tag_Destroyables;
                _childTransform.gameObject.layer = m_layer_Destroyables;
            }

            //mesh auto size base on item bounds

            foreach (GameObject rootGameObject in Selection.gameObjects)
            {
                if (!(rootGameObject.GetComponent<Collider>() is BoxCollider))
                {
                    continue;
                }

                bool hasBounds = false;
                Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

                for (int i = 0; i < rootGameObject.transform.childCount; ++i)
                {
                    Renderer childRenderer = rootGameObject.transform.GetChild(i).GetComponent<Renderer>();
                    if (childRenderer != null)
                    {
                        if (hasBounds)
                        {
                            bounds.Encapsulate(childRenderer.bounds);
                        }
                        else
                        {
                            bounds = childRenderer.bounds;
                            hasBounds = true;
                        }
                    }
                }

                BoxCollider collider = (BoxCollider)rootGameObject.GetComponent<Collider>();
                collider.center = bounds.center - rootGameObject.transform.position;
                collider.size = bounds.size;
            }
        }
    }

}