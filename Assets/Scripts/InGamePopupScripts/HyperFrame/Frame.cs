using UnityEngine.UI;
using UnityEngine;
public class Frame : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button informationButton;
    private FrameInformation frameInfo;
    public int ID { get; set; }
    public int[] Costs { get; set; }
    readonly string isNotImage = "Sprites/DebugImage/IsNotImage";
    private void Awake()
    {
        informationButton.onClick.AddListener(ShowInformation);
    }
    public void Bind(HyperFrameModel model)
    {
        if (model == null || ID < 0 || ID >= model.Names.Length) return;

        string spritePath = $"Sprites/HyperFrames/HyperFrame_{ID}";
        Sprite loadedSprite = Resources.Load<Sprite>(spritePath) ?? Resources.Load<Sprite>(isNotImage);
        image.sprite = loadedSprite;
    }
    public void FrameInfoBind()
    {
        for (int i = 0; i < HyperFrameGroup.Instance.CostList.Count; i++)
        {
            HyperFrameGroup.Instance.CostList[i].image.sprite = Resources.Load<Sprite>($"Sprites/Products/Product_{i}") ?? Resources.Load<Sprite>(isNotImage);
            HyperFrameGroup.Instance.CostList[i].valueText.SetText($"{Costs[i]}");
            Debug.Log($"ID:{ID} | index:{i} | {Costs[i]}");
        }
    }
    public void ShowInformation()
    {
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
        HyperFrameModel currentHyperFrameModel = HyperFrameGroup.Instance.Model;
        FrameInfoBind();
        frameInfo.ID = ID;        
        HyperFrameGroup.Instance.UpdateAllHyperFrameUI(currentHyperFrameModel);
    }

}
