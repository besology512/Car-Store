using Microsoft.AspNetCore.Authentication.OAuth.Claims;

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
        ~WishCart() {}
    }
}
