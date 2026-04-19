using UnityEngine;
using SQLite;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class FinalScore
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int Player1Score { get; set; }

    public int Player2Score { get; set; }
}

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }
    
    private string dbPath;
    private SQLiteConnection dbConnection;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        SetDatabasePath();
        InitializeDatabase();
    }
    
    void SetDatabasePath()
    {
        dbPath = Path.Combine(Application.persistentDataPath, "gamedata.db");
    }
    
    void InitializeDatabase()
    {
        dbConnection = new SQLiteConnection(dbPath);
        CreateFinalScoresTable();
    }
    
    void CreateFinalScoresTable()
    {
        dbConnection.CreateTable<FinalScore>();
        Debug.Log("Match History table created at: " + dbPath);
    }
    
    public void SaveFinalScore(int player1Score, int player2Score)
    {
        FinalScore newScore = new FinalScore
        {
            Player1Score = player1Score,
            Player2Score = player2Score
        };
        
        dbConnection.Insert(newScore);
        Debug.Log("Final Score Saved");
    }
    
    public List<FinalScore> GetRecentMatches(int count)
{
    return dbConnection.Table<FinalScore>()
        .OrderByDescending(score => score.Id)
        .Take(count)
        .ToList();
}
}