using UnityEngine.UI;
using UnityEngine;
public class Frame : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button informationButton;
    private IGameModel gameModel;
    public int ID { get; set; }

    private void Awake()
    {
        gameModel = GameObject.Find("GameManager").GetComponent<IGameModel>();
        informationButton.onClick.AddListener(ShowInformation);
    }
    public void Bind(HyperFrameModel model)
    {
        if (model == null || ID < 0 || ID >= model.Names.Length) return;
        
        string isNotImage = "Sprites/DebugImage/IsNotImage";
        string spritePath = $"Sprites/HyperFrames/HyperFrame_{ID}";
        
        Sprite loadedSprite = Resources.Load<Sprite>(spritePath) ?? Resources.Load<Sprite>(isNotImage);
        image.sprite = loadedSprite;
        
        for (int i = 0; i < HyperFrameGroup.Instance.CostList.Count; i++)
        {
            HyperFrameGroup.Instance.CostList[i].image.sprite = Resources.Load<Sprite>($"Sprites/Products/Product_{i}") ?? Resources.Load<Sprite>(isNotImage);
            HyperFrameGroup.Instance.CostList[i].valueText.SetText($"{model.MaterialsCosts[ID][i]}");
        }
    }
    public void ShowInformation()
    {
        HyperFrameModel currentHyperFrameModel = HyperFrameGroup.Instance.Model;
        HyperFrameGroup.Instance.FrameInfo.image.sprite = Resources.Load<Sprite>($"Sprites/HyperFrames/HyperFrame_{ID}");
        HyperFrameGroup.Instance.UpdateAllHyperFrameUI(currentHyperFrameModel);
    }
}
