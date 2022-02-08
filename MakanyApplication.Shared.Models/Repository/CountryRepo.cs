using MakanyApplication.Shared.Models.DataTransferObjects.Country;
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
    public class CountryRepo
    {
        private readonly MakanyApplicationDbContext _dbContext;

        public CountryRepo(MakanyApplicationDbContext db)
        {
            _dbContext = db;
        }

        public async Task<CommitResult<IndexCountry>> GetDetailsAsync(int Id)
        {
            try
            {
                return new CommitResult<IndexCountry>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Country>().ProjectToType<IndexCountry>().SingleOrDefaultAsync(country => country.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<IndexCountry>
                {
                    IsSuccess = false,
                    ErrorCode = "CTY-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<IEnumerable<IndexCountry>>> GetAsync()
        {
            try
            {
                return new CommitResult<IEnumerable<IndexCountry>>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Country>().Where(country => !country.IsDeleted).ProjectToType<IndexCountry>().ToListAsync()
                };
            }
            catch
            {
                return new CommitResult<IEnumerable<IndexCountry>>
                {
                    IsSuccess = false,
                    ErrorCode = "CTY-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<UpdateCountry>> GetCountryForUpdateAsync(int Id)
        {
            try
            {
                return new CommitResult<UpdateCountry>
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    ErrorType = ErrorType.None,
                    Value = await _dbContext.Set<Country>().ProjectToType<UpdateCountry>().SingleOrDefaultAsync(country => country.Id.Equals(Id))
                };
            }
            catch
            {
                return new CommitResult<UpdateCountry>
                {
                    IsSuccess = false,
                    ErrorCode = "CTY-X0001",
                    ErrorType = ErrorType.Error,
                    Value = default
                };
            }
        }

        public async Task<CommitResult<string>> CreateAsync(CreateCountry model)
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
                            ErrorCode = "CTY-X0002",
                            ErrorType = ErrorType.Error
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Name))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "CTY-X0003",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    if (string.IsNullOrWhiteSpace(model.Description))
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "CTY-X0004",
                            ErrorType = ErrorType.Error,
                            Value = default
                        };
                    }

                    Country tempCountry = await _dbContext.Set<Country>().Where(country => country.Name.Equals(model.Name) && country.Description.Equals(model.Description) && !country.IsDeleted).SingleOrDefaultAsync();

                    if (tempCountry != null)
                    {
                        return new CommitResult<string>
                        {
                            IsSuccess = false,
                            ErrorCode = "CTY-X0005",
                            ErrorType = ErrorType.Error
                        };
                    }

                    tempCountry = new Country
                    {
                        Name = model.Name,
                        Description = model.Description,
                        IsDeleted = false
                    };

                    _dbContext.Set<Country>().Add(tempCountry);

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
                        ErrorCode = "CTY-X0001",
                        ErrorType = ErrorType.Error
                    };
                }
            }
        }

        public async Task<CommitResult<string>> UpdateAsync(UpdateCountry model)
        {
            try
            {
                if (model is null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "CTY-X0002",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "CTY-X0003",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                if (string.IsNullOrWhiteSpace(model.Description))
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "CTY-X0004",
                        ErrorType = ErrorType.Error,
                        Value = default
                    };
                }

                Country tempCountry = await _dbContext.Set<Country>().Where(country => country.Name.Equals(model.Name) && country.Description.Equals(model.Description) && !country.IsDeleted).SingleOrDefaultAsync();

                if (tempCountry != null)
                {
                    return new CommitResult<string>
                    {
                        IsSuccess = false,
                        ErrorCode = "CTY-X0005",
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
                    ErrorCode = "CTY-X0001",
                    ErrorType = ErrorType.Error
                };
            }
        }
    }
}
