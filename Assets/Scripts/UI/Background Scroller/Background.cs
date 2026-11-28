using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private RawImage _scrollingBG;
    [SerializeField] private float _x, _y;

    void Update()
    {
        _scrollingBG.uvRect = new Rect(_scrollingBG.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _scrollingBG.uvRect.size);
    }
}
