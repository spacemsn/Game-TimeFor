using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class SpawnPoint
{
    [Header("����� ������")]
    public int indexSpanwScene;
    public Vector3 position;
    public Quaternion rotation;

    [Header("����������� ������")]
    public bool isSpawn;
}

public class SpawnContoller : MonoBehaviour
{
    public static Action onPlayerSceneLoaded;
    public static bool isPlayerSceneLoaded = false;

    [Header("���������� ������")]
    [SerializeField] private PlayerEntryPoint playerEntry;

    [Header("����� ������")]
    public List<SpawnPoint> spawnPoints;
    public bool firstLoadScene = true;

    [Header("������ �����")]
    public int indexScene;

    private void Start()
    {
        playerEntry = GameObject.FindObjectOfType<PlayerEntryPoint>();
        indexScene = SceneManager.GetActiveScene().buildIndex;

        if (playerEntry.saveData.savedData.Count > 0)
        {
            var data = playerEntry.saveData.savedData[playerEntry.saveData.savedData.Count - 1];
            if (data.levelId == SceneManager.GetActiveScene().buildIndex)
            {
                playerEntry.currentPlayer = Instantiate(playerEntry.playerPrefab, data.position, data.rotation);
                playerEntry.currentCamera = Instantiate(playerEntry.cameraPrefab, data.position, data.rotation).GetComponent<Camera>();
                playerEntry.currentFreeLook = Instantiate(playerEntry.freeLookPrefab, data.position, data.rotation).GetComponent<CinemachineFreeLook>();
                playerEntry.GetComponents();

                isPlayerSceneLoaded = true;
                firstLoadScene = false;
            }
        }

        if (firstLoadScene && spawnPoints[indexScene].isSpawn == true)
        {
            playerEntry.currentPlayer = Instantiate(playerEntry.playerPrefab, spawnPoints[indexScene].position, spawnPoints[indexScene].rotation);
            playerEntry.currentCamera = Instantiate(playerEntry.cameraPrefab, spawnPoints[indexScene].position, spawnPoints[indexScene].rotation).GetComponent<Camera>();
            playerEntry.currentFreeLook = Instantiate(playerEntry.freeLookPrefab, spawnPoints[indexScene].position, spawnPoints[indexScene].rotation).GetComponent<CinemachineFreeLook>();
            playerEntry.GetComponents();

            isPlayerSceneLoaded = true;
        }

        if (isPlayerSceneLoaded) { onPlayerSceneLoaded.Invoke(); }
    }

    private void OnDisable()
    {
        isPlayerSceneLoaded = false;
        onPlayerSceneLoaded.Invoke();
    }
}
