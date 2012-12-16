using UnityEngine;
using System.Collections;

public class CivilianBehavior : MonoBehaviour 
{
    private float religiosity;

    private bool wanderingToDestination;
    private Vector3 currentWanderDestination;
    private Vector3 previousCivilianLocation;

    public float WanderSpeed;

    public float Religiosity
    {
        get
        {
            return this.religiosity;
        }

        set
        {
            this.religiosity = Mathf.Clamp(value, 0.0f, 1.0f);

            this.GetComponentInChildren<ReligiosityBarScript>().ReligiosityPercent = this.religiosity;
        }
    }

	// Use this for initialization
	void Start () 
    {
        this.previousCivilianLocation = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!this.wanderingToDestination)
        {
            this.wanderingToDestination = true;
            this.currentWanderDestination = this.GetNewWanderDestination();
        }
        else
        {
            float currentCivilianX = this.gameObject.transform.position.x;

            // If you've reached the destination.
            if ((this.previousCivilianLocation.x > this.currentWanderDestination.x && currentCivilianX <= this.currentWanderDestination.x) ||
                (this.previousCivilianLocation.x < this.currentWanderDestination.x && currentCivilianX >= this.currentWanderDestination.x))
            {
                this.wanderingToDestination = false;
                return;
            }

            float deltaX;
            if (this.currentWanderDestination.x > this.gameObject.transform.position.x)
            {
                deltaX = Time.smoothDeltaTime * this.WanderSpeed;
            }
            else
            {
                deltaX = Time.smoothDeltaTime * -this.WanderSpeed;
            }

            float newX = this.gameObject.transform.position.x + deltaX;

            this.previousCivilianLocation = this.gameObject.transform.position;
            this.gameObject.transform.position = new Vector3(newX, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
	}

    private Vector3 GetNewWanderDestination()
    {
       // CiviliansLayerBehavior civilianLayerBehavior = this.gameObject.transform.parent.GetComponent<CiviliansLayerBehavior>();
       CiviliansLayerBehavior civilianLayerBehavior = GameObject.Find("CivilianLayer").GetComponent<CiviliansLayerBehavior>();

        float xDestination = Mathf.Lerp(civilianLayerBehavior.MinimumCivilianX + 10f, civilianLayerBehavior.MaximumCivilianX - 10f, Random.value);
        
        return new Vector3(xDestination, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }
}
