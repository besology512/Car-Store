using System.ComponentModel.DataAnnotations;

namespace Car_Store.models
{
    public class used_vehicle
    {
        [Required]
        public int Vehicle_id;

        [Required]
        public int Km;// car moved kilometers

        [Required]
        public int price;

        [Required]
        public string posting_date;

        [Required]
        public int car_class;

        [Required,MaxLength(30)]
        public string city;
    }
}
