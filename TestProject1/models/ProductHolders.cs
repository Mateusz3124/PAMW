using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientClass;
using ProductClass;
using ProductClassInterface;

namespace Holder
{
    public class ProductHolder
    {
        List<Product> list = new List<Product>();

        public string create(Product product)
        {
            list.Add(product);
            return "ok";
        }
        public string remove(Product client)
        {
            int i = 0;
            foreach (Product cl in list)
            {
                if (cl.Id == client.Id)
                {
                    list.RemoveAt(i);
                    return "ok";
                }
                i++;
            }
            throw new Exception("no object found");
        }
        public string update(Product client, int id)
        {
            int i = 0;
            foreach (Product cl in list)
            {
                if (cl.Id == id)
                {
                    list[i] = client;
                    return "ok";
                }
                i++;
            }
            throw new Exception("no object with this id found");
        }
        public Product read(int Id)
        {
            int i = 0;
            foreach (Product cl in list)
            {
                if (cl.Id == Id)
                {
                    return list[i];
                }
                i++;
            }
            throw new Exception("no object found");
        }

        public Boolean contains(int id)
        {
            foreach (Product cl in list)
            {
                if (cl.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean containsObject(IProduct product)
        {
            foreach (Product cl in list)
            {

                if (cl.Id == product.Id && cl.Name.Equals(product.Name) && cl.status.Equals(product.status) && cl.Price == product.Price)
                {
                    return true;
                }
            }
            return false;
        }

        public void chanegstatus(IProduct product)
        {
            int i = 0;
            foreach (Product cl in list)
            {

                if (cl.Id == product.Id && cl.Name.Equals(product.Name) && cl.status.Equals(product.status) && cl.Price == product.Price)
                {
                    cl.changeStatus(product.status);
                    list[i] = cl;
                }
                i++;
            }
        }


        public string add(Product client)
        {
            if (list.Contains(client))
            {
                throw new Exception("object already exists");
            }
            list.Add(client);
            return "ok";
        }

        public double showPrice(int id)
        {
            foreach(Product cl in list)
            {
                if (cl.Id == id)
                {
                    return cl.Price;
                }
            }
            throw new Exception("no product with this Id exists");
        }
    }
}
