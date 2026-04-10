#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.SceneManagement;
using TMPro;

public static class WuxiaMenuCreator
{
    // Color Palette
	static readonly Color BgColor      = new Color(0.078f, 0.039f, 0.024f);
    static readonly Color PanelColor   = new Color(0.110f, 0.063f, 0.031f, 0.92f);
    static readonly Color GoldColor    = new Color(0.784f, 0.592f, 0.102f);
    static readonly Color IvoryColor   = new Color(0.941f, 0.871f, 0.706f);
    static readonly Color CrimsonColor = new Color(0.239f, 0.047f, 0.047f);
    static readonly Color CrimsonHover = new Color(0.420f, 0.082f, 0.082f);
    static readonly Color MutedColor   = new Color(0.102f, 0.102f, 0.102f);

	[MenuItem("Wuxia/Create All Scenes")]
	public static void CreateAllScenes(){
		CreateMainMenuScene();
		CreateStageSelectScene();
		Debug.Log("Done! อย่าลืมเพิ่ม scenes เข้า Build Settings");
	}

	[MenuItem("Wuxia/Create MainMenu Scene")]
	public static void CreateMainMenuScene()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
        scene.name = "MainMenu";

        // ── Camera ──
        var camGO = new GameObject("Main Camera");
        EditorSceneManager.MoveGameObjectToScene(camGO, scene);
        var cam = camGO.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = BgColor;
        camGO.tag = "MainCamera";

        // ── GameManager ──
        var gmGO = new GameObject("_GameManager");
        EditorSceneManager.MoveGameObjectToScene(gmGO, scene);
        gmGO.AddComponent<GameManager>();

        // ── Canvas ──
        var canvasGO = CreateCanvas("MainMenuCanvas", scene);

        // ── Background ──
        var bg = CreateImage(canvasGO.transform, "Background", BgColor);
        StretchFull(bg.GetComponent<RectTransform>());

        // ── Title ──
        var titlePanel = CreatePanel(canvasGO.transform, "TitlePanel",
            new Vector2(0f, 180f), new Vector2(700f, 200f), Color.clear);

        var titleText = CreateTMP(titlePanel.transform, "TitleText",
            "CARD BATTLE", 96, GoldColor, FontStyles.Bold);
        titleText.anchoredPosition = new Vector2(0f, 30f);

        var subtitleText = CreateTMP(titlePanel.transform, "SubtitleText",
            "武俠牌戰", 40, IvoryColor, FontStyles.Normal);
        subtitleText.anchoredPosition = new Vector2(0f, -40f);
        subtitleText.GetComponent<TextMeshProUGUI>().characterSpacing = 20f;

