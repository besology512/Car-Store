using System.ComponentModel.DataAnnotations;

namespace Car_Store.models
{
    public class vehicles
    {
        [Required, MaxLength(20)]
        string car_status; // status of the car used,new 

        [MaxLength(100)]
        string showroom;

        [Required,MaxLength(50)]
        string Brand;

        [Required]
        int CC_Range;

        [Required,MaxLength(20)]
        string color;

        [Required]
        int year_model;

        [Required, MaxLength(20)]
        string Gearing; // the car manual or automatic

        [Required,MaxLength(20)]
        string Body_Style; // style of the car --> suv,sedan,copeh

        [MaxLength(150)]
        string car_image;
    }
}
