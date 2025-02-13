using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour {

	public static GamePlayController instance;

	[SerializeField]
	private Text scoreText, endScore, bestScore, gameOverText;

	[SerializeField]
	private Button restartGameButton, instructionButton;

	[SerializeField]
	private GameObject pausePanel;

	[SerializeField]
	private GameObject finishPanel;

	[SerializeField]
	private GameObject[] birds;

	[SerializeField]
	private Sprite[] medals;

	[SerializeField]
	private Image medalImage;

	void Awake() {
		MakeInstance ();
		Time.timeScale = 0f;
	}


	void Start () {
		
	}

	void MakeInstance () {
		if (instance == null) {
			instance = this;
		}
	}

	public void pauseGame() {
		if (BirdScripts.instance != null) {
			if (BirdScripts.instance.isAlive) {
				pausePanel.SetActive (true);
				finishPanel.SetActive (false);
				gameOverText.gameObject.SetActive (false);
				endScore.text = "" + BirdScripts.instance.score;
				bestScore.text = "" + GameControllers.instance.GetHighScore();
				Time.timeScale = 0f;
				restartGameButton.onClick.RemoveAllListeners ();
				restartGameButton.onClick.AddListener (() => resumeGame());
			}
		}
	}

	public void goToMenu() {
		SceneManager.LoadScene ("MainMenu");
	}

	public void resumeGame() {
		pausePanel.SetActive (false);
		Time.timeScale = 1f;
	}

	public void restartGame() {
		SceneManager.LoadScene ("GamePlayScene");
	}

	public void playGame() {
		scoreText.gameObject.SetActive (true);
		birds [GameControllers.instance.GetSelectedBird ()].SetActive (true);
		instructionButton.gameObject.SetActive (false);
		Time.timeScale = 1f;
	}
																																																																								
	public void setScore(int score) {
		scoreText.text = "" + score;
	}

	public void finishGame() {
		pausePanel.SetActive (false);
		finishPanel.SetActive (true);
	}

	public void playerDiedShowScore(int score) {
		pausePanel.SetActive (true);
		finishPanel.SetActive (false);
		gameOverText.gameObject.SetActive (true);
		scoreText.gameObject.SetActive (false);

		endScore.text = "" + score;

		if (score > GameControllers.instance.GetHighScore ()) {
			GameControllers.instance.SetHighScore (score);
		}

		bestScore.text = "" + GameControllers.instance.GetHighScore ();

		if (score <= 20) {
			medalImage.sprite = medals [0];

		} else if (score > 20 && score < 40) {
			medalImage.sprite = medals [1];

			if (GameControllers.instance.IsGreenBirdUnlocked () == 0) {
				GameControllers.instance.UnlockGreenBird ();
			} 

		} else {
			medalImage.sprite = medals [2];

			if (GameControllers.instance.IsGreenBirdUnlocked () == 0) {
				GameControllers.instance.UnlockGreenBird ();
			}

			if (GameControllers.instance.IsRedBirdUnlocked () == 0) {
				GameControllers.instance.UnlockRedBird ();
			}
		}

		restartGameButton.onClick.RemoveAllListeners ();
		restartGameButton.onClick.AddListener (() => restartGame ());
	}
}






























