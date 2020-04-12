using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Handlers;
using Newtonsoft.Json;
using MyServer.Models;
using System.Net.Http.Headers;

namespace MyServer.Controllers
{
	//[RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
		private MyDBEntities myDBEntities = new MyDBEntities();
		[HttpGet]
		//[Route("find/{id}")]
		public HttpResponseMessage find(int Id)
		{
			try
			{
				var result = new HttpResponseMessage(HttpStatusCode.OK);
				result.Content = new StringContent(JsonConvert.SerializeObject(myDBEntities.Products.Single(p => p.id == Id)));
				result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				return result;
			} 
			catch
			{
				return new HttpResponseMessage(HttpStatusCode.BadRequest);

				
			}
		}


		[HttpDelete]
		[Route("delete/{id}")]
		public HttpResponseMessage delete(int Id)
		{
			try
			{
				var result = new HttpResponseMessage(HttpStatusCode.OK);
				myDBEntities.Products.Remove(myDBEntities.Products.Single(p => p.id == Id));
				myDBEntities.SaveChanges();
				return result;
			}
			catch
			{
				return new HttpResponseMessage(HttpStatusCode.BadRequest);


			}
		}


		[HttpPost]		
		public HttpResponseMessage create(Product product)
		{
			try
			{
				var result = new HttpResponseMessage(HttpStatusCode.OK);
				myDBEntities.Products.Add(product);
				myDBEntities.SaveChanges();
				return result;
			}
			catch
			{
				return new HttpResponseMessage(HttpStatusCode.BadRequest);


			}
		}

		[HttpPut]
		public HttpResponseMessage update(Product product)
		{
			try
			{
				var result = new HttpResponseMessage(HttpStatusCode.OK);
				var newProduct = myDBEntities.Products.Single(p => p.id == product.id);
				newProduct.Name = product.Name;
				newProduct.Price = product.Price;
				newProduct.Quantity = product.Quantity;
				newProduct.Status = product.Status;
				myDBEntities.SaveChanges();
				return result;
			}
			catch
			{
				return new HttpResponseMessage(HttpStatusCode.BadRequest);


			}
		}
	}
}
