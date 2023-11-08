using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientClass;
using ProductClass;

namespace Holder
{
    public class ClientHolder
    {
        List<Client> list = new List<Client>();

        public string create(Client client)
        {
            list.Add(client);
            return "ok";
        }
        public string remove(Client client)
        {
            int i = 0;
            foreach (Client cl in list)
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

        public Boolean contains(int id)
        {
            foreach (Client cl in list)
            {
                if (cl.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public Client read(Client client)
        {
            int i = 0;
            foreach (Client cl in list)
            {
                if (cl.Id == client.Id)
                {
                    return list[i];
                }
                i++;
            }
            throw new Exception("no object found");
        }

        public string add(Client client)
        {
            if (list.Contains(client))
            {
                throw new Exception("no object found");
            }
            list.Add (client);
            return "ok";
        }

    }
}
