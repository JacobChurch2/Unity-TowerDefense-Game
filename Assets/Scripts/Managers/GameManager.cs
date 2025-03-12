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
    private GameObject winScreen;

    [SerializeField]
    private GameObject loseScreen;
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
				break;
			case GameState.LastWave:
				goToLoseState();
				goToWinFromLastWave();
				break;
			case GameState.Win:
				print("Win");
                winScreen?.SetActive(true);
                break;
			case GameState.Lose:
				print("lose");
                loseScreen?.SetActive(true);
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
}
