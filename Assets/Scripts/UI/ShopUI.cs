using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI moneyTxt;
    [SerializeField] int money;

    void Start() {
        moneyTxt.text = "$" + money;
    }

    void Update() {
        
    }

    public void ToggleClick(GameObject toggleObj) {
        toggleObj.SetActive(!toggleObj.activeSelf);
        GetComponent<RectTransform>().position = (toggleObj.activeSelf) ? new Vector3(200, 0, 0) : new Vector3(0, 0, 0);
    }

    public void TowerBtnClick(GameObject tower) {
        
    }
}