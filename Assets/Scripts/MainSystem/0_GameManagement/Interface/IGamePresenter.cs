using System.Collections;
using Unity.VisualScripting;

public interface IGamePresenter
{
    //void BindView(IGameView pView);
    string GetDay();
    string GetMoney();
    void OnDaySkipButton();
    void SystemUpdate();
    void SystemSkipUpdate(float skipTime);
    void OnNextDayButton();
    void OnRestartDayButton();
    void DoTodayResult();
    void OnExchangeTechPointButton(int value);

    void DoLoadGame(bool useDateData);
    void DoSaveGame(bool useDateData);

    void Resume();
    void Pause();
}