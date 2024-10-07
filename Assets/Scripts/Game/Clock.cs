using System.Collections;
using UnityEngine;

public class Clock : MonoBehaviour
{
    GameManager gameManager;

    private float time;
    private float currentTime;

    private int minute;
    private int second;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();

        time = 60.0f * gameManager.minute;
    }
    IEnumerator StartTimer()
    {
        currentTime = time;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            minute = (int)currentTime / 60;
            minute = (int)currentTime % 60;
            yield return null;

            if (currentTime <= 0)
            {
                Debug.Log("Time Over");
                currentTime = 0;
                yield break;
            }
        }
    }
}