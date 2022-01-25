using UnityEngine;

public class PantryUI : MonoBehaviour
{
    public GameObject pantryUI;
    // Start is called before the first frame update
    void Start()
    {
        pantryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pantry")){
            pantryUI.SetActive(!pantryUI.activeSelf);
        }
    }
}
