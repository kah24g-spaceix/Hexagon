using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrameInformation : MonoBehaviour
{
    
    public Image image;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI revenueText;
    [SerializeField] private Button creationButton;
    [SerializeField] private Button sellButton;
    
    public int ID { get; set; }
    private IGameModel gameModel;

    private void Awake()
    {
        gameModel = GameObject.Find("GameManager").GetComponent<IGameModel>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
        creationButton.onClick.AddListener(Creation);
        sellButton.onClick.AddListener(Sell);
    }
    private void Update()
    {
        ChangeInformation();
    }
    public void ChangeInformation()
    {
        HyperFrameModel currentModel = HyperFrameGroup.Instance.Model;
        image.sprite = Resources.Load<Sprite>($"Sprites/HyperFrames/HyperFrame_{ID}");
        countText.SetText($"x{currentModel.Counts[ID]}");
        descriptionText.SetText($"{LocalizationManager.Instance.GetLocalizedText(currentModel.Descriptions[ID])}");
        revenueText.SetText($"{currentModel.Prices[ID]:N0} $");
    }
    private void Creation()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
        HyperFrameModel currentModel = HyperFrameGroup.Instance.Model;
        PlayerMaterialModel playerMaterialModel = gameModel.GetPlayerMaterialModel();
        if (IsBuy(playerMaterialModel))
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFXType.Error);
            return;
        }
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Creation);
        currentModel.Counts[ID]++;
        gameModel.DoMaterialResult(new
        (
            playerMaterialModel.Alloy - currentModel.MaterialsCosts[ID][0],
            playerMaterialModel.Microchip - currentModel.MaterialsCosts[ID][1],
            playerMaterialModel.CarbonFiber - currentModel.MaterialsCosts[ID][2],
            playerMaterialModel.ConductiveFiber - currentModel.MaterialsCosts[ID][3],
            playerMaterialModel.Pump - currentModel.MaterialsCosts[ID][4],
            playerMaterialModel.RubberTube - currentModel.MaterialsCosts[ID][5]
        ));
        gameModel.DoHyperFrameResult(new 
        (
            currentModel.Counts[0],
            currentModel.Counts[1],
            currentModel.Counts[2],
            currentModel.Counts[3],
            currentModel.Counts[4],
            currentModel.Counts[5],
            currentModel.Counts[6]
        ));
        HyperFrameGroup.Instance.UpdateAllHyperFrameUI(currentModel);
    }
    private bool IsBuy(PlayerMaterialModel materialModel)
    {
        HyperFrameModel model = HyperFrameGroup.Instance.Model;
        return
            materialModel.Alloy < model.MaterialsCosts[ID][0] ||
            materialModel.Microchip < model.MaterialsCosts[ID][1] ||
            materialModel.CarbonFiber < model.MaterialsCosts[ID][2] ||
            materialModel.ConductiveFiber < model.MaterialsCosts[ID][3] ||
            materialModel.Pump < model.MaterialsCosts[ID][4] ||
            materialModel.RubberTube < model.MaterialsCosts[ID][5];
    }
    private void Sell()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
        HyperFrameModel currentModel = HyperFrameGroup.Instance.Model;
        PlayerSystemModel playerSystemModel = gameModel.GetPlayerSystemModel();
        if (currentModel.Counts[ID] <= 0)
            Creation();
        if (currentModel.Counts[ID] <= 0) return;

        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Sell);
        currentModel.Counts[ID]--;
        gameModel.DoSystemResult(new
        (
            playerSystemModel.Money + currentModel.Prices[ID],
            playerSystemModel.Employees,
            playerSystemModel.Resistance,
            playerSystemModel.CommunityOpinionValue
        ));

        gameModel.DoHyperFrameResult(new 
        (
            currentModel.Counts[0],
            currentModel.Counts[1],
            currentModel.Counts[2],
            currentModel.Counts[3],
            currentModel.Counts[4],
            currentModel.Counts[5],
            currentModel.Counts[6]
        ));
        HyperFrameGroup.Instance.UpdateAllHyperFrameUI(currentModel);
    }
}