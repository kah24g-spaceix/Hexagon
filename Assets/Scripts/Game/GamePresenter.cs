using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerTechModel;

public class GamePresenter : MonoBehaviour, IGamePresenter
{
    private IGameModel _model;

    private PlayerSaveModel _playerSaveModel;
    private PlayerTechModel _playerTechModel;
    private void Awake()
    {
        _model = GetComponent<IGameModel>();
    }
    private void Start()
    {
        ReloadData();
    }
    public void MoneyPerSecond()
    {
        IEnumerator MonsterSelectRoutine()
        {
            yield return new WaitForSeconds(1f);
        }
    }
    private void ReloadData()
    {
        _playerSaveModel = _model.GetPlayerSaveModel();
        _playerTechModel = _model.GetPlayerTechModel();
    }
    public void OnBuyCommodityButton()
    {
        _model.BuyCommodity();
    }
    public void OnBuyTechPointButton()
    {
        _model.BuyTechPoint();
    }

    public void NextDay()
    {
        _model.Motivation();

        SaveGame();
    }
    public void LoadGame()
    {
        _model.LoadGame();
        ReloadData();
    }
    public void SaveGame()
    {
        _model.SaveGame();
    }
}