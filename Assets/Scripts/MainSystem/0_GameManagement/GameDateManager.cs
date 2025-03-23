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

    private float time;
    public float currentTime;

    private const float gameDayInHours = 24f;

    private float timeScale;

    private bool isFirst;

    private void Awake()
    {
        _gameView = GetComponent<IGameView>();
        _gamePresenter = GetComponent<IGamePresenter>();
        _gameModel = GetComponent<IGameModel>();
        playerDayModel = _gameModel.GetPlayerDayModel();
        time = playerDayModel.DayLength;
        isFirst = true;
        timeScale = gameDayInHours / time;

    }

    public IEnumerator DayCycle()
    {
        playerDayModel = _gameModel.GetPlayerDayModel();
        if (GameStateManager.Instance.IsLoad && isFirst)
        {
            currentTime = playerDayModel.CurrentTime;
            isFirst = false;
        }
        else
            currentTime = playerDayModel.DayLength;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            float totalGameHours = (playerDayModel.DayLength - currentTime) * timeScale;
            hour = Mathf.Floor(totalGameHours) % 24;
            minute = Mathf.Floor(totalGameHours * 60 % 60);

            _gameView.ClockUpdate(GetHour(), GetMinute());
            _gameModel.DoDayResult(new 
            (
                playerDayModel.Day, 
                currentTime,
                playerDayModel.LastDay, 
                playerDayModel.DayLength
            ));
            
            playerDayModel = _gameModel.GetPlayerDayModel();
            yield return null;
        }

        currentTime = 0;
        _gameModel.SetTimeScale(1);
        _gameModel.DoDayResult(new 
        (
            playerDayModel.Day, 
            currentTime, 
            playerDayModel.LastDay, 
            playerDayModel.DayLength
        ));
        playerDayModel = _gameModel.GetPlayerDayModel();
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


    public void SkipTime(float skipHours)
    {
        // 스킵된 시간만큼 바로 계산
        float skipTime = skipHours / timeScale; // 게임 내 시간 -> 실제 경과 시간 변환
        currentTime -= skipTime;
        hour += skipHours;

        if (hour >= 24f)
        {
            hour -= 24f;
        }

        // 필요한 로직을 직접 업데이트
        UpdateSystem(skipTime);

        // UI 업데이트
        _gameView.ClockUpdate(GetHour(), GetMinute());
        _gameView.ViewUpdate();


        // 스킵 후 처리
        if (currentTime <= 0)
        {
            currentTime = 0;
            _gameModel.SetTimeScale(1);
            _gamePresenter.DoTodayResult();
            _gameView.ShowUI(_gameView.ToDayResult);
        }
    }

    private void UpdateSystem(float skipTime)
    {
        _gamePresenter.SystemSkipUpdate(skipTime);
    }

    public IEnumerator SystemUpdate()
    {
        while (currentTime >= 0)
        {
            _gamePresenter.SystemUpdate();
            _gameModel.AddProduct();
            _gameModel.AddContractProduct();
            yield return new WaitForSeconds(1f);
        }
        yield break;
    }

    private float GetHour() => Mathf.Floor(hour);

    private float GetMinute() => Mathf.Floor(minute);
}