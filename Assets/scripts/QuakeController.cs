using UnityEngine;
using System.Collections;

public class QuakeController : MonoBehaviour {

	const int TICKS_PER_SHAKE = 25;
	const float INTENSITY = 5f;
	const float SHAKEINTENSITY = 5f;
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

			Vector3 vectorForShakeObject = new Vector3(Random.Range (-SHAKEINTENSITY, SHAKEINTENSITY), 0, Random.Range (-SHAKEINTENSITY, SHAKEINTENSITY));

			foreach (GameObject obj in objects) {
				obj.transform.parent.rigidbody.velocity = Vector3.zero;
				obj.transform.parent.rigidbody.AddForce(new Vector3(Random.Range (-INTENSITY, INTENSITY), Random.Range (-INTENSITY, INTENSITY), Random.Range (-INTENSITY, INTENSITY)/6.0f), ForceMode.Impulse);
			}

			foreach (GameObject sObj in shakeObjects) {
				sObj.transform.parent.rigidbody.velocity = Vector3.zero;

				sObj.transform.parent.rigidbody.AddForce(vectorForShakeObject, ForceMode.Impulse);
			}
		}
		tick_num = (tick_num + 1) % TICKS_PER_SHAKE;
	}

}
