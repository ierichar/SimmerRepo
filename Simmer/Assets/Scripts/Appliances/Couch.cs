using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Simmer.Interactable;
using Simmer.Player;
using Simmer.CustomTime;

public class Couch : MonoBehaviour
{
    protected InteractableBehaviour _interactable;
    SpriteRendererManager highlightTarget;
    bool interacted = false;

    public Canvas fadeOutCanvas;
    private Image fadeOutImg;
    private TextMeshProUGUI fadeOutText; 

    public PlayerCurrency money;

    void Start() {
        _interactable = GetComponent<InteractableBehaviour>();
        highlightTarget
            = GetComponentInChildren<SpriteRendererManager>();

        highlightTarget.Construct();

        _interactable.Construct(InteractCallBack, highlightTarget, true);

        fadeOutImg = fadeOutCanvas.GetComponentInChildren<Image>();
        fadeOutText = fadeOutCanvas.GetComponentInChildren<TextMeshProUGUI>();
        fadeOutText.enabled = false;
    }

    public void InteractCallBack() {
        //if(TimeManager.AM || TimeManager.Hour<8){
        //    _interactable.isInteractToggle = false;
        //    return;
        //}
        if(!interacted) {
            StartCoroutine(fadeOut());
        } else {
            StartCoroutine(fadeIn());
        }
    }

    public IEnumerator fadeOut() {
        interacted = true;
        //change alpha of some kind of black screen
        while(fadeOutImg.color.a < 1 && interacted) {
            var temp = fadeOutImg.color;
            temp.a += 0.075f;
            fadeOutImg.color = temp;
            yield return new WaitForSeconds(0.075f);

        }

        //give money
        if(interacted) {
            fadeOutText.enabled = true;
            money.addMoney(2);
        }
    }

    public IEnumerator fadeIn() {
        fadeOutText.enabled = false;
        interacted = false;
        bool timeSet = false;
        while(fadeOutImg.color.a > 0 && !interacted) {
            var temp = fadeOutImg.color;
            temp.a -= 0.075f;
            fadeOutImg.color = temp;
            yield return new WaitForSeconds(0.075f);
            if(temp.a >= 0.5f && !timeSet){
                TimeManager.SetTime();
                timeSet = true;
            }
        }
        
    }
}