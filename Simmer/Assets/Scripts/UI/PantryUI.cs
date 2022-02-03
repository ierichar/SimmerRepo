using UnityEngine;
using UnityEngine.UI;

public class PantryUI : MonoBehaviour
{
    public GameObject pantryUI;
    private GraphicRaycaster graphicRaycaster; 
    // Start is called before the first frame update
    void Start()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        pantryUI.SetActive(false);
        graphicRaycaster.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pantry")){
            graphicRaycaster.enabled = !graphicRaycaster.enabled;
            pantryUI.SetActive(!pantryUI.activeSelf);
        }
    }
}
