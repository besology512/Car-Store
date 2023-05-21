using System.Data;
using System.Xml.Linq;
using Car_Store.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Car_Store.Pages
{
    public class AdminViewModel : PageModel
    {
        [BindProperty] public DataTable DeliveringCount { get; set; }
        [BindProperty] public DataTable ProcessingCount { get; set; }
        [BindProperty] public DataTable ProductsCount { get; set; }
        [BindProperty] public DataTable Alerts { get; set; }
        [BindProperty] public DataTable Profit { get; set; }
        [BindProperty] public DataTable BodyStyle { get; set; }
        [BindProperty] public DataTable CarModel { get; set; }
        [BindProperty] public DataTable CarBrands { get; set; }
        [BindProperty] public DataTable BranchSupplier { get; set; }
        [BindProperty] public DataTable CarsInBranches { get; set; }
        [BindProperty] public DataTable EmployeesByBranch { get; set; }
        [BindProperty] public DataTable EmployeesByDep { get; set; }
        [BindProperty] public DataTable BranchByCity { get; set; }
        [BindProperty]
        public DataTable EmployeesByJob { get; set; }
        [BindProperty]
        public DataTable JobSalary { get; set; }
        [BindProperty]
        public List<DataPoint> MaleFemaleDepartment { get; set; }
        private readonly DB dB;
        public AdminViewModel(DB dB)
        {
            this.dB = dB;
            MaleFemaleDepartment = new List<DataPoint>();
        }
        public void OnGet()
        {
            Alerts = (DataTable)dB.Select("select Count(*) count from PENDING_POSTS");
            DeliveringCount = (DataTable)dB.Select("select Count(*) count from orders where order_status  = 'Delivering'");
            ProcessingCount = (DataTable)dB.Select("select Count(*) count from orders where order_status  = 'Processing'");
            ProductsCount = (DataTable)dB.Select("select Count(*) count from VEHICLE");
            Profit = (DataTable)dB.Select("SELECT ORDERS.order_date, SUM(Vehicles_view.Price) AS profit FROM orderItems JOIN ORDERS ON ORDERS.ClientID = orderItems.Customer_ID JOIN Vehicles_view ON Vehicles_view.Vehicle_No = orderItems.vehichle_ID GROUP BY ORDERS.order_date;");
            BodyStyle = (DataTable)dB.Select("SELECT Body_Style, COUNT(*) Count ,COUNT(CASE WHEN Car_Status = 'New' THEN 1 END) AS Num_New_Cars, COUNT(CASE WHEN Car_Status = 'Used' THEN 1 END) AS Num_Used_Cars FROM VEHICLE GROUP BY Body_Style");
            CarModel = (DataTable)dB.Select("SELECT Year_Model, COUNT(*) Count ,COUNT(CASE WHEN Car_Status = 'New' THEN 1 END) AS Num_New_Cars, COUNT(CASE WHEN Car_Status = 'Used' THEN 1 END) AS Num_Used_Cars FROM VEHICLE GROUP BY Year_Model");
            CarBrands = (DataTable)dB.Select("SELECT V.Brand, COUNT(*) AS Num_Cars, AVG(COALESCE(N.Price, U.Price)) AS Avg_New_Price, COUNT(CASE WHEN V.Car_Status = 'New' THEN 1 END) AS Num_New_Cars, COUNT(CASE WHEN V.Car_Status = 'Used' THEN 1 END) AS Num_Used_Cars FROM VEHICLE V LEFT JOIN NEW_VEHICLE N ON V.Vehicle_No = N.Vehicle_ID LEFT JOIN USED_VEHICLE U ON V.Vehicle_No = U.Vehicle_ID GROUP BY V.Brand;");
            BranchSupplier = (DataTable)dB.Select("SELECT B.Bname, COUNT(S.Supplier_Name) AS Num_Suppliers FROM BRANCH B LEFT JOIN SUPPLIER S ON B.BranchID = S.BranchNo GROUP BY B.Bname");
            CarsInBranches = (DataTable)dB.Select("SELECT b.Bname, COUNT(*) AS NumCars,AVG(Price) 'Average Price' FROM BRANCH b JOIN NEW_VEHICLE nv ON b.BranchID = nv.branch_num GROUP BY b.Bname;");
            EmployeesByDep = (DataTable)dB.Select("SELECT d.Department_Name, COUNT(*) as NumEmployees FROM DEPARTMENT d JOIN works_in_branchdep w ON d.DeparmentID = w.Dep_ID GROUP BY d.Department_Name;");
            EmployeesByBranch = (DataTable) dB.Select("SELECT b.Bname, COUNT(*) as NumEmployees FROM BRANCH b JOIN works_in_branchdep w ON b.BranchID = w.Branch_ID GROUP BY b.Bname;");
            BranchByCity = (DataTable)dB.Select("SELECT b.City, COUNT(DISTINCT b.BranchID) AS Branch_Count, COUNT(DISTINCT d.DeparmentID) AS Department_Count FROM BRANCH b LEFT JOIN DEPARTMENT d ON d.BranchNo = b.BranchID GROUP BY b.City;");
            EmployeesByJob = (DataTable)dB.Select("SELECT JOB_TYPE, COUNT(*) AS Employee_Count FROM EMPLOYEE GROUP BY JOB_TYPE;");
            JobSalary = (DataTable)dB.Select("SELECT JOB_TYPE,  MAX(Salary) AS Highest_Salary, MIN(Salary) AS Lowest_Salary, AVG(Salary) AS Average_Salary FROM EMPLOYEE GROUP BY JOB_TYPE;");
            MaleFemaleDepartment = dB.GetData("SELECT d.Department_Name as Id, SUM(CASE WHEN e.Gender = 'M' THEN 1 ELSE 0 END) AS XValue, SUM(CASE WHEN e.Gender = 'F' THEN 1 ELSE 0 END) AS YValue FROM EMPLOYEE e INNER JOIN works_in_branchdep w ON e.ID = w.Emp_ID INNER JOIN DEPARTMENT d ON w.Dep_ID = d.DeparmentID GROUP BY d.Department_Name");
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("User_Type");
            HttpContext.Session.Remove("User_ID");
            return RedirectToPage("/Index");
        }
    }
}
