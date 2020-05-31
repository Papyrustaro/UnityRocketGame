using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;
//using System.Collections.Generic;

/// <summary>
/// Assets以下のFont置き換えツール
/// </summary>
//public class ReplaceFont : EditorWindow
//{
	/*string[] sarchDir = { "Assets" };

	private Font before;
	private Font after;

	[MenuItem("Tools/UI/Fontの置き換え")]
	static void Open()
	{
		var window = EditorWindow.GetWindow<ReplaceFont>();
		window.Show();
	}

	/// <summary>
	///  親や子オブジェクトも含めた範囲から指定のコンポーネントを全て取得する
	/// </summary>
	private static List<T> GetComponentsInParentAndChildren<T>(GameObject target) where T : UnityEngine.Component
	{
		var list = new List<T>(target.GetComponents<T>());
		list.AddRange(new List<T>(target.GetComponentsInChildren<T>(true)));
		list.AddRange(new List<T>(target.GetComponentsInParent<T>(true)));

		return list;
	}

	/// <summary>
	/// Prefab に_before Fontが割り当てれらている場合 _after Fontへ置き換えます。
	/// </summary>
	private void ReplaceBindFont(Font _before, Font _after)
	{
		var title = string.Format($"Replacing [ + {_before.name} から {_after.name}  へ置換中です]");
		var guids = AssetDatabase.FindAssets("t:prefab", sarchDir);
		var isSave = false;

		for (int ii = 0; ii < guids.Length; ii++)
		{
			var guid = guids[ii];
			var guidPath = AssetDatabase.GUIDToAssetPath(guid);
			EditorUtility.DisplayProgressBar(title, guidPath, (float)ii / (float)guids.Length);
			var go = AssetDatabase.LoadAssetAtPath<GameObject>(guidPath);
			if (go != null)
			{
				var textList = GetComponentsInParentAndChildren<Text>(go);

				foreach (var text in textList)
				{
					if (text.font.name == _before.name)
					{
						text.font = _after;
						text.material = _after.material;
						EditorUtility.SetDirty(text);
					}
				}
				isSave = true;
			}
		}
		if (isSave)
		{
			AssetDatabase.SaveAssets();
		}
		EditorUtility.ClearProgressBar();
	}

	/// <summary>
	///  GUI更新
	/// </summary>
	void OnGUI()
	{
		EditorGUILayout.LabelField("Assets以下に配置されたFontを一括変更します。");

		before = EditorGUILayout.ObjectField("Before Font", before, typeof(Font), true) as Font;
		after = EditorGUILayout.ObjectField("After Font", after, typeof(Font), true) as Font;

		if (before == null || after == null)
		{
			return;
		}

		if (GUILayout.Button("Replace font in all assets"))
		{
			ReplaceBindFont(before, after);
		}
	}*/
//}