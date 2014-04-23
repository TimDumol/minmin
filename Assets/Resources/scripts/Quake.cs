using UnityEngine;
using System.Collections.Generic;

public class Quake : MonoBehaviour
{
    public static float INTENSITY = 60f;
    public static int TICKS_PER_SHAKE = 5;
    public static int SHAKE_TICKS= 540;
    public static int REST_TICKS = 40;
	public GameObject floor = null;

    public AudioClip earthquakeAudio;
	/*
	public class CameraShake
	{
		public bool Enabled;
	}
	*/
    private int ticks;

    HashSet<ContactPoint> hs;
    // Use this for initialization
    void Start ()
    {
        hs = new HashSet<ContactPoint> ();
        ticks = 0;
		if (floor == null)
			floor = this.gameObject;
        // floor = GameObject.FindGameObjectWithTag ("FloorShake");
			
        Vector3 v = floor.transform.position;
        floor.transform.position = new Vector3 (v.x, v.y - 0.1f, v.z);
        floor.transform.position = v;
    }

    Vector3 RandomVector (float range)
    {
        float x = Random.Range (-range, range);
        float y = 0;
        float z = Random.Range (-range, range);
        return new Vector3 (x, y, z);
    }

    void collide (Collision hit)
    {
		try{
	        foreach (ContactPoint p in hit.contacts) {
				try{
	            if (hs.Contains (p))
	                continue;
	            hs.Add (p);
				}
				catch{}
	        }
	        if (ticks < SHAKE_TICKS && ticks % TICKS_PER_SHAKE == 0) {
	            var forces = new Dictionary<GameObject, float> ();
	            Vector3 r = RandomVector (INTENSITY);
	            foreach (ContactPoint contact in hs) {
	                GameObject obj = null;
	                if (contact.thisCollider.gameObject == floor) {
	                    obj = contact.otherCollider.gameObject;
	                } else if (contact.otherCollider.gameObject == floor) {
	                    obj = contact.thisCollider.gameObject;
	                }
	                if (obj == null) {
	                    // Debug.Log ( "(" + id + ") " + "No floor found!" );
	                    continue;
	                }
	                if (!forces.ContainsKey(obj)) {
	                    forces[obj] = 0;
	                }
	                forces[obj] += 1;

	            }  
	            foreach (GameObject obj in forces.Keys) {
	                var force = forces[obj];
	                try {
	                    obj.transform.transform.rigidbody.velocity = Vector3.zero;
	                    obj.transform.transform.rigidbody.AddForce (r/10, ForceMode.Impulse);

	                } catch {
	                    try {
	                        obj.transform.parent.transform.rigidbody.velocity = Vector3.zero;
	                        obj.transform.parent.transform.rigidbody.AddForce (r, ForceMode.Impulse);
	                    } catch {
	                        // Debug.Log ( "(" + id + ") " + "Error " + obj.ToString () );
	                    }
	                }
	            }
	            hs.Clear ();
	        }
		}
		catch {}
    }

    void FixedUpdate ()
    {
        if (ticks == 0) {
            Player player = GameObject.FindObjectOfType<Player>();
            AudioSource.PlayClipAtPoint(earthquakeAudio, player.transform.position);
        }
        ticks = (ticks + 1) % (SHAKE_TICKS + REST_TICKS);
    }

    void OnCollisionStay (Collision hit)
    {
        collide (hit);
    }

    void OnCollisionExit (Collision hit)
    {
        collide (hit);
    }

    void OnCollisionEnter (Collision hit)
    {
        collide (hit);
    }
    // Update is called once per frame
    void Update ()
    {
	
    }
}
