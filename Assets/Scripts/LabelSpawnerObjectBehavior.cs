using UnityEngine;
using System.Collections;

public class LabelSpawnerObjectBehavior : MonoBehaviour 
{

    public GameObject labelTextPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnCashIncrease(GameObject parentObject, float amount)
    {
        GameObject label = (GameObject)GameObject.Instantiate(this.labelTextPrefab);
        label.GetComponent<LabelTextBehavior>().LabelType = LabelType.Cash;
        label.transform.parent = parentObject.transform;
    }

    public void SpawnReligiosityIncrease(GameObject parentObject, float amount)
    {
        Debug.Log("Spawning religiosity increase based on position of parentObject: " + parentObject.name);
        GameObject label = (GameObject)GameObject.Instantiate(this.labelTextPrefab);
        label.GetComponent<LabelTextBehavior>().LabelType = LabelType.Religiosity;
        label.transform.position = parentObject.GetComponentInChildren<tk2dSprite>().transform.position;
    }

    public void SpawnSecularIncrease(GameObject parentObject, float amount)
    {
        GameObject label = (GameObject)GameObject.Instantiate(this.labelTextPrefab);
        label.GetComponent<LabelTextBehavior>().LabelType = LabelType.Secular;
        label.transform.parent = parentObject.transform;
    }
}
