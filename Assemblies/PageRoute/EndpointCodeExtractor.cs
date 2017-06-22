namespace PageRoute
{
	/// <summary>
	/// Determines the endpoint code from the specified file path.
	/// The file path is the Request.FilePath.
	/// </summary>
	public class EndpointCodeExtractor
	{
		public static string GetEndpointCodeFromPath (string FilePath)
		{
			//
			// GET THE FILE PATH COMPONENTS.
			//
			string[] FilePathComponents = FilePath.Split('/');
			
			//
			// THE LAST FILE PATH COMPONENT IS THE FILE NAME.
			//
			string FileName = FilePathComponents[FilePathComponents.Length - 1];
			
			//
			// THE PART OF THE FILE NAME BEFORE THE EXTENSION IS THE ENDPOINT CODE.
			//
			return FileName.Split('.')[0];
		}
	}
}
