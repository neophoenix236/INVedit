using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NBT;

namespace INVedit
{
	public static class Inventory
	{
		public static void Load(string filename, Dictionary<byte, ItemSlot> slots)
		{
			Load(Tag.Load(filename)["Data"]["Player"]["Inventory"], slots);
		}
		public static void Load(Tag inventory, Dictionary<byte, ItemSlot> slots)
		{
			try {
				foreach (ItemSlot slot in slots.Values) slot.Clear();
				foreach (Tag tag in inventory) {
					short id = (short)tag["id"];
					byte slot = (byte)tag["Slot"];
					byte count = (byte)tag["Count"];
					if (count == 0) continue;
					if (!slots.ContainsKey(slot)) {
						MessageBox.Show("Unknown slot '"+slot+"', discarded item.",
						                "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						continue;
					}
					ItemSlot itemSlot = slots[slot];
					itemSlot.Item = new Item(id, count, slot, (short)tag["Damage"]);
				}
			} finally { foreach (ItemSlot slot in slots.Values) slot.Refresh(); }
		}
		
		public static void Save(string filename, Dictionary<byte, ItemSlot> slots)
		{
			Tag tag = Tag.Load(filename);
			Save(tag["Data"]["Player"], slots);
			tag.Save(filename);
		}
		public static void Save(Tag parent, Dictionary<byte, ItemSlot> slots)
		{
			if (parent.Contains("Inventory")) parent["Inventory"].Remove();
			Tag inventory = parent.AddList("Inventory", TagType.Compound);
			foreach (ItemSlot slot in slots.Values) {
				if (slot.Item == null) continue;
				Tag item = inventory.AddCompound();
				item.Add("id", slot.Item.ID);
				item.Add("Slot", slot.Slot);
				item.Add("Count", slot.Item.Count);
				item.Add("Damage", slot.Item.Damage);
			}
		}
	}
}
