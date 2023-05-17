using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Xml.Linq;
using System.ComponentModel;

namespace Car_Store.models
{
    public class vehicle
    {
        public vehicle()
        {
                
        }

        public vehicle(
            string car_status = "",
            string showroom = "",
            string Brand = "",
            int CC_Range = 0,
            string color = "",
            int year_model = 0,
            string Gearing = "",
            string Body_Style = ""
            )
        {
            this.car_status = car_status;
            this.showroom = showroom;
            this.Brand = Brand;
            this.CC_Range = CC_Range;
            this.color = color;
            this.year_model = year_model;
            this.Gearing = Gearing;
            this.Body_Style = Body_Style;
            this.car_image = "";
        }

        public vehicle(
            byte[] car_image,
            string car_status = "",
            string showroom = "",
            string Brand = "",
            int CC_Range = 0,
            string color = "",
            int year_model = 0,
            string Gearing = "",
            string Body_Style = "",
            int Rating = 0
            )
        {
            this.car_status = car_status;
            this.showroom = showroom;
            this.Brand = Brand;
            this.CC_Range = CC_Range;
            this.color = color;
            this.year_model = year_model;
            this.Gearing = Gearing;
            this.Body_Style = Body_Style;
            this.car_image = Convert.ToBase64String(car_image);
            this.Rating = Rating;
        }


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
        [Required, MaxLength(30)]
        public string name;

        [Required]
        public int Engine_Capacity;
        [Required]
        public int hourse_power;
        [Required]
        public int maximum_speed;
        [Required]
        public int Warranty_years;
        [Required]
        public int Warranty_Kilometers;
        [Required]
        public int acceleration;
        [Required]
        public int speeds;
        [Required,MaxLength(20)]
        public string Fuel;
        [Required]
        public string Liter_per_100KM; //note it should be float
        [Required]
        public int width;
        [Required]
        public int height;
        [Required]
        public int Trunk_Size;
        [Required]
        public int Seats;
        [Required, MaxLength(30)]
        public string Traction_Type;
        [Required]
        public int Cylinders;
        [Required,MaxLength(100)]
        public string CarDescription;
        [Required]
        public int Tank_Capacity;
        [Required]
        public string car_image2;
        [Required]
        public string car_image3;
        [Required]
        public int Rating;



    }
}
