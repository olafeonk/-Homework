using System;
using System.Text;

namespace hashes
{
	public class GhostsTask : 
		IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, 
		IMagic
	{
		private static readonly byte[] documentArray = {1};
		private readonly Vector vector = new Vector(1, 1);
		private readonly Segment segment = new Segment(new Vector(1, 1), new Vector(1, 1));
		private readonly Cat cat = new Cat("A","B", DateTime.MinValue);
		private readonly Robot robot = new Robot("a", double.Epsilon);
		private readonly Document document = new Document("a", Encoding.Default, documentArray);
		public void DoMagic()
		{
			cat.Rename("b");
			vector.Add(new Vector(1, 1));
			segment.End.Add(new Vector(1, 1));
			Robot.BatteryCapacity++;
			documentArray[0]++;
		}

		Vector IFactory<Vector>.Create() => vector;

		Segment IFactory<Segment>.Create() => segment;

		Document IFactory<Document>.Create() => document;

		Cat IFactory<Cat>.Create() => cat;

		Robot IFactory<Robot>.Create() => robot;
	}
}