using UnityEngine;

public class Hud : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.initialPlay) {
            startMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameOver) {
            gameOverMenu.SetActive(true);
        }
    }
}
