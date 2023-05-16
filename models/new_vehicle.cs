using System.ComponentModel.DataAnnotations;

namespace Car_Store.models
{
    public class new_vehicle : vehicle
    {
        [Required]
        int vehicle_id;

        [Required]
        int price;
    }
}
