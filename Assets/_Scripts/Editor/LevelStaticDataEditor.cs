using _Scripts.StaticData;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string InitialPointTag = "InitialPoint";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect"))
            {
                levelData.InitialHeroPosition = GameObject.FindWithTag(InitialPointTag).transform.position;
            }
            
            EditorUtility.SetDirty(target); //Сохранение изменений редактора
        }
    }
}