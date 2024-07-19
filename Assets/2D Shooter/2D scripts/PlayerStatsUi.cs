using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsUi : MonoBehaviour
{

    public delegate void PlayerStatsUiDelegate();
    public static PlayerStatsUiDelegate OnPlayerStatsUi;
    [SerializeField] PlayerData playerData;
    [SerializeField] Transform statsPanel;
    [SerializeField] TMP_Text squareCount;
    [SerializeField] TMP_Text circleCount;
    [SerializeField] TMP_Text triangleCount;
    [SerializeField] TMP_Text diamondCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        OnPlayerStatsUi += UpdatePlayerStatsUi;
        UpdatePlayerStatsUi();
    }

    private void OnDisable()
    {
        OnPlayerStatsUi -= UpdatePlayerStatsUi;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void UpdatePlayerStatsUi()
    {
        PlayerData.Player player = playerData.ReadDataFromLocalFile();
        if(player != null)
        {
            var squareCount = 0;
            var circleCount = 0;
            var triangleCount = 0;
            var diamondCount = 0;
            player.ItemCollected.TryGetValue(Obstacle.Type.square.ToString(), out squareCount);
            player.ItemCollected.TryGetValue(Obstacle.Type.circle.ToString(), out circleCount);
            player.ItemCollected.TryGetValue(Obstacle.Type.triangle.ToString(), out triangleCount);
            player.ItemCollected.TryGetValue(Obstacle.Type.diamond.ToString(), out diamondCount);

            this.squareCount.text = squareCount.ToString();
            this.circleCount.text = circleCount.ToString();
            this.triangleCount.text = triangleCount.ToString();
            this.diamondCount.text = diamondCount.ToString();
        }
        Debug.Log($"<color=green>Player Item collected \t {JsonConvert.SerializeObject(player.ItemCollected)}</color>");
    }

}
