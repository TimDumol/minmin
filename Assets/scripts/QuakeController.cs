using UnityEngine;
using System.Collections;

public class QuakeController : MonoBehaviour {

	const int TICKS_PER_SHAKE = 5;
	const float INTENSITY = 1.5f;
	const float SHAKEINTENSITY = 0.5f;
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
			GameObject[] shakeObjects = GameObject.FindGameObjectsWithTag("ShakeSceneObject");

			Vector3 vectorForShakeObject = new Vector3(Random.Range (-SHAKEINTENSITY, SHAKEINTENSITY), 0, 1);

			foreach (GameObject obj in objects) {
				print (obj.name);
				obj.transform.parent.rigidbody.AddForce(new Vector3(Random.Range (-INTENSITY, INTENSITY), Random.Range (-INTENSITY, INTENSITY), Random.Range (-INTENSITY, INTENSITY)/6.0f), ForceMode.Impulse);
			}

			foreach (GameObject sObj in shakeObjects) {
				print (sObj.name);
				sObj.transform.parent.rigidbody.AddForce(vectorForShakeObject, ForceMode.Impulse);
			}
		}
		tick_num = (tick_num + 1) % TICKS_PER_SHAKE;
	}

}
