using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class ExampleContext : DbSetContext
    {
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=ExampleDatabase;Trusted_Connection=true");
            }
            catch (Exception exception)
            {
               
                throw new Exception();
            }
        }
    }
}