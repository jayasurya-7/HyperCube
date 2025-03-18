using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;

	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");

		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}

		if (gameControllerObject == null) {
			Debug.Log("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) {
			// Ignore Boundary
			return;
		}

		if (explosion != null) {
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.CompareTag("Player")) {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameData.events = Array.IndexOf(gameData.spaceEvents, "dead");
			gameController.GameOver();
		} else {
            gameData.events = Array.IndexOf(gameData.spaceEvents, "destroyed");

            gameController.AddScore(scoreValue); // Only add to the score when not hitting the Player!
		}

		if (other.tag != "Player")
		{
			Debug.Log("here");
			Destroy(other.gameObject);
		}
		else
			other.gameObject.SetActive(false);
		
		Destroy(gameObject);
	}
}
