using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entities;

namespace Webshop.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
        {
            var applyConfigurationMethodInfo = modelBuilder
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .First(m => m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));

            var ret = typeof(WebshopContext).Assembly
                .GetTypes()
                .Select(t => (t, i: t.GetInterfaces().FirstOrDefault(i => i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.Ordinal))))
                .Where(it => it.i != null)
                .Select(it => (et: it.i.GetGenericArguments()[0], cfgObj: Activator.CreateInstance(it.t)))
                .Select(it => applyConfigurationMethodInfo.MakeGenericMethod(it.et).Invoke(modelBuilder, new[] { it.cfgObj }))
                .ToList();
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            //add countries
            //add categories
            //add items

            modelBuilder.Entity<Country>().HasData(
                new Country { CountryId = 1, Name = "Denmark", IsShippable = true, ShippingCost = 25 },
                new Country { CountryId = 2, Name = "Sweden", IsShippable = true, ShippingCost = 25 },
                new Country { CountryId = 3, Name = "Germany", IsShippable = true, ShippingCost = 25 },
                new Country { CountryId = 4, Name = "England", IsShippable = true, ShippingCost = 35 },
                new Country { CountryId = 5, Name = "France", IsShippable = true, ShippingCost = 55 }
            );

            List<Category> categories = new List<Category>()
            {
                new Category { CategoryId = 1, Name = "Motherboards", Image = "https://cdn2.techadvisor.co.uk/cmsdata/features/3683149/the_best_motherboards_by_month_thumb800.jpg" },
                new Category { CategoryId = 2, Name = "RAM", Image = "https://brain-images-ssl.cdn.dixons.com/9/9/10143299/l_10143299_001.jpg" },
                new Category { CategoryId = 3, Name = "CPUs", Image = "https://media.wired.com/photos/593220fb2a990b06268a8ede/master/w_1100,c_limit/s-Intel-Core-X-Series-processor-family-21-690x460_2.jpg" },
                new Category { CategoryId = 4, Name = "GPUs", Image = "https://venturebeat.com/wp-content/uploads/2018/04/evga-gtx-1080-100663485-orig.png?fit=400%2C259&strip=all" },
                new Category { CategoryId = 5, Name = "Cases", Image = "https://c1.neweggimages.com/ProductImage/11-139-087-V01.jpg" },
                new Category { CategoryId = 6, Name = "HDDs", Image = "https://images-eu.ssl-images-amazon.com/images/I/41G%2B6GaAMnL._SL500_AC_SS350_.jpg" },
                new Category { CategoryId = 7, Name = "SSDs", Image = "https://images-na.ssl-images-amazon.com/images/I/61U7T1koQqL._SL1500_.jpg" },
                new Category { CategoryId = 8, Name = "PSUs", Image = "https://www.scan.co.uk/images/infopages/corsair_psu/cxm-old/750w-psu.png" },
                new Category { CategoryId = 9, Name = "Electricity Meters", Image = "https://images-na.ssl-images-amazon.com/images/I/71oW3kqnkJL._SX425_.jpg" },
                new Category { CategoryId = 10, Name = "Cooling Paste", Image = "https://www.gamingscan.com/wp-content/uploads/2017/09/best-thermal-paste.jpg" },
                new Category { CategoryId = 11, Name = "CPU Coolers", Image = "https://www.overclockers.co.uk/media/image/thumbnail/HS02NRT_177868_800x800.jpg" },
                new Category { CategoryId = 12, Name = "GPU Coolers", Image = "https://cdn.mos.cms.futurecdn.net/7FDZmCduoGkbWuki4ADXP7.jpg" },
                new Category { CategoryId = 13, Name = "Screens", Image = "https://assets.pcmag.com/media/images/469683-viewsonic-vp2768.jpg?width=810&height=456" },
                new Category { CategoryId = 14, Name = "Games", Image = "https://i.ytimg.com/vi/tezghEEHzOA/maxresdefault.jpg" },
                new Category { CategoryId = 15, Name = "Fans", Image = "https://www.corsair.com/corsairmedia/sys_master/productcontent/blog_how-to-build-a-pc-selecting-case-fans-Content-1.png" },
                new Category { CategoryId = 16, Name = "Mice", Image = "https://images-na.ssl-images-amazon.com/images/I/71Yg45FT0bL._SX425_.jpg" },
                new Category { CategoryId = 17, Name = "Mouse Mats", Image = "https://www.scan.co.uk/images/products/2895155-a.jpg" },
                new Category { CategoryId = 18, Name = "USB Sticks", Image = "https://www.thomann.de/pics/bdb/418837/12305327_800.jpg" },
                new Category { CategoryId = 19, Name = "Keyboards", Image = "https://cdn.mos.cms.futurecdn.net/xXvrP7JHWqMh9da2rwbvrT-320-80.jpg" },
                new Category { CategoryId = 20, Name = "De-Dusters", Image = "https://www.santmarliving.com/wp-content/uploads/2016/07/Falcon-Dust-Off-The-Original-Compressed-Gas-Duster.jpg" }
            };
            modelBuilder.Entity<Category>().HasData(categories);

            List<Item> items = new List<Item>();
            int count = 0;
            foreach (var category in categories)
            {
                Random r = new Random();
                int rInt = r.Next(20, 150);
                for (int i = 0; i < rInt; i++)
                {
                    count++;
                    items.Add(new Item
                    {
                        ItemId = count,
                        CategoryId = category.CategoryId,
                        Name = category.Name + " Item " + i,
                        Description = "This item is one of the best " + category.Name + " around!",
                        Price = r.Next(100, 10000),
                        Stock = r.Next(10, 500),
                    });
                }
            }
            modelBuilder.Entity<Item>().HasData(items);

        }
    }
}
