namespace BoilerPipe
{
	/// <summary>Marks all blocks as content.</summary>
	/// <remarks>Marks all blocks as content.</remarks>
	/// <author>Christian Kohlsch√ºtter</author>
	public sealed class MarkEverythingContentFilter : BoilerpipeFilter
	{
		public static readonly MarkEverythingContentFilter INSTANCE = new MarkEverythingContentFilter();

		public MarkEverythingContentFilter()
		{
		}

		/// <exception cref="NBoilerpipe.BoilerpipeProcessingException"></exception>
		public bool Process(TextDocument doc)
		{
			bool changes = false;
			foreach (TextBlock tb in doc.GetTextBlocks())
			{
				if (!tb.IsContent())
				{
					tb.SetIsContent(true);
					changes = true;
				}
			}
			return changes;
		}
	}
}
