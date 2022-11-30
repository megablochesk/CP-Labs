class Program
{
	public static void Main()
	{
		int n = 4;
		int m = 6;

		var roads = new (int City1, int City2)[]
		{
			(1, 2),
			(2, 3),
			(3, 4),
			(4, 1),
			(1, 3),
			(2, 4)
		};

		var combinations = Combinations.CombinationsRosettaWoRecursion(roads, n);

        int tracks = 0;
        foreach(var combination in combinations)
		{
            if (CheckCombination(combination)) tracks++;
		}

        Console.WriteLine(tracks);
	}
    
    public static bool CheckCombination((int City1, int City2)[] combinations)
	{
        var mentions = new Dictionary<int, int>();
        foreach(var (City1, City2) in combinations)
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
