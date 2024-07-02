using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PotteryLowpolyPack
{
    [System.Serializable]
    public class Destroyable_Sound
    {
        public string m_Name;
        public AudioClip m_Clip;
        [Range (0.0f, 1.0f)] public float m_Volume;
        [Range(0.1f, 3.0f)] public float m_Pitch;
        [HideInInspector] public AudioSource m_Source;
    }

    [System.Serializable]

    public class Sound_Group
    {
        public Destroyable_Sound[] m_Destroyable_Sounds;
        public Destroyable_InParts_Name m_beginingOfGroup;
        public Destroyable_InParts_Name m_endOfGroup;
    }
}

