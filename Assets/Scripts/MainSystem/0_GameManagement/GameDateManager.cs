using System.Collections;
using UnityEngine;

public class GameDateManager : MonoBehaviour
{
    IGameView _gameView;
    IGamePresenter _gamePresenter;
    IGameModel _gameModel;
    PlayerDayModel playerDayModel;

    private float currentTime;
    private const float gameDayInHours = 24f;
    private float timeScale;
    private float hour;
    private float minute;

    private void Awake()
    {
        _gameView = GetComponent<IGameView>();
        _gamePresenter = GetComponent<IGamePresenter>();
        _gameModel = GetComponent<IGameModel>();
        playerDayModel = _gameModel.GetPlayerDayModel();

        currentTime = playerDayModel.DayLength;
        timeScale = gameDayInHours / currentTime;
    }
 public IEnumerator DayCycle()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            hour = (hour + Time.deltaTime * timeScale) % 24f;
            minute = (hour - Mathf.Floor(hour)) * 60f;

            _gameView.ClockUpdate(Mathf.Floor(hour), Mathf.Floor(minute));

            yield return null;
        }

        _gameModel.SetTimeScale(1);
        _gamePresenter.Pause();

        if (playerDayModel.Day >= playerDayModel.LastDay)
        {
            _gameView.ShowUI(_gameView.LastDayResult);
        }
        else
        {
            _gamePresenter.DoTodayResult();
            _gameView.ShowUI(_gameView.ToDayResult);
        }
    }

    public IEnumerator SystemUpdate()
    {
        while (currentTime > 0)
        {
            _gamePresenter.SystemUpdate();
            _gameModel.AddProduct();
            _gameModel.AddContractProduct();
            yield return new WaitForSeconds(1f);
        }
    }
}
