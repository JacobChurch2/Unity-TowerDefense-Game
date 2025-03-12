using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI moneyTxt;
    [SerializeField] Currency money;
    [SerializeField] Button toggleBtn;
    [SerializeField] TextMeshProUGUI MoneySpentText;
    [SerializeField] TextMeshProUGUI TowersPlacedText;

    private int TowersPlaced;
    private int MoneySpent;

    public bool sell = false;

    void Start() {
        updateMoney();
    }

	private void Update() {
		updateMoney();
	}

	public void ToggleClick(GameObject toggleObj) {
        toggleObj.SetActive(!toggleObj.activeSelf);
        toggleBtn.GetComponent<RectTransform>().anchoredPosition = (toggleObj.activeSelf) ? new Vector3(-189, 264, -144) : new Vector3(-400, 264, -144);
    }

    public void buyTower(Turret turret, NodeSpot spawnNode) {
        sell = false;
        if (turret.Cost <= money.Amount && !spawnNode.hasTurret) {
            GameObject copy = Instantiate(turret.gameObject, spawnNode.gameObject.transform.position, Quaternion.identity);
            spawnNode.curTurret = copy.GetComponent<Turret>();
            updateMoney(-turret.Cost);
            spawnNode.hasTurret = true;

            if (TowersPlacedText)
            {
                TowersPlaced++;
                TowersPlacedText.text  = TowersPlaced.ToString();
            }

            if (MoneySpentText)
            {
                MoneySpent += turret.Cost;
                MoneySpentText.text = MoneySpent.ToString();
            }
        }
    }

    public void sellClick() {
        sell = !sell;
    }

    public void sellTower(NodeSpot spawnNode) {
        if (sell) {
            updateMoney(spawnNode.curTurret.GetComponent<Turret>().Cost / 2);
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