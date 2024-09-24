using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGamePresenter
{
    //void BindView(IGameView pView);
    void MoneyPerSecond();
    void OnBuyCommodityButton();
    void OnBuyTechPointButton();
    void NextDay();
    void LoadGame();
    void SaveGame();
}