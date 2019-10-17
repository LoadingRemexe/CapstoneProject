using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    [SerializeField] float timerTillBreak;
    PlayerMove playerMove;


    // Start is called before the first frame update
    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(playerMove.transform.position);

        timerTillBreak -= Time.deltaTime;

        if (timerTillBreak <= 0.0f)
        {
            Destroy(gameObject);
        }
    }


 
}
