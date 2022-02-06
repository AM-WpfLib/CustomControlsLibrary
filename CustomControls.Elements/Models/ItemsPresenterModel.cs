using CustomControls.Elements.Interfaces;

namespace CustomControls.Elements.Models
{
    public class ItemsPresenterModel : IItemsPresenterModel
    {
        public ItemsPresenterModel(int sampleId, string sampleText, double sampleValue)
        {
            SampleId = sampleId;
            SampleText = sampleText;
            SampleValue = sampleValue;
        }

        public int SampleId { get; set; }
        public string SampleText { get; set; }
        public double SampleValue { get; set; }
    }
}
