﻿using Microsoft.AspNetCore.Identity;
using SuperShop.Data.Entities;
using SuperShop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;

        public SeedDB(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");

            var user = await _userHelper.GetUserByEmailAsync("ricardofoxbs@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Ricardo",
                    LastName = "Delgado",
                    Email = "ricardofoxbs@gmail.com",
                    UserName = "ricardofoxbs@gmail.com",
                    PhoneNumber = "913948276"
                };

                var result = await _userHelper.AddUserAsync(user, "Passw0rd");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {

            }

            if (!_context.Products.Any())
            {
                AddProduct("iPhone X", user);
                AddProduct("Magic Mouse", user);
                AddProduct("iWatch Series 4", user);
                AddProduct("iPad Mini", user);
                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            _context.Products.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(100),
                User = user
            });
        }
    }
}
