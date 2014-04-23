using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	private bool isNotPaused;
	private GameObject pauseInstance;

	private Material pauseMaterial;

	void Start () {
		isNotPaused = true;
		pauseMaterial = Resources.Load<Material> ("materials/pause");

	}
	
	// Update is called once per frame
	void Update () {
		if( MainControls.Check ( MainControls.Key.Pause ) ){
			if (isNotPaused) {
				Time.timeScale=0;
				Transform cameraTransform = Camera.main.gameObject.transform;
				Vector3 indicatorPos = cameraTransform.position + cameraTransform.forward;
				Quaternion indicatorRot = cameraTransform.rotation;
				
				if (pauseInstance == null) {
					pauseInstance = GameObject.CreatePrimitive(PrimitiveType.Quad);
				}
				
				pauseInstance.renderer.material = pauseMaterial;
				
				pauseInstance.transform.localScale = new Vector3(Camera.main.aspect, 1f, 1f);
				pauseInstance.transform.position = indicatorPos;
				pauseInstance.transform.rotation = indicatorRot;
				isNotPaused = !isNotPaused;

			} else {
				Time.timeScale=1;
				guiTexture.enabled = false;
				pauseInstance = null;

			}

		}
	
	}
}
