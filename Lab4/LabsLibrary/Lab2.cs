namespace LabsLibrary
{
    public class Lab2 : Lab
    {
        static (int m, int p, List<(int, int)> v) LoadFile(string filePath)
        {
			if (File.Exists(filePath))
			{
				int m;
				int p;
				List<(int, int)> exhibitions = null;

				var text = File.ReadAllLines(filePath);

				string[] firstLine = text[0].Split(' ');

				m = StringToNumeric(firstLine[1]);
				p = StringToNumeric(firstLine[2]);

				for (int i = 1; i < text.Length; i++)
				{
					int[] numbersInLine = text[i].Split(' ').Select(x => StringToNumeric(x)).ToArray();

					exhibitions.Add((numbersInLine[0], numbersInLine[1]));
				}

				return (m, p, exhibitions);
			}
			else
			{
				throw CustomException.FileNotFound(filePath);
			}
		}
        static void SaveFile(string filePath, string text)
        {
            File.WriteAllText(filePath, text);
        }

		private static string SolveTask(int m, int p, List<(int, int)> v)
		{
			var t = new SegmentTree(m + 2);

			v.Sort();
			v.Reverse();

			int sum = 0;

			var q = new Queue<int>();

			for (int i = 0; i < p; ++i)
			{
				if (i > 0 && v[i].Item1 != v[i - 1].Item1)
				{
					while (q.Count > 0)
					{
						int top = q.Peek();
						q.Dequeue();

						t.Update(top, 1);
					}
				}

				q.Enqueue(v[i].Item2);

				sum += t.Op(0, v[i].Item2);
			}

			return sum.ToString();
		}


        public static void Run(string inputTextFile = "INPUT.txt", string outputTextFile = "OUTPUT.txt")
        {
			(int m, int p, List<(int, int)> v) = LoadFile(inputTextFile);
			string answer = SolveTask(m, p, v);
            SaveFile(outputTextFile, answer);
        }

		struct SegmentTree
		{
			public int[] Tree;

			int m = 1;

			public SegmentTree(int n)
			{
				while (m < n) m *= 2;
				Tree = new int[2 * m - 1];
			}

			public void Update(int i, int val, int v, int lx, int rx)
			{
				if (rx - lx == 1)
				{
					Tree[v] += val;
					return;
				}

				int mid = (lx + rx) / 2;

				if (i < mid)
				{
					Update(i, val, v * 2 + 1, lx, mid);
				}
				else
				{
					Update(i, val, v * 2 + 2, mid, rx);
				}

				Tree[v] = Tree[v * 2 + 1] + Tree[v * 2 + 2];
			}

			public void Update(int i, int val)
			{
				Update(i, val, 0, 0, m);
			}

			public int Op(int l, int r, int v, int lx, int rx)
			{
				if (l >= rx || lx >= r) return 0;
				if (lx >= l && rx <= r) return Tree[v];

				int mid = (lx + rx) / 2;

				return Op(l, r, v * 2 + 1, lx, mid) + Op(l, r, v * 2 + 2, mid, rx);
			}

			public int Op(int l, int r)
			{
				return Op(l, r, 0, 0, m);
			}
		}
	}
}
