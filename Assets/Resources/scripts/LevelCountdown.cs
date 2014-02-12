using UnityEngine;
using System.Collections;

/**
 * This class creates a GUI square that shows the time limit for the level
 */

public class LevelCountdown : MonoBehaviour {
	
	public GUIStyle progress_empty;
	public GUIStyle progress_full;
	const float TOTAL_HEALTH = 50f;
	
	//current progress
	public float barDisplay;
	
	Vector2 pos = new Vector2(150,150);
	Vector2 size = new Vector2(250,50);

	//Textures for healthbar
	public Texture2D emptyTex;
	public Texture2D fullTex;

	public static float timerInSeconds;
	
	void Start () {
		timerInSeconds = TOTAL_HEALTH;
		InvokeRepeating ("Countdown", 1.0f, 1.0f);
	}

	public static void AddTime (int time) {
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

			GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y), emptyTex, progress_empty);	
			GUI.Box(new Rect(pos.x, pos.y, size.x, size.y), fullTex, progress_full);
			//draw the filled-in part:
			GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
			GUI.Box(new Rect(0, 0, size.x, size.y), fullTex, progress_full);
			GUI.EndGroup();
			GUI.EndGroup();

			GUI.BeginGroup(new Rect(pos.x+0.4f*Screen.width, pos.y, size.x, size.y), emptyTex, progress_empty);	
			GUI.Box(new Rect(pos.x+0.4f*Screen.width, pos.y, size.x, size.y), fullTex, progress_full);
			//draw the filled-in part:
			GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
			GUI.Box(new Rect(0, 0, size.x, size.y), fullTex, progress_full);
			GUI.EndGroup();
			GUI.EndGroup();

		
		}
	}
	
}
