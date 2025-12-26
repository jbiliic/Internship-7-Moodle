namespace Moodle.Application.Common.Model
{
    public class SuccessResponse<TValue> where TValue : class
    {
        public int? Id { get; set; }
        public bool IsSuccess { get; init; }
        public TValue? Item { get; init; }

        public SuccessResponse()
        {

        }
        public SuccessResponse(bool value, int? id, TValue Item)
        {
            IsSuccess = value;
            Item = Item;
            Id = id;
        }
    }
}
