using System.Collections;
using UnityEngine;

public class Clock : MonoBehaviour
{
    IGameView _gameView;
    GameManager gameManager;
    //게임 내 시간
    public float hour;
    public float minute;
    public float second;

    private float time;
    public float currentTime;

    private const float gameDayInHours = 24f;

    private float timeScale;

    private void Awake()
    {
        _gameView = GetComponent<IGameView>();
        gameManager = GetComponent<GameManager>();
        time = gameManager.playTime;
        timeScale = gameDayInHours / time;
    }
    public IEnumerator StartTimer()
    {
        currentTime = time;
        hour = 0f;
        minute = 0f;
        second = 0f;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            hour += Time.deltaTime * timeScale;
            
            if (hour >= 24f)
            {
                hour = 0f;
            }

            minute = (hour - GetHour()) * 60f;
            second = (minute - GetMinute()) * 60f;

            //Debug.Log(string.Format("Game Time: {0:00} hours, {1:00} minutes, {2:00} seconds", GetHour(), GetMinute(), GetSecond()));
            _gameView.ClockUpdate(string.Format("{0:00} : {1:00}", GetHour(), GetMinute()));
            yield return null;

            if (currentTime <= 0)
            {
                Debug.Log("Time Over");
                currentTime = 0;
                yield break;
            }
        }
    }
    public float GetHour()
    {
        return Mathf.Floor(hour);
    }
    public float GetMinute()
    {
        return Mathf.Floor(minute);
    }
    public float GetSecond()
    {
        return Mathf.Floor(second);
    }
}