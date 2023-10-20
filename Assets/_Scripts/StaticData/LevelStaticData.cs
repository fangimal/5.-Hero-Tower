using UnityEngine;

namespace _Scripts.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Config/LevelData")]
    public class LevelStaticData : ScriptableObject
    {
        public int levelBuildIndex;

        public int playerMoveSpeed = 5;
        //public string sceneName;
        public Vector3 InitialHeroPosition;
    }
}