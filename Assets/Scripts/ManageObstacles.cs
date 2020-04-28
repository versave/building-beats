using UnityEngine;

public class ManageObstacles : MonoBehaviour
{
    public float obstacleOffset;
    public float trigggerValue;
    public float spawnOffset;

    private int lastSpawn = 0;
    private float posY = 0;
    private float camY;
    private float lastPosition = 0;

    public GameObject[] prefabs;
    private Camera cam;

    void Start() {
        cam = Camera.main;
        camY = cam.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        camY = cam.transform.position.y;

        if(posY == 0 || camY > posY + spawnOffset) {
            CreateObstacles();
        }
    }

    void CreateObstacles() {
        float yPosition;
        float xPosition;
        float campTopAddition = 0.5f;

        for (int i = 0; i < 8; i++) {
            if (AudioSpectrum.freqBands[i] >= trigggerValue) {
                float camTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y;

                GameObject obstacle = prefabs[i];
                Vector3 obstaclePos = obstacle.transform.position;
                Quaternion rotation = GetRotation(i, lastSpawn);

                yPosition = lastPosition == 0 ? camTop + campTopAddition : lastPosition + obstacleOffset;
                xPosition = GetXPosition(obstacle, i, lastSpawn);
                
                if(yPosition < camTop) {
                    yPosition = camTop + campTopAddition;
                }

                Vector3 position = new Vector3(xPosition, yPosition, obstaclePos.z);

                Instantiate(obstacle, position, rotation);
                
                posY = position.y;
                lastPosition = yPosition;
            }
        }
    }

    private float GetXPosition(GameObject obstacle, int index, int side) {
        float lowPos = side == 0 ? obstacle.transform.position.x : -obstacle.transform.position.x;
        float midPos = (Mathf.Round(Random.Range(-1.662f, 1.662f) * 100)) / 100.0f;
        float highPos = side == 0 ? obstacle.transform.position.x : -obstacle.transform.position.x;

        void SetLastSpawn(int spawn) {
            lastSpawn = spawn == 1 ? 0 : 1;
        }

        if (index <= 2) {
            SetLastSpawn(side);
            return lowPos;
        } else if (index > 2 && index <= 5) {
            return midPos;
        } else {
            SetLastSpawn(side);
            return highPos;
        }
    }

    private Quaternion GetRotation(int index, int side) {
        if (index > 2 && index <= 5) {
            return Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        } else {
            return side == 0 ? Quaternion.Euler(0.0f, 0.0f, 0.0f) : Quaternion.Euler(0.0f, 180f, 0.0f);
        }
    }
}
