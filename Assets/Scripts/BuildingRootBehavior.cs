using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;

public class BuildingRootBehavior : MonoBehaviour 
{
    public float InitialReligiosityPercent;
    public float InfluenceRadius;
    //public float InfluenceAmountPerSecond;
    public float MaxInfluenceAmountPerSecond;
    public float MinInfluenceAmountPerSecond;
    public bool IsInfluenceEnabled;
    public bool CanBeInfluencedByCivilians;

    private float religiosity;

    private CiviliansLayerBehavior civilianLayerBehavior;

    private Bounds influenceBounds;

    public float InfluenceTickIntervalSeconds;

    private Stopwatch stopWatch;

	// Use this for initialization
	void Start () 
    {
        // this sets it for the children as well.
        this.Religiosity = this.InitialReligiosityPercent;

        this.civilianLayerBehavior = GameObject.Find("CivilianLayer").GetComponent<CiviliansLayerBehavior>();

        this.influenceBounds = new Bounds(this.gameObject.transform.position, new Vector3(this.InfluenceRadius * 2f, 800f));

        this.stopWatch = Stopwatch.StartNew();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (this.stopWatch.Elapsed.TotalSeconds > this.InfluenceTickIntervalSeconds)
        {
            if (this.IsInfluenceEnabled)
            {
                // Tick the religiosity
                this.UpdateCivvieInfluences();

                this.stopWatch.Reset();
                this.stopWatch.Start();
            }
        }
                
	}

    public float Religiosity
    {
        get
        {
            return this.religiosity;
        }

        set
        {
            this.religiosity = value;

            this.GetComponentInChildren<ReligiosityBarScript>().ReligiosityPercent = this.religiosity;
        }
    }

    private void UpdateCivvieInfluences()
    {
        foreach (var civCollider in this.collidersInRange)
        {
            float religiosityChangePerSecond = Mathf.Lerp(this.MinInfluenceAmountPerSecond, this.MaxInfluenceAmountPerSecond, this.Religiosity);
            float secularChangePerSecond = Mathf.Lerp(this.MinInfluenceAmountPerSecond, this.MaxInfluenceAmountPerSecond, (1.0f - this.Religiosity));
            civCollider.gameObject.transform.parent.GetComponent<CivilianBehavior>().Religiosity += (religiosityChangePerSecond * this.InfluenceTickIntervalSeconds);

            civCollider.gameObject.transform.parent.GetComponent<LabelSpawnerBehaviorScript>().SpawnReligiosityGain(34.0f);

            civCollider.gameObject.transform.parent.GetComponent<CivilianBehavior>().Religiosity -= (secularChangePerSecond * this.InfluenceTickIntervalSeconds);
        }
    }

    private List<Collider> collidersInRange = new List<Collider>();

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Civilian")
        {
            collidersInRange.Add(collider);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Civilian")
        {
            collidersInRange.Remove(collider);
        }
    }

}
