using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Simmer.Items;
public class ApplianceUIManager : MonoBehaviour
{
    public ApplianceSlotManager slots;
    private Transform _negativeFeedback;
    public void Construct(ItemFactory itemFactory){
        slots = gameObject.GetComponentInChildren<ApplianceSlotManager>();
        slots.Construct(itemFactory);
        _negativeFeedback = transform.GetChild(1);
        _negativeFeedback.gameObject.SetActive(false);
    }

    public GameObject GetNegFeedbackObjRef(){
        return _negativeFeedback.gameObject;
    }

}
