namespace BoilerPipe
{
	/// <summary>
	/// Adds labels to a
	/// <see cref="NBoilerpipe.Document.TextBlock">NBoilerpipe.Document.TextBlock</see>
	/// if the given criteria are met.
	/// </summary>
	/// <author>Christian Kohlsch√ºtter</author>
	public sealed class ConditionalLabelAction : LabelAction
	{
		private readonly TextBlockCondition condition;

		public ConditionalLabelAction(TextBlockCondition condition, params string[] labels
			) : base(labels)
		{
			this.condition = condition;
		}

		public override void AddTo(TextBlock tb)
		{
			if (condition.MeetsCondition(tb))
			{
				AddLabelsTo(tb);
			}
		}
	}
}
