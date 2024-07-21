using System.Threading.Tasks;
using Xunit;
using Moq;
using Core.Interfaces;
using Application.Services;
using Application.DTOs;
using System.Collections.Generic;
using FluentAssertions;
using Core.Entity;

namespace WebApi.Tests.Services
{
    public class PoolTableServiceTests
    {
        private readonly Mock<IPoolTableRepository> _mockPoolTableRepository;
        private readonly PoolTableService _service;

        public PoolTableServiceTests()
        {
            _mockPoolTableRepository = new Mock<IPoolTableRepository>();
            _service = new PoolTableService(_mockPoolTableRepository.Object);
        }

        [Fact]
        public async Task GetAllPoolTablesAsync_ShouldReturnAllPoolTables()
        {
            // Arrange
            var pooltables = new List<PoolTable>
            {
                new PoolTable { Id = 1, Name = "PoolTable1" },
                new PoolTable { Id = 2, Name = "PoolTable2" }
            };

            _mockPoolTableRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(pooltables);

            // Act
            var result = await _service.GetAllPoolTablesAsync();

            // Assert
            result.Should().BeEquivalentTo(pooltables.Select(c => new PoolTableDto { Id = c.Id, Name = c.Name }));
        }

        [Fact]
        public async Task GetPoolTableByIdAsync_ShouldReturnPoolTable_WhenPoolTableExists()
        {
            // Arrange
            var pooltable = new PoolTable { Id = 1, Name = "PoolTable1" };

            _mockPoolTableRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(pooltable);

            // Act
            var result = await _service.GetPoolTableByIdAsync(1);

            // Assert
            result.Should().BeEquivalentTo(new PoolTableDto { Id = pooltable.Id, Name = pooltable.Name });
        }

        [Fact]
        public async Task GetPoolTableByIdAsync_ShouldReturnNull_WhenPoolTableDoesNotExist()
        {
            // Arrange
            _mockPoolTableRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PoolTable)null);

            // Act
            var result = await _service.GetPoolTableByIdAsync(1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddPoolTableAsync_ShouldCallRepository()
        {
            // Arrange
            var createPoolTableDto = new CreatePoolTableDto { Id = 3, Name = "PoolTable3" };

            // Act
            await _service.AddPoolTableAsync(createPoolTableDto);

            // Assert
            _mockPoolTableRepository.Verify(x => x.AddAsync(It.Is<PoolTable>(c => c.Id == createPoolTableDto.Id && c.Name == createPoolTableDto.Name)), Times.Once);
        }

        [Fact]
        public async Task UpdatePoolTableAsync_ShouldCallRepository_WhenPoolTableExists()
        {
            // Arrange
            var updatePoolTableDto = new UpdatePoolTableDto { Id = 1, Name = "UpdatedPoolTable" };
            var existingPoolTable = new PoolTable { Id = 1, Name = "PoolTable1" };

            _mockPoolTableRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(existingPoolTable);

            // Act
            await _service.UpdatePoolTableAsync(updatePoolTableDto);

            // Assert
            _mockPoolTableRepository.Verify(x => x.UpdateAsync(It.Is<PoolTable>(c => c.Id == updatePoolTableDto.Id && c.Name == updatePoolTableDto.Name)), Times.Once);
        }

        [Fact]
        public async Task DeletePoolTableAsync_ShouldCallRepository_WhenPoolTableExists()
        {
            // Arrange
            var pooltable = new PoolTable { Id = 1, Name = "PoolTable1" };

            _mockPoolTableRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(pooltable);

            // Act
            await _service.DeletePoolTableAsync(1);

            // Assert
            _mockPoolTableRepository.Verify(x => x.DeleteAsync(It.Is<PoolTable>(c => c.Id == pooltable.Id)), Times.Once);
        }

        [Fact]
        public async Task DeletePoolTableAsync_ShouldNotCallRepository_WhenPoolTableDoesNotExist()
        {
            // Arrange
            _mockPoolTableRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PoolTable)null);

            // Act
            await _service.DeletePoolTableAsync(1);

            // Assert
            _mockPoolTableRepository.Verify(x => x.DeleteAsync(It.IsAny<PoolTable>()), Times.Never);
        }
    }
}
