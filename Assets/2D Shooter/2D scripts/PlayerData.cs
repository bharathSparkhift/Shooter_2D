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

    public delegate void PlayerDataDelegate(string userName, string collectableItem);
    public static PlayerDataDelegate OnPlayerData;

    [Serializable]
    class Player
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
    class Data
    {
        
    }

    [Serializable]
    public class Login
    {
        public string user_name;
        public string score;
        public string date_time;
        public string logged_in;
    }

    public string FilePath => Application.persistentDataPath + "/Shooter2D.txt";
    public string PlayerUserName => PlayerPrefs.GetString("user_name");
    public string PlayerScore => PlayerPrefs.GetString("score");
    public string PlayerLoginDateTime => PlayerPrefs.GetString("login_date_time");
    public string PlayerLoggedIn => PlayerPrefs.GetString("logged_in");
    public PlayerDataWrapper PlayerData_Wrapper { get; private set; }
    public CollectItem Collect_Item { get; private set; }

    Player player;

    [HideInInspector]
    public Login login;

    string _reportContent;



    private void OnEnable()
    {
        OnPlayerData += UpdatePlayerData;
        OnPlayerData += UpdatePlayerCollection;
        
    }


    private void OnDisable()
    {
        OnPlayerData -= UpdatePlayerData;
        OnPlayerData -= UpdatePlayerCollection;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = new Player();
        login = new Login();
        PlayerData_Wrapper = new PlayerDataWrapper();
        Collect_Item = new CollectItem();
        player = ReadDataFromLocalFile();
       
        Debug.Log($"File path \t {FilePath}");
    }

    private void OnApplicationQuit()
    {

        _reportContent = JsonConvert.SerializeObject(player, Formatting.Indented);
        SaveDataToLocalFile();
        Debug.Log($"<color=orange>{nameof(OnApplicationQuit)}</color> \t Report {_reportContent}");
    }

    public void UpdatePlayerData(string username, string obstacleName)
    {
        if (!string.IsNullOrEmpty(username))
        {
            player = ReadDataFromLocalFile();
            player.UserName = username;
            _reportContent = JsonConvert.SerializeObject(player);
            SaveDataToLocalFile();
            Debug.Log($"{nameof(UpdatePlayerData)}");
        }
            
        
    }


    /// <summary>
    /// Update Player collected items to the dictionary.
    /// Save the details in the local json file
    /// </summary>
    /// <param name="obstacleName"></param>
    void UpdatePlayerCollection(string userName, string obstacleName)
    {
        if (!string.IsNullOrEmpty(obstacleName))
        {
            Collect_Item.Name = obstacleName;
            Collect_Item.Count += 1;
            PlayerData_Wrapper.CollectItems.Add(Collect_Item);

            int value = 0;
            if (!player.ItemCollected.ContainsKey(obstacleName))
            {
                player.ItemCollected.Add(obstacleName, 0);
            }
            player.ItemCollected.TryGetValue(obstacleName, out value);
            value += 1;
            player.ItemCollected[obstacleName] = value;
            _reportContent = JsonConvert.SerializeObject(player); // report.
            SaveDataToLocalFile();

            Debug.Log($"Update player collection {_reportContent}");
        }


        
    }

    /// <summary>
    /// Save the data to the local JSON file.
    /// </summary>
    /// <param name="data"></param>
    void SaveDataToLocalFile()
    {
        try
        {
            File.WriteAllText(FilePath, _reportContent);
            Debug.Log($"{nameof(SaveDataToLocalFile)}");

        }
        catch (Exception ex)
        {
            Debug.Log($"<color=red>{ex.Message}</color>");
        }
        
    }


    /// <summary>
    /// Read the data from the Local JSON file.
    /// </summary>
    Player ReadDataFromLocalFile()
    {
        if (!File.Exists(FilePath))
        {
            // Initialize a new report
            player = new Player();
            player.UserName = string.Empty;
            player.Score = "0";
            player.PlayTime = string.Empty;
            player.StartDateTime = string.Empty;
            player.ItemCollected = new Dictionary<string, int>();
            player.ItemCollected.Add(Obstacle.Type.square.ToString(), 0);
            player.ItemCollected.Add(Obstacle.Type.circle.ToString(), 0);
            player.ItemCollected.Add(Obstacle.Type.triangle.ToString(), 0);
            player.ItemCollected.Add(Obstacle.Type.diamond.ToString(), 0);

            _reportContent = JsonConvert.SerializeObject(player);

            SaveDataToLocalFile();
        }
        else
        {
            try
            {
                
                // Read data from the existing file
                string fileContent = File.ReadAllText(FilePath);
                if (!string.IsNullOrEmpty(fileContent))
                {
                    player = JsonConvert.DeserializeObject<Player>(fileContent);

                }
            }
            catch (IOException ex)
            {
                Debug.LogError("IOException: " + ex.Message);
            }
            Debug.Log($"{nameof(ReadDataFromLocalFile)} \n File path {FilePath} \n {JsonConvert.SerializeObject(player)}");
        }
        return player;
    }

    void AddDataToList()
    {
        player.Score = "0";
        player.PlayTime = string.Empty;
        player.StartDateTime = string.Empty;
        player.ItemCollected = new Dictionary<string, int>();
        player.ItemCollected.Add(Obstacle.Type.square.ToString(), 0);
        player.ItemCollected.Add(Obstacle.Type.circle.ToString(), 0);
        player.ItemCollected.Add(Obstacle.Type.triangle.ToString(), 0);
        player.ItemCollected.Add(Obstacle.Type.diamond.ToString(), 0);
        
        _reportContent = JsonConvert.SerializeObject(player);
        
        SaveDataToLocalFile();
        
    }

    

}
