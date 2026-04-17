using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private void Start()
{
    BoardState board = GameManager.Instance.Board;
    int player1FinalScore = board.GetPlayerTiles(PlayerId.Player1).Count;
    int player2FinalScore = board.GetPlayerTiles(PlayerId.Player2).Count;
    scoreText.text = "Final Score: " + player1FinalScore + " to " + player2FinalScore;

    DatabaseManager.Instance.SaveFinalScore(player1FinalScore, player2FinalScore);
}
    
    public void OnSubmitScore()
    {
        SceneManager.LoadScene("MatchHistory");
    }

    public void Retry()
    {
        //GameManager.Instance.ResetGame();
        SceneManager.LoadScene("GameScene1");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}