using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour {
	const int TICKS_PER_INCREASE = 20;
	const float EMISSION_INCREASE_FACTOR = 1.01f; // how the amt of particles increase per tick
	const float ENERGY_INCREASE_FACTOR = 1.01f; // how the duration of the particles increase per tick
	const float RND_VEL_FACTOR = 1.01f; // multiplier for random velocity added to particles per tick
	const float RND_VEL_RATE = 0.25f; // addend for random velocity added to particles per tick;
	const int MAX_FIRES = 25;


	int tick_num;
	
	// Use this for initialization
	void Start () {
		tick_num = 1;
	}

	
	void FixedUpdate() {
		if (tick_num == 0) {
			GameObject[] fires = GameObject.FindGameObjectsWithTag("Fire");
			int nFires = fires.Length;
			foreach (GameObject fire in fires) {
				foreach (ParticleEmitter emitter in fire.GetComponentsInChildren<ParticleEmitter>() ) {
					emitter.maxEnergy *= 1.01f;
					emitter.minEnergy *= 1.01f;
					emitter.maxEmission *= 1.01f;
					emitter.minEmission *= 1.01f;
					Vector3 rndVel = emitter.rndVelocity;
					rndVel.x = rndVel.x*1.005f + 0.2f;
					rndVel.z = rndVel.z*1.005f + 0.2f;
					emitter.rndVelocity = rndVel;
				}
			}
		}
		tick_num = (tick_num + 1) % TICKS_PER_INCREASE;
	}
}