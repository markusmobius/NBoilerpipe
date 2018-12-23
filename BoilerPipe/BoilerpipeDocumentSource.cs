namespace BoilerPipe
{
	/// <summary>
	/// Something that can be represented as a
	/// <see cref="NBoilerpipe.Document.TextDocument">NBoilerpipe.Document.TextDocument</see>
	/// .
	/// </summary>
	public interface BoilerpipeDocumentSource
	{
		/// <exception cref="NBoilerpipe.BoilerpipeProcessingException"></exception>
		TextDocument ToTextDocument();
	}
}
