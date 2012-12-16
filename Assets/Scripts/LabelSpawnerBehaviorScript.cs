using UnityEngine;
using System.Collections;

public class LabelSpawnerBehaviorScript : MonoBehaviour {

    private GameObject labelSpawner;

	// Use this for initialization
	void Start () 
    {
        this.labelSpawner = GameObject.Find("LabelSpawner");
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void SpawnReligiosityGain(float amount)
    {
        Vector3 spawnPosition = this.gameObject.transform.position;

        Debug.Log("Spawn position: " + spawnPosition);

        this.labelSpawner.GetComponent<LabelSpawnerObjectBehavior>().SpawnReligiosityIncrease(this.gameObject, amount);
    }

    public void SpawnSecularGain(float amount)
    {

    }

    public void SpawnCashGain(float amount)
    {

    }
}
