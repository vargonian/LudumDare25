using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class UniversitySpeechBubbleBehavior : MonoBehaviour {


    private List<List<string>> universityQuotes = new List<List<string>>();

    public GameObject universityBuilding;

    private Stopwatch updateBubbleTimer;
    public float ChangeTextInterval;

	// Use this for initialization
	void Start () 
    {
        List<string> smartQuotes = new List<string>();
        smartQuotes.Add("That which can be\nasserted without evidence\ncan be dismissed\nwithout evidence.");
        smartQuotes.Add("There are wonders\nenough without our\ninventing any.");
        smartQuotes.Add("Evolution is a\ntheory and a fact.\nThese are not\nmutually exclusive in\nscience.");

        List<string> smartMediumQuotes = new List<string>();
        smartMediumQuotes.Add("The universe gives me\nan almost spiritual\nfeeling of wonder.");        


        List<string> mediumQuotes = new List<string>();
        mediumQuotes.Add("All faiths may\nbe referencing\ncertain universal\ntruths.");        

        List<string> mediumDumbQuotes = new List<string>();
        mediumDumbQuotes.Add("The universe\nmust have had\na beginning.");
        mediumDumbQuotes.Add("The universe is\ntoo complex to\nhave happened\nby chance.");
        mediumDumbQuotes.Add("We should teach\nthe controversy\nsurrounding the\ntheory of evolution.");        
            

        List<string> dumbQuotes = new List<string>();
        dumbQuotes.Add("Morality is derived\nfrom the One\nTrue God.");
        dumbQuotes.Add("If we evolved from\nmonkeys, why are\nthere still monkeys?");
        dumbQuotes.Add("Evolution is\njust a theory.");
            

        this.universityQuotes.Add(smartQuotes);
        this.universityQuotes.Add(smartMediumQuotes);
        this.universityQuotes.Add(mediumQuotes);
        this.universityQuotes.Add(mediumDumbQuotes);
        this.universityQuotes.Add(dumbQuotes);

        this.updateBubbleTimer = Stopwatch.StartNew();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (this.updateBubbleTimer.Elapsed.TotalSeconds > this.ChangeTextInterval)
        {
            float uniRelig = this.universityBuilding.GetComponent<BuildingRootBehavior>().Religiosity;
            //UnityEngine.Debug.Log("Uni relig: " + uniRelig);

            System.Random randGen = new System.Random();
            string newQuote;

            if (uniRelig >= 0 && uniRelig < 0.2f)
            {
                //UnityEngine.Debug.Log("LOW RELIG");
               newQuote = this.universityQuotes[0][randGen.Next(this.universityQuotes[0].Count)];
            }
            else if (uniRelig >= 0.2f && uniRelig < 0.4f)
            {
                newQuote = this.universityQuotes[1][randGen.Next(this.universityQuotes[1].Count)];
            }
            else if (uniRelig >= 0.4f && uniRelig < 0.6f)
            {
                newQuote = this.universityQuotes[2][randGen.Next(this.universityQuotes[2].Count)];
            }
            else if (uniRelig >= 0.6f && uniRelig < 0.8f)
            {
                newQuote = this.universityQuotes[3][randGen.Next(this.universityQuotes[3].Count)];
            }
            else
            {
                newQuote = this.universityQuotes[4][randGen.Next(this.universityQuotes[4].Count)];
            }

            var textMesh = this.GetComponentInChildren<tk2dTextMesh>();

            textMesh.text = newQuote;
            textMesh.Commit();

            this.updateBubbleTimer.Reset();
            this.updateBubbleTimer.Start();
        }
	}
}
