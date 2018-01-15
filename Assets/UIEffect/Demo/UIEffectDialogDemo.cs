using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Coffee.UIExtensions
{
	public class UIEffectDialogDemo : MonoBehaviour
	{
		public void Open()
		{
			gameObject.SetActive(true);
			GetComponent<Animator>().SetTrigger("Open");
		}

		public void Close()
		{
			GetComponent<Animator>().SetTrigger("Close");
		}

		public void Closed()
		{
			gameObject.SetActive(false);
		}

		public void CaptureBackground()
		{
			GetComponentInChildren<UIEffectCapturedImage>().Capture();
		}

		void OnDestroy()
		{
			if (shared_0 != null)
			{
				shared_0.Unload(true);
				shared_0 = null;
			}
		}

		AssetBundle shared_0;

		public void Load(string name)
		{
			if (shared_0 == null)
			{
				shared_0 = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/shared_0");
				Debug.Log("Shared AssetBundle loaded " + shared_0);
			}
			var bundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + name);
			var prefab = bundle.LoadAsset<GameObject>(name);
			bundle.Unload(false);
			bundle = null;

			var go = GameObject.Instantiate(prefab,transform, false);
		}


		#if UNITY_EDITOR
		[MenuItem("Export Package/Assetbundle Build")]
		static void build()
		{
			BuildPipeline.BuildAssetBundles(
				Application.streamingAssetsPath,
				BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.DeterministicAssetBundle,
				EditorUserBuildSettings.activeBuildTarget
			);
		}
		#endif

	}
}