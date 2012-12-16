using UnityEngine;
using System.Collections;
using System.Diagnostics;

public enum LabelType
{
    Religiosity,
    Secular,
    Cash
}

public class LabelTextBehavior : MonoBehaviour 
{
    public float scrollDurationSeconds = 2.0f;

    public float scrollSpeed = 30.0f;

    Stopwatch stopWatch;

    private tk2dTextMesh textMesh;

    public LabelType LabelType = LabelType.Religiosity;

	// Use this for initialization
	void Start () 
    {
        this.stopWatch = Stopwatch.StartNew();

        this.textMesh = this.GetComponent<tk2dTextMesh>();

        if (this.LabelType == global::LabelType.Cash)
        {
            this.textMesh.color = Color.green;
            this.textMesh.color2 = Color.green;
            this.textMesh.useGradient = true;
        }
        else if (this.LabelType == global::LabelType.Religiosity)
        {
            this.textMesh.color = Color.red;
            this.textMesh.color2 = Color.red;
            this.textMesh.useGradient = true;
        }
        else if (this.LabelType == global::LabelType.Secular)
        {
            this.textMesh.color = Color.blue;
            this.textMesh.color2 = Color.blue;
            this.textMesh.useGradient = true;
        }

        this.textMesh.Commit();
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.gameObject.transform.position += new Vector3(0, this.scrollSpeed * Time.smoothDeltaTime, 0);

        if (this.stopWatch.Elapsed.TotalSeconds > this.scrollDurationSeconds)
        {
            Object.Destroy(this.gameObject);
        }
	}

    
}
