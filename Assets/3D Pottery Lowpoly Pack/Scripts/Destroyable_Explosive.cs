using System.Collections;
using UnityEngine;
namespace PotteryLowpolyPack
{
    public class Destroyable_Explosive : MonoBehaviour
    {
        int m_maxHit = 10;
        float m_radius = 20f;
        float[] m_radiusCascadeSteps;
        int m_cascadeSteps;
        Collider[] m_hittedColliders;
        LayerMask m_hitLayerMask;
        WaitForSecondsRealtime m_cascadeDelay;
        bool m_limitGarbageCollection;
        bool m_noteObstacles;
        LayerMask m_obstaclesLayerMask;
        float m_distance;
        Vector3 m_explosionPosition;

        private void OnDisable()
        {
            Destroy(this);
        }

        public void Setup(int _maxHit, float _radius, int _cascadeSteps, float _cascadeDelay, bool _limitGarbageCollection, bool _noteObstacles, LayerMask _obstaclesLayerMask)
        {
            m_maxHit = _maxHit;
            m_radius = _radius;
            m_cascadeSteps = _cascadeSteps;
            m_cascadeDelay = new WaitForSecondsRealtime(_cascadeDelay);
            m_limitGarbageCollection = _limitGarbageCollection;
            m_noteObstacles = _noteObstacles;
            m_obstaclesLayerMask = _obstaclesLayerMask;
            m_explosionPosition = this.transform.position;
        }


        public void Explode()
        {
            StartCoroutine(SelfExplosion());
            StartCoroutine(Explosion());
        }

        IEnumerator Explosion()
        {
            m_hittedColliders = new Collider[m_maxHit];
            m_radiusCascadeSteps = new float[m_cascadeSteps];
            for (int i = 0; i < m_cascadeSteps; i++)
            {
                m_radiusCascadeSteps[i] = m_radius * ((float)(i + 1) / m_cascadeSteps);
            }

            int _hits = 0;

            for (int i = 0; i < m_cascadeSteps; i++)
            {
                if (m_limitGarbageCollection) _hits = Physics.OverlapSphereNonAlloc(m_explosionPosition, m_radiusCascadeSteps[i], m_hittedColliders);
                else
                {
                    m_hittedColliders = Physics.OverlapSphere(m_explosionPosition, m_radiusCascadeSteps[i]);
                    _hits = m_hittedColliders.Length;
                }
                for (int j = 0; j < _hits; j++)
                {
                    if (m_hittedColliders[j].TryGetComponent<Destroyable_WholeItem>(out Destroyable_WholeItem _destroyable_WholeItem))
                    {
                        if (m_noteObstacles)
                        {

                            if (!Physics.Raycast(m_explosionPosition, (m_hittedColliders[j].transform.position - m_explosionPosition).normalized, Vector3.Distance(m_explosionPosition, m_hittedColliders[j].transform.position), m_obstaclesLayerMask.value))
                            {
                                //Obstacle was not hitted so need to be destroyed
                                _destroyable_WholeItem.Destroy();
                            }

                        }
                        else _destroyable_WholeItem.Destroy();
                    }
                }
                yield return m_cascadeDelay;
            }
        }
        IEnumerator SelfExplosion()
        {
            yield return new WaitForFixedUpdate();
            Transform[] _childTransforms = GetComponentsInChildren<Transform>();
            foreach (Transform _childTransform in _childTransforms)
            {
                if (_childTransform.TryGetComponent<Rigidbody>(out Rigidbody _rigidbody))
                {
                    _rigidbody.AddExplosionForce(m_radius * 100f, new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z), m_radius);
                }
            }
        }
    }
}