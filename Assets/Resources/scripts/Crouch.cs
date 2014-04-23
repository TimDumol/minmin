using UnityEngine;
using System.Collections;

public class Crouch : MonoBehaviour {
	public bool Enabled = true;
	public bool isCrouching = false;
	public const float CrouchRatio = 0.4f;
	// Use this for initialization
	void Start () {
	}
	
	void crouch(){
		if( !Enabled || isCrouching ) return;
		isCrouching = true;
		transform.localScale = new Vector3( transform.localScale.x, transform.localScale.y * CrouchRatio, transform.localScale.z );
	}
	
	void stand(){
		if( !Enabled || !isCrouching ) return;
		isCrouching = false;
		transform.position += new Vector3( 0, transform.localScale.y / CrouchRatio / 1.9f, 0 );
		transform.localScale = new Vector3( transform.localScale.x, transform.localScale.y / CrouchRatio, transform.localScale.z );
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown( "c" ) ) Debug.Log( "crouch now" );
		if( MainControls.Check( MainControls.Key.Crouch ) ){
			if( isCrouching ) stand();
			else crouch();
		}
	}
}
