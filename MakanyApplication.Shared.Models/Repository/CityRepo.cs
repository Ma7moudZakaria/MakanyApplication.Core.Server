using MakanyApplication.Shared.Models.DataTransferObjects.City;
using MakanyApplication.Shared.Models.ErrorHandler;
using MakanyApplication.Shared.Models.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakanyApplication.Shared.Models.Repository
{
    public class CityRepo
    {
        private readonly MakanyApplicationDbContext _dbContext;

        public CityRepo(MakanyApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IndexCity>> GetDetailsAsync(int Id)
        {
            try
            {
                return new CommitResult<IndexCity>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<City>().ProjectToType<IndexCity>().SingleOrDefaultAsync(city => city.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<IndexCity>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexCity>>> GetAsync()
        {
            try
            {
                return new CommitResult<IEnumerable<IndexCity>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<City>().Where(city => !city.IsDeleted).ProjectToType<IndexCity>().ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexCity>>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<UpdateCity>> GetCityForUpdateAsync(int Id)
        {
            try
            {
                return new CommitResult<UpdateCity>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<City>().ProjectToType<UpdateCity>().SingleOrDefaultAsync(city => city.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<UpdateCity>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<string>> CreateAsync(CreateCity model)
        {
            using (IDbContextTransaction tran = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (model is null)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "CT-X0002",
                            ErrorType = ErrorType.Error
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Name))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "CT-X0003",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Description))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "CT-X0004",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    City tempCity = await _dbContext.Set<City>().Where(city => city.Name.Equals(model.Name) && city.Description.Equals(model.Description) && !city.IsDeleted).SingleOrDefaultAsync();

                    if (tempCity != null)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "CT-X0005",
                            ErrorType = ErrorType.Error
                        };
                    }

                    tempCity = new City
                    {
                        Name = model.Name,
                        Description = model.Description,
                        IsDeleted = false
                    };

                    _dbContext.Set<City>().Add(tempCity);

                    await _dbContext.SaveChangesAsync();
                    await tran.CommitAsync();

                    return new CommitResult<string>
                    {
                        IsSuccess = true,
                        ErrorCode = string.Empty,
                        ErrorType = ErrorType.None,
                        Value = "Created Successfully"
                    };
                }
                catch
                {
                    await tran.RollbackAsync();
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
            }
        }

        public async Task<CommitResult<string>> UpdateAsync(UpdateCity model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Description))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                City tempCity = await _dbContext.Set<City>().Where(city => city.Name.Equals(model.Name) && city.Description.Equals(model.Description) && !city.IsDeleted).SingleOrDefaultAsync();

                if (tempCity != null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "CT-X0005",
                        ErrorType = ErrorType.Error
                    };
                }

                return new CommitResult<string>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = "Updated Successfully"
                };
            }
            catch
            {
                return new CommitResult<string>
                {
                    IsSuccess = false,
                    ErrorCode = "CT-X0001",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
