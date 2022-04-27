using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    private Image fillImage;
    private float maxAmount;
    private float currAmount;

    public void Construct(float setMaxAmount){
        fillImage = transform.GetChild(0).GetComponent<Image>();
        currAmount = 0;
        maxAmount = setMaxAmount;
        setFill();
    }

    private void setFill(){
        fillImage.fillAmount = currAmount/maxAmount;
    }

    public void incrementFill(){
        currAmount += 1;
        if(currAmount >= maxAmount){
            currAmount = maxAmount;
            
        }
        setFill();
    }

    public void setMaxAmount(float amount){
        maxAmount = amount;
    }

    public void reset(){
        currAmount = 0;
        setFill();
    }
    public void changeColor(bool start){
        if(start){
            fillImage.color = new Color32(29, 214, 39, 180);
        }else{
            fillImage.color = new Color32(255, 0, 0, 255);
        }
        
        //green: 1DD623
        //red: FF0000
    }
}
