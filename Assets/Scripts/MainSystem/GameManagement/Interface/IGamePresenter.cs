using System.Collections;

public interface IGamePresenter
{
    //void BindView(IGameView pView);
    IEnumerator DataChangeUpdate();
    string GetDay();
    string GetMoney();
    void MoneyPerSecond();
    void OnNextDayButton();
    void OnRestartDayButton();
    void DoTodayResult();
    void OnExchangeTechPointButton(int value);

    void DoSell();
    void DoUpdatePlant();
    string[] GetPlantText();

    void DoLoadGame();
    void DoSaveGame();
}