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

    private MouseData mouseData;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeMouseData();
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
        return mouseData.lifespan > 0;
    }

    private void GenerateNextGenerationMouse(){ // Include stats from previous generation or use the number of collected cheese as a factor
        //Do we need random or just increase the stats?
        mouseData.health = UnityEngine.Random.Range(INITIAL_MAX_HEALTH, mouseData.max_health + 1);
        mouseData.speed = UnityEngine.Random.Range(INITIAL_SPEED, mouseData.speed);
        mouseData.lifespan = UnityEngine.Random.Range(INITIAL_LIFESPAN, mouseData.lifespan);
    }

}
