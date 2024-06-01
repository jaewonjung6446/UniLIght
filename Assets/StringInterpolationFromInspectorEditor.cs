using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StringInterpolationFromInspector))]
public class MultiplePeopleGroupManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty peopleGroupListsProp = serializedObject.FindProperty("WhatToSay");

        if (peopleGroupListsProp != null)
        {
            for (int i = 0; i < peopleGroupListsProp.arraySize; i++)
            {
                SerializedProperty peopleGroupListProp = peopleGroupListsProp.GetArrayElementAtIndex(i);

                EditorGUILayout.PropertyField(peopleGroupListProp);

                if (peopleGroupListProp.isExpanded)
                {
                    SerializedProperty groupNameProp = peopleGroupListProp.FindPropertyRelative("stringInfo");
                    SerializedProperty insertPoint = peopleGroupListProp.FindPropertyRelative("insertPoint");

                }
            }
            if (GUILayout.Button($"Remove Group"))
            {
                peopleGroupListsProp.DeleteArrayElementAtIndex(0);
            }
            if (GUILayout.Button("말할 내용 추가"))
            {
                peopleGroupListsProp.InsertArrayElementAtIndex(peopleGroupListsProp.arraySize);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
