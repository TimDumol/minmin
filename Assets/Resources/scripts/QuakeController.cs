using UnityEngine;
using System.Collections;

public class QuakeController : MonoBehaviour {

	const int TICKS_PER_SHAKE = 5;
	const float INTENSITY = 20f;
	const float SHAKEINTENSITY = 10f;
	int tick_num;

	// Use this for initialization
	void Start () {
		tick_num = 0;
	}
		
	// Update is called once per frame
	void Update () {

	}

	void FloorShake(){
	}

	void FixedUpdate() {
        return;
		if (tick_num == 0) {
			FloorShake();
			
			GameObject[] objects = GameObject.FindGameObjectsWithTag("SceneObject");
			GameObject[] shakeObjects = GameObject.FindGameObjectsWithTag("ShakeSceneObject");



			/*foreach (GameObject obj in objects) {
				obj.transform.parent.rigidbody.velocity = Vector3.zero;
				obj.transform.parent.rigidbody.AddForce(RandomVector (INTENSITY), ForceMode.Impulse);
			}*/

			foreach (GameObject sObj in shakeObjects) {
                sObj.transform.parent.rigidbody.AddExplosionForce(SHAKEINTENSITY, sObj.transform.parent.position, 0, 0, ForceMode.Impulse);
			}
			

		}
		tick_num = (tick_num + 1) % TICKS_PER_SHAKE;
	}

}