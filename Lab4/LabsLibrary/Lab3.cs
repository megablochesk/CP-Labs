using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LabsLibrary
{
    public class Lab3 : Lab
    {
		static (int m, (int City1, int City2)[] roads) LoadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
				int m;
				List<(int City1, int City2)> roads = null;

				var text = File.ReadAllLines(filePath);

				m = StringToNumeric(text[0].Split(' ')[0]);

				for (int i = 1; i < text.Length; i++)
				{
					int[] numbersInLine = text[i].Split(' ').Select(x => StringToNumeric(x)).ToArray();

					roads.Add((numbersInLine[0], numbersInLine[1]));
				}

				return (m, roads.ToArray());
			}
            else
            {
                throw CustomException.FileNotFound(filePath);
            }
        }

		static void SaveFile(string filePath, string text) => File.WriteAllText(filePath, text);

		static string SolveTask(int n, (int City1, int City2)[] roads)
		{
			var combinations = Combinations.CombinationsRosettaWoRecursion(roads, n);

			int tracks = 0;
			foreach (var combination in combinations)
			{
				if (CheckCombination(combination)) tracks++;
			}

			return tracks.ToString();
		}

        public static void Run(string inputTextFile = "INPUT.txt", string outputTextFile = "OUTPUT.txt")
        {
            (int n, (int City1, int City2)[] roads) = LoadFile(inputTextFile);
			string answer = SolveTask(n, roads);
            SaveFile(outputTextFile, answer);
        }

		public static bool CheckCombination((int City1, int City2)[] combinations)
		{
			var mentions = new Dictionary<int, int>();
			foreach (var (City1, City2) in combinations)
			{
				if (mentions.ContainsKey(City1))
				{
					mentions[City1]++;
				}
				else
				{
					mentions.Add(City1, 1);
				}

				if (mentions.ContainsKey(City2))
				{
					mentions[City2]++;
				}
				else
				{
					mentions.Add(City2, 1);
				}
			}

			return !mentions.ContainsValue(1);
		}
	}

	static class Combinations
	{
		private static IEnumerable<int[]> CombinationsRosettaWoRecursion(int m, int n)
		{
			int[] result = new int[m];
			Stack<int> stack = new(m);
			stack.Push(0);
			while (stack.Count > 0)
			{
				int index = stack.Count - 1;
				int value = stack.Pop();
				while (value < n)
				{
					result[index++] = value++;
					stack.Push(value);
					if (index != m) continue;
					yield return (int[])result.Clone();
					break;
				}
			}
		}

		public static IEnumerable<T[]> CombinationsRosettaWoRecursion<T>(T[] array, int m)
		{
			if (array.Length < m)
				throw new ArgumentException("Array length can't be less than number of selected elements");
			if (m < 1)
				throw new ArgumentException("Number of selected elements can't be less than 1");
			T[] result = new T[m];

			foreach (int[] j in CombinationsRosettaWoRecursion(m, array.Length))
			{
				for (int i = 0; i < m; i++)
				{
					result[i] = array[j[i]];
				}
				yield return result;
			}
		}
	}
}
