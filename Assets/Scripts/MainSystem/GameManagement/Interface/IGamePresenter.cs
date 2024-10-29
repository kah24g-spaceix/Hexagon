using System.Collections;

public interface IGamePresenter
{
    //void BindView(IGameView pView);
    string GetDay();
    string GetMoney();
    IEnumerator MoneyPerSecond();
    void OnExchangeTechPointButton(int value);
    void OnNextDayButton();
    void OnRestartDayButton();
    void DoTodayResult();
    void LoadGame();
    void SaveGame();
}