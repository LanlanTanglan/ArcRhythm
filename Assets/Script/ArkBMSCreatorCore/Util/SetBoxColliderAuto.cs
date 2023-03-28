using UnityEngine;

//制动设置Collider的大小
public class SetBoxColliderAuto : MonoBehaviour
{
    void Awake()
    {
        // 获取RectTransform组件
        RectTransform rectTransform = GetComponent<RectTransform>();

        // 获取BoxCollider2D组件
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();

        // 设置BoxCollider2D的大小和RectTransform的大小相同
        boxCollider.size = rectTransform.sizeDelta;
    }
}