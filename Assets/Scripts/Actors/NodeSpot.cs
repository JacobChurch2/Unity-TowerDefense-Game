using UnityEngine;

public class NodeSpot : MonoBehaviour {
    BoxCollider col;

    public Turret curTurret;
    public bool hasTurret = false;

    void Start() {
        col = GetComponent<BoxCollider>();

        Collider[] objects = Physics.OverlapBox(col.center, col.size, col.transform.rotation);

        foreach (Collider c in objects) {
            if (c.gameObject.GetComponent<Turret>() != null) {
                curTurret = c.gameObject.GetComponent<Turret>();
                hasTurret = true;
                break;
			}
        }
    }

	void OnMouseDown() {
        ShopUI shop = GameObject.Find("ShopperUI").GetComponent<ShopUI>();
		
        if (shop.sell && hasTurret) {
            shop.sellTower(this);
        }
	}
}