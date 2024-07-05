using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;





[Serializable]
public class PlayerDataWrapper
{
    public List<CollectItem> CollectItems = new List<CollectItem>();
}

[Serializable]
public class CollectItem
{
    public string   Name;
    public int      Count;
}


public class PlayerData : MonoBehaviour
{

    public delegate void PlayerDataDelegate(string name);
    public static PlayerDataDelegate OnPlayerData;

    [Serializable]
    class Report
    {
        public string user_name;
        public string score;
        public string play_date_time;

        public override string ToString()
        {
            return $"user name {user_name} \n score {score} \n play date time {play_date_time}";
        }
    }

    [Serializable]
    public class Login
    {
        public string user_name;
        public string score;
        public string date_time;
        public string logged_in;
    }

    public string FilePath => Application.persistentDataPath + "/Shooter2D.json";

    public string PlayerUserName => PlayerPrefs.GetString("user_name");
    public string PlayerScore => PlayerPrefs.GetString("score");
    public string PlayerLoginDateTime => PlayerPrefs.GetString("login_date_time");
    public string PlayerLoggedIn => PlayerPrefs.GetString("logged_in");

    public PlayerDataWrapper PlayerData_Wrapper { get; private set; }
    public CollectItem Collect_Item { get; private set; }

    public Dictionary<string, int> ItemCollected = new Dictionary<string, int>();

    Report report;
    [HideInInspector]
    public Login login;

    [SerializeField] TMP_Text loginStatus;

    private void Awake()
    {
        report = new Report();
        login = new Login();
        PlayerData_Wrapper = new PlayerDataWrapper();
        Collect_Item = new CollectItem();
        
    }

    private void OnEnable()
    {
        OnPlayerData += UpdatePlayerCollection;
    }



    private void OnDisable()
    {
        OnPlayerData -= UpdatePlayerCollection;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void UpdatePlayerLogin()
    {
        PlayerLoginVerify(login);
        Debug.Log($"<color=green>Player login updated in PlayerPrefs settings.</color>");
    }

    void PlayerLoginVerify(Login login)
    {
        UpdatePlayerPref(login);
    }

    void UpdatePlayerPref(Login login)
    {
        
        if (PlayerPrefs.HasKey(PlayerUserName) && PlayerPrefs.HasKey(PlayerScore) && PlayerPrefs.HasKey(PlayerLoginDateTime) && PlayerPrefs.HasKey(PlayerLoggedIn))
        {
            if(PlayerPrefs.GetString(PlayerLoggedIn) == "true")
            {
                UiHandler.OnUiHandler?.Invoke(1);
                loginStatus.text = $"<color=green>{PlayerUserName} \n {PlayerScore} \n {PlayerLoginDateTime} \n {PlayerLoggedIn}</color>";
            }
            else if(PlayerPrefs.GetString(PlayerLoggedIn) == "false")
            {
                UiHandler.OnUiHandler?.Invoke(0);
                loginStatus.text = string.Empty;
            }
        }
        else
        {
            loginStatus.text = $"<color=red>Failed to load the player data</color>";
        }
    }

    void UpdatePlayerReport(Login login)
    {
        report = new Report()
        {
            user_name = login.user_name,
            score = login.score,
            play_date_time = login.date_time
        };
        
        Debug.Log(report.ToString());
    }

    void UpdatePlayerCollection(string obstacleName)
    {
        Collect_Item.Name = obstacleName;
        Collect_Item.Count += 1;
        PlayerData_Wrapper.CollectItems.Add(Collect_Item);

        int value;
        if (!ItemCollected.ContainsKey(obstacleName))
        {
            ItemCollected.Add(obstacleName, 0);
        }
        
        ItemCollected.TryGetValue(obstacleName, out value);
        value += 1;
        ItemCollected[obstacleName] = value;

        
        string serializedItemCollected = JsonConvert.SerializeObject(ItemCollected);
        HandleDataWithLocalFile(serializedItemCollected);
        Debug.Log($"{serializedItemCollected}");
    }

    void HandleDataWithLocalFile(string data)
    {
        
        
    }

    void ReadDataFromLocalFile()
    {
        if (!File.Exists(FilePath))
        {
            File.Create(FilePath);
        }
        string fileContent = File.ReadAllText(FilePath);
        if (fileContent != null || !string.IsNullOrEmpty(fileContent))
        {
            ItemCollected = JsonConvert.DeserializeObject<Dictionary<string, int>>(fileContent);
            
        }
        Debug.Log($"");
    }
}
