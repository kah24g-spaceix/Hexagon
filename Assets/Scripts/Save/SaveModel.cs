using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using static PlayerTechModel;

public class SaveModel : MonoBehaviour, IPlayerModel
{
    private PlayerSaveData _playerSaveData;
    private PlayerSaveModel _playerISaveModel;
    private PlayerTechModel _playerTechModel;

    public PlayerSaveModel GetPlayerSaveModel()
    {
        return _playerISaveModel;
    }

    public PlayerTechModel GetPlayerTechModel()
    {
        return _playerTechModel;
    }

    public void DoPlayerInfoResult()
    {
        throw new NotImplementedException();
    }

    public void DoTechResult()
    {
        throw new NotImplementedException();
    }
    public void SaveGame()
    {
        PlayerSaveData saveData = _playerSaveData;
        string json = JsonConvert.SerializeObject(saveData);
        Debug.Log(json);
        PlayerPrefs.SetString("Save", json);
    }
    public bool LoadGame()
    {
        if (!PlayerPrefs.HasKey("Save"))
            return false;
        string value = PlayerPrefs.GetString("Save");
        PlayerSaveData saveData = JsonConvert.DeserializeObject<PlayerSaveData>(value);
        int money = saveData.Money;
        int commodity = saveData.Commodity;
        int employees = saveData.Employees;
        int resistance = saveData.Resistance;
        int techPoint = saveData.TechPoint;
        int day = saveData.Day;
        int revenueValue = saveData.RevenueValue;
        int communityOpinion = saveData.CommunityOpinion;
        int transportationTimeValue = saveData.TransportationTimeValue;
        int maxEmployees = saveData.MaxEmployee;
        int[] techLevels = saveData.TechLevels;
        _playerSaveData = new PlayerSaveData(
            money,
            commodity,
            employees,
            resistance,
            techPoint,
            day,
            revenueValue,
            communityOpinion,
            transportationTimeValue,
            maxEmployees,
            techLevels);
        return true;
    }
}