using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public TextMeshProUGUI currentPlayerText;



    void Start()
    {
        GameManager.Instance.OnBoardChanged += UpdateText;
    }

    void OnDisable()
    {
        GameManager.Instance.OnBoardChanged -= UpdateText;
    }

    void UpdateText(int newScore)
    {
        if (GameManager.Instance == null)
        return;
 
        BoardState board = GameManager.Instance.Board;
        PlayerId currentPlayer = GameManager.Instance.CurrentPlayer;

        int player1Score = board.GetPlayerTiles(PlayerId.Player1).Count;
        int player2Score = board.GetPlayerTiles(PlayerId.Player2).Count;

        if (player1ScoreText != null)
            player1ScoreText.text = $"Player 1: {player1Score}";

        if (player2ScoreText != null)
            player2ScoreText.text = $"Player 2: {player2Score}";

        if (currentPlayerText != null)
            currentPlayerText.text = $"Current Turn: {(currentPlayer == PlayerId.Player1 ? "Player 1" : "Player 2")}";
    }
}
