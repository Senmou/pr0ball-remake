﻿using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(menuName = "EnemyHP")]
public class EnemyHP : ScriptableObject {

    public StepValues[] stepValueList;

    public int MaxHP {
        get => CalcHP(LevelData.Level);
    }

    [Serializable]
    public struct StepValues {
        public int step;
        public int value;
    }

    public int maxHP;

    [Range(1,100)]
    public int testLevel;

    public int CalcHP(int level) {
        maxHP = 0;
        for (int i = 0; i < stepValueList.Length; i++) {
            maxHP += HP(level, stepValueList[i].value, stepValueList[i].step);
        }

        // a little randomness
        float bonusPercantage = UnityEngine.Random.Range(1f, 1.2f);
        maxHP = (int)(maxHP * bonusPercantage);

        return maxHP;
    }

    // Helper function for adding healthPoints to enemies after "step" levels
    private int HP(int level, int hp, int step) => (level / step) * hp;
}

#if UNITY_EDITOR

[CustomEditor(typeof(EnemyHP))]
[CanEditMultipleObjects]
public class EnemyHPEditor : Editor {

    private EnemyHP myScript;

    SerializedProperty maxHP;
    SerializedProperty testLevel;
    SerializedProperty stepValueList;

    void OnEnable() {
        myScript = target as EnemyHP;
        maxHP = serializedObject.FindProperty("maxHP");
        testLevel = serializedObject.FindProperty("testLevel");
        stepValueList = serializedObject.FindProperty("stepValueList");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(maxHP);
        int level = EditorGUILayout.IntSlider(myScript.testLevel, 1, 100);
        EditorGUILayout.PropertyField(stepValueList, true);

        if (GUILayout.Button("Calculate")) {
            myScript.CalcHP(testLevel.intValue);
        }

        if (level != myScript.testLevel) {
            myScript.CalcHP(level);
            myScript.testLevel = level;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif