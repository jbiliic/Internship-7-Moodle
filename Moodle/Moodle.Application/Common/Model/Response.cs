namespace Moodle.Application.Common.Model
{
    public class SuccessResponse<TValue,T> where TValue : class
    {
        public int? Id { get; set; }
        public bool? IsSuccess { get; init; }
        public TValue? Value { get; init; }
        public T? ExtraData { get; init; }

        public SuccessResponse()
        {

        }
        public SuccessResponse(bool? value, int? id, TValue Item, T? extraData)
        {
            IsSuccess = value;
            Value = Item;
            Id = id;
            ExtraData = extraData;
        }
    }
}
