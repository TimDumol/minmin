using UnityEngine;
using System.Collections;

public class QuakeController : MonoBehaviour {

	const int TICKS_PER_SHAKE = 5;
	const float INTENSITY = 1;
	int tick_num;

	// Use this for initialization
	void Start () {
		tick_num = 0;
	}
		
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		if (tick_num == 0) {
			GameObject[] objects = GameObject.FindGameObjectsWithTag("SceneObject");
			Vector3 v = new Vector3(Random.Range (-INTENSITY, INTENSITY), Random.Range (-INTENSITY, INTENSITY), Random.Range (-INTENSITY, INTENSITY)/6.0f);
			foreach (GameObject obj in objects) {
				obj.transform.parent.rigidbody.AddForce(v, ForceMode.Impulse);
			}
		}
		tick_num = (tick_num + 1) % TICKS_PER_SHAKE;
	}

}
