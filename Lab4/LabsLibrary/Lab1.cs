namespace LabsLibrary
{
	public class Lab1 : Lab
	{
		static int[][] LoadFile(string filename)
		{
			int[][] matrix;

			if (File.Exists(filename))
			{
				var text = File.ReadAllLines(filename);

				int numberOfFriends = StringToNumeric(text[0]);

				if (text.Length - 1 != numberOfFriends) 
				{
					throw CustomException.WrongMatrixSizes(numberOfFriends);
				}

				matrix = new int[numberOfFriends][];
				for (int i = 1; i < text.Length; i++)
				{
					string[] numbersInLine = text[i].Split(' ');

					int[] formattedNumbersInLines = numbersInLine.Select(x => StringToNumeric(x)).ToArray();

					if (formattedNumbersInLines.Length == numberOfFriends)
					{
						matrix[i - 1] = formattedNumbersInLines;
					}
					else
					{
						throw CustomException.WrongMatrixSizes(numberOfFriends);
					}
				}
			}
			else
			{
				throw CustomException.FileNotFound(filename);
			}

			return matrix;
		}

		static void CreateFileWithOutput(int[][] sample, string filename)
		{
			foreach (int[] line in sample)
			{
				foreach(int number in line)
				{
					if (number * number != number)
					{
						CustomException.WrongNumber();
					}
				}
			}

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

			string answer = string.Empty;
			int color = 2;
			for (int i = 0; i < sample.Length; i++)
			{
				if (i > 0)
				{
					answer += " ";
				}

				if (((bestCode >> i) & 1) != 0)
				{
					answer += "1";
				}
				else
				{
					answer += $"{color}";
					color++;
				}
			}

			File.WriteAllText(filename, answer);
		}

		public static void Run(string inputTextFile = "INPUT.txt", string outputTextFile = "OUTPUT.txt")
		{   
			int[][] number = LoadFile(inputTextFile);
			CreateFileWithOutput(number, outputTextFile);
		}
	}
}
