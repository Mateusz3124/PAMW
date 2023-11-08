using ClientClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderClass;
using ProductClass;
using ClientClass;

namespace Holder
{
    public class OrderHolder
    {
        List<Order> list = new List<Order>();

        public string create(Order order, Product product, Client client)
        {
            list.Add(order);
            return "ok";
        }
        public string remove(Order client)
        {
            int i = 0;
            foreach (Order cl in list)
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
        public Order read(Order client)
        {
            int i = 0;
            foreach (Order cl in list)
            {
                if (cl.Id == client.Id)
                {
                    return list[i];
                }
                i++;
            }
            throw new Exception("no object found");
        }

        public string add(Order client)
        {
            if (list.Contains(client))
            {
                throw new Exception("no object found");
            }
            return "ok";
        }

        public void cancel(int id)
        {
            int i = 0;
            foreach (Order cl in list)
            {
                if (cl.Id == id)
                {
                    list[i].status = "Canceled";
                }
                i++;
            }
        }

    }
}
