using UnityEngine;
using System.Collections;

/**
 * This class creates a GUI square that shows the time limit for the level
 */

public class LevelCountdown : MonoBehaviour {
	public static int timerInSeconds = 60;

	const float GUI_WIDTH = 0.15f;
	const float GUI_HEIGHT = 0.15f;
	
	void Start () {
		InvokeRepeating ("Countdown", 1.0f, 1.0f);
	}

	public static void AddTime (int time) {
		timerInSeconds += time;
	}

	void Countdown() {
		if (--timerInSeconds <= 0) {
			CancelInvoke ("Countdown");
			MasterController.endGame(false, "Time's up! Hurry up next time!");
		}
	}
	void CountdownWindow(int windowId) {
		GUILayout.BeginVertical ();
		GUILayout.Label ("Time Left: " + timerInSeconds);
		GUILayout.EndVertical ();
	}
	
	void OnGUI() {
		if (MasterController.state == GameState.STARTED) {
			GUILayout.Window (0, new Rect(1, 1, Screen.width * GUI_WIDTH, Screen.height * GUI_HEIGHT), CountdownWindow, "");	
		}
	}
}
