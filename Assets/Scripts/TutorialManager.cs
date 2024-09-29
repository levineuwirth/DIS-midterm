using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    
    public GameObject[] popUps;
    private int popUpsIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < popUps.Length; i++) {
            if(i == popUpsIndex) {
                popUps[i].SetActive(true);
            }
            else {
                popUps[i].SetActive(false);
            }
        }

        if (popUpsIndex == 0) {
            if(Input.GetKeyDown(PlayerController.Instance.left) || Input.GetKeyDown(PlayerController.Instance.right)) {
                popUpsIndex++;
            }
        }
        else if (popUpsIndex == 1) {
            if(Input.GetKeyDown(PlayerController.Instance.jump)) {
                popUpsIndex++;
            }
        }
    }
}
