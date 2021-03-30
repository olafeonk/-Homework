namespace yield
{
	public class Factory
	{
		public static IDataAnalyzer CreateAnalyzer()
		{
			//return new сhecking.DataAnalyzerSolvedImpl();
			return new DataAnalyzerImpl();
		}
	}
}