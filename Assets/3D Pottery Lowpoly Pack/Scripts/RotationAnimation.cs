using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotteryLowpolyPack
{
    public class RotationAnimation : MonoBehaviour
    {
        [SerializeField][Range(.01f, 2f)] float m_speed = .7f;
        void Update()
        {

            transform.RotateAround(transform.position, transform.up, m_speed);
        }
    }
}
