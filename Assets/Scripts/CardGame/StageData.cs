using UnityEngine;

[CreateAssetMenu(fileName = "NewStage", menuName = "CardGame/Stage_Data")]
public class StageData : ScriptableObject
{
    [Header("Identity")]
	public string stageName = "Stage 1";
	public string stageSubtitle = "";
	[TextArea] public string description = "";

	[Header("Difficulty")]
	[Range(1,3)] public int difficulty = 1;

	[Header("Progress")]
	public bool isLocked = false;
	[Range(0, 3)] public int bestStars = 0;

	[Header("Scene")]
	public int sceneIndex = 1;
}
