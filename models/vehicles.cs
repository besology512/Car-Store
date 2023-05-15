using System.ComponentModel.DataAnnotations;

namespace Car_Store.models
{
    public class vehicles
    {
        [Required, MaxLength(20)]
        public string car_status; // status of the car used,new 

        [MaxLength(100)]
        public string showroom;

        [Required,MaxLength(50)]
        public string Brand;

        [Required]
        public int CC_Range;

        [Required,MaxLength(20)]
        public string color;

        [Required]
        public int year_model;

        [Required, MaxLength(20)]
        public string Gearing; // the car manual or automatic

        [Required,MaxLength(20)]
        public string Body_Style; // style of the car --> suv,sedan,copeh

        [MaxLength(150)]
        public string car_image;
    }
}
