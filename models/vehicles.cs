using System.ComponentModel.DataAnnotations;

namespace Car_Store.models
{
    public class vehicle
    {
        public vehicle()
        {
                
        }
        public vehicle(
            int id = 0,
            string car_status = "",
            string showroom = "",
            string Brand = "",
            int CC_Range = 0,
            string color = "",
            int year_model = 0,
            string Gearing = "",
            string Body_Style = "",
            string car_image = "")
        {
            this.Id= id;
            this.car_status = car_status;
            this.showroom = showroom;
            this.Brand = Brand;
            this.CC_Range = CC_Range;
            this.color = color;
            this.year_model = year_model;
            this.Gearing = Gearing;
            this.Body_Style = Body_Style;
            this.car_image = car_image;
        }


        string car_image;

        [Required]
        public int Id;


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

        
        public IFormFile car_image1;
        public IFormFile car_image2;
        public IFormFile car_image3;

        [Required,MaxLength(1000)]
        public string cardesc;

        [MaxLength(30)]
        public string carmodel;

        [MaxLength(20)]
        public string fuel;
    }
}
