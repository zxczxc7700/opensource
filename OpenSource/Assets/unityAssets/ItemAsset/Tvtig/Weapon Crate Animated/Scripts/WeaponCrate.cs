using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponCrate : MonoBehaviour
{
    [SerializeField]
    public GameObject[] items = new GameObject[21];
    public GameObject spawnPoint;
    public GameObject openEffect;

    GameObject player;
    Player pStat;
    Animator _animator;

    bool isPlayerEnter;
    bool isClosed;

    void Start()
    {
        pStat = GameObject.Find("PLAYER").GetComponent<Player>();
        _animator = GetComponentInChildren<Animator>();
        isClosed = false;
    }

    public void Open()
    {
        if (!isClosed && pStat.coin >= 10)
        {
            _animator.SetTrigger("doOpen");
            pStat.coin -= 10;
            StartCoroutine(OpenBox());
            isClosed = true;
        }
    }

    IEnumerator OpenBox()
    {
        yield return new WaitForSeconds(0.8f);

        GameObject openeffect = Instantiate(openEffect, transform.position + new Vector3(0, 1.0f, 0), transform.rotation);

        yield return new WaitForSeconds(0.2f);

        int grade = Random.Range(0, 100);

        if (grade < 70)  // 0 - 69
        {
            int i = Random.Range(7, 14);    // 7 - 13
            spawnPoint = Instantiate(items[i], spawnPoint.transform.position + new Vector3(0, 1.3f, 0), items[i].transform.rotation, null);
            Debug.Log("grade : " + grade + " i : " + i);
        }
        else if (grade >= 70 && grade < 90) // 70 - 89
        {
            int i = Random.Range(14, 21);    // 14 - 20
            spawnPoint = Instantiate(items[i], spawnPoint.transform.position + new Vector3(0, 1.3f, 0), items[i].transform.rotation, null);
            Debug.Log("grade : " + grade + " i : " + i);
        }
        else    // 90 - 100
        {
            int i = Random.Range(0, 7);    // 0 - 6
            spawnPoint = Instantiate(items[i], spawnPoint.transform.position + new Vector3(0, 1.3f, 0), items[i].transform.rotation, null);
            Debug.Log("grade : " + grade + " i : " + i);
        }

        yield return new WaitForSeconds(1.0f);

        Destroy(openeffect);
    }
}
