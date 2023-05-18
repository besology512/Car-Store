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
            int id = 0,
            string car_status = "",
            string showroom = "",
            string Brand = "",
            int CC_Range = 0,
            string color = "",
            int year_model = 0,
            string Gearing = "",
            string Body_Style = "",
            string car_path = ""

            )
        {
            this.Id = id;
            this.car_status = car_status;
            this.showroom = showroom;
            this.Brand = Brand;
            this.CC_Range = CC_Range;
            this.color = color;
            this.year_model = year_model;
            this.Gearing = Gearing;
            this.Body_Style = Body_Style;
            this.car_path = car_path;
        }

        public vehicle(
            string car_image = "",
            string car_image2 = "",
            string car_image3 = "",
            int id = 0,
            string car_status = "",
            string showroom = "",
            string Brand = "",
            int CC_Range = 0,
            string color = "",
            int year_model = 0,
            string Gearing = "",
            string Body_Style = "",
            int Rating = 0,
            int visibility = 0,
            int Price = 0,
            int Count = 0,
            string car_path = ""
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
            this.Rating = Rating;
            this.Id = id;
            this.Rating = Rating;
            this.car_image = car_image;
            this.car_image2 = car_image2;
            this.car_image3 = car_image3;
            switch (visibility)
            {
                case 0: this.visibility = "Pending"; break;
                case 1: this.visibility = "In Stock"; break;
                case 2: this.visibility = "Out of Stock"; break;
                default: this.visibility = "Pending"; break;
            }
            this.Price = Price;
            this.Count = Count;
            this.car_path = car_path;

        }
        [Required]
        public int Count;
        [Required]
        public int Price;
        [Required]
        public string visibility;


        public string car_path { get; set; }
        [Required]
        public int Id;
        [Required, MaxLength(20)]
        public string car_status; // status of the car used,new 

        [MaxLength(100)]
        public string showroom;

        [Required, MaxLength(50)]
        public string Brand;

        [Required]
        public int CC_Range;

        [Required, MaxLength(20)]
        public string color;

        [Required]
        public int year_model;

        [Required, MaxLength(20)]
        public string Gearing; // the car manual or automatic

        [Required, MaxLength(20)]
        public string Body_Style; // style of the car --> suv,sedan,copeh


        [Required, MaxLength(1000)]

        public string cardesc;

        [MaxLength(30)]
        public string carmodel;

        public string car_image { get; set; }
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
        [Required, MaxLength(20)]
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
