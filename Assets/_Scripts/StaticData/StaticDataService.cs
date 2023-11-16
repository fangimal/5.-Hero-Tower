using System.Collections.Generic;
using System.Linq;
using _Scripts.StaticData.Windows;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataWindowsPath = "StaticData/WindowStaticData";
        private const string StaticDataLevelsPath = "StaticData/LevelData";
        private const string StaticDataAudioPath = "StaticData/AudioStaticData";
        
        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        private Dictionary<int, LevelStaticData> _levels;
        private AudioStaticData _audioConfig;

        public void LoadDatas()
        {
            _windowConfigs = Resources.
                Load<WindowStaticData>(StaticDataWindowsPath).
                Configs
                .ToDictionary(x => x.WindowId, x => x);

            _levels = Resources.LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.levelBuildIndex, x => x);

            _audioConfig = Resources.Load<AudioStaticData>(StaticDataAudioPath);
        }

        public WindowConfig ForWindow(WindowId windowId) =>
            _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig) 
                ? windowConfig 
                : null;

        public LevelStaticData ForLevel(int sceneBuildIndex) =>
            _levels.TryGetValue(sceneBuildIndex, out LevelStaticData staticData) 
                ? staticData 
                : null;

        public AudioStaticData GetAudioConfig()
        {
            return _audioConfig;
        }
    }
}