using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineRestaurant.Repository
{
    public class Generic_Repository<T> : IRepository<T> where T : class
    {
        public RestaurantContext ctxt { get; set; }

        public Generic_Repository(RestaurantContext context)
        {
            this.ctxt = context;
        }

        public void Add(T obj)
        {
            ctxt.Set<T>().Add(obj);
        }

        public void Delete(int id)
        {
            if (ctxt.Set<T>().Find(id) != null)
            {
                ctxt.Remove(ctxt.Set<T>().Find(id));
            }
        }

        public List<T> GetAll()
        {
            return ctxt.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return ctxt.Set<T>().Find(id);
        }

        public void Update(T obj)
        {
            ctxt.Update(obj);
        }

        public int Save()
        {
          return ctxt.SaveChanges();
        }
    }
}