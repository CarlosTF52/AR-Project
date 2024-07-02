using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotteryLowpolyPack
{

    public class Destroyable_WholeItem : MonoBehaviour
    {
        public Destroyable_InParts_Name m_DestroyableType;
        public ColisionConditionType m_CollistionActionType;
        public ActionType m_ActionType;
        public string[] m_MultipleTAGs;
        public string m_SingleTAG;

        bool m_wasDestroyed = false;

        [SerializeField] public bool m_addExplosoiveForce;
        [SerializeField][Range(1f, 10f)] public float m_radius;
        [SerializeField][Range(1, 10)] public int m_cascadeSteps;
        [SerializeField][Range(0.01f, 1f)] public float m_cascadeDelay;

        [SerializeField] public bool m_limitGarbageCollection;
        [SerializeField][Range(20, 100)] public int m_maxHit;

        [SerializeField] public bool m_noteObstacles;
        [SerializeField] public LayerMask m_obstaclesLayerMask;


        [SerializeField] public bool m_OverideManager_Flash;
        [SerializeField] public bool m_OverideManager_Condition;
        [SerializeField] public bool m_OverideManager_Action;

        [SerializeField] public bool m_addFlash;
        [SerializeField][Range(0.01f, 1f)] public float m_flashLength;
        bool m_flashRoutineOngoing = false;

        [SerializeField][Range(1, 10)] public int m_InitialDurablility;
        int m_currentDurabliity;
        [SerializeField][Range(1, 100)] public int m_DestroyChance;
        [SerializeField][Range(1, 10)] public int m_DamageMinimal;
        [SerializeField][Range(1, 10)] public int m_DamageMaximal;

        List <MeshRenderer> m_meshRenderers;

        bool m_wasEnabled = false;
        void enable()
        {
            if (!m_OverideManager_Condition) m_CollistionActionType = Destroyable_Manager.m_Instance.m_OnCollisionActionType;
            if (!m_OverideManager_Action)
            {
                m_ActionType = Destroyable_Manager.m_Instance.m_ActionType;
                m_currentDurabliity = Destroyable_Manager.m_Instance.m_InitialDurability;
                m_DestroyChance = Destroyable_Manager.m_Instance.m_DestroyChance;
                m_DamageMinimal = Destroyable_Manager.m_Instance.m_DamageMinimal;
                m_DamageMaximal = Destroyable_Manager.m_Instance.m_DamageMaximal;
            }else
            {
                m_currentDurabliity = m_InitialDurablility;
            }
            if (m_DestroyableType == Destroyable_InParts_Name.Custom_Fast)
            {
                m_meshRenderers = new List<MeshRenderer>();

                Transform[] _childTransforms = this.transform.GetComponentsInChildren<Transform>();
                foreach (Transform _childTransform in _childTransforms)
                {
                    if (_childTransform.TryGetComponent<MeshRenderer>(out MeshRenderer _meshRender))
                    {
                        m_meshRenderers.Add(_meshRender);
                    }
                }
            }
            m_wasEnabled = true;
        }


        public void TryDestroy_AcordingToAction()
        {
            if (m_wasDestroyed) return;
            if (!m_wasEnabled) enable();

            if (m_CollistionActionType == ColisionConditionType.None) return;

            conductAction();
        }
        public void Destroy()
        {
            if (m_wasDestroyed) return;
            if (!m_wasEnabled) enable();
            m_wasDestroyed = true;


            StartCoroutine(DestroyRoutine());
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (!m_wasEnabled) enable();

            if (m_CollistionActionType == ColisionConditionType.None) return;

            if (m_CollistionActionType == ColisionConditionType.Simple)
            {
                conductAction();
                return;
            }

            if (m_CollistionActionType == ColisionConditionType.Single_Tag_Comparsion)
            {
                if (Destroyable_Manager.m_Instance.Single_TAG_Confirmation(collision.gameObject.tag)) conductAction();
                return;
            }

            if (m_CollistionActionType == ColisionConditionType.Multiple_Tag_Comparsion)
            {
                if (Destroyable_Manager.m_Instance.Multiple_TAG_Confirmation(collision.gameObject.tag)) conductAction();
                return;
            }
        }

        void conductAction()
        {
            if (m_ActionType == ActionType.Destroy) Destroy();
            if (m_ActionType == ActionType.DamageConstant)
            {
                m_currentDurabliity--;
                if (m_currentDurabliity <= 0) Destroy();
                else StartCoroutine(DamgeRoutine());
            }
            if (m_ActionType == ActionType.DamageRandom)
            {
                m_currentDurabliity-= Random.Range(m_DamageMinimal, m_DamageMaximal);
                if (m_currentDurabliity <= 0) Destroy();
                else StartCoroutine(DamgeRoutine());
            }
            if (m_ActionType == ActionType.ChanceRandom)
            {
                if (Random.Range(0, 100) <= m_DestroyChance) Destroy();
                else StartCoroutine(DamgeRoutine());
            }
        }

        void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            if (!m_addExplosoiveForce) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, m_radius);
        }

        IEnumerator DamgeRoutine()
        {
            if (m_OverideManager_Flash)
            {
                if (m_addFlash) yield return FlashCustomType();
            }
            else if (Destroyable_Manager.m_Instance.m_AddFlash) yield return FlashMangaerType();
        }
        IEnumerator DestroyRoutine()
        {
            if (m_OverideManager_Flash)
            {
                if (m_addFlash) yield return FlashCustomType();
            }
            else if (Destroyable_Manager.m_Instance.m_AddFlash) yield return FlashMangaerType();



            if (m_DestroyableType != Destroyable_InParts_Name.Custom_Fast)
            {
                Destroyable_InParts _new_Destroyable_InParts = Destroyable_Manager.m_Instance.Grab_Destroyable_InParts(m_DestroyableType);
                _new_Destroyable_InParts.transform.position = this.transform.position;
                _new_Destroyable_InParts.transform.rotation = this.transform.rotation;
                _new_Destroyable_InParts.transform.localScale = this.transform.localScale;
                if (m_addExplosoiveForce)
                {
                    Destroyable_Explosive _newDestroyableExplosive = _new_Destroyable_InParts.gameObject.AddComponent<Destroyable_Explosive>();
                    _newDestroyableExplosive.Setup(m_maxHit, m_radius, m_cascadeSteps, m_cascadeDelay, m_limitGarbageCollection, m_noteObstacles, m_obstaclesLayerMask);
                    _new_Destroyable_InParts.gameObject.SetActive(true);
                    _newDestroyableExplosive.Explode();
                }
                else
                    _new_Destroyable_InParts.gameObject.SetActive(true);

                Destroy(gameObject);
            }
            else
            {
                Destroy(GetComponent<BoxCollider>());
                Transform[] _childTransforms = GetComponentsInChildren<Transform>();
                MeshCollider _meshColider;
                foreach (Transform _childTransform in _childTransforms)
                {
                    _childTransform.gameObject.AddComponent<Rigidbody>();
                    _meshColider = _childTransform.gameObject.AddComponent<MeshCollider>();
                    _meshColider.convex = true;
                }
                if (m_addExplosoiveForce)
                {
                    Destroyable_Explosive _newDestroyableExplosive = this.gameObject.AddComponent<Destroyable_Explosive>();
                    _newDestroyableExplosive.Setup(m_maxHit, m_radius, m_cascadeSteps, m_cascadeDelay, m_limitGarbageCollection, m_noteObstacles, m_obstaclesLayerMask);
                    _newDestroyableExplosive.Explode();
                }
                this.gameObject.AddComponent<Destroyable_InParts>().SetDestroyable_InParts_Name(Destroyable_InParts_Name.Custom_Fast);
                Destroy(this);
            }

            yield return null;
        }
        IEnumerator FlashCustomType()
        {
            if (!m_flashRoutineOngoing)
            {
                m_flashRoutineOngoing = true;

                if (m_DestroyableType != Destroyable_InParts_Name.Custom_Fast)
                {
                    MeshRenderer _meshRenderer = GetComponent<MeshRenderer>();
                    Material[] _materialsOrginal = _meshRenderer.materials;
                    Material[] _materials = _meshRenderer.materials;
                    _materials[0] = Destroyable_Manager.m_Instance.m_FlashMaterial;
                    _meshRenderer.materials = _materials;
                    yield return new WaitForSecondsRealtime(m_flashLength);
                    _meshRenderer.materials = _materialsOrginal;
                }
                else
                {
                    Material[] _materialsOrginal = default;
                    Material[] _materials = default;

                    for (int i=0; i<m_meshRenderers.Count;i++)
                    {
                        _materialsOrginal = m_meshRenderers[i].materials;
                        _materials = m_meshRenderers[i].materials;
                        _materials[0] = Destroyable_Manager.m_Instance.m_FlashMaterial;
                        m_meshRenderers[i].materials = _materials;
                    }

                    yield return new WaitForSecondsRealtime(m_flashLength);

                    for (int i = 0; i < m_meshRenderers.Count; i++)
                    {
                        m_meshRenderers[i].materials = _materialsOrginal;
                    }
 
                }
                m_flashRoutineOngoing = false;
            }



            

            yield return null;

        }
        IEnumerator FlashMangaerType()
        {
            if (!m_flashRoutineOngoing)
            {
                m_flashRoutineOngoing = true;

                if (m_DestroyableType != Destroyable_InParts_Name.Custom_Fast)
                {
                    MeshRenderer _meshRenderer = GetComponent<MeshRenderer>();
                    Material[] _materialsOrginal = _meshRenderer.materials;
                    Material[] _materials = _meshRenderer.materials;
                    _materials[0] = Destroyable_Manager.m_Instance.m_FlashMaterial;
                    _meshRenderer.materials = _materials;
                    yield return new WaitForSecondsRealtime(Destroyable_Manager.m_Instance.m_FlashLength);
                    _meshRenderer.materials = _materialsOrginal;
                }
                else
                {
                    Material[] _materialsOrginal = default;
                    Material[] _materials = default;

                    for (int i = 0; i < m_meshRenderers.Count; i++)
                    {
                        _materialsOrginal = m_meshRenderers[i].materials;
                        _materials = m_meshRenderers[i].materials;
                        _materials[0] = Destroyable_Manager.m_Instance.m_FlashMaterial;
                        m_meshRenderers[i].materials = _materials;
                    }

                    yield return new WaitForSecondsRealtime(m_flashLength);

                    for (int i = 0; i < m_meshRenderers.Count; i++)
                    {
                        m_meshRenderers[i].materials = _materialsOrginal;
                    }
                }

                m_flashRoutineOngoing = false;


            }

            yield return null;
        }
    }

    public enum Destroyable_InParts_Name
    {
        Pottery_1_1b,
        Pottery_1_2b,
        Pottery_1_3b,
        Pottery_2_1b,
        Pottery_2_2b,
        Pottery_2_3b,
        Pottery_3_1b,
        Pottery_3_2b,
        Pottery_3_3b,
        Pottery_4_1b,
        Pottery_4_2b,
        Pottery_4_3b,
        Pottery_5_1b,
        Pottery_5_2b,
        Pottery_5_3b,
        Pottery_6_1b,
        Pottery_6_2b,
        Pottery_6_3b,
        Pottery_7_1b,
        Pottery_7_2b,
        Pottery_7_3b,
        Pottery_8_1b,
        Pottery_8_2b,
        Pottery_8_3b,
        Pottery_9_1b,
        Pottery_9_2b,
        Pottery_9_3b,
        Pottery_10_1b,
        Pottery_10_2b,
        Pottery_10_3b,
        Pottery_11_1b,
        Pottery_11_2b,
        Pottery_11_3b,
        Pottery_12_1b,
        Pottery_12_2b,
        Pottery_12_3b, 
        Pottery_13_1b,
        Pottery_13_2b,
        Pottery_13_3b,
        Pottery_14_1b,
        Pottery_14_2b,
        Pottery_14_3b,
        Pottery_15_1b,
        Pottery_15_2b,
        Pottery_15_3b,
        Pottery_16_1b,
        Pottery_16_2b,
        Pottery_16_3b,
        Pottery_17_1b,
        Pottery_17_2b,
        Pottery_17_3b,
        Pottery_18_1b,
        Pottery_18_2b,
        Pottery_18_3b,
        Pottery_19_1b,
        Pottery_19_2b,
        Pottery_19_3b,
        Pottery_20_1b,
        Pottery_20_2b,
        Pottery_20_3b,
        Pottery_21_1b,
        Pottery_21_2b,
        Pottery_21_3b,
        Pottery_22_1b,
        Pottery_22_2b,
        Pottery_22_3b,
        Pottery_23_1b,
        Pottery_23_2b,
        Pottery_23_3b,
        Pottery_24_1b,
        Pottery_24_2b,
        Pottery_24_3b,
        Barrel_1_1b,
        Barrel_1_2b,
        Barrel_1_3b,
        Barrel_1_4b,
        Barrel_1_5b,
        Barrel_1_6b,
        Barrel_1_7b,
        Barrel_1_8b,
        Barrel_1_9b,
        Barrel_1_10b,
        Barrel_1_11b,
        Barrel_1_12b,
        BoxWood_1_1b,
        BoxWood_1_2b,
        BoxWood_1_3b,
        BoxWood_1_4b,
        BoxWood_1_5b,
        BoxWood_1_6b,
        BoxWood_1_7b,
        BoxWood_1_8b,
        BoxWood_1_9b,
        BoxWood_1_10b,
        BoxWood_1_11b,
        BoxWood_1_12b,
        Barrel_2_1b,
        Barrel_2_2b,
        Barrel_2_3b,
        Barrel_2_4b,
        Barrel_2_5b,
        Barrel_2_6b,
        Custom_Fast,
        Custom_Chair_01
    }
}

