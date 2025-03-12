using System;
using TMPro;
using UnityEngine;

public enum GameState
{
	Start,
	Active,
	LastWave,
	Win,
	Lose
}


public class GameManager : MonoBehaviour
{
	GameState state;

	[SerializeField]
	WaveManager waveManager;

	[SerializeField]
	End end;

	[SerializeField]
	TextMeshProUGUI EnemiesDeafeatedText;

	[SerializeField]
	GameObject winUI, loseUI, shopUI;

	[NonSerialized]
	public int EnemiesDefeated = 0;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		state = GameState.Start;
	}

	// Update is called once per frame
	void Update()
	{
		switch (state)
		{
			case GameState.Start:
				state = GameState.Active;
				break;
			case GameState.Active:
				goToLastWave();
				goToLoseState();
				updateEnemiesDeafeated();
				break;
			case GameState.LastWave:
				goToLoseState();
				goToWinFromLastWave();
				updateEnemiesDeafeated();
				break;
			case GameState.Win:
				shopUI.SetActive(false);
				winUI.SetActive(true);
				print("Win");
				break;
			case GameState.Lose:
				shopUI.SetActive(false);
				loseUI.SetActive(true);
				print("lose");
				break;
			default:
				break;
		}
	}

	private void goToLastWave()
	{
		if (waveManager != null && waveManager.LastWave)
		{
			state = GameState.LastWave;
		}
	}

	private void goToWinFromLastWave()
	{
		if (GameObject.FindFirstObjectByType<Enemy>() == null)
		{
			state = GameState.Win;
		}
	}

	private void goToLoseState()
	{
		if (end != null && end.health <= 0)
		{
			state = GameState.Lose;
		}
	}

	private void updateEnemiesDeafeated()
	{
		if (EnemiesDeafeatedText)
		{
			EnemiesDeafeatedText.text = EnemiesDefeated.ToString();
		}
	}
}
