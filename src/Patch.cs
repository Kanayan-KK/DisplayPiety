using HarmonyLib;

namespace DisplayPiety
{
    [HarmonyPatch]
    public static class Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(WidgetStatsBar), nameof(WidgetStatsBar.Add))]
        public static void WidgetStatsBar_Add_Postfix(WidgetStatsBar __instance, Element ele)
        {
            // ウィジェットバーの魅力ステータスアイコンの後に追加
            if(ele is not (Element)null && ele?.id == 77)
                AddPietyWidget(__instance);
        }

        private static void AddPietyWidget(WidgetStatsBar w)
        {
            w.Add(null, "piety", WidgetStatsBar.Instance.iconGodMood, () =>
            {
                if (EClass.pc == null)
                    return "-/-";

                // 信仰心スキル値
                var piety = EClass.pc.Evalue(85);

                // 信仰スキル値
                var faith = EClass.pc.Evalue(306);

                return $"{piety} / {faith}";
            });
        }
    }
}