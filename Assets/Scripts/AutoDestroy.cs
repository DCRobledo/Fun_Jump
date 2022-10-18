using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)]
    private float delay;

    void Start()
    {
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy() {
        yield return new WaitForSeconds(delay);

        Destroy(this.gameObject);
    }
}
