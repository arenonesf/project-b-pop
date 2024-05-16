using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatSceneChanger : MonoBehaviour
{
    [SerializeField] private List<SpawnPosition> spawnPositionList;
    [SerializeField] private List<SceneReference> sceneReferenceList;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeScene(sceneReferenceList[0], spawnPositionList[0]);
        }
    }

    private void ChangeScene(SceneReference scene, SpawnPosition spawnPosition)
    {
        SceneController.Instance.LoadScene(scene, spawnPosition);
    }
}
