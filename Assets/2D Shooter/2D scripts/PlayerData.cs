using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public delegate void PlayerDataDelegate(PlayerData.Login login);
    public static PlayerDataDelegate OnPlayerDataCalled;



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



    public string PlayerUserName => PlayerPrefs.GetString("user_name");
    public string PlayerScore => PlayerPrefs.GetString("score");
    public string PlayerLoginDateTime => PlayerPrefs.GetString("login_date_time");

    public string PlayerLoggedIn => PlayerPrefs.GetString("logged_in");

    Report report;
    [HideInInspector]
    public Login login;

    [SerializeField] TMP_Text loginStatus;

    private void Awake()
    {
        report = new Report();
        login = new Login();
        
    }

    private void OnEnable()
    {
        OnPlayerDataCalled += UpdatePlayerLogin;
    }



    private void OnDisable()
    {
        OnPlayerDataCalled -= UpdatePlayerLogin;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }





    void UpdatePlayerLogin(Login login)
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
                // Enable Player canvas
                /*PlayerPrefs.SetString(PlayerUserName, PlayerPrefs.GetString(PlayerUserName));
                PlayerPrefs.SetString(PlayerScore, PlayerPrefs.GetString(PlayerScore));
                PlayerPrefs.SetString(PlayerLoginDateTime, PlayerPrefs.GetString(PlayerLoginDateTime));
                PlayerPrefs.SetString(PlayerLoggedIn, PlayerPrefs.GetString(PlayerLoggedIn));*/
                UiHandler.OnUiHandler?.Invoke(1);
                loginStatus.text = $"<color=green>{PlayerUserName} \n {PlayerScore} \n {PlayerLoginDateTime} \n {PlayerLoggedIn}</color>";
            }
            else if(PlayerPrefs.GetString(PlayerLoggedIn) == "false")
            {
                // Disable Player canvas
                /*PlayerPrefs.SetString(PlayerUserName, PlayerPrefs.GetString(PlayerUserName));
                PlayerPrefs.SetString(PlayerScore, PlayerPrefs.GetString(PlayerScore));
                PlayerPrefs.SetString(PlayerLoginDateTime, PlayerPrefs.GetString(PlayerLoginDateTime));
                PlayerPrefs.SetString(PlayerLoggedIn, PlayerPrefs.GetString(PlayerLoggedIn));*/
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
}
