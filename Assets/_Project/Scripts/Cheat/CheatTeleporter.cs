using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatTeleporter : MonoBehaviour
{
    [SerializeField] private List<SpawnPosition>  spawnPositionList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Teleport(spawnPositionList[0]);
        }
    }

    private void Teleport(SpawnPosition spawnPosition)
    {
        GameManager.Instance.GetPlayer().GetComponent<CharacterController>().enabled = false;
        GameManager.Instance.GetPlayer().transform.SetPositionAndRotation(spawnPosition.Position, spawnPosition.Rotation);
        GameManager.Instance.GetPlayer().GetComponent<CharacterController>().enabled = true;
    }
}
