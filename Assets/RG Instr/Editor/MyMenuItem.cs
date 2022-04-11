using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//#if UNITY_EDITOR
using UnityEditor;

//#endif
public class MyMenuItem : Editor
	{
		[MenuItem("GameObject/UI/StyledUI/Icon Button")]
		static public void CreateIconButton()
		{
			var p = GetOrCreateCanvasGameObject().transform;

			var obj = new GameObject("Icon Button");
			Undo.RegisterCreatedObjectUndo(obj, "Create " + "Icon Button");
			obj.transform.SetParent(p, false);

			var vi = obj.AddComponent<Image>();
			var objRect = obj.GetComponent<RectTransform>();
			objRect.sizeDelta = Vector2.one * 60;
			vi.sprite = Resources.Load<Sprite>("FilledSprite");
			vi.preserveAspect = true;
			vi.color = new Color(0,0.3362752f,0.8018868f);
			var btn = obj.AddComponent<Button>();
			var outl = obj.AddComponent<Outline>();
			outl.effectColor = Color.white;
			outl.effectDistance = new Vector2(2,2);
			
			var icon = new GameObject("Icon");
			icon.transform.SetParent(obj.transform, false);
			var iconImg = icon.AddComponent<Image>();
			iconImg.sprite = Resources.Load<Sprite>("TestIcon");
			iconImg.preserveAspect = true;
			var Rect = icon.GetComponent<RectTransform>();
			Rect.anchorMin = Vector2.zero;
			Rect.anchorMax = Vector2.one;
			Rect.pivot = Vector2.one * 0.5f;
			Rect.offsetMin = new Vector2(5,10);
			Rect.offsetMax = new Vector2(-10,-5);
			//var randColor = Random.ColorHSV();
			//vi.color = new Color(randColor.r,randColor.g,randColor.b,1);
			//outl.effectColor = new Color(1-randColor.r,1-randColor.g,1-randColor.b,1);
			//iconImg.color = outl.effectColor;
		
		}
		
		[MenuItem("GameObject/UI/StyledUI/Outline Icon Button")]
		static public void CreateOutlineIconButton()
		{
			var p = GetOrCreateCanvasGameObject().transform;

			var obj = new GameObject("Outline Icon Button");
			Undo.RegisterCreatedObjectUndo(obj, "Create " + "Outline Icon Button");
			obj.transform.SetParent(p, false);

			var vi = obj.AddComponent<Image>();
			var objRect = obj.GetComponent<RectTransform>();
			objRect.sizeDelta = Vector2.one * 60;
			vi.sprite = Resources.Load<Sprite>("OutlineSprite");
			vi.preserveAspect = true;
			vi.color = Color.black;
			var btn = obj.AddComponent<Button>();
			
			var icon = new GameObject("Icon");
			icon.transform.SetParent(obj.transform, false);
			var iconImg = icon.AddComponent<Image>();
			iconImg.sprite = Resources.Load<Sprite>("TestIcon");
			iconImg.preserveAspect = true;
			var Rect = icon.GetComponent<RectTransform>();
			Rect.anchorMin = Vector2.zero;
			Rect.anchorMax = Vector2.one;
			Rect.pivot = Vector2.one * 0.5f;
			Rect.offsetMin = new Vector2(5,10);
			Rect.offsetMax = new Vector2(-10,-5);
			iconImg.color = Color.black;
		
		}
		
		[MenuItem("GameObject/UI/StyledUI/Switchable Icon Button")]
		static public void CreateSwitchableIconButton()
		{
			var p = GetOrCreateCanvasGameObject().transform;

			var obj = new GameObject("Switchable Icon Button");
			Undo.RegisterCreatedObjectUndo(obj, "Create " + "Switchable Icon Button");
			obj.transform.SetParent(p, false);

			var vi = obj.AddComponent<Image>();
			var objRect = obj.GetComponent<RectTransform>();
			objRect.sizeDelta = Vector2.one * 60;
			vi.sprite = Resources.Load<Sprite>("OutlineSprite");
			vi.preserveAspect = true;
			vi.color = Color.white;
			var btn = obj.AddComponent<Button>();
			obj.AddComponent<SwitchableButton>();
			
			var obj1 = new GameObject("fill");
			obj1.transform.SetParent(obj.transform, false);

			var vi1 = obj1.AddComponent<Image>();
			var objRect1 = obj1.GetComponent<RectTransform>();
			objRect.sizeDelta = Vector2.one * 60;
			vi1.sprite = Resources.Load<Sprite>("FilledSprite");
			vi.preserveAspect = true;
			vi1.color = Color.white;
			obj1.SetActive(false);
			var Rect2 = obj1.GetComponent<RectTransform>();
			Rect2.anchorMin = Vector2.zero;
			Rect2.anchorMax = Vector2.one;
			Rect2.pivot = Vector2.one * 0.5f;
			Rect2.offsetMin = new Vector2(0,0);
			Rect2.offsetMax = new Vector2(-0,-0);
			
			var icon = new GameObject("Icon");
			icon.transform.SetParent(obj.transform, false);
			var iconImg = icon.AddComponent<Image>();
			iconImg.sprite = Resources.Load<Sprite>("TestIcon");
			iconImg.preserveAspect = true;
			var Rect = icon.GetComponent<RectTransform>();
			Rect.anchorMin = Vector2.zero;
			Rect.anchorMax = Vector2.one;
			Rect.pivot = Vector2.one * 0.5f;
			Rect.offsetMin = new Vector2(5,10);
			Rect.offsetMax = new Vector2(-10,-5);
			iconImg.color = Color.white;

		
		}
		
		[MenuItem("GameObject/UI/StyledUI/Text Icon Button")]
		static public void CreateTextIconButton()
		{
			var p = GetOrCreateCanvasGameObject().transform;

			var obj = new GameObject("Text Icon Button");
			Undo.RegisterCreatedObjectUndo(obj, "Create " + "Text Icon Button");
			obj.transform.SetParent(p, false);

			var vi = obj.AddComponent<Image>();
			var objRect = obj.GetComponent<RectTransform>();
			objRect.sizeDelta = new Vector2(165,52);
			vi.sprite = Resources.Load<Sprite>("FilledSprite");
			vi.type = Image.Type.Sliced;
			vi.color = new Color(0,0.3362752f,0.8018868f);
			var btn = obj.AddComponent<Button>();
			
			var icon = new GameObject("Icon");
			icon.transform.SetParent(obj.transform, false);
			var iconImg = icon.AddComponent<Image>();
			iconImg.sprite = Resources.Load<Sprite>("TestIcon");
			iconImg.preserveAspect = true;
			var Rect = icon.GetComponent<RectTransform>();
			Rect.anchorMin = Vector2.zero;
			Rect.anchorMax = Vector2.one;
			Rect.pivot = Vector2.one * 0.5f;
			Rect.offsetMin = new Vector2(17,1.8f);
			Rect.offsetMax = new Vector2(-110,-1.8f);
			
			var text = new GameObject("Text");
			text.transform.SetParent(obj.transform, false);
			var textInput = text.AddComponent<Text>();
			textInput.text = "Button";
			textInput.fontStyle = FontStyle.Bold;
			textInput.fontSize = 20;
			textInput.alignment = TextAnchor.MiddleCenter;
			var Rect1 = text.GetComponent<RectTransform>();
			Rect1.anchorMin = Vector2.zero;
			Rect1.anchorMax = Vector2.one;
			Rect1.pivot = Vector2.one * 0.5f;
			Rect1.offsetMin = new Vector2(55,2.5f);
			Rect1.offsetMax = new Vector2(-2.5f,-2.5f);

		
		}
		
		[MenuItem("GameObject/UI/StyledUI/Text Button")]
		static public void CreateTextButton()
		{
			var p = GetOrCreateCanvasGameObject().transform;

			var obj = new GameObject("Text Button");
			Undo.RegisterCreatedObjectUndo(obj, "Create " + "Text Button");
			obj.transform.SetParent(p, false);

			var vi = obj.AddComponent<Image>();
			var objRect = obj.GetComponent<RectTransform>();
			objRect.sizeDelta = new Vector2(165,45);
			vi.sprite = Resources.Load<Sprite>("FilledSprite");
			vi.type = Image.Type.Sliced;
			vi.color = new Color(0,0.3362752f,0.8018868f);
			var btn = obj.AddComponent<Button>();
			
			var text = new GameObject("Text");
			text.transform.SetParent(obj.transform, false);
			var textInput = text.AddComponent<Text>();
			textInput.text = "Button";
			textInput.fontStyle = FontStyle.Bold;
			textInput.fontSize = 20;
			textInput.alignment = TextAnchor.MiddleCenter;
			var Rect1 = text.GetComponent<RectTransform>();
			Rect1.anchorMin = Vector2.zero;
			Rect1.anchorMax = Vector2.one;
			Rect1.pivot = Vector2.one * 0.5f;
			Rect1.offsetMin = new Vector2(0,0);
			Rect1.offsetMax = new Vector2(-0,-0);

		
		}
		
		[MenuItem("GameObject/UI/StyledUI/Outline Text Button")]
		static public void CreateOutlineTextButton()
		{
			var p = GetOrCreateCanvasGameObject().transform;

			var obj = new GameObject("Outline Text Button");
			Undo.RegisterCreatedObjectUndo(obj, "Create " + "Outline Text Button");
			obj.transform.SetParent(p, false);

			var vi = obj.AddComponent<Image>();
			var objRect = obj.GetComponent<RectTransform>();
			objRect.sizeDelta = new Vector2(165,45);
			vi.sprite = Resources.Load<Sprite>("OutlineSprite");
			vi.type = Image.Type.Sliced;
			vi.color = new Color(0,0.3362752f,0.8018868f);
			var btn = obj.AddComponent<Button>();
			
			var text = new GameObject("Text");
			text.transform.SetParent(obj.transform, false);
			var textInput = text.AddComponent<Text>();
			textInput.text = "Button";
			textInput.fontStyle = FontStyle.Bold;
			textInput.fontSize = 20;
			textInput.alignment = TextAnchor.MiddleCenter;
			textInput.color = new Color(0,0.3362752f,0.8018868f);
			var Rect1 = text.GetComponent<RectTransform>();
			Rect1.anchorMin = Vector2.zero;
			Rect1.anchorMax = Vector2.one;
			Rect1.pivot = Vector2.one * 0.5f;
			Rect1.offsetMin = new Vector2(0,0);
			Rect1.offsetMax = new Vector2(-0,-0);

		
		}
		
		[MenuItem("GameObject/UI/StyledUI/Switchable Rect Button")]
		static public void CreateSwitchableRectButton()
		{
			var p = GetOrCreateCanvasGameObject().transform;

			var obj = new GameObject("Switchable Rect Button");
			Undo.RegisterCreatedObjectUndo(obj, "Create " + "Switchable Rect Button");
			obj.transform.SetParent(p, false);

			var vi = obj.AddComponent<Image>();
			var objRect = obj.GetComponent<RectTransform>();
			objRect.sizeDelta = new Vector2(165,45);
			vi.sprite = Resources.Load<Sprite>("OutlineSprite");
			vi.type = Image.Type.Sliced;
			vi.color = new Color(0,0.3362752f,0.8018868f);
			var btn = obj.AddComponent<Button>();
			obj.AddComponent<SwitchableButton>();
			
			var obj1 = new GameObject("fill");
			obj1.transform.SetParent(obj.transform, false);

			var vi1 = obj1.AddComponent<Image>();
			var objRect1 = obj1.GetComponent<RectTransform>();
			objRect1.sizeDelta = new Vector2(165,45);
			vi1.sprite = Resources.Load<Sprite>("FilledSprite");
			vi1.type = Image.Type.Sliced;
			vi1.color = new Color(0,0.3362752f,0.8018868f);
			obj1.SetActive(false);
			var Rect2 = obj1.GetComponent<RectTransform>();
			Rect2.anchorMin = Vector2.zero;
			Rect2.anchorMax = Vector2.one;
			Rect2.pivot = Vector2.one * 0.5f;
			Rect2.offsetMin = new Vector2(0,0);
			Rect2.offsetMax = new Vector2(-0,-0);
			
			var text = new GameObject("Text");
			text.transform.SetParent(obj.transform, false);
			var textInput = text.AddComponent<Text>();
			textInput.text = "Button";
			textInput.fontStyle = FontStyle.Bold;
			textInput.fontSize = 20;
			textInput.alignment = TextAnchor.MiddleCenter;
			textInput.color = new Color(0,0.3362752f,0.8018868f);
			var Rect1 = text.GetComponent<RectTransform>();
			Rect1.anchorMin = Vector2.zero;
			Rect1.anchorMax = Vector2.one;
			Rect1.pivot = Vector2.one * 0.5f;
			Rect1.offsetMin = new Vector2(0,0);
			Rect1.offsetMax = new Vector2(-0,-0);

		
		}
		
		[MenuItem("GameObject/UI/StyledUI/Rounded Rect")]
		static public void CreateRoundedRect()
		{
			var p = GetOrCreateCanvasGameObject().transform;

			var obj = new GameObject("Rounded Rect");
			Undo.RegisterCreatedObjectUndo(obj, "Create " + "Rounded Rect");
			obj.transform.SetParent(p, false);

			var vi = obj.AddComponent<Image>();
			
			vi.sprite = Resources.Load<Sprite>("FilledSprite");
			vi.type = Image.Type.Sliced;
			

			
		}
		[MenuItem("GameObject/UI/StyledUI/Rounded Rect Outline")]
		static public void CreateRoundedRectOutline()
		{
			var p = GetOrCreateCanvasGameObject().transform;

			var obj = new GameObject("Rounded Rect Outline");
			Undo.RegisterCreatedObjectUndo(obj, "Create " + "Rounded Rect Outline");
			obj.transform.SetParent(p, false);

			var vi = obj.AddComponent<Image>();
			
			vi.sprite = Resources.Load<Sprite>("OutlineSprite");
			vi.type = Image.Type.Sliced;
			
			
			
		}
		
		[MenuItem("GameObject/UI/StyledUI/Circle")]
		static public void CreateCircle()
		{
			var p = GetOrCreateCanvasGameObject().transform;

			var obj = new GameObject("Circle");
			Undo.RegisterCreatedObjectUndo(obj, "Create " + "Circle");
			obj.transform.SetParent(p, false);

			var vi = obj.AddComponent<Image>();
			
			vi.sprite = Resources.Load<Sprite>("FilledSprite");
			vi.preserveAspect = true;
		
			
		}
		
		[MenuItem("GameObject/UI/StyledUI/Circle Outline")]
		static public void CreateCircleOutline()
		{
			var p = GetOrCreateCanvasGameObject().transform;

			var obj = new GameObject("Circle Outline");
			Undo.RegisterCreatedObjectUndo(obj, "Create " + "Circle Outline");
			obj.transform.SetParent(p, false);

			var vi = obj.AddComponent<Image>();
			
			vi.sprite = Resources.Load<Sprite>("OutlineSprite");
			vi.preserveAspect = true;
		
			
		}
		[MenuItem("GameObject/UI/StyledUI/Outlined Circle")]
		static public void CreateOutlinedCircle()
		{
			var p = GetOrCreateCanvasGameObject().transform;

			var obj = new GameObject("Outlined Circle");
			Undo.RegisterCreatedObjectUndo(obj, "Create " + "Outlined Circle");
			obj.transform.SetParent(p, false);

			var vi = obj.AddComponent<Image>();
			
			vi.sprite = Resources.Load<Sprite>("FilledSprite");
			vi.preserveAspect = true;
			
			var outl = obj.AddComponent<Outline>();
			
			outl.effectDistance = new Vector2(2,2);
		
			
		}
		
		[MenuItem("GameObject/UI/StyledUI/Outlined Rect")]
		static public void CreateOutlinedRect()
		{
			var p = GetOrCreateCanvasGameObject().transform;

			var obj = new GameObject("Outlined Rect");
			Undo.RegisterCreatedObjectUndo(obj, "Create " + "Outlined Rect");
			obj.transform.SetParent(p, false);

			var vi = obj.AddComponent<Image>();
			
			vi.sprite = Resources.Load<Sprite>("FilledSprite");
			vi.type = Image.Type.Sliced;
			
			var outl = obj.AddComponent<Outline>();
			
			outl.effectDistance = new Vector2(2,2);
		
			
		}
		
		

		static public GameObject GetOrCreateCanvasGameObject()
		{
			GameObject selectedGo = Selection.activeGameObject;
			
			if(selectedGo != null && selectedGo.GetComponent<RectTransform>() && selectedGo.GetComponentInParent<Canvas>())
				return selectedGo;
			// Try to find a gameobject that is the selected GO or one if its parents.
			Canvas canvas = (selectedGo != null) ? selectedGo.GetComponentInParent<Canvas>() : null;
			if (canvas != null && canvas.gameObject.activeInHierarchy)
				return canvas.gameObject;

			// No canvas in selection or its parents? Then use just any canvas..
			canvas = Object.FindObjectOfType(typeof(Canvas)) as Canvas;
			if (canvas != null && canvas.gameObject.activeInHierarchy)
				return canvas.gameObject;

			// No canvas in the scene at all? Then create a new one.
			return CreateNewUI();
		}

		static public GameObject CreateNewUI()
		{
			var root = new GameObject("Canvas");
			root.layer = LayerMask.NameToLayer("UI");
			Canvas canvas = root.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			root.AddComponent<CanvasScaler>();
			root.AddComponent<GraphicRaycaster>();
			Undo.RegisterCreatedObjectUndo(root, "Create " + root.name);

			CreateEventSystem(false, null);
			return root;
		}

		private static void CreateEventSystem(bool select, GameObject parent)
		{
			var esys = Object.FindObjectOfType<EventSystem>();
			if (esys == null)
			{
				var eventSystem = new GameObject("EventSystem");
				GameObjectUtility.SetParentAndAlign(eventSystem, parent);
				esys = eventSystem.AddComponent<EventSystem>();
				eventSystem.AddComponent<StandaloneInputModule>();

				Undo.RegisterCreatedObjectUndo(eventSystem, "Create " + eventSystem.name);
			}

			if (select && esys != null)
			{
				Selection.activeGameObject = esys.gameObject;
			}
		}
}


