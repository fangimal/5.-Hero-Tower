using System;
using System.Runtime.InteropServices;
using _Scripts.Data;
using UnityEngine;

namespace _Scripts.Infrastructure
{
    public class VKProvider : MonoBehaviour
    {
        [SerializeField] private string _link = "https://vk.com/fangstarmedia";
        
        [DllImport("__Internal")]
        private static extern void GetDataVK();

        [DllImport("__Internal")]
        private static extern void SetData(string data);

        [DllImport("__Internal")]
        private static extern void GoToGroupExtern();
        
        [DllImport("__Internal")]
        private static extern void VKFriendExtern();
        
        [DllImport("__Internal")]
        public static extern void VKShowAdvExtern();
        
        [DllImport("__Internal")]
        private static extern void VKRewardAdvExtern();

        private const string PLAYER_DATA_KEY = "PlayerData";

        public DataGroup DG = new DataGroup();

        public static VKProvider Instance;
        public event Action OnLoadData;
        public event Action OnGetReward;
        public event Action OnErrorReward;
        public event Action OnCloseInterstitial;
        public event Action OnPause;
        public event Action OnUnPause;
        
        private void Awake()
        {
            Debug.Log("VKProvider");
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
        
        public void GetSocialLink()
        {
            Application.OpenURL(_link);
        }
        
        public void FriendLink()
        {
            Debug.Log("FriendLink");
            VKFriendExtern();
        }

        public void VKRewardAdv()
        {
#if !UNITY_EDITOR
            VKRewardAdvExtern();
#elif UNITY_EDITOR
            GetReward();
#endif
        }
        
        public void VKShowAdv()
        {
#if !UNITY_EDITOR
            VKShowAdvExtern();
#elif UNITY_EDITOR
            CloseInterstitial();
#endif
        }

        public void ErrorReward()
        {
            OnErrorReward?.Invoke();
        }

        public void GetReward()
        {
            OnGetReward?.Invoke();
        }

        public void CloseInterstitial()
        {
            OnCloseInterstitial?.Invoke();
        }

        public void StartShowADV()
        {
            Debug.Log("Pause");
            OnPause?.Invoke();
            Time.timeScale = 0f;
        }

        public void HideADV()
        {
            Debug.Log("UnPause");
            OnUnPause?.Invoke();
            Time.timeScale = 1f;
        }
    }
}