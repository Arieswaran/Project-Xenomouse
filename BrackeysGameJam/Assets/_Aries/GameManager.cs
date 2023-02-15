using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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

    public void GenerateNextGenerationMouse(){ // Include stats from previous generation or use the number of collected cheese as a factor
        previousGenerationMice.Add(mouseData);

        //Do we need random or just increase the stats?
        mouseData = new MouseData();
        mouseData.health = UnityEngine.Random.Range(INITIAL_MAX_HEALTH, mouseData.max_health + 1);
        mouseData.max_health = mouseData.health;
        mouseData.speed = UnityEngine.Random.Range(INITIAL_SPEED, mouseData.speed);
        mouseData.lifespan = UnityEngine.Random.Range(INITIAL_LIFESPAN, mouseData.lifespan);
        mouseData.max_actions = UnityEngine.Random.Range(INITIAL_MAX_ACTIONS, mouseData.max_actions);
        mouseData.actions = mouseData.max_actions;
        mouseData.generation_count++;
    }

    public List<MouseData> GetPreviousGenerationMice(){
        return previousGenerationMice;
    }

}
