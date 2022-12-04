namespace LabsLibrary
{
	public abstract class Lab
	{
		static protected int StringToNumeric(string number)
		{
			try
			{
				return Convert.ToInt32(number);
			}
			catch
			{
				throw CustomException.NotNumeric(number);
			}
		}
	}
}
