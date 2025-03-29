using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GamblingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button blackChoiceButton, colorChoiceButton;
    [SerializeField] private Button noneButton, restartButton, BetButton;
    [SerializeField] private GameObject BetPopup;

    [SerializeField] private GameObject card1, card2;
    Button cardButton1, cardButton2;
    CanvasGroup cardCanvas1, cardCanvas2;
    
    [SerializeField] private Sprite blackCard, colorCard;
    [SerializeField] private Sprite cardBackSprite;

    private Coroutine coroutine1, coroutine2;
    private Image cardImage;
    private long betValue;
    private int multiple;
    private BetManager betManager;
    private string selectedColor = "";
    private string card1Color, card2Color;
    private IGameModel gameModel;
    private PlayerSystemModel playerSystemModel;
    void Awake()
    {
        gameModel = GameObject.Find("GameManager").GetComponent<IGameModel>();
        cardButton1 = card1.GetComponent<Button>();
        cardButton2 = card2.GetComponent<Button>();
        cardCanvas1 = card1.GetComponent<CanvasGroup>();
        cardCanvas2 = card2.GetComponent<CanvasGroup>();
        betManager = GetComponent<BetManager>();

    }
    void Start()
    {
        playerSystemModel = gameModel.GetPlayerSystemModel();
        blackChoiceButton.onClick.AddListener(() => ChooseColor("black"));
        colorChoiceButton.onClick.AddListener(() => ChooseColor("color"));

        cardButton1.onClick.AddListener(() => SelectCard(card1, card1Color));
        cardButton2.onClick.AddListener(() => SelectCard(card2, card2Color));
        noneButton.onClick.AddListener(CheckNone);
        restartButton.onClick.AddListener(ResetGame);

        for (int i = 0; i < betManager.amountButtons.Count; i++)
        {
            int num = 1000;
            betManager.amountButtons[i].onClick.AddListener(() => Bet(num));
            num *= 10;
        }
        ResetGame();
    }
    private void Bet(int num)
    {
        betValue += num;
        betManager.currentBetText.SetText($"$ {betValue}");
    }
    void ResetGame()
    {
        selectedColor = "";
        if (coroutine1 != null) StopCoroutine(coroutine1);
        if (coroutine2 != null) StopCoroutine(coroutine2);
        resultText.SetText("흑백 또는 컬러 중 하나를 선택하세요.");

        cardButton1.gameObject.SetActive(false);
        cardButton2.gameObject.SetActive(false);
        noneButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        card1.GetComponent<Image>().sprite = cardBackSprite;
        card2.GetComponent<Image>().sprite = cardBackSprite;

        Block(true);
        noneButton.interactable = true;
        blackChoiceButton.interactable = true;
        colorChoiceButton.interactable = true;
    }

    void ChooseColor(string color)
    {
        selectedColor = color;

        long money = playerSystemModel.Money - betValue;
        gameModel.DoSystemResult(new
        (
            money, 
            playerSystemModel.Employees,
            playerSystemModel.Resistance,
            playerSystemModel.CommunityOpinionValue
        ));
        playerSystemModel = gameModel.GetPlayerSystemModel();
        
        resultText.SetText($"{(color == "black" ? "흑백" : "컬러")} 카드를 선택하세요.");

        BetButton.interactable = false;
        blackChoiceButton.interactable = false;
        colorChoiceButton.interactable = false;
        
        bool isCard1Black = Random.Range(0, 2) == 0;
        bool isCard2Black = Random.Range(0, 2) == 0;
        card1Color = isCard1Black ? "black" : "color";
        card2Color = isCard2Black ? "black" : "color";

        cardButton1.gameObject.SetActive(true);
        cardButton2.gameObject.SetActive(true);
        noneButton.gameObject.SetActive(true);

        card1.transform.localScale = Vector3.zero;
        card2.transform.localScale = Vector3.zero;
        card1.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
        card2.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }

    void SelectCard(GameObject cardObj, string cardColor)
    {
        cardImage = cardObj.GetComponent<Image>();
        Sprite selectedSprite = (cardColor == "black") ? blackCard : colorCard;

        // 카드 뒤집기 애니메이션
        cardObj.transform.DOScaleX(0, 0.3f).OnComplete(() =>
        {
            cardImage.sprite = selectedSprite;
            cardObj.transform.DOScaleX(1, 0.3f);
        });

        // 승패 판정
        SameColor(cardColor == selectedColor);

        if (cardObj != card1)
            coroutine1 = StartCoroutine(CardEffect(card1, card1Color));
        else
            coroutine2 = StartCoroutine(CardEffect(card2, card2Color));

        EndGame();
    }

    void CheckNone()
    {
        NoneColor(card1Color == selectedColor || card2Color == selectedColor);
        coroutine1 = StartCoroutine(CardEffect(card1, card1Color));
        coroutine2 = StartCoroutine(CardEffect(card2, card2Color));
        EndGame();
    }

    void EndGame()
    {
        Block(false);
        noneButton.interactable = false;
        restartButton.gameObject.SetActive(true);
    }

    private void Block(bool isAcive)
    {
        if (cardCanvas1 != null) cardCanvas1.blocksRaycasts = isAcive;
        if (cardCanvas2 != null) cardCanvas2.blocksRaycasts = isAcive;
    }
    IEnumerator CardEffect(GameObject cardObj, string cardColor)
    {
        yield return new WaitForSeconds(0.3f);
        Sprite selectedSprite = (cardColor == "black") ? blackCard : colorCard;
        cardObj.transform.DOScaleX(0, 0.3f).OnComplete(() =>
        {
            cardObj.GetComponent<Image>().sprite = selectedSprite;
            cardObj.transform.DOScaleX(1, 0.3f);
        });
    }
    private void SameColor(bool IsSame)
    {
        if (IsSame)
        {
            multiple = 2;
            resultText.SetText("같은 색을 선택하여 승리!");
        }
        else
        {
            multiple = 0;
            resultText.SetText("다른 색을 선택하여 패배!");
            betManager.currentBetText.SetText("모두 잃었습니다...");
        }
        BetResultValue();
        betManager.multipleText.SetText($"x{multiple}");
    }
    private void NoneColor(bool IsExist)
    {
        if (!IsExist)
        {
            multiple = 5;
            resultText.SetText("선택한 색이 없어서 승리!");
        }
        else
        {
            multiple = 0;
            resultText.SetText("선택한 색이 존재하여 패배!");
            betManager.currentBetText.SetText($"모두 잃었습니다...");
        }
        BetResultValue();
        betManager.multipleText.SetText($"x{multiple}");
    }
    private void BetResultValue()
    {
        long money = playerSystemModel.Money + (betValue * multiple);
        gameModel.DoSystemResult(new
        (
            money, 
            playerSystemModel.Employees,
            playerSystemModel.Resistance,
            playerSystemModel.CommunityOpinionValue
        ));
        playerSystemModel = gameModel.GetPlayerSystemModel();
    }
}
