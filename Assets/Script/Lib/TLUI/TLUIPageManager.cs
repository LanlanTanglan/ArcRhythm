using System.Collections.Generic;
using System.Collections;
using TLTemplate;
using UnityEngine;

namespace TLUI
{
    public class TLUIPanelManager : Singleton<TLUIPanelManager>
    {
        public string _root = "Prefab/UI/";
        public Stack<TLPanel> _panelStack = new Stack<TLPanel>();
        public GameObject _canvas = null;
        public TLPanel _currentPage = null;

        /// <summary>
        /// Panel预制体
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <typeparam name="GameObject"></typeparam>
        /// <returns></returns>
        public Dictionary<string, GameObject> _panelPrefabObjs = new Dictionary<string, GameObject>();

        /// <summary>
        /// Panel实例化的
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <typeparam name="GameObject"></typeparam>
        /// <returns></returns>
        public Dictionary<string, GameObject> _panelObjs = new Dictionary<string, GameObject>();

        public TLUIPanelManager LoadPanelPrefab(string name)
        {
            if (!_panelPrefabObjs.ContainsKey(name))
            {
                GameObject o = Resources.Load<GameObject>(_root + name);
                _panelPrefabObjs[name] = o;
                Debug.Log($"加载{name}");
            }
            return this;
        }

        public TLUIPanelManager LoadPanelPrefab(string root, string name)
        {
            if (!_panelPrefabObjs.ContainsKey(name))
            {
                GameObject o = Resources.Load<GameObject>(root + name);
                _panelPrefabObjs[name] = o;
                Debug.Log($"加载{name}");
            }
            return this;
        }

        /// <summary>
        /// 打开某一个Panel但不显示
        /// </summary>
        public void OpenPanel(string name)
        {
            if (!_panelObjs.ContainsKey(name))
            {
                if (_canvas == null)
                {
                    _canvas = GameObject.Find("Canvas");
                }
                _panelObjs[name] = (GameObject)Instantiate(_panelPrefabObjs[name], _canvas.transform, false);
            }
        }

        /// <summary>
        /// 显示Panel
        /// </summary>
        /// <param name="name"></param>
        public void ShowPanel(string name)
        {
            if (_currentPage != null)
                _currentPage.Hide();
            _panelStack.Push(_currentPage);
            _currentPage = _panelObjs[name].GetComponent<TLPanel>();
            _currentPage.Show();
        }

        /// <summary>
        /// 退出Panel
        /// </summary>
        public void Leave1Panel()
        {
            _currentPage.Hide();
            _currentPage = _panelStack.Pop();
            if (_currentPage != null)
                _currentPage.Show();
        }

        /// <summary>
        /// 销毁所有的物体
        /// </summary>
        /// <param name="names"></param>
        public void ClosePanel(List<string> names)
        {
            foreach (string s in names)
            {
                Destroy(_panelObjs[s]);
                _panelObjs.Remove(s);
                _panelPrefabObjs.Remove(s);
            }
        }
    }
}

