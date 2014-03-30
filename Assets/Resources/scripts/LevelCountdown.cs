using UnityEngine;
using System.Collections;

/**
 * This class creates a GUI square that shows the time limit for the level
 */

public class LevelCountdown : MonoBehaviour {

	//CONSTANTS
	const float TOTAL_HEALTH = 30f;

	//VARIABLES
	public static float timerInSeconds;

	//HEALTH BAR
	public float barDisplay;
	public GUIStyle progress_empty;
	public GUIStyle progress_full;
	Vector2 pos = new Vector2(150,150);
	Vector2 size = new Vector2(0.1f,0.03f);

	//TEXTURES
	public Texture2D emptyTex;
	public Texture2D fullTex;
	
	//SOUNDS
	public AudioClip gameOverAudio;
	
	void Start () {
		timerInSeconds = TOTAL_HEALTH;
		InvokeRepeating ("Countdown", 1.0f, 1.0f);
	}

	public static void AddTime (float time) {
		if (timerInSeconds + time > TOTAL_HEALTH) { 
			timerInSeconds = TOTAL_HEALTH;
		} 
		else {
			timerInSeconds += time;
		}
	}

	void Countdown() {
		if (--timerInSeconds <= 0) {
			CancelInvoke ("Countdown");
			AudioSource.PlayClipAtPoint(gameOverAudio, transform.position, 3.0f); 
			MasterController.endGame(false, null);
		}
	}

	void Update()
	{
		//the player's health
		barDisplay = timerInSeconds/TOTAL_HEALTH;
	}

	void OnGUI() {
		if (MasterController.state == GameState.STARTED) {

			GUI.BeginGroup(new Rect(pos.x, pos.y, size.x*Screen.width, size.y*Screen.height), emptyTex, progress_empty);	
			GUI.Box(new Rect(pos.x, pos.y, size.x*Screen.width, size.y*Screen.height), fullTex, progress_full);
			//draw the filled-in part:
			GUI.BeginGroup(new Rect(0, 0, size.x*Screen.width*barDisplay, size.y*Screen.height));
			GUI.Box(new Rect(0, 0, size.x*Screen.width, size.y*Screen.height), fullTex, progress_full);
			GUI.EndGroup();
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(pos.x+0.4f*Screen.width, pos.y, size.x*Screen.width, size.y*Screen.height), emptyTex, progress_empty);	
			GUI.Box(new Rect(pos.x+0.4f*Screen.width, pos.y, size.x*Screen.width, size.y*Screen.height), fullTex, progress_full);
			//draw the filled-in part:
			GUI.BeginGroup(new Rect(0, 0, size.x*Screen.width*barDisplay, size.y*Screen.height));
			GUI.Box(new Rect(0, 0, size.x*Screen.width, size.y*Screen.height), fullTex, progress_full);
			GUI.EndGroup();
			GUI.EndGroup();

		
		}
	}
	
}
