using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//scene management
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int INITIAL_MAX_HEALTH = 2;
    [SerializeField] private int INITIAL_SPEED = 5;
    [SerializeField] private int INITIAL_LIFESPAN = 2;
    [SerializeField] private int INITIAL_MAX_ACTIONS = 2;

    private MouseData mouseData;

    private PlayerData playerData;
    
    private List<MouseData> previousGenerationMice = new List<MouseData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeMouseData();
            InitializePlayerData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeMouseData()
    {
        Debug.Log("Initializing mouse data");
        mouseData = new MouseData();
        mouseData.health = INITIAL_MAX_HEALTH;
        mouseData.max_health = INITIAL_MAX_HEALTH;
        mouseData.speed = INITIAL_SPEED;
        mouseData.lifespan = INITIAL_LIFESPAN;
        mouseData.max_actions = INITIAL_MAX_ACTIONS;
        mouseData.actions = INITIAL_MAX_ACTIONS;
    }

    private void InitializePlayerData()
    {
        playerData = new PlayerData();
    }

    public MouseData GetMouseData()
    {
        return mouseData;
    }

    public void IncreaseMouseHealth(int amount)
    {
        mouseData.health += amount;
        mouseData.OnStatsChanged?.Invoke();
    }

    public void IncreaseMouseSpeed(int amount)
    {
        mouseData.speed += amount;
        mouseData.OnStatsChanged?.Invoke();
    }

    public void IncreaseMouseLifespan(int amount)
    {
        mouseData.lifespan += amount;
        mouseData.OnStatsChanged?.Invoke();
    }

    public bool CanDoActions(){
        return mouseData.actions > 0;
    }

    public void DecreaseActions(){
        mouseData.actions--;
        mouseData.OnStatsChanged?.Invoke();
    }

    public void IncreaseRedCheese(int amount = 1){
        playerData.red_cheese_count += amount;
    }

    public void IncreaseBlueCheese(int amount = 1){
        playerData.blue_cheese_count += amount;
    }

    public void IncreaseGreenCheese(int amount = 1){
        playerData.green_cheese_count += amount;
    }

    public void IncreasePlayedCount(){
        mouseData.played_count++;
    }

    public void IncreaseBrushedCount(){
        mouseData.brushed_count++;
    }

    public void GenerateNextGenerationMouse(){ // Include stats from previous generation or use the number of collected cheese as a factor
        previousGenerationMice.Add(mouseData);

        float extra_stats = 5f;
        if(mouseData.brushed_count > 0){
            if(UnityEngine.Random.Range(0,100) < 40){
                extra_stats += 2f;
            }
        }
        if(mouseData.played_count > 0){
            if(UnityEngine.Random.Range(0,100) < 60){
                extra_stats += 1f;
            }
        }
        Debug.Log("Extra stats: " + extra_stats);
        MouseData newMouseData = new MouseData();
        newMouseData.health = INITIAL_MAX_HEALTH + Mathf.CeilToInt(mouseData.max_health * extra_stats / 100f);
        newMouseData.max_health = mouseData.health;
        newMouseData.speed = INITIAL_SPEED + Mathf.CeilToInt(mouseData.speed * extra_stats / 100f);
        newMouseData.lifespan = INITIAL_LIFESPAN + Mathf.CeilToInt(mouseData.lifespan * extra_stats / 100f);
        newMouseData.max_actions = INITIAL_MAX_ACTIONS;//+ (int) (mouseData.max_actions * extra_stats / 100f);
        newMouseData.actions = mouseData.max_actions;
        newMouseData.generation_count++;
        Debug.Log("New mouse data: " + newMouseData.max_health);
        Debug.Log("New mouse data: " + newMouseData.speed);
        Debug.Log("New mouse data: " + newMouseData.lifespan);
        mouseData = newMouseData;
        Debug.Log("New mouse data: " + mouseData.max_health);
        Invoke("LoadRaiseScene", 3f);
    }

    public List<MouseData> GetPreviousGenerationMice(){
        return previousGenerationMice;
    }

    public void LoadMazeScene(){
        SceneManager.LoadScene("MazePhaseScene");
    }

    public void LoadRaiseScene(){
        SceneManager.LoadScene("RaisePhaseScene");
    }

    public PlayerData GetPlayerData(){
        return playerData;
    }

}
