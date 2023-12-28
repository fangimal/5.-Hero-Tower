using System;
using System.Runtime.InteropServices;
using _Scripts.Data;
using UnityEngine;

namespace _Scripts.Infrastructure
{
    public class VKProvider : MonoBehaviour
    {
        //VK

        [DllImport("__Internal")]
        private static extern void GetDataVK();

        [DllImport("__Internal")]
        private static extern void SetData(string data);

        [DllImport("__Internal")]
        private static extern void GoToGroupExtern();

        private const string PLAYER_DATA_KEY = "PlayerData";

        public DataGroup DG = new DataGroup();

        public static VKProvider Instance;

        public event Action OnLoadData;


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void LoadWEBData()
        {
            //DG = new();
            //SaveData();
#if !UNITY_EDITOR
            GetDataVK();
#elif UNITY_EDITOR
            bool hasData = PlayerPrefs.HasKey(PLAYER_DATA_KEY);

            if (hasData)
            {
                DG = JsonUtility.FromJson<DataGroup>(PlayerPrefs.GetString(PLAYER_DATA_KEY));
            }

            {
                DG = new();
            }
            Debug.Log("LoadWEBData");
            OnLoadData?.Invoke();
#endif
        }

        public void FirstGetData()
        {
            Debug.Log("SET NEW DATA! FirstGetData");
            DG = new();
            SaveData();
            OnLoadData?.Invoke();
        }

        public void DataGetting(string data)
        {
            Debug.Log("DataGetting: " + data);

            string parsText = data.Replace("\\", "");
            Debug.Log("parsText: " + parsText);

            string st1 = parsText.Substring(parsText.IndexOf("playerData") - 2);
            Debug.Log("st1: " + st1);

            string st2 = st1.Remove(st1.Length - 4);
            Debug.Log("st2: " + st2);

            DG = JsonUtility.FromJson<DataGroup>(st2);
            Debug.Log("DG: " + DG + " , DG.playerData: " + DG.playerData);

            Debug.Log("Load data scrips! DataGetting: " + DG + " data: " + data + " st1: " + st1 + " st2: " + st2);
            OnLoadData?.Invoke();
        }

        public void SavePlayerData(PlayerData playerData)
        {
            DG.playerData = playerData;
            SaveData();
        }

        private void SaveData()
        {
            string dataString = JsonUtility.ToJson(DG);

#if !UNITY_EDITOR
            SetData(dataString);
#elif UNITY_EDITOR
            PlayerPrefs.SetString(PLAYER_DATA_KEY, dataString);
#endif
        }

        public void GoToGroup()
        {
            GoToGroupExtern();
        }
    }
}