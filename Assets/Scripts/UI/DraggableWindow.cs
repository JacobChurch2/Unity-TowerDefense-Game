using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] private Canvas canvas;

    [SerializeField] private float minY = 100f; // Set your min Y limit
    [SerializeField] private float maxY = 100f;  // Set your max Y limit
    
    private RectTransform rectTransform;
    private float startY;
    private float minYDeviation, maxYDeviation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y;

        // Set dynamic limits
        minYDeviation = startY - minY;
        maxYDeviation = startY + maxY;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPosition = rectTransform.anchoredPosition;
        newPosition.y += eventData.delta.y / canvas.scaleFactor; // Only update Y position
        newPosition.y = Mathf.Clamp(newPosition.y, minYDeviation, maxYDeviation);  // Clamp within limits

        rectTransform.anchoredPosition = newPosition;
    }
}
