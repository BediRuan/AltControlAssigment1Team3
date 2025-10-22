using System.Collections;
using UnityEngine;

public class C_KillThis : MonoBehaviour
{

    public float killTime;
    void Start()
    {
        StartCoroutine(killMe());
    }

    IEnumerator killMe()
    {
        yield return new WaitForSeconds(killTime);
        Destroy(gameObject);
    }
}
