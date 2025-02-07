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
        time = playerDayModel.DayLength;
        timeScale = gameDayInHours / time;
    }

    public IEnumerator DayCycle()
    {
        currentTime = playerDayModel.DayLength;
        hour = 0f;
        minute = 0f;
        second = 0f;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            hour += Time.deltaTime * timeScale;

            minute = (hour - GetHour()) * 60f;

            // UI 업데이트
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

    // 스킵 기능 추가
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
        // 예: 리소스, 적 상태 등을 스킵 시간만큼 업데이트
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
