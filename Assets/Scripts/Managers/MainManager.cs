using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    #region Ball Fields.
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    #endregion

    #region Players Data.
    private PlayerData bestResult;
    #endregion

    #region UI Controls.
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private Text bestScoreText;
    #endregion

    #region Game Fields.
    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;
    #endregion

    #region Start, Update.
    void Start()
    {
        bestResult = DataManager.Instance.LoadData();
        bestScoreText.text = $"Best score: {bestResult?.Nickname} : {bestResult?.Result}";
        m_Points = DataManager.Instance.PlayerData.Result;
        scoreText.text = $"Score : {DataManager.Instance.PlayerData.Result.ToString()}";
        InitGame();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                StartGame();
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                RestartGame();
        }
        else if (FindObjectsOfType<Brick>().Length == 0)
            RestartGame();
    }
    #endregion

    #region Game Manipulation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="point"></param>
    void AddPoint(int point)
    {
        m_Points += point;
        scoreText.text = $"Score : {m_Points}";
        DataManager.Instance.PlayerData.Result += point;
    }

    /// <summary>
    /// 
    /// </summary>
    public void GameOver()
    {
        m_GameOver = true;
        gameOverText.SetActive(true);
        if (bestResult < DataManager.Instance.PlayerData)
            DataManager.Instance.SaveData();
    }

    /// <summary>
    /// 
    /// </summary>
    public void StartGame()
    {
        m_Started = true;
        float randomDirection = Random.Range(-1.0f, 1.0f);
        Vector3 forceDir = new Vector3(randomDirection, 1, 0);
        forceDir.Normalize();

        Ball.transform.SetParent(null);
        Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
    }

    /// <summary>
    /// 
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (m_GameOver)
            DataManager.Instance.PlayerData.Result = 0;
            
    }

    /// <summary>
    /// 
    /// </summary>
    public void InitGame()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }
    #endregion

    #region UI Manipulation.
    /// <summary>
    /// 
    /// </summary>
    public void ExitToMenu()
    {
        if (DataManager.Instance.PlayerData.Result > bestResult.Result)
            DataManager.Instance.SaveData();
        DataManager.Instance.PlayerData = null;
        SceneManager.LoadScene(0);
    }
    #endregion
}
