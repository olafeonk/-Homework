using System.Collections.Generic;

namespace func.brainfuck
{
	public static class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
			var openBrackets = new Dictionary<int, int>();
			var closeBrackets = new Dictionary<int, int>();
			var stack = new Stack<int>();
			for (var i = 0; i < vm.Instructions.Length; i++)
			{
				switch (vm.Instructions[i])
				{
					case '[':
						stack.Push(i);
						break;
					case ']':
						closeBrackets[i] = stack.Peek();
						openBrackets[stack.Pop()] = i;
						break;
				}
			}
			RegisterLoops(vm, openBrackets, closeBrackets);
		}

		private static void RegisterLoops(IVirtualMachine vm, Dictionary<int, int> openBrackets,
			Dictionary<int, int> closeBrackets)
		{
			vm.RegisterCommand('[', b =>
			{
				if (b.Memory[b.MemoryPointer] == 0)
				{
					b.InstructionPointer = openBrackets[b.InstructionPointer];
				}
			});
			vm.RegisterCommand(']', b =>
			{
				if (b.Memory[b.MemoryPointer] != 0)
				{
					b.InstructionPointer = closeBrackets[b.InstructionPointer];
				}
			});
		}
	}
}