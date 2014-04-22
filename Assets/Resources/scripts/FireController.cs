using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour {
	const int TICKS_PER_INCREASE = 20;


	int tick_num;
	
	// Use this for initialization
	void Start () {
		tick_num = 1;
	}

	
	void FixedUpdate() {
		if (tick_num == 0) {
			GameObject[] fires = GameObject.FindGameObjectsWithTag("Fire");
			//int nFires = fires.Length;
			foreach (GameObject fire in fires) {
				foreach (ParticleEmitter emitter in fire.GetComponentsInChildren<ParticleEmitter>() ) {
                    // Adjust the following constants as needed to change fire spread.
					emitter.maxEnergy *= 1.002f;
					emitter.minEnergy *= 1.002f;
					emitter.maxEmission *= 1.002f;
					emitter.minEmission *= 1.002f;
					Vector3 rndVel = emitter.rndVelocity;
					rndVel.x = rndVel.x*1.002f + 0.10f;
					rndVel.z = rndVel.z*1.002f + 0.10f;
					emitter.rndVelocity = rndVel;
				}
			}
		}
		tick_num = (tick_num + 1) % TICKS_PER_INCREASE;
	}
}