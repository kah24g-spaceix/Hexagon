using UnityEngine;

public class UIKeepInScreen : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Rect rect = rectTransform.rect;

        Vector2 leftBottom = transform.TransformPoint(rect.min);
        Vector2 rightTop = transform.TransformPoint(rect.max);
        Vector2 UISize = rightTop - leftBottom;

        rightTop = new Vector2(Screen.width, Screen.height) - UISize;

        float x = Mathf.Clamp(leftBottom.x, 0, rightTop.x);
        float y = Mathf.Clamp(leftBottom.y, 0, rightTop.y);

        Vector2 offset = (Vector2)transform.position - leftBottom;
        transform.position = new Vector2(x, y) + offset;

    }
}