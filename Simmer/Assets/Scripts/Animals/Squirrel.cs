using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : MonoBehaviour
{
    Vector3 lerp;
    Vector3 startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private float lerpDuration;
    [SerializeField] private float delayTime;
    private bool delayStart = false;
    float deltaPos = 0.1f;
    const int ticks = 50;
    int currentTick = 0;
    float totalTicks;
    private bool finished;
    // Start is called before the first frame update
    void Start()
    {
        finished = false;
        delayStart = false;
        startPos = transform.position;
        totalTicks=lerpDuration*ticks;
        //StartCoroutine(finishDelay());
    }

    void FixedUpdate(){
        if(delayStart || finished) return;
        float amount = currentTick/totalTicks;
        transform.position = Vector2.Lerp(startPos, endPos.position, amount);
        currentTick++;
        if(currentTick > totalTicks){
            currentTick = 0;
            finished = true;
        }
    }
    private IEnumerator finishDelay(){
        yield return new WaitForSeconds(delayTime+Random.Range(0, 5));
        delayStart = false;
    }
}
