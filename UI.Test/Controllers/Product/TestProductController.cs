﻿using Application.Contracts.ProuductServices;
using Application.Dtos.ProductDtos;
using Application.Dtos.ShoppingCart;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using UI.Controllers;

namespace UI.Test.Controllers.Product
{
    public class TestProductController
    {
        private readonly Fixture _fixture;
        private readonly Mock<IProductServices> _moqServices =new();
        public TestProductController()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Get_ShouldReturnOkResult_WhenDataFound()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);

            //Act
            var result = productMock.MiniDetailsProducts();

            //Assert
            Assert.IsType<OkObjectResult>(result);

        }
        [Fact]
        public void Get_ShouldReturnBadRequestResult_WhenException()
        {
            //Arrange
            _moqServices.Setup(p => p.MiniDetailsProducts()).Throws(new Exception());
            var productMock = new ProductController(_moqServices.Object);
            //Act
            var result = productMock.MiniDetailsProducts();

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }

        [Fact]
        public void GetById_ShouldReturnOkResult_WhenDataFound()
        {
            //Arrange
            var productId = 3;
            var productsDto = _fixture.Create<DetailedProductDto>();
            _moqServices.Setup(p => p.GetProductById(productId)).Returns(productsDto);
            var productMock = new ProductController(_moqServices.Object);

            //Act
            var result = productMock.ProudactById(productId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetById_ShouldReturnNotFoundResult_WhenDataNotFound()
        {
            //Arrange
            var productId = 3;
            var productsDto = _fixture.Create<DetailedProductDto>();
            _moqServices.Setup(p => p.GetProductById(productId)).Returns(productsDto);
            var productMock = new ProductController(_moqServices.Object);
            
            //Act
            var result = productMock.ProudactById(5);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public void GetById_ShouldReturnBadRequestResult_WhenExceptions()
        {
            //Arrange
            var productId = 3;
            var productsDto = _fixture.Create<DetailedProductDto>();
            _moqServices.Setup(p => p.GetProductById(productId)).Throws(new Exception());
            var productMock = new ProductController(_moqServices.Object);

            //Act
            var result = productMock.ProudactById(productId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnNotFoundResult_WhenDataNotFound()
        {
            //Arrange
            int productId = 2;
            var productMock = new ProductController(_moqServices.Object);
            var productsDto = _fixture.Create<DetailedProductDto>();
            _moqServices.Setup(p => p.DeleteProduct(productId));

            //Act
            var result = productMock.DeleteProduct(3);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Delete_ShouldReturnOkResult_WhenDataFound()
        {
            //Arrange
            int productId = 2;
            var productMock = new ProductController(_moqServices.Object);
            var productsDto = _fixture.Create<DetailedProductDto>();
            _moqServices.Setup(p => p.DeleteProduct(productId)).Returns(true);
           
            //Act
            var result = productMock.DeleteProduct(productId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Delete_ShouldReturnBadRequestResult_WhenExceptions()
        {
            //Arrange
            int productId = 2;
            var productMock = new ProductController(_moqServices.Object);
            var productsDto = _fixture.Create<DetailedProductDto>();
            _moqServices.Setup(p => p.DeleteProduct(productId)).Throws(new Exception());

            //Act
            var result = productMock.DeleteProduct(productId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Add_ShouldReturnOkResult_WhenDataValid()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);
            var productDto = _fixture.Create<AddProductDto>();
            _moqServices.Setup(p => p.AddProduct(productDto));
            //Act
            var result = productMock.AddProduct(productDto);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Add_ShouldReturnBadRequestResult_WhenExceptions()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);
            var productDto = _fixture.Create<AddProductDto>();
            _moqServices.Setup(p => p.AddProduct(productDto)).Throws(new Exception());
            //Act
            var result = productMock.AddProduct(productDto);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void FilteringData_ShouldReturnOkResult_WhenDataFound()
        {
            //Arrange
            string productName = "pro";
            var productMock = new ProductController(_moqServices.Object);
            var productDto = new List<MiniProductDto>
            {
                new MiniProductDto(){
                Name = "pro",
                ImageUrl = "Test",
                Price = 500 }
            };
            _moqServices.Setup(p => p.FilteringData(productName)).Returns(productDto);

            //Act
            var result = productMock.FilteringData(productName);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        [Fact]
        public void FilteringData_ShouldReturnNotFoundResult_WhenDataNotFound()
        {
            //Arrange
            string productName = "aass";
            string Name = "aaa";
            var productMock = new ProductController(_moqServices.Object);
            var productDto = new List<MiniProductDto>
            {
               new MiniProductDto(){
              Name = "pro",
              ImageUrl = "Test",
              Price = 500 }
            };
            _moqServices.Setup(p => p.FilteringData(productName)).Returns(productDto);

            //Act
            var result = productMock.FilteringData(Name);

            //Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }
        
        [Fact]
        public void Update_ShouldReturnOkResult_WhenDataValid()
        {
            //Arrange
            var productMock = new ProductController(_moqServices.Object);
            var productDto = new UpdateProductDto
            {
              Quantity= 100,
              Name = "pro",
              ImageUrl = "Test",
              Price = 500 ,
              Description = "pro",
              Id = 1
            };
            var updateProductDto = new UpdateProductDto
            {
                Quantity = 100,
                Name = "pro updated",
                ImageUrl = "Test",
                Price = 500,
                Description = "pro",
                Id = 1
            };

            _moqServices.Setup(p => p.UpdateProduct(productDto));

            //Act
            var result = productMock.UpdateProduct(updateProductDto);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        

    }
}