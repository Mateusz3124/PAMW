using Holder;
using ProductClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderClass
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public List<Product> Items { get; set; }
        public string status { get; set; }

        public Order()
        {

        }

        public Order(int IdGiven, int ClientIdGiven, List<Product> ItemsGiven, ClientHolder clientHolder, ProductHolder productHolder) 
        {
            int i = 0;
            foreach (Product item in ItemsGiven) 
            {
                if (!productHolder.contains(item.Id))
                {
                    throw new Exception("Product is not offered");
                }
            }
            if(!clientHolder.contains(ClientIdGiven))
            {
                throw new Exception("Client is not logged");
            }
            Items = ItemsGiven;
            status = "Nowe";
            Id = IdGiven;
            ClientId = ClientIdGiven;
        }
        

    }


}
