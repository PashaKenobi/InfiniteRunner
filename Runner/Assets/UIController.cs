using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIController : MonoBehaviour
{
    Player player;
    Text distanceText;
    Text CoinText;
    Text FinalCoinText;
    Text finalDistanceText;

    GameObject results;
    GameObject scoreboard;
    public int distance;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
        finalDistanceText = GameObject.Find("FinalDistanceText").GetComponent<Text>();
        CoinText = GameObject.Find("CoinNumber").GetComponent<Text>();
        if (PlayerPrefs.HasKey("Coin"))
        {
            player.numberOfCoins = PlayerPrefs.GetInt("Coin");
            CoinText.text = player.numberOfCoins.ToString();
        }
        FinalCoinText = GameObject.Find("CoinText").GetComponent<Text>();
        results = GameObject.Find("Results");
        scoreboard = GameObject.Find("Score");
        results.SetActive(false);
        scoreboard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m";
        CoinText.text = player.numberOfCoins.ToString();

        if (player.isDead)
        {
            results.SetActive(true);
            finalDistanceText.text  = distance + " m";
            FinalCoinText.text = CoinText.text;
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Scoreboard()
    {
        results.SetActive(false);
        scoreboard.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
