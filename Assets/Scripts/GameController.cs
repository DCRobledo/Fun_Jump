using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField]
    private GameObject pickUpPrefab;

    [SerializeField]
    private float pickUpSpawnCooldown = 0.5f;

    private bool canSpawnPickUp = true;

    void Start()
    {
        instance = this;
    }

    private void FixedUpdate() {
        if(canSpawnPickUp) {
            SpawnPickUp();

            StartCoroutine(SpawnPickUpCooldown());
        }
    }

    private IEnumerator SpawnPickUpCooldown() {
        canSpawnPickUp = false;

        yield return new WaitForSeconds(pickUpSpawnCooldown);

        canSpawnPickUp = true;
    }

    private void SpawnPickUp() {
        Vector3 pickUpPosition = new Vector3(12f, Random.Range(-1.5f, HUD.instance.GetJumpForceLevel() * 1.2f), 0f);
        GameObject pickUp = Instantiate(pickUpPrefab, pickUpPosition, Quaternion.identity);

        float directionSelector = Random.Range(-1, 2);

        if(directionSelector > 0) {
            Vector3 position = pickUp.transform.position;

            position.x *= -1;

            pickUp.transform.position = position;

            pickUp.GetComponent<PickUp>().SwitchDirection();
        }
    }
}
