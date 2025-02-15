using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HyperFrame : MonoBehaviour, IView<HyperFrameModel>
{
    [Header("Image")]
    [SerializeField] private Image image;
    [SerializeField] private Image regionImage;

    private IGameModel playerModel;
    public int ID {get; set;}

    private void Awake()
    {
        playerModel = GameObject.Find("GameManager").GetComponent<IGameModel>();
    }
    private void Update()
    {
        
    }
    public void Bind(HyperFrameModel model)
    {
        
    }
}