﻿using DataAccess.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Facade
{
    public class ProductFacade : IFacadeCRUD<Common.Entities.Product>
    {
        private ProductDAOSql implementation; // todo: esto podría ser un factory

        public ProductFacade() {
            implementation = new ProductDAOSql();
        }

        public void Create(Common.Entities.Product p)
        {
            implementation.Create(productToDataEntityProduct(p));
        }

        public void Update(Common.Entities.Product p)
        {
            implementation.Update(productToDataEntityProduct(p));
        }

        public void Delete(int id)
        {
            implementation.Delete(id);
        }

        public Common.Entities.Product Get(int id)
        {
            return dataEntityProductToProduct(implementation.Get(id));
        }

        public IEnumerable<Common.Entities.Product> GetAll()
        {
            DBEntities db = new DBEntities();

            List<Common.Entities.Product> products = new List<Common.Entities.Product>();
            //IEnumerable<Product> dataEntityProducts = implementation.GetAll();
            IEnumerable<Product> dataEntityProducts = db.Product;

            foreach (Product e in dataEntityProducts)
            {
                products.Add(dataEntityProductToProduct(e));
            }

            return products;
        }

        // TODO: estos mappers no deberían estar en Facade, tal vez debería ser responsabilidad de la implementación
        private Product productToDataEntityProduct(Common.Entities.Product p)
        {
            Product entity = new Product();

            entity.Id = p.Id;
            entity.ArtistId = p.ArtistId;
            entity.AvgStars = p.AvgStars;
            entity.ChangedBy = p.ChangedBy;
            entity.ChangedOn = p.ChangedOn;
            entity.CreatedBy = p.CreatedBy;
            entity.CreatedOn = p.CreatedOn;
            entity.Description = p.Description;
            entity.QuantitySold = p.QuantitySold;
            entity.Image = p.Image;
            entity.Price = p.Price;
            entity.Title = p.Title;

            return entity;
        }

        private Common.Entities.Product dataEntityProductToProduct(Product entity)
        {
            Common.Entities.Product product = new Common.Entities.Product();

            product.Id = entity.Id;
            product.ArtistId = entity.ArtistId;
            product.AvgStars = entity.AvgStars;
            product.ChangedBy = entity.ChangedBy;
            product.ChangedOn = entity.ChangedOn;
            product.CreatedBy = entity.CreatedBy;
            product.CreatedOn = entity.CreatedOn;
            product.Description = entity.Description;
            product.QuantitySold = entity.QuantitySold;
            product.Image = entity.Image;
            product.Price = entity.Price;
            product.Title = entity.Title;

            return product;
        }
    }
}