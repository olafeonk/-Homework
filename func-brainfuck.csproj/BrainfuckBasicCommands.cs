using System;


namespace func.brainfuck
{
	public static class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			RegisterCommandPrint(vm, write);
			RegisterCommandsIncAndDec(vm);
			RegisterCommandsShift(vm);
			RegisterCommandRead(vm, read);
			RegisterCommandConstants(vm);
		}
		
		private static void RegisterCommandPrint(IVirtualMachine vm, Action<char> write)
		{
			vm.RegisterCommand('.', b => write((char) b.Memory[b.MemoryPointer]));
		}

		private static void RegisterCommandsIncAndDec(IVirtualMachine vm)
		{
			unchecked
			{
				vm.RegisterCommand('+', f => vm.Memory[vm.MemoryPointer]++);
				vm.RegisterCommand('-', f => vm.Memory[vm.MemoryPointer]--);
			}
		}

		private static void RegisterCommandsShift(IVirtualMachine vm)
		{
			vm.RegisterCommand('>', b =>
			{
				b.MemoryPointer = (b.MemoryPointer + 1) % b.Memory.Length;
			});
			vm.RegisterCommand('<', b =>
			{
				b.MemoryPointer = (b.MemoryPointer + b.Memory.Length - 1) % b.Memory.Length;
			});
		}
		
		private static void RegisterCommandRead(IVirtualMachine vm, Func<int> read)
		{
			vm.RegisterCommand(',', b => b.Memory[b.MemoryPointer] = (byte) read());
		}

		private static void RegisterCommandConstants(IVirtualMachine vm)
		{
			foreach (var elem in "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray())
			{
				vm.RegisterCommand(elem, b => b.Memory[b.MemoryPointer] = (byte) elem);
			}
		}
	}
}