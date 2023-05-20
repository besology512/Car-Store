using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System.Data;

namespace Car_Store.models
{
    public class WishCart
    {
        private DB db = new DB();
        public int CId { get; set; }
        public int PId { get; set; }
        public int typepv { get; set; }
        public int typecw { get; set; }

        public void insert() {
            if (typepv == 0 && typecw == 0)
            {
                db.insertCartWish("Cart_products", CId, PId);
            }
            else if (typepv == 0 && typecw == 1)
            {
                db.insertCartWish("wishlist_products", CId, PId);
            }
            else if (typepv == 1 && typecw == 1)
            {
                db.insertCartWish("wishlist_vehicle", CId, PId);
            }
            else if (typepv == 1 && typecw == 0)
            {
                db.insertCartWish("Cart_vehicle", CId, PId);
            }
        }

        public DataTable getCartUsed()
        {
            return (DataTable)db.getCarUsed(CId, "Cart_vehicle");
        }        
        public DataTable getCartNew()
        {
            return (DataTable)db.getCarNew(CId, "Cart_vehicle");
        }        
        public DataTable getWishUsed()
        {
            return (DataTable)db.getCarUsed(CId, "wishlist_vehicle");
        }        
        public DataTable getWishNew()
        {
            return (DataTable)db.getCarNew(CId, "wishlist_vehicle");
        }

        public void delete(string table, string column) {
            db.deleteCartVehicle( table, PId, CId, column);
        }        
        public void addToOrders(string city, string street, string building, string house) {
            db.order(city, street, building, house, this.CId);
        }
        public void addVehicleOrdered(int Vid) {
            db.purchaseCar(Vid, this.CId);
        }
        public void minusone(int Vid) {
            db.minusNewCar(Vid);
        }
        public void changeVisibility(int Vid) {
            db.notVisible(Vid);
        }

        ~WishCart() { }
    }
}
