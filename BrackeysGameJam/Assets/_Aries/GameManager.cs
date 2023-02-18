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
        if (mouseData.health > mouseData.max_health)
        {
            mouseData.max_health = mouseData.health;
        }
        mouseData.OnStatsChanged?.Invoke();
    }

    public void IncreaseMouseSpeed(int percentage)
    {
        mouseData.speed += Mathf.FloorToInt(mouseData.speed * percentage / 100f);
        mouseData.OnStatsChanged?.Invoke();
    }

    public void IncreaseMouseLifespan(int amount)
    {
        mouseData.lifespan += amount;
        mouseData.OnStatsChanged?.Invoke();
    }

    public void IncreaseMouseLifespanByPercentageFromPreviousMouse(int percentage)
    {
        MouseData previousMouseData = previousGenerationMice[previousGenerationMice.Count - 1];
        mouseData.lifespan += Mathf.FloorToInt(previousMouseData.lifespan * percentage / 100f);
        mouseData.OnStatsChanged?.Invoke();
    }

    public void IncreaseMouseSpeedByPercentageFromPreviousMouse(int percentage)
    {
        MouseData previousMouseData = previousGenerationMice[previousGenerationMice.Count - 1];
        mouseData.speed += Mathf.FloorToInt(previousMouseData.speed * percentage / 100f);
        mouseData.OnStatsChanged?.Invoke();
    }

    public void IncreaseMouseHealthByPercentageFromPreviousMouse(int percentage)
    {
        MouseData previousMouseData = previousGenerationMice[previousGenerationMice.Count - 1];
        mouseData.health += Mathf.FloorToInt(previousMouseData.health * percentage / 100f);
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

        float extra_stats = 30f;
        if(mouseData.brushed_count > 0){
            if(UnityEngine.Random.Range(0,100) < 40){
                extra_stats += 6f;
            }
        }
        if(mouseData.played_count > 0){
            if(UnityEngine.Random.Range(0,100) < 60){
                extra_stats += 4f;
            }
        }
        MouseData newMouseData = new MouseData();
        newMouseData.health = INITIAL_MAX_HEALTH + Mathf.FloorToInt(mouseData.max_health * extra_stats / 100f);
        newMouseData.max_health = mouseData.health;
        newMouseData.speed = INITIAL_SPEED + Mathf.FloorToInt(mouseData.speed * extra_stats / 100f);
        newMouseData.lifespan = INITIAL_LIFESPAN + Mathf.FloorToInt(mouseData.lifespan * extra_stats / 100f);
        newMouseData.max_actions = INITIAL_MAX_ACTIONS;
        newMouseData.actions = mouseData.max_actions;
        newMouseData.generation_count = mouseData.generation_count;
        newMouseData.generation_count++;
        mouseData = newMouseData;
        Invoke(nameof(LoadRaiseScene), 3f);
    }

    public List<MouseData> GetPreviousGenerationMice(){
        return previousGenerationMice;
    }

    public void LoadMazeScene(){
        GameSceneManager.Load(GameSceneManager.Scene.MazePhaseScene);
    }

    public void LoadRaiseScene(){
        GameSceneManager.Load(GameSceneManager.Scene.RaisePhaseScene);
    }

    public PlayerData GetPlayerData(){
        return playerData;
    }

    public int GetGenerationCount(){
        return mouseData.generation_count;
    }

}
