using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "Config/Window static data")]
    public class WindowStaticData: ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}