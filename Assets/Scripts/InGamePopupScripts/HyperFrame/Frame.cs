using UnityEngine.UI;
using UnityEngine;
public class Frame : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button informationButton;
    private IGameModel gameModel;
    private FrameInformation frameInfo;
    public int ID { get; set; }
    public int[] Costs { get; set; }
    readonly string isNotImage = "Sprites/DebugImage/IsNotImage";
    private void Awake()
    {
        gameModel = GameObject.Find("GameManager").GetComponent<IGameModel>();
        informationButton.onClick.AddListener(ShowInformation);
    }
    private void Start()
    {
        frameInfo = HyperFrameGroup.Instance.HyperFrameValue.GetComponent<FrameInformation>();
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
        AudioManager.Instance.PlaySFX("Select");
        HyperFrameModel currentHyperFrameModel = HyperFrameGroup.Instance.Model;
        HyperFrameGroup.Instance.HyperFrameValue.SetActive(true);
        FrameInfoBind();
        frameInfo.ID = ID;        
        HyperFrameGroup.Instance.UpdateAllHyperFrameUI(currentHyperFrameModel);
    }

}
