using UnityEngine;
using System.Collections;
using System;

public class MasterController : MonoBehaviour {

	//CONSTANTS
	const float MSG_WIDTH = 0.15f;
	const float MSG_HEIGHT = 0.2f;	

	//VARIABLES
	public static GameState state = GameState.STARTED;
	static string endMessage = null;
	static bool showMsg = false;
	static string msgText = "";

	public AudioClip youve_escaped;



	public static void endGame(bool win, string endMessage) {
		if (win) 
		{
			state = GameState.WON;
		} else
		{
			state = GameState.LOST;
		}
		Time.timeScale = 0;
	    Player.Reset ();
		MasterController.endMessage = endMessage;
	}
	
	public static string GetEndGameMessage() {
		string message;
		if (state == GameState.WON) {
			message = "You've escaped the floor!";
		} else {
			if (endMessage != null) {
				message = string.Format(endMessage);
			} else {
				message = "Sorry, you have died :(";
            }
        }
		return message;
    }

    // Use this for initialization
	void Start () {
	}
	
	void EndGameWindow(int windowId) {
		GUILayout.BeginVertical ();
		GUILayout.Label (GetEndGameMessage ());
		int currentLevel = Application.loadedLevel;
		if (state == GameState.LOST) {
			GUILayout.Label ("Press X (key R) to restart");
			if (MainControls.Check(MainControls.Key.Restart,false)) {
				Application.LoadLevel (currentLevel);
				state = GameState.STARTED;
				Time.timeScale = 1;
			}
		}
		else if (state == GameState.WON) {
			GUILayout.Label ("Press X (key R) to restart");
			GUILayout.Label ("Press A (key Enter) to go to the next level");
			// if (Input.GetKeyDown (KeyCode.Return)) {
			if (MainControls.Check(MainControls.Key.NextLevel,false)) {
				Application.LoadLevel(currentLevel+1);
				state = GameState.STARTED;
				Time.timeScale =1;
			}
			if (MainControls.Check(MainControls.Key.Restart,false)) {
				Application.LoadLevel (currentLevel);
				state = GameState.STARTED;
				Time.timeScale = 1;
			}
		}
		HealthController.health = 50;
		GUILayout.EndVertical ();
	}

	void OnGUI() {
		if (state != GameState.STARTED) {
			float baseX = (Screen.width - Screen.width * MSG_WIDTH)/4.0f;
			float baseY = (Screen.height - Screen.height * MSG_HEIGHT)/4.0f;

			GUILayout.Window (0, new Rect(baseX,baseY, Screen.width * MSG_WIDTH, Screen.height * MSG_HEIGHT),
				        EndGameWindow, "");
			GUILayout.Window (1, new Rect(0.4f*Screen.width + baseX,baseY, Screen.width * MSG_WIDTH, Screen.height * MSG_HEIGHT),
			                  EndGameWindow, "");
		}
		else if (showMsg) {
			const float baseX = 250f;
			const float baseY = 100f;
			GUI.Label(new Rect(baseX,baseY,200,100),msgText);
			GUI.Label(new Rect(0.4f*Screen.width + baseX,baseY,200,100),msgText);
			StartCoroutine(TakeABreak());
		}
	}

	static DateTime lastMessageShow = new DateTime(0);

	public static void ShowMessage (string msg) {
		msgText = msg;
		showMsg = true;
		lastMessageShow = DateTime.Now ;
	}

	static IEnumerator TakeABreak() {
		yield return new WaitForSeconds (2);
		if ((DateTime.Now - lastMessageShow).Seconds >= 2 - 1e-3) {
			showMsg = false;
		}
		yield break;
	}
	

}
