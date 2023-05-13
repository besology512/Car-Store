using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Car_Store.models
{
    public class ShowRoom
    {
        private DB db = new DB();
        [Required, MaxLength(100)]
        public string Name;
        [Required, MaxLength(50)]
        public string City;
        [Required, MaxLength(100)]
        public string Street;        
        [Required, MaxLength(20)]
        public string PhoneNumber;
        public void insert()
        {
            db.insertShowRoom(this.Name, this.City, this.Street, this.PhoneNumber);
        }
        public object getAll() {
            return db.ReadAll("SHOWROOM");
        }
        public object getTuple()
        {
            return db.ReadTuple(Name,"SHOWROOM", "Show_Room_Name");
        }
        public void update(string PreviousName)
        {
            db.updateTupleRoom(PreviousName, Name, City, Street, PhoneNumber);
        }
        public void delete(string Name)
        {
            db.deletetuple("SHOWROOM", Name, "Show_Room_Name");
        }
    }
}
