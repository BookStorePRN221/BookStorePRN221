﻿using BookStoreAPI.Core.Interface;
using BookStoreAPI.Core.Model;
using BookStoreAPI.Infracstructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreAPI.Infracstructure.Repositories
{
    public class ImageRepository : GenericRepository<BookStoreAPI.Core.Model.ImageBook>, IImageBookRepository
    {
        public ImageRepository(DbContextClass context) : base(context)
        {
        }
    }
}
