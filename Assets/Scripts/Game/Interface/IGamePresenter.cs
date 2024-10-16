using System.Collections;

public interface IGamePresenter
{
    //void BindView(IGameView pView);
    string GetDay();
    string GetMoney();
    IEnumerator MoneyPerSecond();
    void OnBuyCommodityButton();
    void OnExchangeTechPointButton(int value);
    void NextDay();
    void LoadGame();
    void SaveGame();
}