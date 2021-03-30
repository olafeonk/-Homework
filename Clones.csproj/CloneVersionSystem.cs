using System.Collections.Generic;

namespace Clones
{
	public class Node<T>
	{
		public T Value { get;  }
		public Node<T> Next { get; set; }
		public Node(T value)
		{
			Value = value;
		}
	}

	public class LinkedList<T>
	{
		public Node<T> Head;
		
		public void Add(T data)
		{
			var node = new Node<T>(data) {Next = Head};
			Head = node;
		}

		public T Pop()
		{
			var result = Head;
			Head = Head.Next;
			return result.Value;
		}
	}
	
	public class Clone
	{
		private readonly LinkedList<string> programs;
		private LinkedList<string> canceledPrograms;

		public Clone()
		{
			programs = new LinkedList<string>();
			canceledPrograms = new LinkedList<string>();	
		}

		public Clone(Clone parent)
		{
			programs = new LinkedList<string> {Head = parent.programs.Head};
			canceledPrograms = new LinkedList<string> {Head = parent.canceledPrograms.Head};
		}
		
		public void Learn(string program)
		{
			programs.Add(program);
			canceledPrograms = new LinkedList<string>();
		}

		public void Rollback()
		{
			canceledPrograms.Add(programs.Pop());
		}

		public void Relearn()
		{
			programs.Add(canceledPrograms.Pop());
		}

		public string Check()
		{
			return programs.Head == null ? "basic" : programs.Head.Value;
		}
	}
	
	public class CloneVersionSystem : ICloneVersionSystem
	{
		private readonly List<Clone> clones = new List<Clone>();

		private string DoCommand(Clone clone, string command, string[] query)
		{
			switch (command)
			{
				case "learn":
					clone.Learn(query[2]);
					break;
				case "rollback":
					clone.Rollback();
					break;
				case "relearn":
					clone.Relearn();
					break;
				case "clone":
					clones.Add(new Clone(clone));
					break;
				case "check":
					return clone.Check();
			}
			return null;
		}
		
		public string Execute(string query)
		{
			if (clones.Count == 0)
			{
				clones.Add(new Clone());
			}
			return DoCommand(clones[int.Parse(query.Split()[1]) - 1], query.Split()[0], query.Split());
		}
	}
}