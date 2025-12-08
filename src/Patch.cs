using HarmonyLib;
using UnityEngine;

namespace DisplayPiety
{
    [HarmonyPatch]
    public static class Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(WidgetStatsBar), nameof(WidgetStatsBar.Add))]
        public static void WidgetStatsBar_Add_Postfix(WidgetStatsBar __instance, string id)
        {
            if (id == "mood")
            {
                AddPietyWidget(__instance);
            }
        }

        private static void AddPietyWidget(WidgetStatsBar w)
        {
            var sprite =  createSprite(); 

            w.Add(null, "piety", sprite, () =>
            {
                if (EClass.pc == null) return "-/-";
                
                // 信仰度
                var piety = EClass.pc.Evalue(85);
                
                // 信仰スキル値
                var faith = EClass.pc.Evalue(306);

                return $"{piety}/{faith}";
            });
        }
        
        private static Sprite createSprite()
        {
            var moneyIconSprite = WidgetStatsBar.Instance.iconMoney;

            var originSprite = CharaGen.Create("isca").GetSprite();
            var texture = originSprite.texture;
            var textureRect = originSprite.textureRect;
            var bottomRect = new Rect(
                textureRect.x,
                textureRect.y,
                textureRect.width,
                textureRect.height / 2f
            );
            var pivot = new Vector2(
                originSprite.pivot.x / textureRect.width,
                originSprite.pivot.y / textureRect.height
            );
            var pixelsPerUnit = texture.width / (moneyIconSprite.texture.width / moneyIconSprite.pixelsPerUnit) * 0.6f;

            return Sprite.Create(texture, bottomRect, pivot, pixelsPerUnit);
        }
    }
}