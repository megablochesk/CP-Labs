class Program
{
	public static void Main()
	{
		int n = 3;
		int m = 3;
		int p = 4;

		var t = new SegmentTree(m + 2);

		var v = new List<(int, int)>
		{
			(3, 1),
			(1, 2),
			(1, 3),
			(3, 2)
		};

		v.Sort();
		v.Reverse();

		int sum = 0;

		var q = new Queue<int>();

		for (int i = 0; i < p; ++i)
		{
			if (i > 0 && v[i].Item1 != v[i - 1].Item1)
			{
				while(q.Count > 0)
				{
					int top = q.Peek();
					q.Dequeue();

					t.Update(top, 1);
				}
			}

			q.Enqueue(v[i].Item2);

			sum += t.Op(0, v[i].Item2);
		}

		Console.WriteLine(sum);
	}


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
