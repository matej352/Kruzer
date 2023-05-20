using System;
using System.Threading.Tasks;
using FluentAssertions;
using KruzerApp.Controllers;
using KruzerApp.DTOs;
using KruzerApp.Models;
using KruzerApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace TestKruzerApp
{
    [TestFixture]
    public class LokacijaControllerTest
    {

        private LokacijaController _controller;
        private Mock<ILokacijaRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<ILokacijaRepository>();
            _controller = new LokacijaController(_repository.Object);
        }

        [Test]
        public async Task CreateLokacija_ValidInput_ReturnsOkWithLokacijaDto()
        {
            // Arrange
            var newLokacijaDto = new CreateLokacijaDto
            {
                Grad = "Napulj",
                Država = "Italija"
            };

            int newLokacijaId = 1; // Set the expected new Lokacija Id

            var expectedLokacijaDto = new LokacijaDto
            {
                Id = 1,
                Grad = "Napulj",
                Država = "Italija"
            };

            _repository.Setup(r => r.Save(newLokacijaDto)).ReturnsAsync(newLokacijaId);
            _repository.Setup(r => r.GetById(newLokacijaId)).ReturnsAsync(expectedLokacijaDto);

            // Act
            var result = await _controller.CreateLokacija(newLokacijaDto);

            // Assert
            result.Should().BeOfType<ActionResult<LokacijaDto>>();

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.StatusCode.Should().Be(StatusCodes.Status200OK);

            var returnedLokacijaDto = okResult?.Value as LokacijaDto;
            returnedLokacijaDto.Should().NotBeNull();
            returnedLokacijaDto.Should().BeEquivalentTo(expectedLokacijaDto);
        }

        
    }



}
