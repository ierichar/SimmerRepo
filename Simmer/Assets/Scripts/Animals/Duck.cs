using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    [SerializeField] private Vector3 leftPoint, rightPoint;
    [SerializeField] private float lerpDuration;
    [SerializeField] private float startStep;
    private SpriteRenderer _spriteRenderer;

    private bool movingRight;
    Vector3 resultVec;
    private float currStep;
    private float totalSteps;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_spriteRenderer.flipX==true){
            movingRight = false;
        }else if(_spriteRenderer.flipX==false){
            movingRight = true;
        }
        resultVec = rightPoint-leftPoint;
        totalSteps = 50*lerpDuration;
        currStep = totalSteps*startStep;
        transform.position = Vector2.Lerp(leftPoint, rightPoint, startStep);
    }

    void FixedUpdate(){
        if(transform.position == rightPoint){
            movingRight = false;
            _spriteRenderer.flipX = true;
        }else if(transform.position == leftPoint){
            movingRight = true;
            _spriteRenderer.flipX = false;
        }
        if(movingRight){
            currStep++;
            transform.position = Vector2.Lerp(leftPoint, rightPoint, currStep/totalSteps);
        }else{
            currStep--;
            transform.position = Vector2.Lerp(leftPoint, rightPoint, currStep/totalSteps);
        }
    }
}
