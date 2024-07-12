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
        public string UserName;
        public string Score;
        public string PlayTime;
        public string StartDateTime;
        public string EndDateTime;
        public Dictionary<string, int> ItemCollected = new Dictionary<string, int>();
        public override string ToString()
        {
            return $"score {Score} \n start date time {StartDateTime} \n play date time {PlayTime}";
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

    Report report;

    [HideInInspector]
    public Login login;

    string _reportContent;

    private void Awake()
    {
        
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
        report = new Report();
        login = new Login();
        PlayerData_Wrapper = new PlayerDataWrapper();
        Collect_Item = new CollectItem();
        report = ReadDataFromLocalFile();
        report.StartDateTime = DateTime.Now.ToString();
        Debug.Log($"File path \t {FilePath}");
    }

    private void OnApplicationQuit()
    {
        report.EndDateTime = DateTime.Now.ToString();
        var timeDiff = (DateTime.Now - DateTime.Parse(report.StartDateTime));
        report.PlayTime = timeDiff.ToString();
        _reportContent = JsonConvert.SerializeObject(report);
        SaveDataToLocalFile();
        Debug.Log($"<color=orange>{nameof(OnApplicationQuit)}</color> \t Report {_reportContent}");
    }

    /// <summary>
    /// Update Player collected items to the dictionary.
    /// Save the details in the local json file
    /// </summary>
    /// <param name="obstacleName"></param>
    void UpdatePlayerCollection(string obstacleName)
    {
        Collect_Item.Name = obstacleName;
        Collect_Item.Count += 1;
        PlayerData_Wrapper.CollectItems.Add(Collect_Item);

        int value = 0;
        if (!report.ItemCollected.ContainsKey(obstacleName))
        {
            report.ItemCollected.Add(obstacleName, 0);
        }
        
        report.ItemCollected.TryGetValue(obstacleName, out value);
        value += 1;
        report.ItemCollected[obstacleName] = value;

        _reportContent = JsonConvert.SerializeObject(report); // report.
        SaveDataToLocalFile();
        Debug.Log($"Update player collection {_reportContent}");
    }

    /// <summary>
    /// Save the data to the local JSON file.
    /// </summary>
    /// <param name="data"></param>
    void SaveDataToLocalFile()
    {
        File.WriteAllText(FilePath, _reportContent);
        Debug.Log($"{nameof(SaveDataToLocalFile)}");
    }


    /// <summary>
    /// Read the data from the Local JSON file.
    /// </summary>
    Report ReadDataFromLocalFile()
    {
        if (!File.Exists(FilePath))
        {
            // Initialize a new report
            report = new Report
            {
                UserName = string.Empty,
                Score = "0",
                PlayTime = string.Empty,
                StartDateTime = string.Empty,
                ItemCollected = new Dictionary<string, int>
            {
                { Obstacle.Type.square.ToString(), 0 },
                { Obstacle.Type.circle.ToString(), 0 },
                { Obstacle.Type.triangle.ToString(), 0 },
                { Obstacle.Type.diamond.ToString(), 0 }
            }
            };
            _reportContent = JsonConvert.SerializeObject(report);
            try
            {
                // Create the file and write initial data
                using (FileStream fileStream = File.Create(FilePath))
                {
                    using (StreamWriter writer = new StreamWriter(fileStream))
                    {
                        writer.Write(_reportContent);
                    }
                }
            }
            catch (IOException ex)
            {
                Debug.LogError("IOException: " + ex.Message);
            }
            Invoke(nameof(SaveDataToLocalFile), 1f);
        }
        else
        {
            try
            {
                // Read data from the existing file
                string fileContent = File.ReadAllText(FilePath);
                if (!string.IsNullOrEmpty(fileContent))
                {
                    report = JsonConvert.DeserializeObject<Report>(fileContent);
                }
            }
            catch (IOException ex)
            {
                Debug.LogError("IOException: " + ex.Message);
            }
            Debug.Log($"{nameof(ReadDataFromLocalFile)} \n File path {FilePath}");
        }
        return report;
    }

    public void LoginOnButtonClick(string username)
    {
        report = ReadDataFromLocalFile();
        report.UserName = username;
        SaveDataToLocalFile();
        StartSceneManager.OnStartSceneManager?.Invoke();
    }

}
