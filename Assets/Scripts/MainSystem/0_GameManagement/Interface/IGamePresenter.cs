using System.Collections;

public interface IGamePresenter
{
    //void BindView(IGameView pView);
    void SystemUpdate();
    string GetDay();
    string GetMoney();
    void MoneyPerSecond();
    void OnNextDayButton();
    void OnRestartDayButton();
    void DoTodayResult();
    void OnExchangeTechPointButton(int value);

    void DoLoadGame();
    void DoSaveGame();
}