using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragHandle : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler
{
    private GameObject targetObject;
    private RectTransform rectTransform;
    private Vector2 offset;

    [SerializeField] private Button closeButton;

    void Awake()
    {
        targetObject = transform.parent.gameObject;
        rectTransform = targetObject.GetComponent<RectTransform>();
    }

    void Start()
    {
        closeButton.onClick.AddListener(Close);
    }
    
    private void Update()
    {
        Rect rect = rectTransform.rect;

        Vector2 leftBottom = targetObject.transform.TransformPoint(rect.min);
        Vector2 rightTop = targetObject.transform.TransformPoint(rect.max);
        Vector2 UISize = rightTop - leftBottom;

        rightTop = new Vector2(Screen.width, Screen.height) - UISize;

        float x = Mathf.Clamp(leftBottom.x, 0, rightTop.x);
        float y = Mathf.Clamp(leftBottom.y, 0, rightTop.y);

        Vector2 offset = (Vector2)targetObject.transform.position - leftBottom;
        targetObject.transform.position = new Vector2(x, y) + offset;

    }

    void Close()
    {
        targetObject.SetActive(false);
        AudioManager.Instance.PlaySFX(AudioManager.SFXType.Select);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = (Vector2)targetObject.transform.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        targetObject.transform.position = eventData.position + offset;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        targetObject.transform.SetAsLastSibling(); // 클릭한 UI를 맨 앞으로 가져오기
    }
}
