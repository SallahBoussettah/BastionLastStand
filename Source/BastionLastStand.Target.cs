using UnrealBuildTool;

public class BastionLastStandTarget : TargetRules
{
	public BastionLastStandTarget(TargetInfo Target) : base(Target)
	{
		Type = TargetType.Game;
		DefaultBuildSettings = BuildSettingsVersion.Latest;
		IncludeOrderVersion = EngineIncludeOrderVersion.Latest;
		ExtraModuleNames.Add("BastionLastStand");
	}
}
