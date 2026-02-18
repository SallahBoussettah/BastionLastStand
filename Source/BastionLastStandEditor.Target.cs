using UnrealBuildTool;

public class BastionLastStandEditorTarget : TargetRules
{
	public BastionLastStandEditorTarget(TargetInfo Target) : base(Target)
	{
		Type = TargetType.Editor;
		DefaultBuildSettings = BuildSettingsVersion.Latest;
		IncludeOrderVersion = EngineIncludeOrderVersion.Latest;
		ExtraModuleNames.Add("BastionLastStand");
	}
}
