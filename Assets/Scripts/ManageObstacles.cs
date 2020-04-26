using UnityEngine;

public class ManageObstacles : MonoBehaviour
{
    public int obstacleOffset;
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

        for (int i = 0; i < 8; i++) {
            if (AudioSpectrum.freqBands[i] >= trigggerValue) {
                float camTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y;

                GameObject obstacle = prefabs[i];
                Vector3 obstaclePos = obstacle.transform.position;

                yPosition = lastPosition == 0 ? camTop : lastPosition + obstacleOffset;
                xPosition = GetXPosition(i, lastSpawn);
                
                if(yPosition < camTop) {
                    yPosition = camTop;
                }

                Vector3 position = new Vector3(xPosition, yPosition, obstaclePos.z);
                Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));

                Instantiate(obstacle, position, obstacle.transform.rotation.z != 0 ? rotation : Quaternion.identity);
                
                posY = position.y;
                lastPosition = yPosition;
            }
        }
    }

    private float GetXPosition(int index, int side) {
        float midPos = (Mathf.Round(Random.Range(-1.59f, 1.59f) * 100)) / 100.0f;
        float[] lowPos = new float[2] { -1.774f, 1.774f };
        float[] highPos = new float[2] { -2.592f, 2.592f };

        void SetLastSpawn(int spawn) {
            lastSpawn = spawn == 1 ? 0 : 1;
        }

        if (index <= 2) {
            SetLastSpawn(side);
            return lowPos[side];
        } else if (index > 2 && index <= 5) {
            return midPos;
        } else {
            SetLastSpawn(side);
            return highPos[side];
        }
    }
}
