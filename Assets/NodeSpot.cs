using UnityEngine;

public class NodeSpot : MonoBehaviour {
    [SerializeField] Turret curTurret;
    [SerializeField] BoxCollider col;

    void Start() {
        col = GetComponent<BoxCollider>();

        Collider[] objects = Physics.OverlapBox(col.center, col.size, col.transform.rotation);

        foreach (Collider c in objects) {
            if (c.gameObject.GetComponent<Turret>() != null) {
                curTurret = c.gameObject.GetComponent<Turret>();
                break;
			}
        }
    }

    void Update() {
        
    }

    public void newTurret(bool place) {
        if (place) {
            
        } else {
            Destroy(curTurret.gameObject);       
        }
    }
}