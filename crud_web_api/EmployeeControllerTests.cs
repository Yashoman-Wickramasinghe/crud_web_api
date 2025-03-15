using crud_web_api.Controllers;
using crud_web_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace crud_web_api.Tests
{
    public class EmployeeControllerTests
    {
        private readonly Mock<EmployeeContext> _mockContext;
        private readonly EmployeeController _controller;
        private readonly Mock<DbSet<Employee>> _mockSet;

        public EmployeeControllerTests()
        {
            _mockContext = new Mock<EmployeeContext>(new DbContextOptions<EmployeeContext>());
            _mockSet = new Mock<DbSet<Employee>>();
            _controller = new EmployeeController(_mockContext.Object);
        }

        [Fact]
        public async Task GetEmployees_ReturnsAllEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Age = "30", isActive = 1 },
                new Employee { Id = 2, Name = "Jane Doe", Age = "25", isActive = 1 }
            }.AsQueryable();

            _mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employees.Provider);
            _mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            _mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            _mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            _mockContext.Setup(c => c.Employees).Returns(_mockSet.Object);

            // Act
            var result = await _controller.GetEmployees();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Employee>>>(result);
            var returnValue = Assert.IsType<List<Employee>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetEmployee_ReturnsEmployeeById()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John Doe", Age = "30", isActive = 1 };
            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(employee);
            _mockContext.Setup(c => c.Employees).Returns(_mockSet.Object);

            // Act
            var result = await _controller.GetEmployee(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Employee>>(result);
            var returnValue = Assert.IsType<Employee>(actionResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task PostEmployee_AddsEmployee()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John Doe", Age = "30", isActive = 1 };
            _mockContext.Setup(c => c.Employees).Returns(_mockSet.Object);

            // Act
            var result = await _controller.PostEmployee(employee);

            // Assert
            _mockSet.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once());
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once());
            var actionResult = Assert.IsType<ActionResult<Employee>>(result);
            var returnValue = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal("GetEmployee", returnValue.ActionName);
        }

        [Fact]
        public async Task PutEmployee_UpdatesEmployee()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John Doe", Age = "30", isActive = 1 };
            _mockContext.Setup(c => c.Employees).Returns(_mockSet.Object);

            // Act
            var result = await _controller.PutEmployee(1, employee);

            // Assert
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once());
            var actionResult = Assert.IsType<ActionResult<Employee>>(result);
            Assert.IsType<OkResult>(actionResult.Result);
        }

        [Fact]
        public async Task DeleteEmployee_RemovesEmployee()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John Doe", Age = "30", isActive = 1 };
            _mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(employee);
            _mockContext.Setup(c => c.Employees).Returns(_mockSet.Object);

            // Act
            var result = await _controller.DeleteEmployee(1);

            // Assert
            _mockSet.Verify(m => m.Remove(It.IsAny<Employee>()), Times.Once());
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once());
            var actionResult = Assert.IsType<ActionResult<Employee>>(result);
            Assert.IsType<OkResult>(actionResult.Result);
        }
    }
}
