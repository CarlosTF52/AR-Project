using System;
using UnityEditor;
using UnityEngine;

namespace PotteryLowpolyPack
{
    [CustomEditor(typeof(Destroyable_WholeItem))]
    public class Destroyable_WholeItem_Editor : Editor
    {
        SerializedProperty m_radius;
        SerializedProperty m_maxHit;
        SerializedProperty m_cascadeSteps;
        SerializedProperty m_cascadeDelay;
        SerializedProperty m_addExplosoiveForce;
        SerializedProperty m_DestroyableType;
        SerializedProperty m_limitGarbageCollection;
        SerializedProperty m_addFlash;
        SerializedProperty m_flashLength;
        SerializedProperty m_noteObstacles;
        SerializedProperty m_obstaclesLayerMask;
        SerializedProperty m_overideManager_Flash;
        SerializedProperty m_overideManager_Condition;
        SerializedProperty m_overideManager_Action;
        SerializedProperty m_collistionActionType;
        SerializedProperty m_actionType;
        SerializedProperty m_singleTAG;
        SerializedProperty m_multipleTAGs;
        SerializedProperty m_destroyChance;
        SerializedProperty m_damageMinimal;
        SerializedProperty m_damageMaximal;
        SerializedProperty m_initialDurablility;
        GUILayoutOption[] m_gUILayoutOption;

