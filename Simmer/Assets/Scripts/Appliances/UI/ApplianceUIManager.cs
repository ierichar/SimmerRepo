using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Simmer.Items;
public class ApplianceUIManager : MonoBehaviour
{
    public ApplianceSlotManager slots;
    private Transform _negativeFeedback;
    public GameObject _toggleButton { get; private set; }
    public void Construct(ItemFactory itemFactory){
        slots = gameObject.GetComponentInChildren<ApplianceSlotManager>();
        slots.Construct(itemFactory);
        _negativeFeedback = transform.GetChild(1);
        _negativeFeedback.gameObject.SetActive(false);
        _toggleButton = gameObject.GetComponentInChildren<Button>().gameObject;
    }

    public GameObject GetNegFeedbackObjRef(){
        return _negativeFeedback.gameObject;
    }

}
