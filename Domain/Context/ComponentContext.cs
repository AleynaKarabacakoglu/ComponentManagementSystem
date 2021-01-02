using Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Context
{
    public class ComponentContext: DbContext
    {
        
            public ComponentContext()
            {
            }

            public ComponentContext(DbContextOptions<ComponentContext> options) : base(options)
            {


            }
          
            public virtual DbSet<Layout> Layouts { get; set; }
            public DbSet<LayoutItem> LayoutItems { get; set; }
            public DbSet<Page> Pages { get; set; }
            public DbSet<PageContent> PageContents { get; set; }
           
            public DbSet<User> Users { get; set; }
            public DbSet<Menu> Menus { get; set; }
            public DbSet<Slider> Sliders { get; set; }
            public DbSet<SliderContent> SliderContents { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-UOVAQCJ\\SQLEXPRESS;Initial Catalog=CMSDB;Integrated Security=True;");
            }

        }
    }

