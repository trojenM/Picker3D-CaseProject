using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Picker3D
{
    public class LevelWindow : EditorWindow
    {
        private static Level levelData;

        public static void ShowWindow(Level level)
        {
            levelData = level;
            var window = EditorWindow.GetWindow<LevelWindow>(level.name);
            window.Show();
        }

        private void OnGUI()
        {
            Editor.CreateEditor(levelData).OnInspectorGUI();
        }
    }
}
