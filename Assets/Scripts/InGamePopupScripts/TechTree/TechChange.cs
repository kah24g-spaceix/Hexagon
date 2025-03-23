using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TechChange : MonoBehaviour
{
    public TextMeshProUGUI techPoint;
    public TextMeshProUGUI price;
    public Button plus;
    public Button minus;
    public Slider slider;
    public Button Change;

    private void Start()
    {
        slider.minValue = 1;
        plus.onClick.AddListener(Plus);
        minus.onClick.AddListener(Minus);
    }
    private void Plus()
    {
        if (slider.value == slider.maxValue) return;
        slider.value += 1;
    }
    private void Minus()
    {
        if (slider.value == slider.minValue) return;
        slider.value -= 1;
    }
}
