using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Picker3D
{
	[CustomEditor(typeof(LevelEditor))]
	public class LevelEditorEditor : Editor
	{
		private LevelEditor levelEditor;
		private SerializedProperty levelsProperty;
		private SerializedProperty thisTransformProperty;
		
		private void OnEnable() 
		{
			levelEditor = (LevelEditor)target;
			levelsProperty = serializedObject.FindProperty("m_levels");
			thisTransformProperty = serializedObject.FindProperty("m_thisTransform");
		}
		
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			
			DrawHeader("Level Editor Save/Edit");
			DrawHorizontalLine();
			EditorGUILayout.Space(5f);
			DrawLevelEditor();
			EditorGUILayout.Space(5f);
			DrawHorizontalLine();
			EditorGUILayout.LabelField("Note!!: Save button only saves children of LevelEditor(this).", new GUIStyle(EditorStyles.boldLabel));
			
			serializedObject.ApplyModifiedProperties();
		}
		
		private void DrawLevelEditor()
		{
			EditorGUILayout.PropertyField(thisTransformProperty);
			EditorGUILayout.LabelField("Levels");
			EditorGUI.indentLevel += 1;

			EditorGUILayout.PropertyField(levelsProperty.FindPropertyRelative("Array.size"));
			for (int i = 0; i < levelsProperty.arraySize; i++)
			{
				SerializedProperty levelProperty = levelsProperty.GetArrayElementAtIndex(i);
				Level levelRefValue = (Level)levelProperty.objectReferenceValue;

				GUILayout.BeginHorizontal();

				EditorGUILayout.PropertyField(levelProperty);

				GUI.enabled = levelRefValue == null;

				if (GUILayout.Button("Add", EditorStyles.miniButtonLeft, GUILayout.ExpandWidth(false)))
				{
					Level level = ScriptableObject.CreateInstance<Level>();
					AssetDatabase.CreateAsset(level, $"Assets/ScriptableObjects/LevelData/Level{i}.asset");
					AssetDatabase.SaveAssets();
					AssetDatabase.Refresh();
					EditorUtility.FocusProjectWindow();
					levelProperty.objectReferenceValue = level;
				}

				GUI.enabled = true;

				if (GUILayout.Button("Save", EditorStyles.miniButtonMid, GUILayout.ExpandWidth(false)))
				{
					levelEditor.SaveLevel(levelRefValue);
				}

				if (GUILayout.Button("Load", EditorStyles.miniButtonMid, GUILayout.ExpandWidth(false)))
				{
					levelEditor.LoadLevel(levelRefValue);
				}

				GUI.enabled = levelRefValue != null;

				if (GUILayout.Button("Open", EditorStyles.miniButtonRight, GUILayout.ExpandWidth(false)))
				{
					LevelWindow.ShowWindow(levelRefValue);
				}

				GUI.enabled = true;

				GUILayout.EndHorizontal();
			}
		}
		
		private void DrawHeader(string headerText)
		{
			GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
			style.fontSize = 14; // You can adjust the font size if needed.
			EditorGUILayout.LabelField(headerText, style);
		}
		private void DrawHorizontalLine()
		{
			Rect rect = EditorGUILayout.GetControlRect(false, 3);
			EditorGUI.DrawRect(rect, Color.white);
		}
	}
}
