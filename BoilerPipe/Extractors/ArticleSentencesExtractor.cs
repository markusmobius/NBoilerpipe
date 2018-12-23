namespace BoilerPipe
{
	/// <summary>A full-text extractor which is tuned towards extracting sentences from news articles.
	/// 	</summary>
	/// <remarks>A full-text extractor which is tuned towards extracting sentences from news articles.
	/// 	</remarks>
	/// <author>Christian Kohlsch√ºtter</author>
	public sealed class ArticleSentencesExtractor : ExtractorBase
	{
		public static readonly ArticleSentencesExtractor INSTANCE = new ArticleSentencesExtractor
			();

		/// <summary>
		/// Returns the singleton instance for
		/// <see cref="ArticleSentencesExtractor">ArticleSentencesExtractor</see>
		/// .
		/// </summary>
		public static ArticleSentencesExtractor GetInstance()
		{
			return INSTANCE;
		}

		/// <exception cref="NBoilerpipe.BoilerpipeProcessingException"></exception>
		public override bool Process(TextDocument doc)
		{
			return ArticleExtractor.INSTANCE.Process(doc) | SplitParagraphBlocksFilter.INSTANCE
				.Process(doc) | MinClauseWordsFilter.INSTANCE.Process(doc);
		}
	}
}
