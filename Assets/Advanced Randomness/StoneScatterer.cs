using UnityEngine;

namespace Backgroud
{
    public class StoneScatterer : MonoBehaviour
    {
        public static StoneScatterer instance;

        private void Awake() {
            instance = this;    
        }

        public int gridCountX;
        public int gridCountY;

        public int gridWidth;
        public int gridHeight;
        [HideInInspector]
        public int seed;
        [HideInInspector]
        public float w = 1.28f;
        [HideInInspector]
        public float h = 1.28f;

        int oldX;
        int oldY;

        public Sprite[] sprites;
        public GameObject baseObject;
        GameObject[][] spawns;
        private void Start() {
            seed = Random.Range(0, int.MaxValue);    
            Bounds baseBounds = sprites[0].bounds;
            w = baseBounds.size.y;
            h = baseBounds.size.x;

            int x = (int)Mathf.Round(Camera.main.transform.position.x / (gridWidth * w));
            int y = (int)Mathf.Round(Camera.main.transform.position.y / (gridHeight * h));


            spawns = new GameObject[gridCountX][];
            for(int i = 0; i < gridCountX; i++)
            {
                spawns[i] = new GameObject[gridCountY];
                for(int j = 0; j < gridCountY; j++) {
                    spawns[i][j] = Instantiate(baseObject, GetPos(i, j, x, y), Quaternion.identity);
                    spawns[i][j].GetComponent<GridUnit>().SetPos(x + i - gridCountX/2, y + j - gridCountY/2);
                    spawns[i][j].transform.parent = this.transform;
                }
            }
        }

        Vector3 GetPos(int i, int j, int cameraX, int cameraY) {
            return new Vector3(
                (cameraX + i - Mathf.CeilToInt(gridCountX/2)) * w * gridWidth,
                (cameraY + j - Mathf.CeilToInt(gridCountY/2)) * h * gridHeight,
                10);
        }

        public bool InRange(int gX, int gY) {
            int x = (int)Mathf.Round(Camera.main.transform.position.x / (gridWidth * w));
            int y = (int)Mathf.Round(Camera.main.transform.position.y / (gridHeight * h));

            if(gX < x - gridCountX/2 || gX > x + gridCountX/2) return false;
            if(gY < y - gridCountY/2 || gY > y + gridCountY/2) return false;
            return true;
        }

        void Update() {
            int x = (int)Mathf.Round(Camera.main.transform.position.x / (gridWidth * w));
            int y = (int)Mathf.Round(Camera.main.transform.position.y / (gridHeight * h));

            if(oldX > x) {
                SpawnVertically(0, x, y);
            }
            else if(oldX < x) {
                SpawnVertically(gridCountX - 1, x, y);
            }

            if(oldY > y) {
                SpawnHorizontally(0, x, y);
            }
            else if(oldY < y) {
                SpawnHorizontally(gridCountY - 1, x, y);
            }


            oldX = x;
            oldY = y;
        }

        void SpawnVertically(int i, int x, int y) {
            for(int j = 0; j < gridCountY; j++) {
                spawns[i][j] = Instantiate(baseObject, GetPos(i, j, x, y), Quaternion.identity);
                spawns[i][j].GetComponent<GridUnit>().SetPos(x + i - gridCountX/2, y + j - gridCountY/2);
                spawns[i][j].transform.parent = this.transform;
            }
        }

        void SpawnHorizontally(int j, int x, int y) {
            for(int i = 0; i < gridCountX; i++) {
                spawns[i][j] = Instantiate(baseObject, GetPos(i, j, x, y), Quaternion.identity);
                spawns[i][j].GetComponent<GridUnit>().SetPos(x + i - gridCountX/2, y + j - gridCountY/2);
                spawns[i][j].transform.parent = this.transform;
            }
        }


        private void OnDrawGizmosSelected() {
            float tw = 1.28f;
            float th = 1.28f;
            int x = (int)Mathf.Round(Camera.main.transform.position.x / (gridWidth * tw));
            int y = (int)Mathf.Round(Camera.main.transform.position.y / (gridHeight * th));

            for (int i = 0; i < gridCountX; i++)
            {
                for (int j = 0; j < gridCountY; j++)
                {
                    Debug.Log(x + i - Mathf.FloorToInt(gridCountX/2));
                    Vector3 pos = new Vector3(
                        (x + i - Mathf.CeilToInt(gridCountX/2)) * tw * gridWidth,
                        (y + j - Mathf.CeilToInt(gridCountY/2)) * th * gridHeight,
                        0);
                    pos.x += (gridWidth * tw) / 2;
                    pos.y += (gridHeight * th) / 2;
                    Gizmos.DrawCube(pos, new Vector3(tw * gridWidth - 0.1f, th * gridHeight - 0.1f, 1));
                }
            }
        }

    }
}