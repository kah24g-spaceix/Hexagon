using System.Collections;
using Unity.VisualScripting;

public interface IGamePresenter
{
    //void BindView(IGameView pView);
    void SystemUpdate();
    string GetDay();
    string GetMoney();
    void MoneyPerSecond();
    void OnDaySkipButton();
    void OnNextDayButton();
    void OnRestartDayButton();
    void DoTodayResult();
    void OnExchangeTechPointButton(int value);

    void DoLoadGame(bool useDateData);
    void DoSaveGame(bool useDateData);

    void Resume();
    void Pause();
}