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
        int obstacleIndex;
        float yPosition;
        float xPosition;

        for (int i = 0; i < 8; i++) {
            if (AudioSpectrum.freqBands[i] >= trigggerValue) {
                float camTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y;

                obstacleIndex = GetObstacleIndex(i);

                GameObject obstacle = prefabs[obstacleIndex];
                Vector3 obstaclePos = obstacle.transform.position;

                yPosition = lastPosition == 0 ? camTop : lastPosition + obstacleOffset;
                xPosition = GetXPosition(obstacleIndex, lastSpawn);
                
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

    private int GetObstacleIndex(int i) {
        if (i <= 2) {
            return 0;
        } else if (i > 2 && i <= 5) {
            return 1;
        } else {
            return 2;
        }
    }

    private float GetXPosition(int index, int side) {
        float[] acEdges = new float[2] { -1.774f, 1.774f };
        float[] windowEdges = new float[2] { -2.592f, 2.592f };
        float cactusPos = (Mathf.Round(Random.Range(-1.59f, 1.59f) * 100)) / 100.0f;

        void SetLastSpawn(int spawn) {
            lastSpawn = spawn == 1 ? 0 : 1;
        }

        switch (index) {
            case 0:
                SetLastSpawn(side);
                return acEdges[side];
            case 1:
                return cactusPos;
            case 2:
                SetLastSpawn(side);
                return windowEdges[side];
            default:
                return 0;
        }
    }
}
