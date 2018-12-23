namespace BoilerPipe
{
	/// <summary>Marks everything as content.</summary>
	/// <remarks>Marks everything as content.</remarks>
	/// <author>Christian Kohlsch√ºtter</author>
	public sealed class KeepEverythingExtractor : ExtractorBase
	{
		public static readonly KeepEverythingExtractor INSTANCE = new KeepEverythingExtractor();

		public KeepEverythingExtractor()
		{
		}

		/// <exception cref="NBoilerpipe.BoilerpipeProcessingException"></exception>
		public override bool Process(TextDocument doc)
		{
			return MarkEverythingContentFilter.INSTANCE.Process(doc);
		}
	}
}
