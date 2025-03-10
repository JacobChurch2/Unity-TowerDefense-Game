using UnityEngine;
using UnityEngine.EventSystems;

public class ShopTowerButton : MonoBehaviour, IDragHandler, IEndDragHandler {
	[SerializeField] protected bool canDrag;

	[SerializeField] protected Canvas canvas;
	[SerializeField] protected RectTransform draggingPlane;

	protected GameObject item;
	protected RectTransform itemRectTransform;
	protected Vector3 originalPosition;

	Vector3 towerSpawnPos;
	Vector3 mousePos;

	[SerializeField] GameObject TowerObj;
	[SerializeField] ShopUI shop;

	private void Start() {
		canDrag = true;
	}

	private void Update() {
		mousePos = Input.mousePosition;
	}

	private void Awake() {
		item = gameObject;
		itemRectTransform = item.GetComponent<RectTransform>();
		originalPosition = itemRectTransform.position;
	}

	public void OnDrag(PointerEventData eventData) { if (canDrag) Move(eventData); }

	public void OnEndDrag(PointerEventData eventData) {
		if (canDrag) {
			if (Condition(eventData) == true) Drop(eventData);

			itemRectTransform.position = originalPosition;
		}
	}

	protected void Move(PointerEventData eventData) {
		if (eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null) {
			draggingPlane = eventData.pointerEnter.transform as RectTransform;
		}

		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingPlane, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePosition)) {
			itemRectTransform.position = globalMousePosition;
			itemRectTransform.rotation = draggingPlane.rotation;
		}
	}

	protected bool Condition(PointerEventData eventData) {
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) {
			if (hit.collider.gameObject.GetComponent<Turret>() != null) {

				return true;
			}
		}

		return false;
	}

	protected void Drop(PointerEventData eventData) {
		shop.buyTower(TowerObj.GetComponent<Turret>(), towerSpawnPos);
	}

	public void Press() {}
}