using System.Collections.Generic;
using Sharpen;

namespace BoilerPipe
{
	public sealed class ContentFusion : BoilerpipeFilter
	{
		public static readonly ContentFusion INSTANCE = new  ContentFusion();

		/// <summary>
		/// Creates a new
		/// <see cref="ContentFusion">ContentFusion</see>
		/// instance.
		/// </summary>
		public ContentFusion()
		{
		}

		/// <exception cref="NBoilerpipe.BoilerpipeProcessingException"></exception>
		public bool Process(TextDocument doc)
		{
			IList<TextBlock> textBlocks = doc.GetTextBlocks();
			if (textBlocks.Count < 2)
			{
				return false;
			}
			TextBlock prevBlock = textBlocks[0];
			bool changes = false;
			do
			{
				changes = false;
				for (ListIterator<TextBlock> it = textBlocks.ListIterator(1); it.HasNext(); )
				{
					TextBlock block = it.Next();
					if (prevBlock.IsContent() && block.GetLinkDensity() < 0.56 && !block.HasLabel(DefaultLabels
						.STRICTLY_NOT_CONTENT))
					{
						prevBlock.MergeNext(block);
						it.Remove();
						changes = true;
					}
					else
					{
						prevBlock = block;
					}
				}
			}
			while (changes);
			return true;
		}
	}
}
