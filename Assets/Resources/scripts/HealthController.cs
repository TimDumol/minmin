using UnityEngine;
using System.Collections;

/**
 * This class creates a GUI square that shows the time limit for the level
 */

public class HealthController : MonoBehaviour
{

    //CONSTANTS
    const float MAX_HEALTH = 30f;
    const float MED_HEALTH = 20f;
    const float LOW_HEALTH = 10f;
    const float CRIT_HEALTH = 5f;

    //VARIABLES
    public static float health;
  

    //TEXTURES
    public Texture2D emptyTex;
    public Texture2D fullTex;
    
    //SOUNDS
    public AudioClip gameOverAudio;

    private enum HealthStatus
    {
        CRIT,
        LOW,
        MED,
        OK
    }

    private GameObject medHealthIndicator;
    private HealthStatus prevHealthStatus;
    private GameObject healthIndicatorInstance;
    
    void Start ()
    {
        health = MAX_HEALTH;
        InvokeRepeating ("CheckHealth", 1.0f, 1.0f);
        medHealthIndicator = Resources.Load<GameObject> ("prefabs/MedHealthIndicator");
        healthIndicatorInstance = null;
        prevHealthStatus = HealthStatus.OK;
    }

    void Update ()
    {
        if (health <= MED_HEALTH) {
            GameObject healthIndicator = medHealthIndicator;

            Transform cameraTransform = transform.Find ("Main Camera").gameObject.transform; // Change Main Camera to OVRCameraController for rift.
            // Materialize the fire indicator thing
            Vector3 indicatorPos = cameraTransform.position + cameraTransform.forward;
            Quaternion indicatorRot = cameraTransform.rotation;
            if (healthIndicatorInstance == null) {
                healthIndicatorInstance = Instantiate (healthIndicator, indicatorPos, indicatorRot) as GameObject;
            } else {
                healthIndicatorInstance.transform.position = indicatorPos;
                healthIndicatorInstance.transform.rotation = indicatorRot;
            }
        } else {
            if (healthIndicatorInstance != null) {
                Destroy (healthIndicatorInstance);
                healthIndicatorInstance = null;
            }
            prevHealthStatus = HealthStatus.OK;
        }
    }

    public static void AddHealth (float health)
    {
        if (health + health > MAX_HEALTH) { 
            health = MAX_HEALTH;
        } else {
            health += health;
        }
    }

    void CheckHealth ()
    {
        if (--health <= 0) {
            CancelInvoke ("CheckHealth");
            AudioSource.PlayClipAtPoint (gameOverAudio, transform.position, 3.0f); 
            MasterController.endGame (false, null);
        }
    }
}
