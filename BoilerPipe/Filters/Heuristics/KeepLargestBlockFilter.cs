using System.Collections.Generic;
using Sharpen;

namespace BoilerPipe
{
	/// <summary>
	/// Keeps the largest
	/// <see cref="NBoilerpipe.Document.TextBlock">NBoilerpipe.Document.TextBlock</see>
	/// only (by the number of words). In case of
	/// more than one block with the same number of words, the first block is chosen.
	/// All discarded blocks are marked "not content" and flagged as
	/// <see cref="NBoilerpipe.Labels.DefaultLabels.MIGHT_BE_CONTENT">NBoilerpipe.Labels.DefaultLabels.MIGHT_BE_CONTENT
	/// 	</see>
	/// .
	/// Note that, by default, only TextBlocks marked as "content" are taken into consideration.
	/// </summary>
	/// <author>Christian Kohlsch√ºtter</author>
	public sealed class KeepLargestBlockFilter : BoilerpipeFilter
	{
		public static readonly KeepLargestBlockFilter INSTANCE = new KeepLargestBlockFilter(false);

		public static readonly KeepLargestBlockFilter INSTANCE_EXPAND_TO_SAME_TAGLEVEL = new KeepLargestBlockFilter(true);

		private readonly bool expandToSameLevelText;

		public KeepLargestBlockFilter(bool expandToSameLevelText)
		{
			this.expandToSameLevelText = expandToSameLevelText;
		}

		/// <exception cref="NBoilerpipe.BoilerpipeProcessingException"></exception>
		public bool Process(TextDocument doc)
		{
			IList<TextBlock> textBlocks = doc.GetTextBlocks();
			if (textBlocks.Count < 2)
			{
				return false;
			}
			int maxNumWords = -1;
			TextBlock largestBlock = null;
			int level = -1;
			int i = 0;
			int n = -1;
			foreach (TextBlock tb in textBlocks)
			{
				if (tb.IsContent())
				{
					int nw = tb.GetNumWords();
					if (nw > maxNumWords)
					{
						largestBlock = tb;
						maxNumWords = nw;
						n = i;
						if (expandToSameLevelText)
						{
							level = tb.GetTagLevel();
						}
					}
				}
				i++;
			}
			foreach (TextBlock tb_1 in textBlocks)
			{
				if (tb_1 == largestBlock)
				{
					tb_1.SetIsContent(true);
				}
				else
				{
					tb_1.SetIsContent(false);
					tb_1.AddLabel(DefaultLabels.MIGHT_BE_CONTENT);
				}
			}
			if (expandToSameLevelText && n != -1)
			{
				for (ListIterator<TextBlock> it = textBlocks.ListIterator(n); it.HasPrevious(); )
				{
					TextBlock tb_2 = it.Previous();
					int tl = tb_2.GetTagLevel();
					if (tl < level)
					{
						break;
					}
					else
					{
						if (tl == level)
						{
							tb_2.SetIsContent(true);
						}
					}
				}
				for (ListIterator<TextBlock> it_1 = textBlocks.ListIterator(n); it_1.HasNext(); )
				{
					TextBlock tb_2 = it_1.Next();
					int tl = tb_2.GetTagLevel();
					if (tl < level)
					{
						break;
					}
					else
					{
						if (tl == level)
						{
							tb_2.SetIsContent(true);
						}
					}
				}
			}
			return true;
		}
	}
}
