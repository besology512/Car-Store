using System.ComponentModel.DataAnnotations;

namespace Car_Store.models
{
    public class new_vehicle : vehicles
    {
        [Required]
        int vehicle_id;

        [Required]
        int Number_of_classes;

        [Required]
        int start_price;

        [Required]
        int end_price;
    }
}
