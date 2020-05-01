using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public bool playAnimation = true;

    void Start()
    {
        if (!GameManager.playIntro) gameObject.SetActive(false);
    }

    void Update() {
        GameManager.playIntro = playAnimation;
    }
}
