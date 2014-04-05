using UnityEngine;
using System.Collections;

public class QuakeController : MonoBehaviour {

	const int TICKS_PER_SHAKE = 24;
	const float INTENSITY = 20f;
	const float SHAKEINTENSITY = 20f;
	int tick_num;

	// Use this for initialization
	void Start () {
		tick_num = 0;
	}
		
	// Update is called once per frame
	void Update () {

	}

	Vector3 RandomVector( float range ){
		float x = Random.Range ( -range, range );
		float y = Random.Range ( -range, range );
		float z = Random.Range ( -range, range );
		return new Vector3( x, y, z );
	}

	void FloorShake(){
	}

	void FixedUpdate() {
		if (tick_num == 0) {
			FloorShake();
			/*
			GameObject[] objects = GameObject.FindGameObjectsWithTag("SceneObject");
			GameObject[] shakeObjects = GameObject.FindGameObjectsWithTag("ShakeSceneObject");



			foreach (GameObject obj in objects) {
				obj.transform.parent.rigidbody.velocity = Vector3.zero;
				obj.transform.parent.rigidbody.AddForce(RandomVector (INTENSITY), ForceMode.Impulse);
			}


			foreach (GameObject sObj in shakeObjects) {
				sObj.transform.parent.rigidbody.velocity = Vector3.zero;
				Vector3 vectorForShakeObject = RandomVector (SHAKEINTENSITY);
				sObj.transform.parent.rigidbody.AddForce(vectorForShakeObject, ForceMode.Impulse);
			}
			*/

		}
		tick_num = (tick_num + 1) % TICKS_PER_SHAKE;
	}

}