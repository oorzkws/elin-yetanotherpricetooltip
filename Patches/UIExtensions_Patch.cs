using static YetAnotherPriceTooltip.EzTranspiler;

namespace YetAnotherPriceTooltip.Patches;

[HarmonyAfter("yuof.elin.uiExtensions.mod")]
public static class UIExtensions_Patch {
    private static MethodBase targetMethod;

    public static bool Prepare() {
        targetMethod = AccessTools.Method("Elin_UI.Tooltip:Thing_WriteNote");
        if (targetMethod is null) {
            Logging.Log("Skipping UI Extensions patch, method not found");
            return false;
        }
        return true;
    }

    public static MethodBase TargetMethod() => targetMethod;

    [UsedImplicitly]
    public static CodeInstructions Transpiler(CodeInstructions instructions, MethodBase method) {
        var editor = new CodeMatcher(instructions);

        var pattern = InstructionMatchSignature((Thing instance) => instance.GetValue(false) != 0);
        // ReSharper disable once ConvertClosureToMethodGroup we need to keep the call here or the function itself is converted
        var replacement = InstructionSignature(() => false);
        editor.Start().Replace(pattern, replacement);
        return editor.InstructionEnumeration();
    }
}