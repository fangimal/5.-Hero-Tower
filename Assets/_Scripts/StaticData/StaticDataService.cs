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
        
        private Dictionary<WindowId, WindowConfig> _windowConfigs;

        public void LoadDatas()
        {
            _windowConfigs = Resources.
                Load<WindowStaticData>(StaticDataWindowsPath).
                Configs
                .ToDictionary(x => x.WindowId, x => x);
        }

        public WindowConfig ForWindow(WindowId windowId) =>
            _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig) 
                ? windowConfig 
                : null;
    }
}