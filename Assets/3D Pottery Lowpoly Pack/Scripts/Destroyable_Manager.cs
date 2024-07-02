using System.Collections.Generic;
using UnityEngine;

namespace PotteryLowpolyPack
{

    public class Destroyable_Manager : MonoBehaviour
    {
        public static Destroyable_Manager m_Instance;


        [SerializeField] bool m_usePooling;
        public bool m_UsePooling => m_usePooling;
        [SerializeField] bool m_useSounds;
        public bool m_UseSounds => m_useSounds;
        [SerializeField] Sound_Group[] m_SoundGroups;

        [SerializeField] ColisionConditionType m_conditionType;
        public ColisionConditionType m_OnCollisionActionType => m_conditionType;

        [SerializeField] ActionType m_actionType;
        public ActionType m_ActionType => m_actionType;

        [SerializeField] string[] m_multipleTAGs;
        [SerializeField] string m_singleTAG;

        [SerializeField][Range(1f, 5f)] float m_timeOfDeasapiring;
        public float m_TimeOfDeseapiring => m_timeOfDeasapiring;

        [SerializeField] Destroyable_InParts[] m_destroyable_InParts;

        Dictionary<Destroyable_InParts_Name, Queue<Destroyable_InParts>> m_dictionary_Of_Quenes = new Dictionary<Destroyable_InParts_Name, Queue<Destroyable_InParts>>();
        Dictionary<Destroyable_InParts_Name, Destroyable_InParts> m_dictionary_Of_Prefabs = new Dictionary<Destroyable_InParts_Name, Destroyable_InParts>();

        [SerializeField] Material m_flashMaterial;
        public Material m_FlashMaterial => m_flashMaterial;

        [SerializeField] bool m_addFlash;
        public bool m_AddFlash => m_addFlash;
        [SerializeField][Range(0.01f, 1f)] float m_flashLength;
        public float m_FlashLength => m_flashLength;

        [SerializeField][Range(1, 10)]  int m_initialDurablility;
        public int m_InitialDurability => m_initialDurablility;

        [SerializeField][Range(1, 10)] int m_damageMinimal;
        public int m_DamageMinimal => m_damageMinimal;

        [SerializeField][Range(1, 10)] int m_damageMaximal;
        public int m_DamageMaximal => m_damageMaximal;


        [SerializeField][Range(1, 100)] int m_destroyChance;
        public int m_DestroyChance => m_destroyChance;


        void Awake()
        {
            if (m_Instance == null) m_Instance = this;
            else Destroy(gameObject);
        }

        void Start()
        {
            for (int i = 0; i < m_destroyable_InParts.Length; i++)
            {
                if (!m_dictionary_Of_Quenes.ContainsKey(m_destroyable_InParts[i].m_DestroyableType))
                {
                    m_dictionary_Of_Quenes.Add(m_destroyable_InParts[i].m_DestroyableType, new Queue<Destroyable_InParts>());
                }
                else Debug.Log("Error: multiple destroyables have same Destroyable_InParts_Name assigned");


                if (!m_dictionary_Of_Prefabs.ContainsKey(m_destroyable_InParts[i].m_DestroyableType))
                {
                    m_dictionary_Of_Prefabs.Add(m_destroyable_InParts[i].m_DestroyableType, m_destroyable_InParts[i]);
                }
                else Debug.Log("Error: multiple destroyables have same Destroyable_InParts_Name assigned");
            }

            if (m_useSounds)
            {

                foreach (Sound_Group _sound_Group in m_SoundGroups)
                {
                    foreach (Destroyable_Sound _destroyable_Sound in _sound_Group.m_Destroyable_Sounds)
                    {
                        _destroyable_Sound.m_Source = gameObject.AddComponent<AudioSource>();
                        _destroyable_Sound.m_Source.clip = _destroyable_Sound.m_Clip;
                        _destroyable_Sound.m_Source.volume = _destroyable_Sound.m_Volume;
                        _destroyable_Sound.m_Source.pitch = _destroyable_Sound.m_Pitch;
                    }
                }
            }

        }

        public Destroyable_InParts Grab_Destroyable_InParts(Destroyable_InParts_Name _required_Destroyable_InParts_Name)
        {
            if (!m_dictionary_Of_Quenes.ContainsKey(_required_Destroyable_InParts_Name))
            {
                Debug.Log("Error: required destroyable not found in dictionary");
            }



            if (m_dictionary_Of_Quenes[_required_Destroyable_InParts_Name].Count > 0)
            {
                if (m_useSounds) PlayRandomSound(_required_Destroyable_InParts_Name);
                return m_dictionary_Of_Quenes[_required_Destroyable_InParts_Name].Dequeue();
            }
            else
            {
                Destroyable_InParts _new_destroyable_InParts = Instantiate(m_dictionary_Of_Prefabs[_required_Destroyable_InParts_Name]);
                _new_destroyable_InParts.transform.SetParent(this.transform);
                if (m_useSounds) PlayRandomSound(_required_Destroyable_InParts_Name);
                return _new_destroyable_InParts;
            };
        }
        public void ReturnToQuene(Destroyable_InParts_Name _returning_destroyableType, Destroyable_InParts _destroyable_InParts)
        {
            if (!m_dictionary_Of_Quenes.ContainsKey(_returning_destroyableType))
            {
                Debug.Log("Error: returning destroyable not found in dictionary");
            }

            m_dictionary_Of_Quenes[_returning_destroyableType].Enqueue(_destroyable_InParts);
        }
        public bool Multiple_TAG_Confirmation(string _tagOfCollidedObject)
        {
            if (m_multipleTAGs.Length == 0) return false;
            for (int i = 0; i < m_multipleTAGs.Length; i++)
            {
                if (m_multipleTAGs[i] == _tagOfCollidedObject) return true;
            }
            return false;
        }
        public bool Single_TAG_Confirmation(string _tagOfCollidedObject)
        {
            if (m_singleTAG == _tagOfCollidedObject) return true;
            return false;
        }
        public void PlayRandomSound(Destroyable_InParts_Name _required_Destroyable_InParts_Name)
        {
            int _rightIndex = -1;
            for (int i = 0; i < m_SoundGroups.Length; i++)
            {
                if (_rightIndex < 0 &&
                    (int)m_SoundGroups[i].m_beginingOfGroup <= (int)_required_Destroyable_InParts_Name &&
                    (int)m_SoundGroups[i].m_endOfGroup >= (int)_required_Destroyable_InParts_Name)
                {
                    _rightIndex = i;
                }
            }

            if (_rightIndex != -1) m_SoundGroups[_rightIndex].m_Destroyable_Sounds[Random.Range(0, m_SoundGroups[_rightIndex].m_Destroyable_Sounds.Length)].m_Source.Play();
        }
    }

    public enum ColisionConditionType
    {
        None,
        Simple,
        Single_Tag_Comparsion,
        Multiple_Tag_Comparsion
    }
    public enum ActionType
    {
        Destroy,
        DamageConstant,
        DamageRandom,
        ChanceRandom,
        //DelayedTriger,    //TODO in the next update
    }
}
   