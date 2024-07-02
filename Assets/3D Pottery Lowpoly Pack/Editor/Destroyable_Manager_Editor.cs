using UnityEditor;
using UnityEngine;


namespace PotteryLowpolyPack
{
    [CustomEditor(typeof(Destroyable_Manager))]
    public class Destroyable_Manager_Editor : Editor
    {

        SerializedProperty m_usePooling;
        SerializedProperty m_conditionType;
        SerializedProperty m_actionType;

        SerializedProperty m_multipleTAGs;
        SerializedProperty m_singleTAG;
        SerializedProperty m_timeOfDeseapiring;
        SerializedProperty m_destroyable_InParts;
        SerializedProperty m_useSounds;
        SerializedProperty m_SoundGroups;
        SerializedProperty m_addFlash;
        SerializedProperty m_flashLength;

        SerializedProperty m_initialDurablility;
        SerializedProperty m_destroyChance;
        SerializedProperty m_damageMinimal;
        SerializedProperty m_damageMaximal;


        GUILayoutOption[] m_gUILayoutOption;

        private void OnEnable()
        {
            m_usePooling = serializedObject.FindProperty("m_usePooling");
            m_conditionType = serializedObject.FindProperty("m_conditionType");
            m_actionType = serializedObject.FindProperty("m_actionType");

            m_multipleTAGs = serializedObject.FindProperty("m_multipleTAGs");
            m_singleTAG = serializedObject.FindProperty("m_singleTAG");
            m_timeOfDeseapiring = serializedObject.FindProperty("m_timeOfDeasapiring");
            m_destroyable_InParts = serializedObject.FindProperty("m_destroyable_InParts");
            m_useSounds = serializedObject.FindProperty("m_useSounds");
            m_SoundGroups = serializedObject.FindProperty("m_SoundGroups");
            m_addFlash = serializedObject.FindProperty("m_addFlash");
            m_flashLength = serializedObject.FindProperty("m_flashLength");

            m_initialDurablility = serializedObject.FindProperty("m_initialDurablility");
            m_destroyChance = serializedObject.FindProperty("m_destroyChance");
            m_damageMinimal = serializedObject.FindProperty("m_damageMinimal");
            m_damageMaximal = serializedObject.FindProperty("m_damageMaximal");

            m_gUILayoutOption = new GUILayoutOption[1];
            m_gUILayoutOption[0] = GUILayout.Width(300);

        }

        public override void OnInspectorGUI()
        {
            Destroyable_Manager _destroyable_Manager = (Destroyable_Manager)target;

            serializedObject.Update();


            EditorGUILayout.LabelField("Pooling System Settings");

            EditorGUILayout.PropertyField(m_usePooling, m_gUILayoutOption);
            if (_destroyable_Manager.m_UsePooling)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
                EditorGUILayout.PropertyField(m_timeOfDeseapiring, m_gUILayoutOption);
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);

            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("On Colision");

            EditorGUILayout.PropertyField(m_conditionType, m_gUILayoutOption);

            if (_destroyable_Manager.m_OnCollisionActionType == ColisionConditionType.Single_Tag_Comparsion)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
                EditorGUILayout.TagField(m_singleTAG.stringValue, m_gUILayoutOption);
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
            }
            if (_destroyable_Manager.m_OnCollisionActionType == ColisionConditionType.Multiple_Tag_Comparsion)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
                EditorGUILayout.PropertyField(m_multipleTAGs, m_gUILayoutOption);
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
            }

            if (_destroyable_Manager.m_OnCollisionActionType != ColisionConditionType.None)
            {
                EditorGUILayout.PropertyField(m_actionType, m_gUILayoutOption);
                if (_destroyable_Manager.m_ActionType == ActionType.DamageConstant)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
                    EditorGUILayout.PropertyField(m_initialDurablility, m_gUILayoutOption);
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
                }

                if (_destroyable_Manager.m_ActionType == ActionType.ChanceRandom)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
                    EditorGUILayout.PropertyField(m_destroyChance, m_gUILayoutOption);
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
                }

                if (_destroyable_Manager.m_ActionType == ActionType.DamageRandom)
                {
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
                    EditorGUILayout.PropertyField(m_initialDurablility, m_gUILayoutOption);
                    EditorGUILayout.PropertyField(m_damageMinimal, m_gUILayoutOption);
                    EditorGUILayout.PropertyField(m_damageMaximal, m_gUILayoutOption);
                    if (_destroyable_Manager.m_DamageMaximal < _destroyable_Manager.m_DamageMinimal) EditorGUILayout.HelpBox("Warrning: Maximum value of damage supposed to be bigger than minimal value", MessageType.Warning);
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
                }
            }


            EditorGUILayout.PropertyField(m_useSounds);
            if (_destroyable_Manager.m_UseSounds)
            {
               EditorGUILayout.PropertyField(m_SoundGroups, true);
            }

            EditorGUILayout.PropertyField(m_addFlash);
            if (_destroyable_Manager.m_AddFlash)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
                EditorGUILayout.PropertyField(m_flashLength);
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
            }


            serializedObject.ApplyModifiedProperties();
        }
    }
}

