namespace BoilerPipe
{
	/// <summary>
	/// Keeps only those content blocks which contain at least k full-text words
	/// (measured by
	/// <see cref="HeuristicFilterBase.GetNumFullTextWords(NBoilerpipe.Document.TextBlock)
	/// 	">HeuristicFilterBase.GetNumFullTextWords(NBoilerpipe.Document.TextBlock)</see>
	/// ). k is 30 by default.
	/// </summary>
	/// <author>Christian Kohlsch√ºtter</author>
	public sealed class MinFulltextWordsFilter : HeuristicFilterBase, BoilerpipeFilter
	{
		public static readonly MinFulltextWordsFilter DEFAULT_INSTANCE = new MinFulltextWordsFilter(30);

		private readonly int minWords;

		public static MinFulltextWordsFilter GetDefaultInstance
			()
		{
			return DEFAULT_INSTANCE;
		}

		public MinFulltextWordsFilter(int minWords)
		{
			this.minWords = minWords;
		}

		/// <exception cref="NBoilerpipe.BoilerpipeProcessingException"></exception>
		public bool Process(TextDocument doc)
		{
			bool changes = false;
			foreach (TextBlock tb in doc.GetTextBlocks())
			{
				if (!tb.IsContent())
				{
					continue;
				}
				if (GetNumFullTextWords(tb) < minWords)
				{
					tb.SetIsContent(false);
					changes = true;
				}
			}
			return changes;
		}
	}
}
