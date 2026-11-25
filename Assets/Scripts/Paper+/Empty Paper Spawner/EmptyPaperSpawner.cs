using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPaperSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _emptyPaperPrefab;

    private Vector2 _mousePosition;

    private void Update()
    {
        _mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        Instantiate(_emptyPaperPrefab, _mousePosition, Quaternion.identity);
        AudioSystem.Instance.PlaySound(SoundType.SFX, 0);
    }

}
