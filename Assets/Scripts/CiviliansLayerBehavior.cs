using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class CiviliansLayerBehavior : MonoBehaviour 
{
    public float minCivilianYPosition;
    public float maxCivilianYPosition;

    public int civilianCount;
    public float CiviliansWanderSpeed;

    public GameObject civilianPrefab;

    //private BoxCollider boxCollider;

    private const float InfluenceRange = 200.0f;

    // this is how much each civilian is influenced by the others each tick.
    private float[] civilianInfluences;

    public float MinimumCivilianX
    {
        get
        {
            return -1891f;
        }
    }

    public float MaximumCivilianX
    {
        get
        {
            return 1077f;
        }
    }

	// Use this for initialization
	void Start () 
    {
        //this.boxCollider = this.gameObject.GetComponent<BoxCollider>();
        this.sortedCivilians = new List<GameObject>();
        this.InstantiateCivilians();
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.sortedCivilians = this.sortedCivilians.OrderBy(civ => civ.transform.position.x).ToList();

        this.UpdateCivilianReligiosities();
	}

    private void InstantiateCivilians()
    {
        for (int i = 0; i < this.civilianCount; i++)
        {
            GameObject civilianObject = (GameObject)GameObject.Instantiate(this.civilianPrefab);
            // NO!  too much trouble.  civilianObject.transform.parent = this.transform;

            // Set a random position within the bounds.
            float xStart = Mathf.Lerp(this.MinimumCivilianX, this.MaximumCivilianX, UnityEngine.Random.value);

            civilianObject.transform.position = new Vector3(xStart, Mathf.Lerp(this.minCivilianYPosition, this.maxCivilianYPosition, UnityEngine.Random.value), civilianObject.transform.position.z);

            civilianObject.GetComponentInChildren<ReligiosityBarScript>().ReligiosityPercent = Mathf.Lerp(0.0f, 0.5f, UnityEngine.Random.value);
            civilianObject.GetComponent<CivilianBehavior>().WanderSpeed = this.CiviliansWanderSpeed;
            civilianObject.GetComponentInChildren<BoxCollider>().isTrigger = true;

            this.sortedCivilians.Add(civilianObject);
        }

        this.sortedCivilians = this.sortedCivilians.OrderBy(civ => civ.transform.position.x).ToList();
        this.civilianInfluences = new float[this.sortedCivilians.Count];
    }

    public List<GameObject> SortedCivilians
    {
        get
        {
            return this.sortedCivilians;
        }
    }

    private List<GameObject> sortedCivilians;

    /// <summary>
    /// Update the civilian religiosities based on surrounding influence of villagers.
    /// </summary>
    private void UpdateCivilianReligiosities()
    {
        Array.Clear(this.civilianInfluences, 0, this.civilianInfluences.Length);

        for (int influencerIndex = 0; influencerIndex < this.civilianInfluences.Length; influencerIndex++)
        {
            var influencer = this.sortedCivilians[influencerIndex];

            // check left
            for (int influenceeIndex = influencerIndex - 1; influenceeIndex >= 0; influenceeIndex--)
            {
                float distanceBetween = Mathf.Abs(influencer.transform.position.x - this.sortedCivilians[influenceeIndex].transform.position.x);

                if (distanceBetween > InfluenceRange)
                {
                    // you no longer have influence on anyone to your left.
                    break;
                }
                else
                {
                    // Compute your influence on them.
                    float exertedReligiousForce = this.ComputeReligiousInfluence(influencer, this.sortedCivilians[influenceeIndex], distanceBetween);
                    this.civilianInfluences[influenceeIndex] += exertedReligiousForce;

                    float exertedSecularForce = this.ComputeSecularInfluence(influencer, this.sortedCivilians[influenceeIndex], distanceBetween);
                    this.civilianInfluences[influenceeIndex] -= exertedSecularForce;
                }
            }

            // check right
            for (int influenceeIndex = influencerIndex + 1; influenceeIndex < this.sortedCivilians.Count; influenceeIndex++)
            {
                float distanceBetween = Mathf.Abs(influencer.transform.position.x - this.sortedCivilians[influenceeIndex].transform.position.x);

                if (distanceBetween > InfluenceRange)
                {
                    // you no longer have influence on anyone to your left.
                    break;
                }
                else
                {
                    // Compute your influence on them.
                    float exertedReligiousForce = this.ComputeReligiousInfluence(influencer, this.sortedCivilians[influenceeIndex], distanceBetween);
                    this.civilianInfluences[influenceeIndex] += exertedReligiousForce;

                    float exertedSecularForce = this.ComputeSecularInfluence(influencer, this.sortedCivilians[influenceeIndex], distanceBetween);
                    this.civilianInfluences[influenceeIndex] -= exertedSecularForce;
                }
            }
        }

        for (int i = 0; i < this.sortedCivilians.Count; i++)
        {
          //  Debug.Log("Influence " + i + " " + this.civilianInfluences[i]);
            this.sortedCivilians[i].GetComponent<CivilianBehavior>().Religiosity += this.civilianInfluences[i];
        }
    }

    public float civilianGravityConstant;

    private float ComputeReligiousInfluence(GameObject influencer, GameObject influencee, float distanceBetween)
    {
        float influencerReligiosity = influencer.GetComponent<CivilianBehavior>().Religiosity;
        float influenceeReligiosity = influencee.GetComponent<CivilianBehavior>().Religiosity;

        float exertedForce = civilianGravityConstant * influencerReligiosity * influenceeReligiosity / (distanceBetween * distanceBetween + 1.0f) * Time.smoothDeltaTime;

        return exertedForce;
    }

    private float ComputeSecularInfluence(GameObject influencer, GameObject influencee, float distanceBetween)
    {
        float influencerReligiosity = 1.0f - influencer.GetComponent<CivilianBehavior>().Religiosity;
        float influenceeReligiosity = 1.0f - influencee.GetComponent<CivilianBehavior>().Religiosity;

        float exertedForce = civilianGravityConstant * influencerReligiosity * influenceeReligiosity / (distanceBetween * distanceBetween + 1.0f) * Time.smoothDeltaTime;

        return exertedForce;
    }
}
