public class Program
{
	static void Main()
	{
		var sample = new int[][]
		{
			new int[] { 1, 1, 0, 0, 1 },
			new int[] { 1, 1, 1, 0, 1 },
			new int[] { 0, 1, 1, 1, 0 },
			new int[] { 0, 0, 1, 1, 0 },
			new int[] { 1, 1, 0, 0, 1 }
		};

		int bestCode = -1;
		int bestSize = 0;
		for (int groupCode = 1; groupCode < (1 << sample.Length); groupCode++)
		{
			int size = 0;
			for (int i = 0; i < sample.Length; i++)
			{
				if (((groupCode >> i) & 1) != 0)
				{
					size++;
				}
			}

			if (size > 5)
			{
				continue;
			}

			if (size <= bestSize) continue;

			bool good = true;
			for (int i = 0; i < sample.Length && good; i++)
				for (int j = 0; j < sample.Length && good; j++)
				{
					if ((((groupCode >> i) & 1) != 0) && (((groupCode >> j) & 1) != 0) && sample[i][j] != 1)
					{
						good = false;
					}
				}

			if (good)
			{
				bestCode = groupCode;
				bestSize = size;
			}
		}

		Console.Write($"{sample.Length - bestSize + 1} ");

		int color = 2;
		for (int i = 0; i < sample.Length; i++)
		{
			if (i > 0)
			{
				Console.Write(" ");
			}

			if (((bestCode >> i) & 1) != 0)
			{
				Console.Write("1");
			}
			else
			{
				Console.Write($"{color}");
				color++;
			}
		}
	}
}
