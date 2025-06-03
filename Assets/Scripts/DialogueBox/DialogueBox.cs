using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private CanvasGroup tailImage;
    public Transform target { get; set; }
    public TextMeshProUGUI text;
    private RectTransform boxContainer;
    private Camera mainCamera;
    private Vector3 screenPos;
    private float offsetY = 50f;


    private void Awake()
    {
        boxContainer = GetComponent<RectTransform>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (target == null) return;

        screenPos = mainCamera.WorldToScreenPoint(target.position);

        bool isOffScreen = IsTargetOffScreen(screenPos);

        tailImage.alpha = isOffScreen ? 0 : 1;

        ClampScreenPosition(ref screenPos);

        UpdateBoxPosition(screenPos);
    }

    private bool IsTargetOffScreen(Vector3 position)
    {
        return position.x < 0 || position.y < 0 || position.x > Screen.width || position.y > Screen.height;
    }

    private void ClampScreenPosition(ref Vector3 position)
    {
        Vector2 boxSize = boxContainer.sizeDelta;
        float halfWidth = boxSize.x / 2;
        float halfHeight = boxSize.y / 2;

        position.x = Mathf.Clamp(position.x, halfWidth, Screen.width - halfWidth);
        position.y = Mathf.Clamp(position.y + offsetY, halfHeight, Screen.height - halfHeight);
    }

    private void UpdateBoxPosition(Vector3 screenPosition)
    {
        SpriteRenderer spriteRenderer = target.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Vector2 spriteSize = spriteRenderer.bounds.size;
            
            float targetHeight = spriteSize.y;

            Vector2 anchoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                boxContainer.parent as RectTransform, screenPosition, null, out anchoredPos);

            anchoredPos.y += targetHeight / 2 + offsetY;
            boxContainer.anchoredPosition = anchoredPos;
        }
    }
}
