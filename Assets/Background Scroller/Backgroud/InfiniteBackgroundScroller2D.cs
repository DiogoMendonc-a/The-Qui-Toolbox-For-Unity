using System.Collections.Generic;
using UnityEngine;

namespace Backgroud
{
    public class InfiniteBackgroundScroller2D : MonoBehaviour
    {
        public GameObject background;
        List<GameObject> bgs;

        public int sideCount;

        float height;
        float width;

        private void Start() {
            InitializeBackgrounds();
            UpdateBackGrounds();
        }

        void InitializeBackgrounds() {
            bgs = new List<GameObject>();

            Bounds baseBounds = background.GetComponent<SpriteRenderer>().bounds;
            height = baseBounds.size.y;
            width = baseBounds.size.x;
    
            for (int i = 0; i < 9; i++)
            {
                bgs.Add(Instantiate(background, Vector3.zero, Quaternion.identity));
            }
        }

        void Update() {
            UpdateBackGrounds();
        }

        void UpdateBackGrounds() {
            Vector2Int centerPosition = new Vector2Int();
            centerPosition.x = (int)Mathf.Round(Camera.main.transform.position.x / width);
            centerPosition.y = (int)Mathf.Round(Camera.main.transform.position.y / height);

            for (int i = 0; i < bgs.Count; i++)
            {   
                Vector2Int pos = BGPosition(i, centerPosition);
                bgs[i].transform.position = new Vector3(pos.x * width, pos.y * height, 10);
            }   

        }

        Vector2Int BGPosition(int index, Vector2Int center) {
            int x = index / 3 - 1;
            int y = index % 3 - 1;
            return new Vector2Int(center.x + x, center.y + y);
        }
    }
}