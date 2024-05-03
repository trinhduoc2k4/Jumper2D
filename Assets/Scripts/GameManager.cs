using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player playerPrefab;
    public Platform platformPrefab;
    public float minSpawnX;
    public float maxSpawnX;
    public float minSpawnY;
    public float maxSpawnY;
    Player m_player;
    int m_score;
    public CamController mainCam;
    public float powerBarUp;
    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        base.Start();

        GameGUIManager.Ins.UpdateScoreCounting(m_score);

        AudioController.Ins.PlayBackgroundMusic();

    }

    public void PlayGame()
    {
        StartCoroutine(PlatFormInit());

        GameGUIManager.Ins.ShowGameGui(true);
    }

    IEnumerator PlatFormInit()
    {
        Platform platformClone = null;
        if(platformPrefab)
        {
            platformClone = Instantiate(platformPrefab, new Vector2(0, Random.Range(minSpawnY, maxSpawnY)), Quaternion.identity);
            platformClone.id = platformClone.gameObject.GetInstanceID();
        }

        yield return new WaitForSeconds(0.5f);

        if(playerPrefab)
        {
            m_player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            m_player.lastPlatformId = platformClone.id;
        }

        if(platformPrefab)
        {
            float spawnX = m_player.transform.position.x + minSpawnX;
            float spawnY = Random.Range(minSpawnY, maxSpawnY);  
            Platform platformClone02 = Instantiate(platformPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);
            platformClone02.id = platformClone02.gameObject.GetInstanceID();
        }
    }

    public void CreatePlatform()
    {
        if (!platformPrefab || !m_player) return;

        float spawnX = Random.Range(m_player.transform.position.x + minSpawnX, m_player.transform.position.x + maxSpawnX);

        float spawnY = Random.Range(minSpawnY, maxSpawnY);

        Platform platformClone = Instantiate(platformPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);
        platformClone.id = platformClone.gameObject.GetInstanceID();    
    }

    public void CreatePlatformAndLerp(float playerXPos)
    {
        if (mainCam) mainCam.LerpTrigger(playerXPos + minSpawnX);

        CreatePlatform();
    }

    public void AddScore()
    {
        m_score++;
        Prefs.bestScore = m_score;
        GameGUIManager.Ins.UpdateScoreCounting(m_score);
        AudioController.Ins.PlaySound(AudioController.Ins.getScore);
    }
}
