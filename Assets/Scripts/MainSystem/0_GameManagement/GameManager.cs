using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maxDay;
    [SerializeField] private int playTime;
    public int MaxDay { get; private set; }
    public int PlayTime { get; private set; }

    void Awake()
    {
        MaxDay = maxDay;
        PlayTime = playTime;
    }
}