        private void OnEnable()
        {
            m_radius = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_radius));
            m_maxHit = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_maxHit));
            m_cascadeSteps = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_cascadeSteps));
            m_cascadeDelay = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_cascadeDelay));
            m_addExplosoiveForce = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_addExplosoiveForce));
            m_DestroyableType = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_DestroyableType));
            m_limitGarbageCollection = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_limitGarbageCollection));
            m_addFlash = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_addFlash));
            m_flashLength = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_flashLength));
            m_noteObstacles = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_noteObstacles));
            m_obstaclesLayerMask = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_obstaclesLayerMask));
            m_overideManager_Flash = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_OverideManager_Flash));
            m_overideManager_Condition = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_OverideManager_Condition));
            m_overideManager_Action = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_OverideManager_Action));
            m_collistionActionType = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_CollistionActionType));
            m_singleTAG = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_SingleTAG));
            m_multipleTAGs = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_MultipleTAGs));
            m_actionType = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_ActionType));
            m_destroyChance = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_DestroyChance));
            m_damageMinimal = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_DamageMinimal));
            m_damageMaximal = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_DamageMaximal));
            m_initialDurablility = serializedObject.FindProperty(nameof(Destroyable_WholeItem.m_InitialDurablility));
            m_gUILayoutOption = new GUILayoutOption[1];
            m_gUILayoutOption[0] = GUILayout.Width(300);

        }

        public override void OnInspectorGUI()
        {

            serializedObject.Update();


            m_DestroyableType.enumValueIndex = (int)(Destroyable_InParts_Name)EditorGUILayout.EnumPopup("Type", (Destroyable_InParts_Name)Enum.GetValues(typeof(Destroyable_InParts_Name)).GetValue(m_DestroyableType.enumValueIndex));

            EditorGUILayout.Space();
            m_addExplosoiveForce.boolValue = EditorGUILayout.Toggle("Add Explosion", m_addExplosoiveForce.boolValue, m_gUILayoutOption);

            if (m_addExplosoiveForce.boolValue)   //myTarget.m_addExplosoiveForce
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);

                m_radius.floatValue = EditorGUILayout.Slider("   Radius", m_radius.floatValue, 1, 10, m_gUILayoutOption);
                m_cascadeSteps.intValue = EditorGUILayout.IntSlider("   Cascades", m_cascadeSteps.intValue, 1, 10, m_gUILayoutOption);
                m_cascadeDelay.floatValue = EditorGUILayout.Slider("   Cascades Delay", m_cascadeDelay.floatValue, 0.01f, 1f, m_gUILayoutOption);

                m_limitGarbageCollection.boolValue = EditorGUILayout.Toggle("   Limit GC", m_limitGarbageCollection.boolValue, m_gUILayoutOption);

                if (m_limitGarbageCollection.boolValue)
                {
                    m_maxHit.intValue = EditorGUILayout.IntSlider("   Max Hit", m_maxHit.intValue, 10, 50, m_gUILayoutOption);
                }

                m_noteObstacles.boolValue = EditorGUILayout.Toggle("   Note Obstacles", m_noteObstacles.boolValue, m_gUILayoutOption);

                if (m_noteObstacles.boolValue)
                {
                    int _amountOfLayers = 0;
                    for (int i = 0; i <= 31; i++)
                    {
                        var _layerN = LayerMask.LayerToName(i);
                        if (_layerN.Length > 0)
                            _amountOfLayers++;
                    }

                    int _addedSoFar = 0;
                    string[] _layerNames = new string[_amountOfLayers];
                    for (int i = 0; i <= 31; i++)
                    {
                        var _layerN = LayerMask.LayerToName(i);
                        if (_layerN.Length > 0)
                        {
                            _layerNames[_addedSoFar] = _layerN;
                            _addedSoFar++;
                        }
                    }
                    m_obstaclesLayerMask.intValue = (LayerMask)EditorGUILayout.MaskField("   Obstacle Layer", m_obstaclesLayerMask.intValue, _layerNames, m_gUILayoutOption);
                }
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Override Manager");

            m_overideManager_Condition.boolValue = EditorGUILayout.Toggle("Override Condition", m_overideManager_Condition.boolValue, m_gUILayoutOption);
            if (m_overideManager_Condition.boolValue)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
                m_collistionActionType.enumValueIndex = (int)(ColisionConditionType)EditorGUILayout.EnumPopup("Condition Type", (ColisionConditionType)Enum.GetValues(typeof(ColisionConditionType)).GetValue(m_collistionActionType.enumValueIndex), m_gUILayoutOption);
                if (m_collistionActionType.enumValueIndex == (int)ColisionConditionType.Single_Tag_Comparsion)
                {
                    m_singleTAG.stringValue = EditorGUILayout.TagField("   TAG", m_singleTAG.stringValue, m_gUILayoutOption);
                }
                if (m_collistionActionType.enumValueIndex == (int)ColisionConditionType.Multiple_Tag_Comparsion)
                {
                    EditorGUILayout.PropertyField(m_multipleTAGs, m_gUILayoutOption);
                }
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
            }

            m_overideManager_Action.boolValue = EditorGUILayout.Toggle("Override Action", m_overideManager_Action.boolValue, m_gUILayoutOption);
            if (m_overideManager_Action.boolValue)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
                m_actionType.enumValueIndex = (int)(ActionType)EditorGUILayout.EnumPopup("Action Type", (ActionType)Enum.GetValues(typeof(ActionType)).GetValue(m_actionType.enumValueIndex), m_gUILayoutOption);

                if (m_actionType.enumValueIndex == (int)ActionType.DamageConstant)
                {
                    EditorGUILayout.PropertyField(m_initialDurablility, m_gUILayoutOption);
                }
                if (m_actionType.enumValueIndex == (int)ActionType.DamageRandom)
                {
                    EditorGUILayout.PropertyField(m_initialDurablility, m_gUILayoutOption);
                    EditorGUILayout.PropertyField(m_damageMinimal, m_gUILayoutOption);
                    EditorGUILayout.PropertyField(m_damageMaximal, m_gUILayoutOption);
                }
                if (m_actionType.enumValueIndex == (int)ActionType.ChanceRandom)
                {
                    EditorGUILayout.PropertyField(m_destroyChance, m_gUILayoutOption);
                }
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
            }

            m_overideManager_Flash.boolValue = EditorGUILayout.Toggle("Override Flash", m_overideManager_Flash.boolValue, m_gUILayoutOption);
            if (m_overideManager_Flash.boolValue)
            {
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
                m_addFlash.boolValue = EditorGUILayout.Toggle("   Add Flash", m_addFlash.boolValue, m_gUILayoutOption);
                if (m_addFlash.boolValue)
                {
                    m_flashLength.floatValue = EditorGUILayout.Slider("   Flash Length", m_flashLength.floatValue, 0.01f, 1f, m_gUILayoutOption);
                }
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider, m_gUILayoutOption);
            }


            serializedObject.ApplyModifiedProperties();

        }
    }
}
