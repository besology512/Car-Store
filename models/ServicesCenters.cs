using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Car_Store.models
{
    public class ServicesCenters
    {

        private DB db = new DB();
        [Required]
        public int ID { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(200)]
        public string Address{ get; set; }
        [Required, MaxLength(300)]
        public string Services{ get; set; }
        [Required]
        public decimal latitude { get; set; }
        [Required]
        public decimal longitude { get; set; }
        [Required]
        public int stars{ get; set; }

        public void insert()
        {
            db.insertServiceCenter(this.ID, this.Name, this.Address, this.Services, this.latitude, this.longitude, this.stars);
        }
        public object getAll()
        {
            return db.ReadAll("Services_Center");
        }
        public object getTuple() //it return row data
        {
            return db.ReadTuple(ID, "Services_Center", "ID");
        }
        public void update(int ID)
        {
            db.updateServiceCenter(this.ID, this.Name, this.Address, this.Services, this.latitude, this.longitude, this.stars);
        }
        public void delete(string Name)
        {
            db.deletetuple("Services_Center", ID, "ID");
        }



    }
}
