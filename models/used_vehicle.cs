using System.ComponentModel.DataAnnotations;

namespace Car_Store.models
{
    public class used_vehicle:vehicles
    {
        [Required]
        int Vehicle_id;

        [Required]
        int Km;// car moved kilometers

        [Required]
        int price;

        [Required]
        string posting_date;

        [Required]
        int car_class;
    }
}
