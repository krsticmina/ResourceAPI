using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StaffServiceBLL;
using StaffServiceBLL.Exceptions;
using StaffServiceBLL.Models;
using StaffServiceDAL.Entities;
using StaffServiceDAL.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace StaffServiceTests
{
    public class EmployeeServiceTests
    {
        private ServiceProvider sp;
        private readonly Mock<IEmployeeRepository> repositoryMock;

        public EmployeeServiceTests()
        {
            repositoryMock = new Mock<IEmployeeRepository>();
            var sc = new ServiceCollection();
            sc.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            sp = sc.BuildServiceProvider();
        }


        //Get employee
        [Fact]
        public async Task GetEmployeeById_EmployeeNotFound_ThrowsEmployeeNotFoundExceptionAsync()
        {
            repositoryMock.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<Employee>(null));

            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            await Assert.ThrowsAsync<EmployeeNotFoundException>
                (async () => await employeeService.GetEmployeeByIdAsync(employeeId: 1, currentEmployeeId: 1, isAdmin: true, isEmployee: false));
        }

        [Fact]
        public async Task GetEmployeeById_ManagerDoesNotHaveAccess_ThrowsUnauthorizedAsync()
        {
            repositoryMock.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Employee("Employee")
                {
                    Id = 3,
                    Email = "employee@gmail.com",
                    ManagerId = 3,
                    Position = "Employee"
                });

            repositoryMock.Setup(x => x.CheckIfManagerIsAuthorizedAsync(It.IsAny<int>(), It.IsAny<int>())).
                Returns(Task.FromResult<Employee>(null));

            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            await Assert.ThrowsAsync<UnauthorizedException>
                (async () => await employeeService.GetEmployeeByIdAsync(employeeId: 3, currentEmployeeId: 6, isAdmin: false , isEmployee: false));
        }

        [Fact]
        public async Task GetEmployeeById_EmployeeFound_ReturnsEmployeeModelAsync()
        {
            repositoryMock.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Employee("Employee")
                {
                    Id = 3,
                    Email = "Employee@gmail.com",
                    ManagerId = 6,
                    Position = "Employee"
                });

            repositoryMock.Setup(x => x.CheckIfManagerIsAuthorizedAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Employee("Manager")
                {
                    Email = "manager@gmail.com",
                    Position = "Manager",
                    ManagerId = 6
                });

            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            var employee = await employeeService.GetEmployeeByIdAsync(employeeId: 3, currentEmployeeId: 6, isAdmin: false, isEmployee: false);

            Assert.IsType<EmployeeModel>(employee);
        }


        [Fact]
        public async Task GetEmployeeById_EmployeeDoesNotHaveAccess_ThrowsUnauthorizedExceptionAsync()
        {
            repositoryMock.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Employee("Employee")
                {
                    Id = 3,
                    Email = "Employee@gmail.com",
                    ManagerId = 6,
                    Position = "Employee"
                });

            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            await Assert.ThrowsAsync<UnauthorizedException>
               (async () => await employeeService.GetEmployeeByIdAsync(employeeId: 3, currentEmployeeId: 6, isAdmin: false, isEmployee: true));
        }

       
        //Add employee
        [Fact]
        public async Task AddEmployee_ManagerIdSetToNull_ThrowsUnauthorizedExceptionAsync() 
        {
            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            await Assert.ThrowsAsync<UnauthorizedException>(async() => await employeeService.AddEmployeeAsync(new EmployeeForInsertionModel() 
            {
                Name = "Employee",
                Email = "employee@gmail.com",
                Position = "Employee",
                ManagerId = null
            },
            currentEmployeeId: 1, 
            isAdmin: false
            ));
        
        }

        [Fact]
        public async Task AddEmployee_ManagerForEmployeeNotFound_ThrowsEmployeeNotFoundExceptionAsync()
        {
            repositoryMock.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<int>())).
                Returns(Task.FromResult<Employee>(null)); 

            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            await Assert.ThrowsAsync<EmployeeNotFoundException>(async () => await employeeService.
            AddEmployeeAsync(new EmployeeForInsertionModel()
            {
                Name = "Employee",
                Email = "employee@gmail.com",
                Position = "Employee",
                ManagerId = 1
            }, 
            currentEmployeeId : 1,
            isAdmin : false));

        }

        [Fact]
        public async Task AddEmployee_ManagerForEmployeeNotAuthorized_ThrowsUnauthorizedExceptionAsync()
        {
            repositoryMock.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<int>())).
                ReturnsAsync(new Employee("Manager")
                {
                    Id = 3,
                    Position = "Manager",
                    ManagerId = 2,
                    Email = "manager@gmail.com"
                });
            repositoryMock.Setup(x => x.CheckIfManagerIsAuthorizedAsync(It.IsAny<int>(), It.IsAny<int>())).
                Returns(Task.FromResult<Employee>(null));

            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            await Assert.ThrowsAsync<UnauthorizedException>(async () => await employeeService.AddEmployeeAsync(
            new EmployeeForInsertionModel()
                {
                    Name = "New Employee",
                    Email = "newemployee@gmail.com",
                    Position = "Employee",
                    ManagerId = 3
                },
            currentEmployeeId : 1,
            isAdmin : false
            ));

        }

        [Fact]
        public async Task AddEmployee_ManagerForEmployeeDoesNotHaveManagerialPosition_ThrowsNotManagerExceptionAsync()
        {
            repositoryMock.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<int>())).
                ReturnsAsync(new Employee("Employee")
                {
                    Position = "Employee",
                    ManagerId = 2
                });
            repositoryMock.Setup(x => x.CheckIfManagerIsAuthorizedAsync(It.IsAny<int>(), It.IsAny<int>())).
                ReturnsAsync(new Employee("Employee")
                {
                    Position = "Employee",
                    ManagerId = 2
                });

            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            await Assert.ThrowsAsync<NotManagerException>(async () => await employeeService.
            AddEmployeeAsync(new EmployeeForInsertionModel()
            {
                Name = "New Employee",
                Position = "Employee",
                ManagerId = 2
            },
            currentEmployeeId: 1,
            isAdmin: false
            ));

        }

        [Fact]
        public async Task AddEmployee_EmployeeAddedToDatabase_ReturnsEmployeeModelAsync()
        {
            repositoryMock.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<int>())).
                ReturnsAsync(new Employee("Manager")
                {
                    Position = "Manager",
                    ManagerId = 2
                });

            repositoryMock.Setup(x => x.CheckIfManagerIsAuthorizedAsync(It.IsAny<int>(), It.IsAny<int>())).
                ReturnsAsync(new Employee("Manager")
                {
                    Position = "Manager",
                    ManagerId = 2
                });
            
            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            var employee = await employeeService.AddEmployeeAsync(new EmployeeForInsertionModel()
                {
                    Name = "Employee",
                    Email = "employee@gmail.com",
                    Position = "Employee",
                    ManagerId = 1
                }, 
                currentEmployeeId: 3, 
                isAdmin: false);

            Assert.IsType<EmployeeModel>(employee);

        }

        //Update employee
        [Fact]
        public async Task UpdateEmployee_EmployeeNotFound_ThrowsEmployeeNotFoundExceptionAsync() 
        { 
            repositoryMock.Setup( x => x.GetEmployeeByIdAsync(It.IsAny<int>())).
                Returns(Task.FromResult<Employee>(null));

            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            await Assert.ThrowsAsync<EmployeeNotFoundException>
                (async () => await employeeService.UpdateEmployeeAsync
                (employeeId : 1,
                new EmployeeForUpdateModel()
                {
                    Email = "employee@gmail.com",
                    Position = "Admin",
                    Name = "Employee"
                },
                currentEmployeeId : 1,
                isAdmin : true)) ;
        
        }

        [Fact]
        public async Task UpdateEmployee_ManagerForEmployeeNotAuthorized_ThrowsUnauthorizedExceptionAsync()
        {
            repositoryMock.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<int>())).
                ReturnsAsync(new Employee("Employee")
                {
                    Email = "employee@gmail.com",
                    Position = "Employee",
                    ManagerId = 1
                });
            repositoryMock.Setup(x => x.CheckIfManagerIsAuthorizedAsync(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(Task.FromResult<Employee>(null));


            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            await Assert.ThrowsAsync<UnauthorizedException>
                (async () => await employeeService.UpdateEmployeeAsync
                (employeeId : 1,
                new EmployeeForUpdateModel()
                {
                    Email = "employee@gmail.com",
                    Position = "Admin",
                    ManagerId = 3,
                    Name = "Employee - updated"
                },
                currentEmployeeId : 1,
                isAdmin: false));

        }

        [Fact]
        public async Task UpdateEmployee_ManagerForEmployeeDoesNotHaveManagerialPosition_ThrowsNotManagerExceptionAsync()
        {
            repositoryMock.Setup(x => x.GetEmployeeByIdAsync(It.IsAny<int>())).
                ReturnsAsync(new Employee("Employee")
                {
                    Email = "employee@gmail.com",
                    Position = "Employee",
                    ManagerId = 1
                });

            repositoryMock.Setup(x => x.CheckIfManagerIsAuthorizedAsync(It.IsAny<int>(), It.IsAny<int>()))
               .ReturnsAsync(new Employee("Employee")
               {
                   Email = "employee@gmail.com",
                   Position = "Employee"
               });
           

            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            await Assert.ThrowsAsync<NotManagerException>
                (async () => await employeeService.UpdateEmployeeAsync
                (employeeId: 2,
                new EmployeeForUpdateModel()
                {
                    Email = "employee@gmail.com",
                    Position = "Manager",
                    ManagerId = 3,
                    Name = "Employee - updated"
                },
                currentEmployeeId : 1,
                isAdmin : false));

        }

        //Find user in database
        [Fact]
        public async Task FindUser_UserNotFound_ThrowsEmployeeNotFoundExceptionAsync()
        {
            repositoryMock.Setup(x => x.FindUserInDatabaseAsync(It.IsAny<int>())).
                Returns(Task.FromResult<Employee>(null));

            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            await Assert.ThrowsAsync<EmployeeNotFoundException>
                (async () => await employeeService.FindUserInDatabaseAsync( userId : 1));

        }

        [Fact]
        public async Task FindUser_UserFound_ReturnsEmployeeAsync()
        {
            repositoryMock.Setup(x => x.FindUserInDatabaseAsync(It.IsAny<int>())).
                ReturnsAsync(new Employee("Employee") 
                {
                    ManagerId=2,
                    Position="Employee"
                });

            var employeeService = new EmployeeService(repositoryMock.Object, sp.GetService<IMapper>()!);

            var employee = await employeeService.FindUserInDatabaseAsync(userId: 1);

            Assert.IsType<EmployeeModel>(employee);

        }
    }
}