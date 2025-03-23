using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GamblingManager : MonoBehaviour
{
    public Button blackChoiceButton, colorChoiceButton;
    public Button cardButton1, cardButton2;
    public Button noneButton, restartButton;
    public TextMeshProUGUI resultText;

    public GameObject card1, card2;
    public Sprite blackCard, colorCard;
    public Sprite cardBackSprite;

    private string selectedColor = "";
    private string card1Color, card2Color; // 카드 색상 정보

    void Start()
    {
        blackChoiceButton.onClick.AddListener(() => ChooseColor("black"));
        colorChoiceButton.onClick.AddListener(() => ChooseColor("color"));

        cardButton1.onClick.AddListener(() => SelectCard(card1, card1Color));
        cardButton2.onClick.AddListener(() => SelectCard(card2, card2Color));
        noneButton.onClick.AddListener(CheckNone);
        restartButton.onClick.AddListener(ResetGame);

        ResetGame();
    }

    void ResetGame()
    {
        selectedColor = "";
        resultText.text = "흑백 또는 컬러 중 하나를 선택하세요.";

        cardButton1.gameObject.SetActive(false);
        cardButton2.gameObject.SetActive(false);
        noneButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);

        card1.GetComponent<Image>().sprite = cardBackSprite;
        card2.GetComponent<Image>().sprite = cardBackSprite;

        cardButton1.interactable = true;
        cardButton2.interactable = true;
        noneButton.interactable = true;
    }

    void ChooseColor(string color)
    {
        selectedColor = color;
        resultText.text = $"{(color == "black" ? "흑백" : "컬러")} 카드를 선택하세요.";

        // 랜덤하게 카드 색상 결정
        bool isCard1Black = Random.Range(0, 2) == 0;
        card1Color = isCard1Black ? "black" : "color";
        card2Color = isCard1Black ? "color" : "black";

        // 버튼 활성화
        cardButton1.gameObject.SetActive(true);
        cardButton2.gameObject.SetActive(true);
        noneButton.gameObject.SetActive(true);
    }

    void SelectCard(GameObject cardObj, string cardColor)
    {
        Image cardImage = cardObj.GetComponent<Image>();
        Sprite selectedSprite = (cardColor == "black") ? blackCard : colorCard;

        // 카드 뒤집기 애니메이션
        cardObj.transform.DOScaleX(0, 0.3f).OnComplete(() =>
        {
            cardImage.sprite = selectedSprite;
            cardObj.transform.DOScaleX(1, 0.3f);
        });

        // 승패 판정
        if (cardColor == selectedColor)
        {
            resultText.text = "같은 색을 선택하여 승리!";
        }
        else
        {
            resultText.text = "다른 색을 선택하여 패배!";
        }

        EndGame();
    }

    void CheckNone()
    {
        // 선택한 색이 있는지 검사
        if (card1Color == selectedColor || card2Color == selectedColor)
        {
            resultText.text = "선택한 색이 존재하여 패배!";
        }
        else
        {
            resultText.text = "선택한 색이 없어서 승리!";
        }

        EndGame();
    }

    void EndGame()
    {
        cardButton1.interactable = false;
        cardButton2.interactable = false;
        noneButton.interactable = false;
        restartButton.gameObject.SetActive(true);
    }
}
