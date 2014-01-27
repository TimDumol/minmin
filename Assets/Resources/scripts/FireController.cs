using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour {
	
const int NUMBER_OF_FLAMES = 5;
const int FLOOR_DISTANCE = 15;
GameObject[] flames;
	//Instantiates Flames
	void Start () {
		flames = new GameObject[NUMBER_OF_FLAMES];

		for (int x = 0; x < NUMBER_OF_FLAMES; x++) {
			flames[x] = Resources.Load ("flames") as GameObject;
			print ("Created" + flames[x].name);
		}
		spreadFire (); 
	}
	
	void spreadFire() {	
		for (int x = 0; x< NUMBER_OF_FLAMES; x++) {
			Instantiate(flames[x]);
			Debug.Log ("Instantiated" + flames[x].name);
			flames[x].transform.position = new Vector3 (Random.Range (-FLOOR_DISTANCE, FLOOR_DISTANCE), 0, Random.Range (-FLOOR_DISTANCE, FLOOR_DISTANCE));
		}	
	}
}
	