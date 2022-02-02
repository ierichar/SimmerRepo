using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject shopUI;
    // Start is called before the first frame update
    void Start()
    {
        shopUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Shop")){
            shopUI.SetActive(!shopUI.activeSelf);
        }
    }
}
