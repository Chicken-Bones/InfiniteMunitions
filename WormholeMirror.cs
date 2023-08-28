using System.Linq;
using CodeChickenLib;
using Terraria;
using Terraria.ID;

namespace KnickKnacks
{
	public class WormholeMirror : CCItem
	{
		public override void SetDefaults() {
			Item.rare = ItemRarityID.Green;
			Item.width = 26;
			Item.height = 26;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.WormholePotion, 20)
				.AddIngredient(ItemID.MagicMirror)
				.Register();

			CreateRecipe()
				.AddIngredient(ItemID.WormholePotion, 20)
				.AddIngredient(ItemID.IceMirror)
				.Register();
		}

		private static bool HasWormholeMirror(Player player) =>
			player.inventory.Any(item => item.ModItem is WormholeMirror) || player.useVoidBag() && player.bank4.item.Any(item => item.ModItem is WormholeMirror);

        public override void SetStaticDefaults() {
			On_Player.HasUnityPotion += (orig, self) => {
				if (HasWormholeMirror(self))
					return true;

				return orig(self);
			};

			On_Player.TakeUnityPotion += (orig, self) => {
				if (HasWormholeMirror(self))
					return;

				orig(self);
			};
		}
	}
}
