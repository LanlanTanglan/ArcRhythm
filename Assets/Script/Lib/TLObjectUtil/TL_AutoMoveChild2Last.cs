using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TLObjectUtil
{
    public class TL_AutoMoveChild2Last : MonoBehaviour
    {
        public Transform parentTransform; // 父物体的Transform组件
        public Transform childTransform; // 需要保持在最后面的子物体的Transform组件
        private int childCount; // 子物体数量

        private void Start()
        {
            // 获取初始子物体数量
            childCount = parentTransform.childCount;

            // 将子物体移动到父物体最后面
            MoveChildToLast();
        }

        private void Update()
        {
            // 如果子物体数量发生变化，将子物体移动到父物体最后面
            if (childCount != parentTransform.childCount)
            {
                MoveChildToLast();
                childCount = parentTransform.childCount;
            }
        }

        private void MoveChildToLast()
        {
            // 获取子物体在父物体中的索引
            int childIndex = childTransform.GetSiblingIndex();

            // 将子物体移动到父物体最后面
            childTransform.SetSiblingIndex(parentTransform.childCount - 1);
        }
    }
}

