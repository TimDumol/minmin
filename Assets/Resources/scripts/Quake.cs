using UnityEngine;
using System.Collections;

public class Quake : MonoBehaviour {
	public static float INTENSITY = 45f;
	public static int TICKS_PER_FRAME = 180;

	private int ticks;
	private GameObject floor;
	Hashtable hs;

	// Use this for initialization
	void Start () {
		hs = new Hashtable();
		ticks = 0;
		floor = GameObject.FindGameObjectWithTag( "FloorShake" );

		Vector3 v = floor.transform.position;
		floor.transform.position = new Vector3( v.x, v.y - 0.1f, v.z );
	 	floor.transform.position = v;
	}

	Vector3 RandomVector( float range ){
		float x = Random.Range ( -range, range );
		float y = Random.Range ( 0, range );
		float z = Random.Range ( -range, range );
		return new Vector3( x, y, z );
	}



	void collide( Collision hit ){

		foreach( ContactPoint p in hit.contacts ){
			if( hs.ContainsKey ( p ) ) continue;
			hs.Add ( p, null );
		}
		if( ticks == 0 ){
			Vector3 r = RandomVector (INTENSITY);
			foreach( ContactPoint contact in hs.Keys ){
				GameObject obj = null;
				if( contact.thisCollider.gameObject == floor ){
					obj = contact.otherCollider.gameObject;
				}
				else if( contact.otherCollider.gameObject == floor ){
					obj = contact.thisCollider.gameObject;
				}
				if( obj == null ){
					// Debug.Log ( "(" + id + ") " + "No floor found!" );
					continue;
				}
				try{
					obj.transform.transform.rigidbody.velocity = Vector3.zero;
					obj.transform.transform.rigidbody.AddForce ( RandomVector (INTENSITY/10f), ForceMode.Impulse );
					
				}
				catch{
					try{
						obj.transform.parent.transform.rigidbody.velocity = Vector3.zero;
						obj.transform.parent.transform.rigidbody.AddForce ( r, ForceMode.Impulse );
					}
					catch{
						// Debug.Log ( "(" + id + ") " + "Error " + obj.ToString () );
					}
				}
			}
			hs.Clear ();
		}
	/*
		Vector3 r = RandomVector (INTENSITY);
		foreach( ContactPoint contact in hit.contacts ){
			// int id = (int) ( 5000f * Random.Range ( 0f, 1f ) );
			// Debug.Log ( "(" + id + ") " + "Logging: " + contact.thisCollider.gameObject.ToString () + " " + contact.otherCollider.gameObject.ToString() );
			GameObject obj = null;
			if( contact.thisCollider.gameObject == floor ){
				obj = contact.otherCollider.gameObject;
			}
			else if( contact.otherCollider.gameObject == floor ){
				obj = contact.thisCollider.gameObject;
			}
			if( obj == null ){
				// Debug.Log ( "(" + id + ") " + "No floor found!" );
				continue;
			}
			try{
				obj.transform.transform.rigidbody.velocity = Vector3.zero;
				obj.transform.transform.rigidbody.AddForce ( RandomVector (INTENSITY/10f), ForceMode.Impulse );
			
			}
			catch{
				try{
					obj.transform.parent.transform.rigidbody.velocity = Vector3.zero;
					obj.transform.parent.transform.rigidbody.AddForce ( r, ForceMode.Impulse );
				}
				catch{
					// Debug.Log ( "(" + id + ") " + "Error " + obj.ToString () );
				}
			}
		}

*/
	}

	void FixedUpdate(){
		ticks = (ticks + 1) % TICKS_PER_FRAME;
	}

	void OnCollisionStay( Collision hit ){
		collide (hit);
	}

	void OnCollisionExit( Collision hit ){
		collide (hit);
	}

	void OnCollisionEnter( Collision hit ){
		collide (hit);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
