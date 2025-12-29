namespace Moodle.Application.Common.Model
{
    public class SuccessResponseGet<TValue> where TValue : class
    {
        public int? Id { get; set; }
        public bool IsSuccess { get; init; }
        public TValue? Item { get; init; }

        public SuccessResponseGet()
        {

        }
        public SuccessResponseGet(bool value, int? id, TValue Item)
        {
            IsSuccess = value;
            Item = Item;
            Id = id;
        }
    }
}
