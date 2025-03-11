using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] private Canvas canvas;

    [SerializeField] private float minX = 100f; // Set your min X limit
    [SerializeField] private float maxX = 100f;  // Set your max X limit

    private RectTransform rectTransform;
    private float startX;
    private float minXDeviation, maxXDeviation;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startX = rectTransform.anchoredPosition.x;

        // Set dynamic limits
        minXDeviation = startX - minX;
        maxXDeviation = startX + maxX;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition = rectTransform.anchoredPosition;
        newPosition.x += eventData.delta.x / canvas.scaleFactor; // Only update X position
        newPosition.x = Mathf.Clamp(newPosition.x, minXDeviation, maxXDeviation);  // Clamp within limits

        rectTransform.anchoredPosition = newPosition;
    }
}
