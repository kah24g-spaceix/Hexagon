using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlaytimeManager : MonoBehaviour
{
    [SerializeField] private GameObject textHolder;
    [SerializeField] private GameObject upHolder;
    [SerializeField] private GameObject downHolder;
    
    private TextMeshProUGUI[] numberTexts;
    private Button[] upButtons;
    private Button[] downButtons;
    private int playtimeValue;

    private void Start()
    {
        // 버튼에 동적으로 AddListener 연결
        for (int i = 0; i < 3; i++)
        {
            int index = i; // 로컬 변수로 캡처
            upButtons[i].onClick.AddListener(() => ChangeNumber(index, 1));
            downButtons[i].onClick.AddListener(() => ChangeNumber(index, -1));
        }

        UpdateDisplay();
    }

    // 특정 자릿수의 숫자를 변경
    private void ChangeNumber(int index, int delta)
    {
        int[] digits = GetDigits(playtimeValue);
        digits[index] = Mathf.Clamp(digits[index] + delta, 0, 9); // 0~9로 제한
        playtimeValue = CombineDigits(digits);

        UpdateDisplay();
    }

    // 화면에 숫자 갱신
    private void UpdateDisplay()
    {
        int[] digits = GetDigits(playtimeValue);
        for (int i = 0; i < 3; i++)
        {
            numberTexts[i].text = digits[i].ToString();
        }
    }

    // playtimeValue를 개별 숫자로 분리
    private int[] GetDigits(int value)
    {
        int[] digits = new int[3];
        for (int i = 0; i < 3; i++)
        {
            digits[2 - i] = value % 10; // 뒤에서부터 자릿수 추출
            value /= 10;
        }
        return digits;
    }

    // 자릿수를 합쳐 playtimeValue로 변환
    private int CombineDigits(int[] digits)
    {
        int value = 0;
        for (int i = 0; i < 3; i++)
        {
            value = value * 10 + digits[i];
        }
        return value;
    }
}
