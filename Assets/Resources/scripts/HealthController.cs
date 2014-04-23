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

    private Material medHealthMaterial;
    private Material lowHealthMaterial;
    private Material critHealthMaterial;
    private HealthStatus prevHealthStatus;
    private GameObject healthIndicatorInstance;
    
    void Start ()
    {
        health = MAX_HEALTH;
        InvokeRepeating ("CheckHealth", 1.0f, 1.0f);
        medHealthMaterial = Resources.Load<Material> ("materials/med_health_indicator");
        lowHealthMaterial = Resources.Load<Material> ("materials/low_health_indicator");
        critHealthMaterial = Resources.Load<Material> ("materials/crit_health_indicator");
        //Debug.Log (string.Format ("Medium health indicator: {0}", medHealthIndicator));
        healthIndicatorInstance = null;
        prevHealthStatus = HealthStatus.OK;
    }

    void Update ()
    {
        if (health <= MED_HEALTH) {
            Material healthMaterial = medHealthMaterial;
            HealthStatus healthStatus = HealthStatus.MED;
            if (health <= CRIT_HEALTH) {
                healthMaterial = critHealthMaterial;
                healthStatus = HealthStatus.CRIT;
            } else if (health <= LOW_HEALTH) {
                healthMaterial = lowHealthMaterial;
                healthStatus = HealthStatus.LOW;
            }
            

            Transform cameraTransform = Camera.main.gameObject.transform;

            // Materialize the health indicator thing
            Vector3 indicatorPos = cameraTransform.position + cameraTransform.forward;
            Quaternion indicatorRot = cameraTransform.rotation;

            if (healthIndicatorInstance == null) {
                healthIndicatorInstance = GameObject.CreatePrimitive(PrimitiveType.Quad);
            }

            if (prevHealthStatus != healthStatus) {
                healthIndicatorInstance.renderer.material = healthMaterial;
            }
           
            healthIndicatorInstance.transform.localScale = new Vector3(2f*Camera.main.aspect, 2f, 1f);
            healthIndicatorInstance.transform.position = indicatorPos;
            healthIndicatorInstance.transform.rotation = indicatorRot;

            prevHealthStatus = healthStatus;
        } else {
            if (healthIndicatorInstance != null) {
                Destroy (healthIndicatorInstance);
                healthIndicatorInstance = null;
            }
            prevHealthStatus = HealthStatus.OK;
        }
      
    }

    public static void AddHealth (float _health)
    {
        if (health + _health > MAX_HEALTH) { 
            health = MAX_HEALTH;
        } else {
            health += _health;
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
