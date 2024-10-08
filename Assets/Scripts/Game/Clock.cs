using System.Collections;
using UnityEngine;

public class Clock : MonoBehaviour
{
    GameManager gameManager;

    private float time;
    private float currentTime;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();

        time = gameManager.second;
    }
    IEnumerator StartTimer()
    {
        currentTime = time;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
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