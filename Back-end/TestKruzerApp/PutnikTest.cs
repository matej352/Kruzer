using FluentAssertions;
using KruzerApp.Models;
using KruzerApp.Repositories;
using KruzerApp.Repositories.impl;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace TestKruzerApp
{
    [TestFixture]
    public class PutnikTest
    {
        private KruzerContext? _context;
        private PutnikRepository? _putnikRepository;
        private KrstarenjeRepository? _krstarenjeRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<KruzerContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new KruzerContext(options);
            _krstarenjeRepository = new KrstarenjeRepository(_context);


            _putnikRepository = new PutnikRepository(_context, _krstarenjeRepository);
        }

        [TearDown]
        public void TearDown()
        {
            if (_context is not null)
            {
                _context.Database.EnsureDeleted();
                _context.Dispose();
            }
        }

        [Test]
        public async Task GetById_ExistingId_ReturnsPutnikDto()
        {
            // Arrange
            var putnik = new Putnik
            {
                Id = 1,
                Ime = "John",
                Prezime = "Doe",
                Email = "john.doe@example.com",
                Nadimak = "johndoe",
                Lozinka = "lozinka",
                Spol = 'M'
            };

            _context!.Putniks.Add(putnik);
            await _context.SaveChangesAsync();

            // Act
            var result = await _putnikRepository!.GetById(putnik.Id);

            // Assert
            result.Value.Should().BeOfType<Putnik>();
            result.Value!.Id.Should().Be(putnik.Id);

        }

        [Test]
        public async Task GetById_NonExistingId_ThrowsException()
        {
            // Arrange
            var nonExistingId = 1;

            // Act
            Func<Task> act = async () => await _putnikRepository!.GetById(nonExistingId);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage($"Putnik with id {nonExistingId} does not exists!");

        }
    }
}