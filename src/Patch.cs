using HarmonyLib;

namespace DisplayPiety
{
    [HarmonyPatch]
    public static class Patch
    {
        private const int CHARM_ID = 77;
        private const int PIETY_ID = 85;
        private const int FAITH_ID = 306;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(WidgetStatsBar), nameof(WidgetStatsBar.Add))]
        public static void WidgetStatsBar_Add_Postfix(WidgetStatsBar __instance, Element ele)
        {
            // ウィジェットバーの魅力ステータスアイコンの後に追加
            if (ele is not (Element)null && ele?.id == CHARM_ID)
                AddPietyWidget(__instance);
        }

        private static void AddPietyWidget(WidgetStatsBar w)
        {
            w.Add(null, "piety", WidgetStatsBar.Instance.iconGodMood, () =>
            {
                if (EClass.pc == null)
                    return "-/-";

                // 信仰心スキル値を取得
                var piety = EClass.pc.Evalue(PIETY_ID);
                
                // cfgファイルの設定値がtrueなら信仰心のみを表示する
                if (Plugin.Instance == null || Plugin.Instance.ShowFaithValue == null ||
                    !Plugin.Instance.ShowFaithValue.Value)
                    return $"{piety}";

                // 信仰スキル値を取得
                var faith = EClass.pc.Evalue(FAITH_ID);
                return $"{piety} / {faith}";
            });
        }
    }
}