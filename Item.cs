using System;
using System.Drawing;

namespace INVedit
{
	public class Item
	{
		public short ID { get; set; }
		public byte Count { get; set; }
		public byte Slot { get; set; }
		public short Damage { get; set; }
		
		public bool Known { get; set; }
		public string Name { get; set; }
		public bool Stackable { get; set; }
		public short MaxDamage { get; set; }
		public Image Image { get; set; }
		
		public Item(short id)
			: this(id, 1, 0, 0) {  }
		public Item(short id, byte count)
			: this(id, count, 0, 0) {  }
		public Item(short id, byte count, byte slot)
			: this(id, count, slot, 0) {  }
		public Item(short id, byte count, byte slot, short damage)
		{
			ID = id;
			Count = count;
			Slot = slot;
			Damage = damage;
			
			if (Data.items.ContainsKey(id)) {
				Data.Item item = Data.items[id];
				Known = true;
				Name = item.name;
				Stackable = item.stackable;
				MaxDamage = item.maxDamage;
				Image = Data.list.Images[item.imageIndex];
			} else {
				Name = "Unknown item "+id;
				Known = false;
				Stackable = false;
				MaxDamage = 0;
				Image = Data.unknown;
			}
		}
	}
}
