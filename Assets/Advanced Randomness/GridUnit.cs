using UnityEngine;

namespace Backgroud
{
    public class GridUnit : MonoBehaviour
    {
        public SpriteRenderer sprite;

        int posX;
        int posY;

        public void SetPos(int x, int y) {
            posX = x;
            posY = y;
        }

        void Start()
        {
            int a = Random.Range(0, StoneScatterer.instance.gridWidth);
            int b = Random.Range(0, StoneScatterer.instance.gridHeight);
        
            sprite.transform.localPosition = new Vector3(a * StoneScatterer.instance.w, b * StoneScatterer.instance.h, 0);
            sprite.sprite = StoneScatterer.instance.sprites[Random.Range(0, StoneScatterer.instance.sprites.Length)];
        }

        void Update() {
            if(!StoneScatterer.instance.InRange(posX, posY)) {
                Destroy(this.gameObject);
            }
        }
    }
}
