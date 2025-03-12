using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI moneyTxt;
    [SerializeField] Currency money;

    public bool sell = false;

    void Start() {
        updateMoney();
    }

	private void Update()
	{
		updateMoney();
	}

	public void ToggleClick(GameObject toggleObj) {
        toggleObj.SetActive(!toggleObj.activeSelf);
        GetComponent<RectTransform>().position = (toggleObj.activeSelf) ? new Vector3(200, 0, 0) : new Vector3(0, 0, 0);
    }

    public void buyTower(Turret turret, NodeSpot spawnNode) {
        sell = false;
        if (turret.Cost <= money.Amount && !spawnNode.hasTurret) {
            Instantiate(turret.gameObject, spawnNode.gameObject.transform.position, Quaternion.identity);
            spawnNode.curTurret = turret;
            updateMoney(-turret.Cost);
            spawnNode.hasTurret = true;
        }
    }

    public void sellClick() {
        sell = !sell;
    }

    public void sellTower(NodeSpot spawnNode) {
        if (sell) {
            updateMoney(spawnNode.curTurret.Cost / 2);
            Destroy(spawnNode.curTurret.gameObject);
            spawnNode.hasTurret = false;
            sell = false;
        }
    }

    public void updateMoney() {
		moneyTxt.text = "$" + money.Amount;
	}

    public void updateMoney(int change) {
        money.Amount += change;
        updateMoney();
    }
}