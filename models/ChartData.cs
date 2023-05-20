using System.Data.SqlClient;
namespace Car_Store.models
{
    public class DataPoint
    {
        public object Id { get; set; }
        public object XValue { get; set; }
        public object YValue { get; set; }
        public object ZValue { get; set; }
        public DataPoint(object Id, object XValue , object YValue)
        {
            this.Id = Id;
            this.XValue = XValue;
            this.YValue = YValue;
        }
    }
}
