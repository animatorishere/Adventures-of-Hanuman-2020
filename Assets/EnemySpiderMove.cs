using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpiderMove : MonoBehaviour
{
    public float movePos;
    internal float startPos;
   // public float timeTaken;
    public float currentSpeed;
    internal float targetPos;
    public bool pauseMove;


    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position.x;
       
        targetPos = movePos;
    }
    //void Start()
    //{
    //   // transform.DOMoveX(movePos, timeTaken,false).OnComplete(OnMovePosReached);
      
    //}

    // Update is called once per frame
    //void OnMovePosReached()
    //{
    //    transform.DOMoveX(startPos, timeTaken, false).OnComplete(Start);
    //}

    void Update()
    {
        if (!pauseMove) { 
        transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, targetPos, Time.deltaTime * currentSpeed),
            transform.position.y, transform.position.z);
        }

        if (transform.position.x <= movePos)
        {
            targetPos = startPos;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90f, transform.eulerAngles.z);
        }

        else if (transform.position.x >= startPos)
        {
            targetPos = movePos;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,-90f, transform.eulerAngles.z);

        }


    }
}
