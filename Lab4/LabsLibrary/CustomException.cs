namespace LabsLibrary
{
    public class CustomException : Exception
    {
        public static Exception FileNotFound(string filePath, string solution="create new file.")
        {
            return new Exception($"\nFile \"{filePath}\" not found\nSolution: {solution}");
        }

        public static Exception NotNumeric(string wrongChar)
        {
            return new Exception($"\nWrong character was found in the number: {wrongChar}\nSolution: remove from file all characters except numerics.");
        }

        public static Exception WrongNumber()
        {
            return new Exception($"\nNumber in matrix can be only 1 or 0");
        }

		public static Exception WrongMatrixSizes(int size)
		{
			return new Exception($"\nWrong size of matrix! The maxrix must contain {size} rows and {size} columns");
		}
	}
}
