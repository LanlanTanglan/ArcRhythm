using UnityEngine;

public abstract class Singleton<T> : Singleton where T : MonoBehaviour
{
	public static T Instance
	{
		get
		{
			T result;
			if (Singleton.Quitting)
			{
				Debug.Log(string.Format("[{0}<{1}>] Instance will not be returned because the application is quitting.", "Singleton", typeof(T)));
				result = default(T);
				return result;
			}
			object @lock = Singleton<T>.Lock;
			lock (@lock)
			{
				if (Singleton<T>._instance != null)
				{
					result = Singleton<T>._instance;
				}
				else
				{
					T[] array = Object.FindObjectsOfType<T>();
					int num = array.Length;
					if (num > 0)
					{
						if (num == 1)
						{
							result = (Singleton<T>._instance = array[0]);
						}
						else
						{
							Debug.LogWarning(string.Format("[{0}<{1}>] There should never be more than one {2} of type {3} in the scene, but {4} were found. The first instance found will be used, and all others will be destroyed.", new object[]
							{
								"Singleton",
								typeof(T),
								"Singleton",
								typeof(T),
								num
							}));
							for (int i = 1; i < array.Length; i++)
							{
								Object.Destroy(array[i]);
							}
							result = (Singleton<T>._instance = array[0]);
						}
					}
					else
					{
						Debug.Log(string.Format("[{0}<{1}>] An instance is needed in the scene and no existing instances were found, so a new instance will be created.", "Singleton", typeof(T)));
						result = (Singleton<T>._instance = new GameObject(string.Format("({0}){1}", "Singleton", typeof(T))).AddComponent<T>());
					}
				}
			}
			return result;
		}
	}

	private void Awake()
	{
		if (this._persistent)
		{
            Debug.Log("无法销毁!");
			Object.DontDestroyOnLoad(base.gameObject);
		}
		this.OnAwake();
	}

	protected virtual void OnAwake()
	{
	}

	private static T _instance;

	private static readonly object Lock = new object();

	[SerializeField]
	private bool _persistent = true;
}

public abstract class Singleton : MonoBehaviour
{

	public static bool Quitting { get; private set; }

	private void OnApplicationQuit()
	{
		Singleton.Quitting = true;
	}
}
