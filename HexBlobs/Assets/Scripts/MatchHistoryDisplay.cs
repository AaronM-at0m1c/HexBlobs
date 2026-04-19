using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MatchHistoryDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts;

    void Start()
    {
        DisplayMatchHistory();
    }

    void DisplayMatchHistory()
    {
        List<FinalScore> recentMatches = DatabaseManager.Instance.GetRecentMatches(scoreTexts.Length);

        if (recentMatches.Count == 0)
        {
            for (int i = 0; i < scoreTexts.Length; i++)
            {
                scoreTexts[i].text = (i + 1) + ". No matches yet!";
            }
            return;
        }

        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i < recentMatches.Count)
            {
                FinalScore match = recentMatches[i];
                string winner = match.Player1Score > match.Player2Score ? "Player 1"
                              : match.Player2Score > match.Player1Score ? "Player 2"
                              : "Draw";

                scoreTexts[i].text = $"{i + 1}. {match.Player1Score} - {match.Player2Score}  |  Winner: {winner}";
            }
            else
            {
                scoreTexts[i].text = (i + 1) + ". ---";
            }
        }
    }
}