        var divider = CreateImage(titlePanel.transform, "Divider", GoldColor);
        divider.GetComponent<RectTransform>().sizeDelta = new Vector2(420f, 2f);
        divider.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -80f);

        // ── Buttons ──
        var menuPanel = CreatePanel(canvasGO.transform, "MenuPanel",
            new Vector2(0f, -60f), new Vector2(340f, 260f), Color.clear);
        var vGroup = menuPanel.AddComponent<VerticalLayoutGroup>();
        vGroup.spacing = 18f;
        vGroup.childAlignment = TextAnchor.MiddleCenter;
        vGroup.childForceExpandWidth = true;
        vGroup.childForceExpandHeight = false;

        CreateMenuButton(menuPanel.transform, "Btn_Start",  "Start",  CrimsonColor, GoldColor);
        CreateMenuButton(menuPanel.transform, "Btn_Stages", "Select Stage",   CrimsonColor, GoldColor);
        CreateMenuButton(menuPanel.transform, "Btn_Quit",   "Quit Game",  MutedColor,   IvoryColor);

        // ── Version ──
        var ver = CreateTMP(canvasGO.transform, "VersionText",
            "v0.1 Alpha", 18, new Color(0.4f, 0.4f, 0.4f), FontStyles.Normal);
        ver.anchorMin = new Vector2(1f, 0f);
        ver.anchorMax = new Vector2(1f, 0f);
        ver.pivot = new Vector2(1f, 0f);
        ver.anchoredPosition = new Vector2(-20f, 12f);

        // ── MainMenuController ──
        var mmcGO = new GameObject("MainMenuController");
        EditorSceneManager.MoveGameObjectToScene(mmcGO, scene);
        mmcGO.AddComponent<MainMenuController>();

        EditorSceneManager.SaveScene(scene, "Assets/Scenes/MainMenu.unity");
        AssetDatabase.Refresh();
        Debug.Log("[Wuxia] MainMenu saved");
    }

	[MenuItem("Wuxia/Create StageSelect Scene")]
    public static void CreateStageSelectScene()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
        scene.name = "StageSelect";

        var camGO = new GameObject("Main Camera");
        EditorSceneManager.MoveGameObjectToScene(camGO, scene);
        var cam = camGO.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = BgColor;
        camGO.tag = "MainCamera";

        var canvasGO = CreateCanvas("StageSelectCanvas", scene);

        var bg = CreateImage(canvasGO.transform, "Background", BgColor);
        StretchFull(bg.GetComponent<RectTransform>());

        // ── Header ──
        var header = CreatePanel(canvasGO.transform, "Header",
            Vector2.zero, Vector2.zero, PanelColor);
        var headerRT = header.GetComponent<RectTransform>();
        headerRT.anchorMin = new Vector2(0f, 1f);
        headerRT.anchorMax = new Vector2(1f, 1f);
        headerRT.pivot = new Vector2(0.5f, 1f);
        headerRT.offsetMin = new Vector2(0f, -80f);
        headerRT.offsetMax = Vector2.zero;

        var backBtn = CreateMenuButton(header.transform, "Btn_Back", "← Back", MutedColor, IvoryColor);
        var backRT = backBtn.GetComponent<RectTransform>();
        backRT.anchorMin = new Vector2(0f, 0.5f);
        backRT.anchorMax = new Vector2(0f, 0.5f);
        backRT.pivot = new Vector2(0f, 0.5f);
        backRT.sizeDelta = new Vector2(160f, 50f);
        backRT.anchoredPosition = new Vector2(20f, 0f);

        var headerTitle = CreateTMP(header.transform, "HeaderTitle",
            "Select Stage", 48, GoldColor, FontStyles.Bold);
        headerTitle.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;

        // ── Stage Grid ──
        var gridPanel = CreatePanel(canvasGO.transform, "StageGrid",
            Vector2.zero, Vector2.zero, Color.clear);
        var gridRT = gridPanel.GetComponent<RectTransform>();
        gridRT.anchorMin = new Vector2(0f, 0.2f);
        gridRT.anchorMax = new Vector2(1f, 0.95f);
        gridRT.offsetMin = new Vector2(40f, 0f);
        gridRT.offsetMax = new Vector2(-40f, -90f);

        var grid = gridPanel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(200f, 240f);
        grid.spacing = new Vector2(24f, 24f);
        grid.childAlignment = TextAnchor.UpperCenter;
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 3;

        // ── Info Panel ──
        var infoPanel = CreatePanel(canvasGO.transform, "InfoPanel",
            Vector2.zero, Vector2.zero, PanelColor);
        var infoRT = infoPanel.GetComponent<RectTransform>();
        infoRT.anchorMin = new Vector2(0f, 0f);
        infoRT.anchorMax = new Vector2(1f, 0.18f);
        infoRT.offsetMin = infoRT.offsetMax = Vector2.zero;

        CreateTMP(infoPanel.transform, "InfoName", "Select Stage...", 32, GoldColor, FontStyles.Bold);
        CreateTMP(infoPanel.transform, "InfoDesc", "", 22, IvoryColor, FontStyles.Normal);
        CreateTMP(infoPanel.transform, "InfoDifficulty", "", 22, IvoryColor, FontStyles.Normal);

        var playBtn = CreateMenuButton(infoPanel.transform, "Btn_Play",
            "In to the Fight", CrimsonColor, GoldColor);
        var playRT = playBtn.GetComponent<RectTransform>();
        playRT.anchorMin = new Vector2(1f, 0.5f);
        playRT.anchorMax = new Vector2(1f, 0.5f);
        playRT.pivot = new Vector2(1f, 0.5f);
        playRT.sizeDelta = new Vector2(260f, 60f);
        playRT.anchoredPosition = new Vector2(-30f, 0f);

        var sscGO = new GameObject("StageSelectController");
        EditorSceneManager.MoveGameObjectToScene(sscGO, scene);
        sscGO.AddComponent<StageSelectController>();

        EditorSceneManager.SaveScene(scene, "Assets/Scenes/StageSelect.unity");
        AssetDatabase.Refresh();
        Debug.Log("[Wuxia] StageSelect saved");
    }

	[MenuItem("Wuxia/Create BattleHUD")]
	public static void CreateBattleHUD()
	{
		// Canvas
		var canvasGO = new GameObject("BattleHUD_Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
		var canvas = canvasGO.GetComponent<Canvas>();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvas.sortingOrder = 10;
		var scaler = canvasGO.GetComponent<CanvasScaler>();
		scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		scaler.referenceResolution = new Vector2(1920f, 1080f);
		scaler.matchWidthOrHeight = 0.5f;

		// TopBar
		var topBar = CreatePanel(canvasGO.transform, "TopBar", Vector2.zero, Vector2.zero, PanelColor);
		var topRT = topBar.GetComponent<RectTransform>();
		topRT.anchorMin = new Vector2(0f, 1f);
		topRT.anchorMax = new Vector2(1f, 1f);
		topRT.pivot = new Vector2(0.5f, 1f);
		topRT.offsetMin = new Vector2(0f, -80f);
		topRT.offsetMax = Vector2.zero;
		topBar.AddComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;

		// Player HP
		var playerHP = new GameObject("PlayerHP", typeof(RectTransform), typeof(Slider));
		playerHP.transform.SetParent(topBar.transform, false);
		playerHP.GetComponent<RectTransform>().sizeDelta = new Vector2(300f, 30f);
		var pSlider = playerHP.GetComponent<Slider>();
		pSlider.minValue = 0f;
		pSlider.maxValue = 100f;
		pSlider.value = 100f;

		// Stage Name
		var stageName = CreateTMP(topBar.transform, "StageNameText", "Stage 1", 28, GoldColor, FontStyles.Bold);
		stageName.sizeDelta = new Vector2(300f, 40f);

		// Round
		var round = CreateTMP(topBar.transform, "RoundText", "Round: 1", 24, IvoryColor, FontStyles.Normal);
		round.sizeDelta = new Vector2(200f, 40f);

		// Pause Button
		CreateMenuButton(topBar.transform, "Btn_Pause", "II", MutedColor, IvoryColor)
			.GetComponent<RectTransform>().sizeDelta = new Vector2(60f, 60f);

		// Pause Panel
		var pausePanel = CreatePanel(canvasGO.transform, "PausePanel", Vector2.zero, Vector2.zero, new Color(0f, 0f, 0f, 0.85f));
		StretchFull(pausePanel.GetComponent<RectTransform>());
		var vg = pausePanel.AddComponent<VerticalLayoutGroup>();
		vg.spacing = 20f;
		vg.childAlignment = TextAnchor.MiddleCenter;
		vg.childForceExpandWidth = false;
		vg.childForceExpandHeight = false;

		CreateTMP(pausePanel.transform, "PauseTitle", "PAUSED", 64, GoldColor, FontStyles.Bold);
		CreateMenuButton(pausePanel.transform, "Btn_Resume", "Resume", CrimsonColor, GoldColor);
		CreateMenuButton(pausePanel.transform, "Btn_MainMenu", "Main Menu", CrimsonColor, GoldColor);
		CreateMenuButton(pausePanel.transform, "Btn_Quit", "Quit Game", MutedColor, IvoryColor);

		// BattleHUD script
		canvasGO.AddComponent<BattleHUD>();

        var eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
        eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        Debug.Log("[Wuxia] BattleHUD created in current scene");
	}

    // ── Helpers ───────────────────────────────────────────────────────────

    static GameObject CreateCanvas(string name, UnityEngine.SceneManagement.Scene scene)
    {
        var go = new GameObject(name);
        EditorSceneManager.MoveGameObjectToScene(go, scene);
        var canvas = go.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = go.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;
        go.AddComponent<GraphicRaycaster>();
        return go;
    }

    static GameObject CreateImage(Transform parent, string name, Color color)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        go.GetComponent<Image>().color = color;
        return go;
    }

    static GameObject CreatePanel(Transform parent, string name,
        Vector2 pos, Vector2 size, Color color)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(Image));
        go.transform.SetParent(parent, false);
        var rt = go.GetComponent<RectTransform>();
        rt.anchoredPosition = pos;
        if (size != Vector2.zero) rt.sizeDelta = size;
        go.GetComponent<Image>().color = color;
        return go;
    }

    static RectTransform CreateTMP(Transform parent, string name,
        string text, int size, Color color, FontStyles style)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
        go.transform.SetParent(parent, false);
        var tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = text;
        tmp.fontSize = size;
        tmp.color = color;
        tmp.fontStyle = style;
        tmp.alignment = TextAlignmentOptions.Center;
        var rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(600f, size * 1.4f);
        return rt;
    }

    static GameObject CreateMenuButton(Transform parent, string name,
        string label, Color bgColor, Color textColor)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
        go.transform.SetParent(parent, false);
        go.GetComponent<Image>().color = bgColor;
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(320f, 68f);

        var outline = go.AddComponent<Outline>();
        outline.effectColor = GoldColor;
        outline.effectDistance = new Vector2(1.5f, -1.5f);

        var btn = go.GetComponent<Button>();
        var colors = btn.colors;
        colors.normalColor      = bgColor;
        colors.highlightedColor = CrimsonHover;
        colors.pressedColor     = new Color(bgColor.r * 0.6f, bgColor.g * 0.6f, bgColor.b * 0.6f);
        btn.colors = colors;

        var labelGO = new GameObject("Label", typeof(RectTransform), typeof(TextMeshProUGUI));
        labelGO.transform.SetParent(go.transform, false);
        var lrt = labelGO.GetComponent<RectTransform>();
        lrt.anchorMin = Vector2.zero;
        lrt.anchorMax = Vector2.one;
        lrt.offsetMin = lrt.offsetMax = Vector2.zero;
        var tmp = labelGO.GetComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.fontSize = 28;
        tmp.color = textColor;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontStyle = FontStyles.Bold;

        return go;
    }

    static void StretchFull(RectTransform rt)
    {
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = rt.offsetMax = Vector2.zero;
    }
}
#endif