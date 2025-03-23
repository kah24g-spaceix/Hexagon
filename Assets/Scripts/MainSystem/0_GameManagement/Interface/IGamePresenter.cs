using System.Collections;

public interface IGamePresenter
{
    //void BindView(IGameView pView);
    string GetDay();
    string GetMoney();
    string GetTechPoint();
    string GetR_Value();
    string GetE_Value();
    void OnDaySkipButton();
    
    void SystemUpdate();
    void SystemSkipUpdate(float skipTime);

    void OnNextDayButton();
    void OnRestartDayButton();

    void DoTodayResult();
    void DoLastDayResult();

    void OnChangeTechPoint(float value);
    float MaxTechChange(float maxValue);
    string GetTechPointPrice(float value);

    void DoLoadGame(bool useDateData);
    void DoSaveGame(bool useDateData);

    void Resume();
    void Pause();
}