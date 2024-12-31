using System.Collections;
using UnityEngine;

public class GameDateManager : MonoBehaviour
{
    IGameView _gameView;
    IGamePresenter _gamePresenter;
    IGameModel _gameModel;
    PlayerDayModel playerDayModel;
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
        _gamePresenter = GetComponent<IGamePresenter>();
        _gameModel = GetComponent<IGameModel>();
        playerDayModel = _gameModel.GetPlayerDayModel();
        time = playerDayModel.CurrentTime;
        timeScale = gameDayInHours / time;
    }
    public IEnumerator DayCycle()
    {
        currentTime = playerDayModel.CurrentTime;
        hour = 0f;
        minute = 0f;
        second = 0f;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            hour += Time.deltaTime * timeScale;
            


            minute = (hour - GetHour()) * 60f;
            //second = (minute - GetMinute()) * 60f;

            //Debug.Log(string.Format("Game Time: {0:00} hours, {1:00} minutes, {2:00} seconds", GetHour(), GetMinute(), GetSecond()));
            _gameView.ClockUpdate(GetHour(), GetMinute());
            _gameView.ViewUpdate();
            
            yield return null;

            if (currentTime <= 0)
            {
                currentTime = 0;
                _gameModel.SetTimeScale(1);
                _gamePresenter.DoTodayResult();
                _gameView.ShowUI(_gameView.ToDayResult);
                yield break;
            }
            if (hour >= 24f)
            {
                hour = 0f;
            }
        }
    }
    public IEnumerator SystemUpdate()
    {
        while (currentTime >= 0)
        {
            _gamePresenter.SystemUpdate();
            yield return new WaitForSeconds(1f);
        }
        yield break;
    }
    private float GetHour()
    {
        return Mathf.Floor(hour);
    }
    private float GetMinute()
    {
        return Mathf.Floor(minute);
    }
    private float GetSecond()
    {
        return Mathf.Floor(second);
    }
}