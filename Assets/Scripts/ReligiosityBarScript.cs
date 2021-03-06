using UnityEngine;
using System.Collections;

public class ReligiosityBarScript : MonoBehaviour 
{
    private float religiosityPercent;

    private tk2dSprite religiositySprite;
    private tk2dSprite secularSprite;
    private Vector3 religiositySpriteScale;

    private float OriginalWidth;

    public float CurrentBarWidth
    {
        get
        {
           //todo: find a way to get this from the source texture return this.religiositySprite.s
            return this.religiositySprite.GetBounds().size.x * this.religiositySprite.transform.localScale.x;
        }
    }

    void Awake()
    {
        this.religiositySprite = this.gameObject.transform.FindChild("ReligiosityBarSprite").GetComponent<tk2dSprite>();

        if (this.religiositySprite == null)
        {
            Debug.LogError("ReligiositySprite is NULL");
        }

        this.secularSprite = this.gameObject.transform.FindChild("SecularBarSprite").GetComponent<tk2dSprite>();
        this.religiositySpriteScale = new Vector3(1, 1, 1);

       // Debug.Log("religiositySprite bounds: " + this.religiositySprite.GetBounds());
       // Debug.Log("secularSprite bounds: " + this.secularSprite.GetBounds());

        this.OriginalWidth = this.secularSprite.GetBounds().size.x * this.secularSprite.transform.localScale.x;
    }

	// Use this for initialization
	void Start () 
    {                      
        
	}
	
	// Update is called once per frame
	void Update () 
    {
       // this.ReligiosityPercent -= 0.005f;
	}

    public float ReligiosityPercent
    {
        get
        {
            return this.religiosityPercent;           
        }

        set
        {
            this.religiosityPercent = Mathf.Clamp(value, 0.0f, 1.0f);

            this.religiositySpriteScale.x = this.religiosityPercent;
            this.religiositySprite.scale = this.religiositySpriteScale;
            float newReligiositySpriteWidth = this.religiositySpriteScale.x * this.OriginalWidth;

            float newX = -(OriginalWidth / 2f) + newReligiositySpriteWidth / 2f;
            //float newX = -(190f/2f) + newReligiositySpriteWidth / 2f;

            this.religiositySprite.gameObject.transform.localPosition = new Vector3(newX, 0, -1);
        }
    }
}
