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
                this.UpdateInfluencesOnCivilians();

                // should be separate
                if (this.CanBeInfluencedByCivilians)
                {
                    this.UpdateCivilianInfluencesOnBuilding();
                }

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

    private void UpdateInfluencesOnCivilians()
    {
        foreach (var civCollider in this.collidersInRange)
        {
            float religiosityChangePerSecond = Mathf.Lerp(this.MinInfluenceAmountPerSecond, this.MaxInfluenceAmountPerSecond, this.Religiosity);
            float secularChangePerSecond = Mathf.Lerp(this.MinInfluenceAmountPerSecond, this.MaxInfluenceAmountPerSecond, (1.0f - this.Religiosity));
            float religGained = religiosityChangePerSecond * this.InfluenceTickIntervalSeconds;
            civCollider.gameObject.transform.parent.GetComponent<CivilianBehavior>().Religiosity += (religGained);

            civCollider.gameObject.transform.parent.GetComponent<LabelSpawnerBehaviorScript>().SpawnReligiosityGain(religGained);

            float secularGained = secularChangePerSecond * this.InfluenceTickIntervalSeconds;
            civCollider.gameObject.transform.parent.GetComponent<CivilianBehavior>().Religiosity -= (secularGained);

            civCollider.gameObject.transform.parent.GetComponent<LabelSpawnerBehaviorScript>().SpawnSecularGain(secularGained);
        }
    }

    private void UpdateCivilianInfluencesOnBuilding()
    {
        const float MaxInfluenceFromCiviliansPerSecond = 0.1f;

        foreach (var civCollider in this.collidersInRange)
        {
            CivilianBehavior civBehavior = civCollider.gameObject.transform.parent.GetComponent<CivilianBehavior>();
            float religiousInfluenceFromThisCivilian = Mathf.Lerp(0, MaxInfluenceFromCiviliansPerSecond, civBehavior.Religiosity);

            float secularInfluenceFromThisCivilian = Mathf.Lerp(0, MaxInfluenceFromCiviliansPerSecond, (1.0f - civBehavior.Religiosity));

            this.Religiosity += religiousInfluenceFromThisCivilian;
          //  this.Religiosity -= secularInfluenceFromThisCivilian;


        }
    }

    private List<Collider> collidersInRange = new List<Collider>();

    private void OnTriggerEnter(Collider collider)
    {
        //UnityEngine.Debug.Log("OnTriggerEnter: " + collider.name);
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
