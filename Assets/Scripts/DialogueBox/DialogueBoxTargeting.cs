using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private CanvasGroup tailImage;
    public Transform target { get; set; }
    public TextMeshProUGUI text { get; set; }
    private RectTransform boxContainer;
    private Camera mainCamera;
    private Vector3 screenPos;

    // 오프셋을 스프라이트 크기 기준으로 설정
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

        // 스프라이트의 월드 좌표를 화면 좌표로 변환
        screenPos = mainCamera.WorldToScreenPoint(target.position);

        bool isOffScreen = IsTargetOffScreen(screenPos);

        // 스프라이트가 화면 밖에 있으면 다이얼로그가 보이지 않게 함
        tailImage.alpha = isOffScreen ? 0 : 1;

        // 화면 안에 다이얼로그가 들어오도록 화면 좌표 클램프
        ClampScreenPosition(ref screenPos);

        // 스프라이트 크기를 기준으로 오프셋을 계산하여 UI 다이얼로그 위치 갱신
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
        // SpriteRenderer를 통해 스프라이트 크기 가져오기
        SpriteRenderer spriteRenderer = target.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Vector2 spriteSize = spriteRenderer.bounds.size;  // 스프라이트 크기 (가로, 세로)
            
            float targetHeight = spriteSize.y;  // 스프라이트 높이
            Debug.Log(targetHeight);
            // 화면 좌표를 로컬 좌표로 변환
            Vector2 anchoredPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                boxContainer.parent as RectTransform, screenPosition, null, out anchoredPos);

            anchoredPos.y += targetHeight / 2 + offsetY;
            // 다이얼로그의 위치 업데이트
            boxContainer.anchoredPosition = anchoredPos;
        }
    }
}
